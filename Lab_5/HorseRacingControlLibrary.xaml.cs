using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HorseRacingApp
{
    public partial class HorseRacingControl : UserControl
    {
        private List<Horse> horses;
        private DispatcherTimer timer;
        private Random random;

        public HorseRacingControl()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            horses = new List<Horse>();
            random = new Random();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;

            // Создание лошадей
            for (int i = 0; i < 5; i++)
            {
                var horse = new Horse
                {
                    X = 10,
                    Y = 50 * i + 10,
                    Speed = random.Next(1, 5)
                };
                horses.Add(horse);

                var horseEllipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Brown,
                    DataContext = horse
                };
                horseEllipse.SetBinding(Canvas.LeftProperty, new Binding("X"));
                horseEllipse.SetBinding(Canvas.TopProperty, new Binding("Y"));
                horseEllipse.MouseLeftButtonDown += (s, e) => MessageBox.Show($"Скорость лошади: {horse.Speed}");
                horseEllipse.MouseRightButtonDown += (s, e) => MessageBox.Show($"Позиция лошади: {horse.X}, {horse.Y}");

                gameCanvas.Children.Add(horseEllipse);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var horse in horses)
            {
                horse.Move();
                if (horse.X > gameCanvas.ActualWidth)
                {
                    horse.X = 10;
                }
            }
        }

        public void StartGame() => timer.Start();
        public void PauseGame() => timer.Stop();
        public void ResetGame()
        {
            PauseGame();
            foreach (var horse in horses)
            {
                horse.X = 10;
            }
        }
    }

    public class Horse : INotifyPropertyChanged
    {
        private double x, y;
        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
            }
        }

        public double Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged(nameof(Y));
            }
        }

        public double Speed { get; set; }

        public void Move() => X += Speed;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
