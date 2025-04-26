using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using SchoolDiary.APIConnect;
using System.Windows;

namespace SchoolDiary
{
    public class ScheduleViewModel : INotifyPropertyChanged
    {
        private DateTime CurrentWeekStart { get; set; }

        private DateTime _currentDate;
        private DateTime _buttonDateTag;
        public DateTime ButtonDateTag { get { _buttonDateTag = _buttonDateTag.AddDays(1); return _buttonDateTag; } }
        public string CurrentDateDisplay_ForTheWeek { get { _currentDate = _currentDate.AddDays(1); OnPropertyChanged(nameof(_currentDate)); return _currentDate.ToString("d MMMM"); } }
        public string CurrentWeek { get { var Crdate = CurrentWeekStart; return Crdate.ToString("d MMMM") +'-' + Crdate.AddDays(6).ToString("d MMMM"); } }

       
        public ObservableCollection<Models.Content> WeekSchedule { get; set; }
        public ICommand PreviousWeekCommand { get; }
        public ICommand NextWeekCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        

        public ScheduleViewModel(List<Models.DaySchedule> schedule, DateTime crtday)
        {
            if (WeekSchedule != null) { WeekSchedule.Clear(); }
            CurrentWeekStart = StartOfWeek(crtday, DayOfWeek.Monday);

            _currentDate = CurrentWeekStart.AddDays(-1);
            _buttonDateTag = _currentDate;
            WeekSchedule = new ObservableCollection<Models.Content>();
            UpdateWeekSchedule(schedule);
            
            
            PreviousWeekCommand = new RelayCommand(MoveToPreviousWeek);
            NextWeekCommand = new RelayCommand(MoveToNextWeek);
            AssignRowAndColumnIndexes();
        }

        private static DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        private async Task LoadWeekSchedule(DateTime from, DateTime to)
        {
            try
            {
                var apiConnector = APIConnector.GetInstance();

                
                var schedule = await apiConnector.GetWeekSchedule(from, to);
                CurrentWeekStart = StartOfWeek(from, DayOfWeek.Monday);
                _currentDate = CurrentWeekStart.AddDays(-1);
                _buttonDateTag = _currentDate;

                UpdateWeekSchedule(schedule);

                OnPropertyChanged(nameof(_currentDate));
                OnPropertyChanged(nameof(CurrentWeek));
                OnPropertyChanged(nameof(ButtonDateTag));
                OnPropertyChanged(nameof(_buttonDateTag));
                OnPropertyChanged(nameof(CurrentDateDisplay_ForTheWeek));
                OnPropertyChanged(nameof(WeekSchedule));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки расписания: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void UpdateWeekSchedule(List<Models.DaySchedule> schedule)
        {
            WeekSchedule.Clear();
            WeekSchedule = new ObservableCollection<Models.Content>();

            foreach (var day in schedule)
            {
                foreach (var content in day.Content)
                {
                    
                    WeekSchedule.Add(content);
                }
            }

            AssignRowAndColumnIndexes();
          
        }

        private async void MoveToPreviousWeek()
        {
            CurrentWeekStart = CurrentWeekStart.AddDays(-7);
            await LoadWeekSchedule(CurrentWeekStart, CurrentWeekStart.AddDays(6));
            


        }

        private async void MoveToNextWeek()
        {
            CurrentWeekStart = CurrentWeekStart.AddDays(7);
            await LoadWeekSchedule(CurrentWeekStart, CurrentWeekStart.AddDays(6));
           
        }
            
        private void AssignRowAndColumnIndexes()
        {
            // Список временных интервалов для строк
            var timeSlots = new List<TimeSpan>
            {
                new TimeSpan(8, 30, 0),
                new TimeSpan(9, 25, 0),
                new TimeSpan(10, 20, 0),
                new TimeSpan(11, 15, 0),
                new TimeSpan(12, 20, 0),
                new TimeSpan(13, 15, 0),
                new TimeSpan(14, 10, 0)
            };

            // Маппинг дней недели к индексам столбцов
            var dayToColumnIndex = new Dictionary<DayOfWeek, int>
            {
                { DayOfWeek.Monday, 2 },
                { DayOfWeek.Tuesday, 4 },
                { DayOfWeek.Wednesday, 6 },
                { DayOfWeek.Thursday, 8 },
                { DayOfWeek.Friday, 10 },
                { DayOfWeek.Saturday, 12 }
            };

           
            foreach (var Content in WeekSchedule)
            {
              
                int rowIndex = timeSlots.IndexOf(Content.Lesson.TimeStart.TimeOfDay) * 2 + 2; // Учет отступов между строками
                if (rowIndex == -1)
                {
                    throw new InvalidOperationException($"Неверное время начала урока: {Content.Lesson.TimeStart}");
                }

                
                if (!dayToColumnIndex.TryGetValue(Content.Lesson.Date.DayOfWeek, out int columnIndex))
                {
                    throw new InvalidOperationException($"Неверный день недели: {Content.Lesson.Date.DayOfWeek}");
                }

                // Присваиваем индексы
                Content.Lesson.RowIndex = rowIndex;
                Content.Lesson.ColumnIndex = columnIndex;
               
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}