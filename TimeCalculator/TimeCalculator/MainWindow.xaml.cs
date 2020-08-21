using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TimeCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string MINI_VIEWER_TITLE = "대략 같음";
        private static string[] MINI_VIEWER_VALUES = new string[] { "yr", "주", "d", "시간", "분", "s", "ms", "us" };

        private Time mTime;
        private ViewerGrid mMainViewer;
        private ViewerGrid mSubViewer;
        private ETimeUnit mTopViewerDefaultUnit = ETimeUnit.Minute;
        private ETimeUnit mBottomViewerDefaultUnit = ETimeUnit.Hour;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            xViewerGrid_Top.SetTimeUnit(mTopViewerDefaultUnit);
            xViewerGrid_Bottom.SetTimeUnit(mBottomViewerDefaultUnit);

            mMainViewer = xViewerGrid_Top;
            mSubViewer = xViewerGrid_Bottom;

            mTime = new Time(mMainViewer.GetTimeUnit());

            xViewerGrid_Top.xTextBlock_Time.MouseDown += XTextBlock_Time_MouseDown;
            xViewerGrid_Bottom.xTextBlock_Time.MouseDown += XTextBlock_Time_MouseDown;

            xViewerGrid_Top.xComboBox_TimeUnit.SelectionChanged += XComboBox_TimeUnit_SelectionChanged;
            xViewerGrid_Bottom.xComboBox_TimeUnit.SelectionChanged += XComboBox_TimeUnit_SelectionChanged;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            xViewerGrid_Top.xTextBlock_Time.MouseDown -= XTextBlock_Time_MouseDown;
            xViewerGrid_Bottom.xTextBlock_Time.MouseDown -= XTextBlock_Time_MouseDown;

            xViewerGrid_Top.xComboBox_TimeUnit.SelectionChanged -= XComboBox_TimeUnit_SelectionChanged;
            xViewerGrid_Bottom.xComboBox_TimeUnit.SelectionChanged -= XComboBox_TimeUnit_SelectionChanged;
        }

        private void xButton_NumPad_Click_Clear(object sender, RoutedEventArgs e)
        {
            mTime.Clear();
            updateViewer();
        }

        private void xButton_NumPad_Click_Delete(object sender, RoutedEventArgs e)
        {
            mTime.RemoveLastCharacter();
            updateViewer();
        }

        private void xButton_NumPad_Click_Normal(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                char character = char.Parse(button.Content.ToString());

                Debug.Assert(character >= '0' && character <= '9' || character == '.');

                mTime.AppendCharacter(character);
                updateViewer();
            }
        }

        private void XTextBlock_Time_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender == mMainViewer.xTextBlock_Time)
            {
                return;
            }

            ViewerGrid temp = mMainViewer;
            mMainViewer = mSubViewer;
            mSubViewer = temp;

            mTime.UnitForConvert = mMainViewer.GetTimeUnit();
            mTime.Clear();
        }

        private void XComboBox_TimeUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mTime.UnitForConvert = mMainViewer.GetTimeUnit();
            updateViewer();
        }

        private void updateViewer()
        {
            List<double> times = mTime.GetAllTimes();
            string miniViewerTitle = (times[0] > 0) ? MINI_VIEWER_TITLE : "";

            xViewerGrid_Top.UpdateTime(times);
            xViewerGrid_Bottom.UpdateTime(times);

            xTextBlock_MiniViewerTitle.Text = miniViewerTitle;
            xItemsControl_MiniViewerValues.ItemsSource = formatMiniViewerTimes(times);
        }

        private string formatMiniViewerTime(double time)
        {
            if (time < 10)
            {
                return Math.Round(time, 2).ToString();
            }

            return Math.Round(time).ToString("N0");
        }

        private List<string> formatMiniViewerTimes(List<double> times)
        {
            const double MIN_VALUE = 0.01;

            List<string> formattedTimes = new List<string>(16);

            for (int i = 0; i < times.Count; i++)
            {
                if (i == (int)mMainViewer.GetTimeUnit() || i == (int)mSubViewer.GetTimeUnit())
                {
                    continue;
                }

                double time = times[times.Count - 1 - i];

                if (time >= MIN_VALUE)
                {
                    formattedTimes.Add(formatMiniViewerTime(time) + MINI_VIEWER_VALUES[i] + "  ");
                }
            }

            return formattedTimes;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Window source = e.Source as Window;
            if (source != null)
            {
                switch (e.Key)
                {
                    case Key.Escape:
                    case Key.Delete:
                        xButton_NumPad_Clear.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.Back:
                        xButton_NumPad_Delete.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad0:
                        xButton_NumPad_Zero.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad1:
                        xButton_NumPad_One.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad2:
                        xButton_NumPad_Two.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad3:
                        xButton_NumPad_Three.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad4:
                        xButton_NumPad_Four.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad5:
                        xButton_NumPad_Five.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad6:
                        xButton_NumPad_Six.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad7:
                        xButton_NumPad_Seven.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad8:
                        xButton_NumPad_Eight.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.NumPad9:
                        xButton_NumPad_Nine.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    case Key.Decimal:
                        xButton_NumPad_Decimal.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
