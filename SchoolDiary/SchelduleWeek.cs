using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;

namespace SchoolDiary
{

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }

    public class Lesson
    {
        public string Subject { get; set; } // Название предмета
        public DateTime StartTime { get; set; } // Время начала урока
        public DateTime EndTime { get; set; } // Время окончания урока
        public List<string> Grades { get; set; } = new List<string>(); // Оценки (максимум 3)
        public bool HasHomework { get; set; } // Есть ли домашнее задание

        public string Homework_Marking { get { if (HasHomework) return "Д"; else return ""; } set { } }

        public int RowIndex { get; set; } // Индекс строки в таблице
        public int ColumnIndex { get; set; } // Индекс столбца в таблице
    }
   
    public class DaySchedule
    {
        public DateTime Date { get; set; } // Дата дня недели
                                           // public Dictionary<string, Lesson> Lessons { get; set; } = new Dictionary<string, Lesson>(); // Расписание по времени

        public ObservableCollection<Lesson> Lessons { get; set; } = new ObservableCollection<Lesson>(); // Расписание по времени
    }

    public class ScheduleViewModel: INotifyPropertyChanged
    {
        private DateTime CurrentWeekStart { get; set; }

        private DateTime _currentDate { get; set; }
        
        public DateTime ButtonDateTag { get { var crdate = _currentDate; return crdate.AddDays(1); } }
        public string CurrentDateDisplay_ForTheWeek { get { _currentDate = _currentDate.AddDays(1); return _currentDate.ToString("d MMMM"); } }
         public string CurrentWeek { get { var Crdate = CurrentWeekStart; return Crdate.ToString("d MMMM") +'-' + Crdate.AddDays(6).ToString("d MMMM"); } }

        //public List<DaySchedule> WeekSchedule { get; set; }
        public ObservableCollection<Lesson> WeekSchedule { get; set; }
        public ICommand PreviousWeekCommand { get; }
        public ICommand NextWeekCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ScheduleViewModel()
        {
            PreviousWeekCommand = new RelayCommand(MoveToPreviousWeek);
            NextWeekCommand = new RelayCommand(MoveToNextWeek);
            CurrentWeekStart = DateTimeExtensions.StartOfWeek(new DateTime(2025, 3, 24), DayOfWeek.Monday);

            _currentDate = CurrentWeekStart.AddDays(-1);


            WeekSchedule = new ObservableCollection<Lesson>
{
    // Понедельник
    new Lesson
    {
        Subject = "Геометрия",
        StartTime = new DateTime(2025, 3, 24, 8, 0, 0),
        EndTime = new DateTime(2025, 3, 24, 8, 45, 0),
        Grades = new List<string> { "3", "4", "5" },
        HasHomework = true,
        
    },
    new Lesson
    {
        Subject = "Алгебра",
        StartTime = new DateTime(2025, 3, 24, 9, 0, 0),
        EndTime = new DateTime(2025, 3, 24, 9, 45, 0),
        Grades = new List<string> { "4" },
        HasHomework = false,
        
    },
    new Lesson
    {
        Subject = "Физика",
        StartTime = new DateTime(2025, 3, 24, 10, 0, 0),
        EndTime = new DateTime(2025, 3, 24, 10, 45, 0),
        Grades = new List<string> { "4", "5" },
        HasHomework = true,
       
    },

    // Вторник
    new Lesson
    {
        Subject = "История",
        StartTime = new DateTime(2025, 3, 25, 8, 0, 0),
        EndTime = new DateTime(2025, 3, 25, 8, 45, 0),
        Grades = new List<string> { "2" },
        HasHomework = false,
       
    },
    new Lesson
    {
        Subject = "Химия",
        StartTime = new DateTime(2025, 3, 25, 9, 0, 0),
        EndTime = new DateTime(2025, 3, 25, 9, 45, 0),
        Grades = new List<string> { "3" },
        HasHomework = true,
        
    },
    new Lesson
    {
        Subject = "Литература",
        StartTime = new DateTime(2025, 3, 25, 10, 0, 0),
        EndTime = new DateTime(2025, 3, 25, 10, 45, 0),
        Grades = new List<string> { "4", "5" },
        HasHomework = true,
        
    },

    // Среда
    new Lesson
    {
        Subject = "Биология",
        StartTime = new DateTime(2025, 3, 26, 8, 0, 0),
        EndTime = new DateTime(2025, 3, 26, 8, 45, 0),
        Grades = new List<string> { "5" },
        HasHomework = true,
        
    },
    new Lesson
    {
        Subject = "География",
        StartTime = new DateTime(2025, 3, 26, 9, 0, 0),
        EndTime = new DateTime(2025, 3, 26, 9, 45, 0),
        Grades = new List<string> { "4" },
        HasHomework = false,
        
    },
    new Lesson
    {
        Subject = "Физкультура",
        StartTime = new DateTime(2025, 3, 26, 10, 0, 0),
        EndTime = new DateTime(2025, 3, 26, 10, 45, 0),
        Grades = new List<string>(),
        HasHomework = false,
        
    },

    // Четверг
    new Lesson
    {
        Subject = "Обществознание",
        StartTime = new DateTime(2025, 3, 27, 8, 0, 0),
        EndTime = new DateTime(2025, 3, 27, 8, 45, 0),
        Grades = new List<string> { "4" },
        HasHomework = true,
        
    },
    new Lesson
    {
        Subject = "Английский язык",
        StartTime = new DateTime(2025, 3, 27, 9, 0, 0),
        EndTime = new DateTime(2025, 3, 27, 9, 45, 0),
        Grades = new List<string> { "5" },
        HasHomework = true,
        
    },
    new Lesson
    {
        Subject = "Информатика",
        StartTime = new DateTime(2025, 3, 27, 10, 0, 0),
        EndTime = new DateTime(2025, 3, 27, 10, 45, 0),
        Grades = new List<string> { "5" },
        HasHomework = false,
        
    },

    // Пятница
    new Lesson
    {
        Subject = "Математика",
        StartTime = new DateTime(2025, 3, 28, 8, 0, 0),
        EndTime = new DateTime(2025, 3, 28, 8, 45, 0),
        Grades = new List<string> { "5" },
        HasHomework = true,
        
    },
    new Lesson
    {
        Subject = "Русский язык",
        StartTime = new DateTime(2025, 3, 28, 9, 0, 0),
        EndTime = new DateTime(2025, 3, 28, 9, 45, 0),
        Grades = new List<string> { "4", "5" },
        HasHomework = true,
        
    },
    new Lesson
    {
        Subject = "ОБЖ",
        StartTime = new DateTime(2025, 3, 28, 10, 0, 0),
        EndTime = new DateTime(2025, 3, 28, 10, 45, 0),
        Grades = new List<string> { "5" },
        HasHomework = false,
       
    },

    // Суббота
    new Lesson
    {
        Subject = "Технология",
        StartTime = new DateTime(2025, 3, 29, 8, 0, 0),
        EndTime = new DateTime(2025, 3, 29, 8, 45, 0),
        Grades = new List<string> { "4" },
        HasHomework = false,
        
    },
    new Lesson
    {
        Subject = "Музыка",
        StartTime = new DateTime(2025, 3, 29, 9, 0, 0),
        EndTime = new DateTime(2025, 3, 29, 9, 45, 0),
        Grades = new List<string> { "5" },
        HasHomework = false,
        
    }
};


            



            AssignRowAndColumnIndexes();
        }
        private void MoveToPreviousWeek()
        {
            CurrentWeekStart = CurrentWeekStart.AddDays(-7);
            _currentDate = CurrentWeekStart.AddDays(-1);
            OnPropertyChanged(nameof(CurrentWeek));
            OnPropertyChanged(nameof(CurrentDateDisplay_ForTheWeek));
        }

        private void MoveToNextWeek()
        {
            CurrentWeekStart = CurrentWeekStart.AddDays(7);
            _currentDate = CurrentWeekStart.AddDays(-1);
            OnPropertyChanged(nameof(CurrentWeek));
            OnPropertyChanged(nameof(CurrentDateDisplay_ForTheWeek));
        }

        private void AssignRowAndColumnIndexes()
        {
            // Список временных интервалов для строк
            var timeSlots = new List<TimeSpan>
            {
                new TimeSpan(8, 0, 0),  // 8:00 - 8:45
                new TimeSpan(9, 0, 0),  // 9:00 - 9:45
                new TimeSpan(10, 0, 0), // 10:00 - 10:45
                new TimeSpan(11, 0, 0), // 11:00 - 11:45
                new TimeSpan(12, 0, 0), // 12:00 - 12:45
                new TimeSpan(13, 0, 0), // 13:00 - 13:45
                new TimeSpan(14, 0, 0)  // 14:00 - 14:45
            };

            // Маппинг дней недели к индексам столбцов
            var dayToColumnIndex = new Dictionary<DayOfWeek, int>
            {
                { DayOfWeek.Monday, 2 },    // Понедельник
                { DayOfWeek.Tuesday, 4 },   // Вторник
                { DayOfWeek.Wednesday, 6 }, // Среда
                { DayOfWeek.Thursday, 8 },  // Четверг
                { DayOfWeek.Friday, 10 },   // Пятница
                { DayOfWeek.Saturday, 12 }  // Суббота
            };

            // Присваиваем индексы строк и столбцов
            foreach (var lesson in WeekSchedule)
            {
                //foreach (var lesson in daySchedule.Lessons)
                //{
                // Определяем индекс строки по времени начала
                int rowIndex = timeSlots.IndexOf(lesson.StartTime.TimeOfDay) * 2 + 2; // Учет отступов между строками
                if (rowIndex == -1)
                {
                    throw new InvalidOperationException($"Неверное время начала урока: {lesson.StartTime}");
                }

                // Определяем индекс столбца по дню недели
                if (!dayToColumnIndex.TryGetValue(lesson.StartTime.DayOfWeek, out int columnIndex))
                {
                    throw new InvalidOperationException($"Неверный день недели: {lesson.StartTime.DayOfWeek}");
                }

                // Присваиваем индексы
                lesson.RowIndex = rowIndex;
                lesson.ColumnIndex = columnIndex;
                // }
            }
        }
    }
}