using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TimeCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string SUB_VIEWER_TITLE = "대략 같음";
        private static string[] SUB_VIEWER_VALUES = new string[] { "yr", "주", "d", "시간", "분", "s", "ms", "us" };

        private Time mTime;
        private ETimeUnit mTopViewerDefaultUnit = ETimeUnit.Minute;
        private ETimeUnit mBottomViewerDefaultUnit = ETimeUnit.Hour;

        public MainWindow()
        {
            InitializeComponent();

            mTime = new Time(mTopViewerDefaultUnit);
            xViewerGrid_Top.SetTimeUnit(mTopViewerDefaultUnit);
            xViewerGrid_Bottom.SetTimeUnit(mBottomViewerDefaultUnit);

            xViewerGrid_Top.xComboBox_TimeUnit.SelectionChanged += XComboBox_TimeUnit_SelectionChanged_Top;
            xViewerGrid_Bottom.xComboBox_TimeUnit.SelectionChanged += XComboBox_TimeUnit_SelectionChanged_Bottom;
            DataContext = this;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            xViewerGrid_Top.xComboBox_TimeUnit.SelectionChanged -= XComboBox_TimeUnit_SelectionChanged_Top;
            xViewerGrid_Bottom.xComboBox_TimeUnit.SelectionChanged -= XComboBox_TimeUnit_SelectionChanged_Bottom;
        }

        private void xButton_NumPad_Clear_Click(object sender, RoutedEventArgs e)
        {
            mTime.Clear();
            updateViewer();
        }

        private void xButton_NumPad_Delete_Click(object sender, RoutedEventArgs e)
        {
            mTime.RemoveLastCharacter();
            updateViewer();
        }

        private void xButton_NumPad_Seven_Click(object sender, RoutedEventArgs e)
        {
            mTime.AppendCharacter('7');
            updateViewer();
        }

        private void xButton_NumPad_Eight_Click(object sender, RoutedEventArgs e)
        {
            mTime.AppendCharacter('8');
            updateViewer();
        }

        private void xButton_NumPad_Nine_Click(object sender, RoutedEventArgs e)
        {
            mTime.AppendCharacter('9');
            updateViewer();
        }

        private void xButton_NumPad_Four_Click(object sender, RoutedEventArgs e)
        {
            mTime.AppendCharacter('4');
            updateViewer();
        }

        private void xButton_NumPad_Five_Click(object sender, RoutedEventArgs e)
        {
            mTime.AppendCharacter('5');
            updateViewer();
        }

        private void xButton_NumPad_Six_Click(object sender, RoutedEventArgs e)
        {
            mTime.AppendCharacter('6');
            updateViewer();
        }

        private void xButton_NumPad_One_Click(object sender, RoutedEventArgs e)
        {
            mTime.AppendCharacter('1');
            updateViewer();
        }

        private void xButton_NumPad_Two_Click(object sender, RoutedEventArgs e)
        {
            mTime.AppendCharacter('2');
            updateViewer();
        }

        private void xButton_NumPad_Three_Click(object sender, RoutedEventArgs e)
        {
            mTime.AppendCharacter('3');
            updateViewer();
        }

        private void xButton_NumPad_Zero_Click(object sender, RoutedEventArgs e)
        {
            mTime.AppendCharacter('0');
            updateViewer();
        }

        private void xButton_NumPad_Dot_Click(object sender, RoutedEventArgs e)
        {
            mTime.AppendCharacter('.');
            updateViewer();
        }

        private void XComboBox_TimeUnit_SelectionChanged_Top(object sender, SelectionChangedEventArgs e)
        {
            mTime.UnitForConvert = xViewerGrid_Top.GetTimeUnit();
            updateViewer();
        }

        private void XComboBox_TimeUnit_SelectionChanged_Bottom(object sender, SelectionChangedEventArgs e)
        {
            updateViewer();
        }

        private void updateViewer()
        {
            const double MIN_VALUE = 0.01;

            List<double> times = mTime.GetAllTimes();

            StringBuilder sb = new StringBuilder(128);
            for (int i = 0; i < times.Count; i++)
            {
                double value = times[times.Count - 1 - i];

                if (value >= MIN_VALUE)
                {
                    sb.Append(formatTimeValue(value))
                        .Append(" ")
                        .Append(SUB_VIEWER_VALUES[i])
                        .Append("   ");
                }
            }

            xViewerGrid_Top.UpdateTime(times);
            xViewerGrid_Bottom.UpdateTime(times);

            string title = (sb.Length > 0) ? SUB_VIEWER_TITLE : "";
            xTextBlock_SubViewerTitle.Text = title;
            xTextBlock_SubViewerBody.Text = sb.ToString();
        }

        private string formatTimeValue(double value)
        {
            if (value < 10)
            {
                return Math.Round(value, 2).ToString();
            }

            return Math.Round(value).ToString("N0");
        }
    }
}
