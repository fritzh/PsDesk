using System;
using System.Reactive.Subjects;
using System.Threading;

namespace PsDeskController
{
    public class AutoUpDown
    {
        private readonly Timer _timer;
        private readonly Timer _activityCheck;

        private bool _up = true;
        private bool _enabled;
        private DateTimeOffset _originalChangeAgainAt = DateTimeOffset.MaxValue;
        private DateTimeOffset _changeAgainAt = DateTimeOffset.MaxValue;
        private DateTimeOffset? _inactiveSince;
        private TimeSpan _upTime;
        private TimeSpan _downTime;
        private readonly DeskController _controller;
        private readonly Action _preChangeWarning;

        public Subject<DateTimeOffset?> NextChangeAtSubject { get; private set; }

        public AutoUpDown(DeskController controller, Action preChangeWarning)
        {
            _timer = new Timer(TimerTick, this, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            _activityCheck = new Timer(ActivityTimerTick, this, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            _controller = controller;
            _preChangeWarning = preChangeWarning;
            NextChangeAtSubject = new Subject<DateTimeOffset?>();
        }

        public void SetMode(bool enabled, TimeSpan upTime, TimeSpan downTime)
        {
            _enabled = enabled;
            _upTime = upTime;
            _downTime = downTime;

            ThreadPool.QueueUserWorkItem(o => ToggleDesk());
            SetChangeAgainAtTime();
        }

        private void SetChangeAgainAtTime()
        {
            _changeAgainAt = DateTimeOffset.Now + (_up ? _upTime : _downTime);
            _originalChangeAgainAt = _changeAgainAt;
            NextChangeAtSubject.OnNext(_changeAgainAt);
        }

        private void PollDesk()
        {
            if (DateTimeOffset.Now < _changeAgainAt) return;

            ToggleDesk();
            SetChangeAgainAtTime();
        }

        private void PollActivity()
        {
            var idleForTicks = GetLastUserInput.GetIdleTickCount();
            var isIdle = (idleForTicks > TimeSpan.TicksPerMinute);

            if (_inactiveSince == null && isIdle)
            {
                _inactiveSince = DateTimeOffset.Now - TimeSpan.FromTicks(idleForTicks);
            }
            if (_inactiveSince != null && !isIdle)
            {
                _inactiveSince = null;
            }

            if (_inactiveSince != null)
            {
                _changeAgainAt = _originalChangeAgainAt + (DateTimeOffset.Now - _inactiveSince.Value);
                NextChangeAtSubject.OnNext(_changeAgainAt);
            }
        }

        private void ToggleDesk()
        {
            if (!_enabled)
            {
                return;
            }

            if (_preChangeWarning != null)
            {
                _preChangeWarning();
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }

            if (_up)
            {
                _controller.MoveUp();
            }
            else
            {
                _controller.MoveDown();
            }

            _up = !_up;
        }

        private static void TimerTick(object state)
        {
            ((AutoUpDown) state).PollDesk();
        }

        private static void ActivityTimerTick(object state)
        {
            ((AutoUpDown) state).PollActivity();
        }
    }
}