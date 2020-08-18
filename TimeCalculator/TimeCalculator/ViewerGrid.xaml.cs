using System.Diagnostics;
using System.Windows.Controls;

namespace TimeCalculator
{
    /// <summary>
    /// TimeViewer.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ViewerGrid : UserControl
    {
        private ETimeUnit timeUnit;

        public ViewerGrid()
        {
            InitializeComponent();
        }

        public void UpdateValue()
        {
            xTextBlockTime.Text = MainWindow.Time.ConvertTo(timeUnit);
        }

        private void xComboBoxTimeUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            ComboBoxItem item = box.SelectedItem as ComboBoxItem;

            switch (item.Content)
            {
                case "밀리초":
                    timeUnit = ETimeUnit.MilliSecond;
                    break;
                case "초":
                    timeUnit = ETimeUnit.Second;
                    break;
                case "분":
                    timeUnit = ETimeUnit.Minute;
                    break;
                case "시간":
                    timeUnit = ETimeUnit.Hour;
                    break;
                case "일":
                    timeUnit = ETimeUnit.Day;
                    break;
                case "주":
                    timeUnit = ETimeUnit.Week;
                    break;
                case "년":
                    timeUnit = ETimeUnit.Year;
                    break;
                case "마이크로초":
                default:
                    timeUnit = ETimeUnit.MicroSecond;
                    break;
            }

            UpdateValue();
        }
    }
}
