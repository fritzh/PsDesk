using System;
using System.Windows.Forms;

namespace PsDeskController
{
    public partial class MainForm : Form
    {
        private readonly DeskController _controller;
        private readonly AutoUpDown _autoUpDown;
        private IDisposable _nextChangeSubscription;

        private delegate void StatusUpdateDelegate(StatusUpdateEventArgs args);

        public MainForm()
        {
            InitializeComponent();
            _controller = new DeskController();
            _controller.OnStatusUpdate += (sender, args) =>
            {
                if (InvokeRequired)
                {
                    Invoke(new StatusUpdateDelegate(UpdateFormStatus), args);
                    return;
                }

                UpdateFormStatus(args);
            };

            _autoUpDown = new AutoUpDown(_controller, FlashWindow);
            _nextChangeSubscription = _autoUpDown.NextChangeAtSubject.Subscribe(SetNextChangeLabel);
        }

        private void UpdateFormStatus(StatusUpdateEventArgs args)
        {
            StatusLabel.Text = args.NewStatus;
            Enabled = args.IsConnected;
        }

        private delegate void SetNextChangeLabelDelegate(DateTimeOffset? nextChangeAt);

        private void SetNextChangeLabel(DateTimeOffset? nextChangeAt)
        {
            if (InvokeRequired)
            {
                Invoke(new SetNextChangeLabelDelegate(SetNextChangeLabel), nextChangeAt);
                return;
            }

            NextChangeLabel.Text = nextChangeAt == null ? "(Disabled)" : nextChangeAt.Value.ToString("h:mm:ss tt");
        }

        private delegate void FlashWindowDelegate();

        private void FlashWindow()
        {
            if (InvokeRequired)
            {
                Invoke(new FlashWindowDelegate(FlashWindow));
                return;
            }

            PsDeskController.FlashWindow.Flash(this, 5);
        }

        private void AllUpButton_Click(object sender, EventArgs e)
        {
            _controller.MoveUp();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _controller.StopImmediately();
        }

        private void AllDownButton_Click(object sender, EventArgs e)
        {
            _controller.MoveDown();
        }

        private void UpButton_MouseDown(object sender, MouseEventArgs e)
        {
            _controller.MoveUp();
        }

        private void UpButton_MouseUp(object sender, MouseEventArgs e)
        {
            _controller.StopImmediately();
        }

        private void DownButton_MouseDown(object sender, MouseEventArgs e)
        {
            _controller.MoveDown();
        }

        private void DownButton_MouseUp(object sender, MouseEventArgs e)
        {
            _controller.StopImmediately();
        }

        private void upDownEnabled_CheckedChanged(object sender, EventArgs e)
        {
            _autoUpDown.SetMode(upDownEnabled.Checked, TimeSpan.FromMinutes((double) upTimeSpinner.Value),
                TimeSpan.FromMinutes((double) downTimeSpinner.Value));
        }

        private void CalibrateButton_Click(object sender, EventArgs e)
        {
            var calibrationForm = new CalibrationForm(_controller);
            calibrationForm.ShowDialog();
        }
    }
}