using SportsCentre.WPF.Controls;
using SportsCentre.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SportsCentre.WPF.Views
{
    /// <summary>
    /// Interaction logic for CalendarView.xaml
    /// </summary>
    public partial class CalendarView : UserControl
    {
        private List<DateTime> dates = new List<DateTime>();
        private double fieldHeight;
        private double fieldWidth;

        public CalendarView()
        {
            InitializeComponent();

            fieldHeight = 50;
            fieldWidth = 200;

            dates = GetDates();
            InitCalendar();
        }

        private void InitCalendar()
        {
            double currentField = 1;

            // set time labels
            var start = DateTime.Today;
            var clockQuery = from offset in Enumerable.Range(0, 48)
                             select TimeSpan.FromMinutes(30 * offset);

            int count = 1;
            foreach (var time in clockQuery)
            {
                TextBlock timeLabel = new TextBlock();
                timeLabel.Text = (start + time).ToString("hh:mm tt");
                timeLabel.Foreground = Brushes.Black;
                Canvas.SetLeft(timeLabel, 1);
                Canvas.SetTop(timeLabel, count * fieldHeight);

                calendarCanvas.Children.Add(timeLabel);

                count++;
            }

            for (int i = 1; i <= 10; i++)
            {
                TextBlock timeLabel = new TextBlock();
                timeLabel.Text = "11:00";
                timeLabel.Foreground = Brushes.Black;
                Canvas.SetLeft(timeLabel, 1);
                Canvas.SetTop(timeLabel, i * fieldHeight);

                calendarCanvas.Children.Add(timeLabel);
            }

            // set date labels
            for (int i = 1; i <= 7; i++)
            {
                TextBlock dateLabel = new TextBlock();
                dateLabel.Text = "11:00";
                dateLabel.Foreground = Brushes.Black;
                Canvas.SetLeft(dateLabel, i * fieldWidth);
                Canvas.SetTop(dateLabel, 1);

                calendarCanvas.Children.Add(dateLabel);
            }

            // set fields
            foreach (var date in dates)
            {
                for (int i = 1; i <= 10; i++)
                {
                    var field = new Rectangle()
                    {
                        Width = fieldWidth,
                        Height = fieldHeight,
                        Stroke = Brushes.Black,
                        Fill = Brushes.Blue,
                        StrokeThickness = 2
                    };

                    calendarCanvas.Children.Add(field);

                    field.SetValue(Canvas.LeftProperty, currentField * fieldWidth);
                    field.SetValue(Canvas.TopProperty, i * fieldHeight);
                }

                currentField++;
            }
        }

        private List<DateTime> GetDates()
        {
            List<DateTime> dates = new List<DateTime>();
            DateTime startOfWeek = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)DateTime.Today.DayOfWeek);

            for (int i = 0; i < 7; i++)
            {
                dates.Add(startOfWeek.AddDays(i));
            }

            return dates;
        }



        private void CalendarGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if ((TextBlock)sender != null)
                {
                    var textBox = (TextBlock)sender;
                    textBox.Background = Brushes.Red;
                }
            }
            else
            {
                MessageBox.Show("Control key is up");
            }
        }
    }
}
