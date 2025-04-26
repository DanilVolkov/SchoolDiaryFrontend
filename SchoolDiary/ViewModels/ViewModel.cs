using SchoolDiary.APIConnect;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SchoolDiary.Models;

namespace SchoolDiary
{
    public class ViewModel: INotifyPropertyChanged
    {
        // Словарь для хранения расписания по дням
        private Dictionary<DateTime, ObservableCollection<Models.Content>> _scheduleByDate;
        
        // Текущая дата
        private DateTime _currentDate;

        // Свойство для привязки к интерфейсу
        private ObservableCollection<Models.Content> _subjects;

        public ObservableCollection<Models.Content> Subjects
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
        public ViewModel(List<Models.DaySchedule> schedule, DateTime currentDate)
        {
            _currentDate = currentDate;
            // Преобразуем данные из API в словарь
            _scheduleByDate = new Dictionary<DateTime, ObservableCollection<Models.Content>>();

            ContentToObservable(schedule, currentDate);

            // Инициализация команд
            PreviousDayCommand = new RelayCommand(PreviousDay);
            NextDayCommand = new RelayCommand(NextDay);
        }

        public ViewModel() {
            _currentDate = DateTime.Now;
            _scheduleByDate = new Dictionary<DateTime, ObservableCollection<Models.Content>>();

            PreviousDayCommand = new RelayCommand(PreviousDay);
            NextDayCommand = new RelayCommand(NextDay);
        }

        //refactoring addition 1 - prevented code copying
        private void ContentToObservable(List<Models.DaySchedule> schedule, DateTime Date)
        {
            foreach (var day in schedule)
            {
                var subjects = new ObservableCollection<Models.Content>();

                foreach (var content in day.Content)
                {
                    subjects.Add(new Content
                    {
                        Lesson = new Models.Lesson()
                        {
                            Subject = new Subject() { Name = content.Lesson?.Subject?.Name ?? "Нет предмета" },
                            Teacher = new Teacher()
                            {
                                FirstName = content.Lesson?.Teacher?.FirstName ?? "Не указано",
                                MiddleName = content.Lesson?.Teacher?.MiddleName ?? "Не указано",
                                LastName = content.Lesson?.Teacher?.LastName ?? "Не указано"
                            },
                            Classroom = new Classroom()
                            {
                                Name = content.Lesson?.Classroom.Name ?? "Не указано"
                            },
                            TimeStart = content.Lesson?.TimeStart ?? DateTime.MinValue,
                            TimeEnd = content.Lesson?.TimeEnd ?? DateTime.MaxValue
                        },
                        Homework = new Homework()
                        {
                            Description = content.Homework?.Description ?? ""
                        },
                        Marks = content.Marks ?? new List<Mark>()
                    }) ;
                }

                _scheduleByDate[day.Date] = subjects;
            }

            // Установка текущей даты
            SetupCurrentDate(Date);
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
        public async void PreviousDay()
        {
            var previousDate = _currentDate.AddDays(-1);
            if (previousDate.DayOfWeek == DayOfWeek.Sunday) { previousDate = previousDate.AddDays(-1); }
            await LoadScheduleForDate(previousDate);
            _currentDate = previousDate;
            OnPropertyChanged(nameof(_scheduleByDate));
            OnPropertyChanged(nameof(Subjects));
            OnPropertyChanged(nameof(CurrentDateDisplay));
        }

        // Метод для перехода к следующему дню
        public  async void NextDay()
        {   
            var nextDate = _currentDate.AddDays(1);
            if (nextDate.DayOfWeek == DayOfWeek.Sunday) { nextDate = nextDate.AddDays(1); }
            await LoadScheduleForDate(nextDate);
            _currentDate = nextDate;
            OnPropertyChanged(nameof(_scheduleByDate));
            OnPropertyChanged(nameof(Subjects));
            OnPropertyChanged(nameof(CurrentDateDisplay));
        }

        private async Task LoadScheduleForDate(DateTime date)
        {
            try
            {
                var apiConnector = APIConnector.GetInstance();
                var schedule = await apiConnector.GetWeekSchedule(date, date);
                
                // Очищаем текущий список уроков
                _scheduleByDate.Clear();

                // Преобразуем данные из API в формат ObservableCollection<Subject>
                ContentToObservable(schedule, date);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки расписания: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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