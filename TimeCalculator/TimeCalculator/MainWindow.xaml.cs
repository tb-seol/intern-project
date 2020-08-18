using System.Windows;

namespace TimeCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Time Time { get; } = new Time();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void xButton_NumPad_Clear_Click(object sender, RoutedEventArgs e)
        {
            Time.Clear();
            updateViewer();
        }

        private void xButton_NumPad_Delete_Click(object sender, RoutedEventArgs e)
        {
            Time.Delete();
            updateViewer();
        }

        private void xButton_NumPad_Seven_Click(object sender, RoutedEventArgs e)
        {
            Time.AppendCharacter('7');
            updateViewer();
        }

        private void xButton_NumPad_Eight_Click(object sender, RoutedEventArgs e)
        {
            Time.AppendCharacter('8');
            updateViewer();
        }

        private void xButton_NumPad_Nine_Click(object sender, RoutedEventArgs e)
        {
            Time.AppendCharacter('9');
            updateViewer();
        }

        private void xButton_NumPad_Four_Click(object sender, RoutedEventArgs e)
        {
            Time.AppendCharacter('4');
            updateViewer();
        }

        private void xButton_NumPad_Five_Click(object sender, RoutedEventArgs e)
        {
            Time.AppendCharacter('5');
            updateViewer();
        }

        private void xButton_NumPad_Six_Click(object sender, RoutedEventArgs e)
        {
            Time.AppendCharacter('6');
            updateViewer();
        }

        private void xButton_NumPad_One_Click(object sender, RoutedEventArgs e)
        {
            Time.AppendCharacter('1');
            updateViewer();
        }

        private void xButton_NumPad_Two_Click(object sender, RoutedEventArgs e)
        {
            Time.AppendCharacter('2');
            updateViewer();
        }

        private void xButton_NumPad_Three_Click(object sender, RoutedEventArgs e)
        {
            Time.AppendCharacter('3');
            updateViewer();
        }

        private void xButton_NumPad_Zero_Click(object sender, RoutedEventArgs e)
        {
            Time.AppendCharacter('0');
            updateViewer();
        }

        private void xButton_NumPad_Dot_Click(object sender, RoutedEventArgs e)
        {
            Time.AppendCharacter('.');
            updateViewer();
        }

        private void updateViewer()
        {
            xViewerGrid_Top.UpdateValue();
            xViewerGrid_Bottom.UpdateValue();
        }
    }
}
