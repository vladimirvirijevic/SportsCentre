using System;
using System.Collections.Generic;
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
using SportsCentre.WPF.Calendar;

namespace SportsCentre.WPF.Views
{
    /// <summary>
    /// Interaction logic for CalendarTestView.xaml
    /// </summary>
    public partial class CalendarTestView : UserControl
    {
        private CalendarGrid calendar;

        public CalendarTestView()
        {
            InitializeComponent();

            calendar = new CalendarGrid(mainGrid, datesGrid);
            calendar.InitializeCalendar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.Background = Brushes.Yellow;
        }

        private void ActivityButton_Click(object sender, RoutedEventArgs e)
        {
            calendar.OpenActivityWindow();
        }
    }
}
