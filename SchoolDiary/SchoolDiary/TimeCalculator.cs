using System;

namespace SchoolDiary
{    public class TimeCalculator
    {
        // Метод для вычисления времён
        public static (string UpperTime, string LowerTime) CalculateTimes(DateTime baseTime, int index)
        {
            // Интервалы времени
            TimeSpan interval = TimeSpan.FromMinutes(45); // 45 минут для каждого интервала
            TimeSpan breakTime = TimeSpan.FromMinutes(10); // 10 минут перерыва

            // Вычисляем общий интервал для одного блока
            TimeSpan totalInterval = interval + breakTime;

            // Вычисляем верхнее время
            DateTime upperTime = baseTime.Add(TimeSpan.FromTicks(totalInterval.Ticks * index));

            // Вычисляем нижнее время
            DateTime lowerTime = upperTime.Add(interval);

            // Возвращаем времена в формате строки
            return (upperTime.ToString("HH:mm"), lowerTime.ToString("HH:mm"));
        }
    }
}
