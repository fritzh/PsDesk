using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

namespace PsDeskController
{
    public class StatusUpdateEventArgs : EventArgs
    {
        public string NewStatus { get; private set; }
        public bool IsConnected { get; private set; }

        public StatusUpdateEventArgs(string newStatus, bool isConnected)
        {
            NewStatus = newStatus;
            IsConnected = isConnected;
        }
    }

    public class DeskController
    {
        public event EventHandler<StatusUpdateEventArgs> OnStatusUpdate;

        private readonly SerialCommunicator _communicator;

        private Action<decimal> _levelListenerAction;

        private Thread _levelListenerThread;
        private CancellationTokenSource _levelListenerCancellationTokenSource;

        public DeskController()
        {
            _communicator = new SerialCommunicator("PSDESK");

            var controllerThread = new Thread(ControllerThreadStart) {IsBackground = true};
            controllerThread.Start();

            var heartbeatThread = new Thread(HeartbeatThreadStart) {IsBackground = true};
            heartbeatThread.Start();
        }

        private void ControllerThreadStart()
        {
            var portIndex = -1;

            while (true)
            {
                if (!_communicator.IsConnected)
                {
                    var ports = SerialCommunicator.AvailablePorts;
                    if (ports.Length == 0)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    portIndex++;
                    if (portIndex < 0 || portIndex >= ports.Length) portIndex = 0;
                    var portToAttempt = ports[portIndex];

                    FireStatusUpdateEvent("Connecting (" + portToAttempt + ")...");
                    if (_communicator.AttemptConnect(portToAttempt))
                    {
                        FireStatusUpdateEvent("Connected on " + portToAttempt);
                    }
                    else
                    {
                        FireStatusUpdateEvent("Failed (" + portToAttempt + ").");
                        Thread.Sleep(1000);
                    }
                }

                Thread.Sleep(1000);
            }
        }

        private void FireStatusUpdateEvent(string newStatus)
        {
            if (OnStatusUpdate != null)
            {
                try
                {
                    OnStatusUpdate(this, new StatusUpdateEventArgs(newStatus, _communicator.IsConnected));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }

        private void HeartbeatThreadStart()
        {
            while (true)
            {
                if (_communicator.IsConnected && _communicator.LastHeartbeatAt < DateTime.Now.AddSeconds(-2))
                {
                    _communicator.Disconnect();
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        public void MoveUp()
        {
            _communicator.Send("M10000");
        }

        public void StopImmediately()
        {
            _communicator.Send("!");
        }

        public void MoveDown()
        {
            _communicator.Send("M0");
        }

        public void StartLevelFeed(Action<decimal> func)
        {
            _levelListenerAction = func;
            _communicator.Send("F");

            _levelListenerCancellationTokenSource = new CancellationTokenSource();
            _levelListenerThread = new Thread(() => ListenForLevelInfo(_levelListenerCancellationTokenSource.Token));
            _levelListenerThread.IsBackground = true;
            _levelListenerThread.Start();
        }

        private void ListenForLevelInfo(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var phrase = _communicator.Expect(new Regex("^L=.*$"));
                    var levelString = phrase.Split('=')[1].Split(' ')[0];
                    _levelListenerAction(decimal.Parse(levelString));
                }
                finally
                {
                    Thread.Sleep(10);
                }
            }
        }

        public void StopLevelFeed()
        {
            _levelListenerCancellationTokenSource.Cancel();
            _levelListenerThread.Join();
            _communicator.Send("G");
            _levelListenerCancellationTokenSource = null;
            _levelListenerThread = null;
            _levelListenerAction = null;
        }
    }
}