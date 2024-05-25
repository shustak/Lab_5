using System.Windows;

namespace HorseRacingApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            horseRacingControl.StartGame();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            horseRacingControl.PauseGame();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            horseRacingControl.ResetGame();
        }
    }
}
