using System.Collections.Generic;
using System.Windows.Controls;

namespace TimeCalculator
{
    /// <summary>
    /// TimeViewer.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ViewerGrid : UserControl
    {
        public ViewerGrid()
        {
            InitializeComponent();
        }

        public void UpdateTime(List<double> times)
        {
            double time = times[xComboBox_TimeUnit.SelectedIndex];
            string result = (time % 1 == 0) ? time.ToString("N0") : time.ToString("N");

            xTextBlock_Time.Text = result;
        }

        public ETimeUnit GetTimeUnit()
        {
            return (ETimeUnit)xComboBox_TimeUnit.SelectedIndex;
        }

        public void SetTimeUnit(ETimeUnit timeUnit)
        {
            xComboBox_TimeUnit.SelectedIndex = (int)timeUnit;
        }
    }
}
