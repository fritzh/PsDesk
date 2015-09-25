using System;
using System.Globalization;
using System.Windows.Forms;

namespace PsDeskController
{
    public partial class CalibrationForm : Form
    {
        private readonly DeskController _controller;

        public CalibrationForm(DeskController controller)
        {
            _controller = controller;
            InitializeComponent();

            Shown += CalibrationForm_Shown;
            Closed += CalibrationForm_Closed;
        }

        private void CalibrationForm_Shown(object sender, EventArgs e)
        {
            _controller.StartLevelFeed(SetLevel);
        }

        private delegate void SetLevelDelegate(decimal level);

        private void SetLevel(decimal level)
        {
            if (InvokeRequired)
            {
                Invoke(new SetLevelDelegate(SetLevel), level);
                return;
            }

            AverageLevelLabel.Text = level.ToString(CultureInfo.InvariantCulture);
        }

        private void CalibrationForm_Closed(object sender, EventArgs e)
        {
            _controller.StopLevelFeed();
        }
    }
}