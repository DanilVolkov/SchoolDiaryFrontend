using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SchoolDiary_wpf
{
    public class Subject
    {
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public string RoomNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Homework { get; set; }
        public List<string> Grades { get; set; }
    }

    public class ViewModel: INotifyPropertyChanged
    {
        // Словарь для хранения расписания по дням
        private Dictionary<DateTime, ObservableCollection<Subject>> _scheduleByDate;
        
        // Текущая дата
        private DateTime _currentDate;

        // Свойство для привязки к интерфейсу
        private ObservableCollection<Subject> _subjects;

        public ObservableCollection<Subject> Subjects
        {
            get => _subjects;
            set
            {
                _subjects = value;
                OnPropertyChanged(nameof(Subjects));
            }
        }

        public ICommand PreviousDayCommand { get; }
        public ICommand NextDayCommand { get; }

        public string CurrentDateDisplay => _currentDate.ToString("d MMMM, dddd");

        public ViewModel()
        {
            // Инициализация словаря с расписанием
            _scheduleByDate = new Dictionary<DateTime, ObservableCollection<Subject>>
        {
            {
                new DateTime(2025, 3, 24), // Пример: 24 марта
                new ObservableCollection<Subject>
                {
                    new Subject
                    {
                        Name = "Математика",
                        TeacherName = "Иванов И.И.",
                        RoomNumber = "101",
                        StartTime = "08:00",
                        EndTime = "09:30",
                        Homework = "Решить задачи №1-5",
                        Grades = new List<string> { "5", "3", "5" }
                    },
                    new Subject
                    {
                        Name = "Геометрия",
                        TeacherName = "Петров П.П.",
                        RoomNumber = "202",
                        StartTime = "09:40",
                        EndTime = "11:10",
                        Homework = "Пересказ параграфов 1-2",
                        Grades = new List<string> { "4", "4", "2" }
                    }
                }
            },
            {new DateTime(2025, 3, 23),
                new ObservableCollection<Subject> {
                    new Subject
                    {
                        Name = "Алгебра и геометрия",
                        TeacherName = "Чирков А.Ю.",
                        RoomNumber = "511(Корпус № 6)",
                        StartTime = "10:00",
                        EndTime = "16:00",
                        Homework = "Подготовить доклад",
                        Grades = new List<string> { "2", "2", "2" }
                    },
                }
            },



            {
                new DateTime(2025, 3, 25), // Пример: 25 марта
                new ObservableCollection<Subject>
                {
                    new Subject
                    {
                        Name = "История",
                        TeacherName = "Сидоров С.С.",
                        RoomNumber = "303",
                        StartTime = "10:00",
                        EndTime = "11:30",
                        Homework = "Подготовить доклад",
                        Grades = new List<string> { "5", "4", "5" }
                    },
                     new Subject
                    {
                        Name = "Геометрия",
                        TeacherName = "Васнецова В.С.",
                        RoomNumber = "303",
                        StartTime = "10:00",
                        EndTime = "11:30",
                        Homework = "Подготовить доклад",
                        Grades = new List<string> { "2", "3" }
                    },
                     new Subject
                    {
                        Name = "Геометрия",
                        TeacherName = "Васнецова В.С.",
                        RoomNumber = "303",
                        StartTime = "10:00",
                        EndTime = "11:30",
                        Homework = "Подготовить доклад",
                        Grades = new List<string> { "2", "3" }
                    },
                     new Subject
                    {
                        Name = "Геометрия",
                        TeacherName = "Васнецова В.С.",
                        RoomNumber = "303",
                        StartTime = "10:00",
                        EndTime = "11:30",
                        Homework = "Подготовить доклад",
                        Grades = new List<string> { "2", "3" }
                    },
                     new Subject
                    {
                        Name = "Геометрия",
                        TeacherName = "Васнецова В.С.",
                        RoomNumber = "303",
                        StartTime = "10:00",
                        EndTime = "11:30",
                        Homework = "Подготовить доклад",
                        Grades = new List<string> { "2", "3" }
                    },
                     new Subject
                    {
                        Name = "Геометрия",
                        TeacherName = "Васнецова В.С.",
                        RoomNumber = "303",
                        StartTime = "10:00",
                        EndTime = "11:30",
                        Homework = "Подготовить доклад",
                        Grades = new List<string> { "2", "3" }
                    }

                }
            }

        };

            // Установка начальной даты
            _currentDate = new DateTime(2025, 3, 24);
            Subjects = _scheduleByDate[_currentDate];

            // Инициализация команд
            PreviousDayCommand = new RelayCommand(PreviousDay);
            NextDayCommand = new RelayCommand(NextDay);
        }


        public void SetupCurrentDate(DateTime currentDate)
        {
            if (_scheduleByDate.ContainsKey(currentDate))
            {
                _currentDate = currentDate;
                Subjects = _scheduleByDate[_currentDate];
                OnPropertyChanged(nameof(CurrentDateDisplay)); // Обновляем отображение даты
            }
            else
            {
                _currentDate = new DateTime(2025, 3, 24);
                Subjects = _scheduleByDate[_currentDate];
                OnPropertyChanged(nameof(CurrentDateDisplay));
            }
        }

        // Метод для перехода к предыдущему дню
        public void PreviousDay()
        {
            var previousDate = _currentDate.AddDays(-1);
            if (_scheduleByDate.ContainsKey(previousDate))
            {
                _currentDate = previousDate;
                Subjects = _scheduleByDate[_currentDate];
                OnPropertyChanged(nameof(CurrentDateDisplay)); // Обновляем отображение даты
            }
        }

        // Метод для перехода к следующему дню
        public void NextDay()
        {
            var nextDate = _currentDate.AddDays(1);
            if (_scheduleByDate.ContainsKey(nextDate))
            {
                _currentDate = nextDate;
                Subjects = _scheduleByDate[_currentDate];
                OnPropertyChanged(nameof(CurrentDateDisplay)); // Обновляем отображение даты
            }
        }

        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}