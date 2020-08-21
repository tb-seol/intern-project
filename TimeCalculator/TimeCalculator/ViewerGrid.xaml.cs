namespace TimeCalculator
{
    using System.Collections.Generic;
    using System.Windows.Controls;

    /// <summary>
    /// TimeViewer.xaml에 대한 상호 작용 논리.
    /// </summary>
    public partial class ViewerGrid : UserControl
    {
        private const string INIT_TIME_VALUE = "0";

        public ViewerGrid()
        {
            InitializeComponent();

            this.xTextBlock_Time.Text = INIT_TIME_VALUE;
        }

        public void UpdateTime(List<double> times)
        {
            double time = times[this.xComboBox_TimeUnit.SelectedIndex];
            string result = (time % 1 == 0) ? time.ToString("N0") : time.ToString("N");

            this.xTextBlock_Time.Text = result;
        }

        public ETimeUnit GetTimeUnit()
        {
            return (ETimeUnit)this.xComboBox_TimeUnit.SelectedItem;
        }

        public void SetTimeUnit(ETimeUnit timeUnit)
        {
            this.xComboBox_TimeUnit.SelectedItem = timeUnit;
        }
    }
}
