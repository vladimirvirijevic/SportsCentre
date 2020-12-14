using Microsoft.EntityFrameworkCore;
using SportsCentre.Data;
using SportsCentre.Domain.Models;
using SportsCentre.WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace SportsCentre.WPF.Calendar
{
    public class CalendarGrid
    {
        private List<DateTime> dates = new List<DateTime>();
        private string dateFormat = "dd-MM-yyyy";
        private string dateNameFormat = "dd_MM_yyyy";
        private string[] timeSchedule = { "08_00", "08_30", "09_00" };
        private Grid mainGrid;
        private Grid datesGrid;

        private Brush cellOccupied = Brushes.Red;

        private List<Match> matches = new List<Match>();
        private List<MatchInfo> matchesInfo = new List<MatchInfo>();

        public CalendarGrid(Grid mainGrid, Grid datesGrid)
        {
            this.mainGrid = mainGrid;
            this.datesGrid = datesGrid;
        }

        #region Initialize Calendar
        public void InitializeCalendar()
        {
            InitializeDates();
            InitializeGridCells();
            InitializeMatches();
        }
        #endregion

        #region Initialize Grid Cells
        private void InitializeGridCells()
        {
            int counter = 0;

            foreach (var item in mainGrid.Children)
            {
                if (item is Grid)
                {
                    //time_08_00_grid
                    string gridName = $"time_{timeSchedule[counter]}_grid";
                    Grid grid = item as Grid;

                    if (grid.Name == gridName)
                    {
                        SetGridCells(grid, timeSchedule[counter]);
                        counter++;
                    }
                }
            }
        }

        private void SetGridCells(Grid grid, string time)
        {
            for (int i = 1; i <= 7; i++)
            {
                if (grid.Children[i] is Button)
                {
                    Button buttonCell = grid.Children[i] as Button;
                    string cellName = $"cell_{dates[i - 1].ToString(dateNameFormat)}_{time}";
                    //cell_04_12_2020_08_00
                    buttonCell.Name = cellName;
                    buttonCell.Content = cellName;
                }
            }
        }
        #endregion

        #region Initialize Dates
        private void InitializeDates()
        {
            dates = GetWeekDays(DateTime.Today);

            int currentDate = 0;
            // set dates on datesGrid
            foreach (var item in datesGrid.Children)
            {
                if (item is Label)
                {
                    Label dateLabel = item as Label;
                    dateLabel.Content = dates[currentDate++].ToString(dateFormat);
                }
            }
        }

        private List<DateTime> GetWeekDays(DateTime startDate)
        {
            var endDate = startDate.AddDays(7);
            //the number of days in our range of dates
            var numDays = (int)((endDate - startDate).TotalDays);
            List<DateTime> weekDates = Enumerable
                       //creates an IEnumerable of ints from 0 to numDays
                       .Range(0, numDays)
                       //now for each of those numbers (0..numDays), 
                       //select startDate plus x number of days
                       .Select(x => startDate.AddDays(x))
                       //and make a list
                       .ToList();

            return weekDates;
        }
        #endregion

        #region Initialize Matches
        public void OpenActivityWindow()
        {
            var activityWindow = new AddActivity();
            activityWindow.Show();
        }

        private void InitializeMatches()
        {
            GetMatches();
            PopulateCalendar();
        }

        private void PopulateCalendar()
        {
            foreach (var item in mainGrid.Children)
            {
                if (item is Grid)
                {
                    Grid grid = item as Grid;

                    string partialGridName = !String.IsNullOrWhiteSpace(grid.Name) && grid.Name.Length >= 4 ? grid.Name.Substring(0, 4) : grid.Name;

                    if (partialGridName == "time" )
                    {
                        for (int i = 1; i <= 7; i++)
                        {
                            if (grid.Children[i] is Button)
                            {
                                Button buttonCell = grid.Children[i] as Button;
                                
                                string cellName = buttonCell.Name;
                                Match match = GetCurrentCellMatch(cellName);

                                if (match != null)
                                {
                                    buttonCell.Background = cellOccupied;
                                }
                            }
                        }
                    }
                }
            }
        }

        private Match GetCurrentCellMatch(string cellName)
        {
            //"cell_13_12_2020_08_00"
            //  db 12/14/2020
            string[] cellFields = cellName.Split('_');
            string cellDate = $"{cellFields[2]}/{cellFields[1]}/{cellFields[3]}";

            Match match = null;
            using (var context = new SportsCentreDbContext())
            {
                match = context.Matches.Where(x => x.Date == cellDate).FirstOrDefault();
            }

            return match;
        }

        private void GetMatches()
        {
            matches.Clear();

            List<Match> itemList = new List<Match>();
            List<MatchInfo> matchList = new List<MatchInfo>();

            using (var context = new SportsCentreDbContext())
            {
                itemList = context.Matches.Include(x => x.Court).ToList();

                foreach (Match m in itemList)
                {
                    matches.Add(m);

                    var firstClub = context.Clubs.Find(m.FirstClubId);
                    var secondClub = context.Clubs.Find(m.SecondClubId);

                    if (firstClub != null && secondClub != null)
                    {
                        var matchInfo = new MatchInfo(m);
                        matchInfo.FirstClub = firstClub;
                        matchInfo.SecondClub = secondClub;

                        matchesInfo.Add(matchInfo);
                    }
                }
            }
        }
        #endregion
    }
}
