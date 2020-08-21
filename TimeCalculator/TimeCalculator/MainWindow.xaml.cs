namespace TimeCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string MINI_VIEWER_TITLE = "대략 같음";
        private static readonly string[] MINI_VIEWER_VALUES = new string[] { "yr", "주", "d", "시간", "분", "s", "ms", "us" };

        private readonly Time _time;
        private readonly ETimeUnit _topViewerDefaultUnit = ETimeUnit.Minute;
        private readonly ETimeUnit _bottomViewerDefaultUnit = ETimeUnit.Hour;
        private ViewerGrid _mainViewer;
        private ViewerGrid _subViewer;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.xViewerGrid_Top.SetTimeUnit(this._topViewerDefaultUnit);
            this.xViewerGrid_Bottom.SetTimeUnit(this._bottomViewerDefaultUnit);
            SetMainViewer(this.xViewerGrid_Top);
            SetSubViewer(this.xViewerGrid_Bottom);

            this._time = new Time(this._mainViewer.GetTimeUnit());

            this.xViewerGrid_Top.xTextBlock_Time.MouseDown += XTextBlock_Time_MouseDown;
            this.xViewerGrid_Bottom.xTextBlock_Time.MouseDown += XTextBlock_Time_MouseDown;
            this.xViewerGrid_Top.xComboBox_TimeUnit.SelectionChanged += XComboBox_TimeUnit_SelectionChanged;
            this.xViewerGrid_Bottom.xComboBox_TimeUnit.SelectionChanged += XComboBox_TimeUnit_SelectionChanged;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            this.xViewerGrid_Top.xTextBlock_Time.MouseDown -= XTextBlock_Time_MouseDown;
            this.xViewerGrid_Bottom.xTextBlock_Time.MouseDown -= XTextBlock_Time_MouseDown;

            this.xViewerGrid_Top.xComboBox_TimeUnit.SelectionChanged -= XComboBox_TimeUnit_SelectionChanged;
            this.xViewerGrid_Bottom.xComboBox_TimeUnit.SelectionChanged -= XComboBox_TimeUnit_SelectionChanged;
        }

        private void XButton_NumPad_Click_Clear(object sender, RoutedEventArgs e)
        {
            this._time.Clear();
            UpdateViewer();
        }

        private void XButton_NumPad_Click_Delete(object sender, RoutedEventArgs e)
        {
            this._time.RemoveLastCharacter();
            UpdateViewer();
        }

        private void XButton_NumPad_Click_Normal(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                char character = char.Parse(button.Content.ToString());
                this._time.AppendCharacter(character);
                UpdateViewer();
            }
        }

        private void XTextBlock_Time_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender == this._mainViewer.xTextBlock_Time)
                return;

            SwapMainViewer();

            this._time.UnitForConvert = this._mainViewer.GetTimeUnit();
            this._time.Clear();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var source = e.Source as Window;
            if (source != null)
                switch (e.Key)
                {
                    case Key.Escape:
                    case Key.Delete:
                        this.xButton_NumPad_Clear.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.Back:
                        this.xButton_NumPad_Delete.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad0:
                        this.xButton_NumPad_Zero.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad1:
                        this.xButton_NumPad_One.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad2:
                        this.xButton_NumPad_Two.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad3:
                        this.xButton_NumPad_Three.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad4:
                        this.xButton_NumPad_Four.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad5:
                        this.xButton_NumPad_Five.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad6:
                        this.xButton_NumPad_Six.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad7:
                        this.xButton_NumPad_Seven.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad8:
                        this.xButton_NumPad_Eight.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad9:
                        this.xButton_NumPad_Nine.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.Decimal:
                        this.xButton_NumPad_Decimal.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    default:
                        break;
                }
        }

        private void XComboBox_TimeUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._time.UnitForConvert = this._mainViewer.GetTimeUnit();
            UpdateViewer();
        }

        private void UpdateViewer()
        {
            List<double> times = this._time.GetAllTimes();
            string miniViewerTitle = (times[0] > 0) ? MINI_VIEWER_TITLE : string.Empty;

            this.xViewerGrid_Top.UpdateTime(times);
            this.xViewerGrid_Bottom.UpdateTime(times);

            this.xTextBlock_MiniViewerTitle.Text = miniViewerTitle;
            this.xItemsControl_MiniViewerValues.ItemsSource = FormatMiniViewerTimes(times);
        }

        private static string FormatMiniViewerTime(double time)
        {
            if (time < 10)
                return Math.Round(time, 2).ToString();
            else
                return Math.Round(time).ToString("N0");
        }

        private List<string> FormatMiniViewerTimes(List<double> times)
        {
            const double MIN_VALUE = 0.01;
            var formattedTimes = new List<string>(16);
            for (int i = 0; i < times.Count; i++)
            {
                if (i == (int)this._mainViewer.GetTimeUnit() || i == (int)this._subViewer.GetTimeUnit())
                    continue;

                double time = times[times.Count - 1 - i];
                if (time >= MIN_VALUE)
                    formattedTimes.Add(FormatMiniViewerTime(time) + MINI_VIEWER_VALUES[i] + "  ");
            }

            return formattedTimes;
        }

        private void SetMainViewer(ViewerGrid viewer)
        {
            this._mainViewer = viewer;
            this._mainViewer.xTextBlock_Time.FontWeight = FontWeights.Bold;
        }

        private void SetSubViewer(ViewerGrid viewer)
        {
            this._subViewer = viewer;
            this._subViewer.xTextBlock_Time.FontWeight = FontWeights.Normal;
        }

        private void SwapMainViewer()
        {
            ViewerGrid temp = this._mainViewer;
            SetMainViewer(this._subViewer);
            SetSubViewer(temp);
        }
    }
}
