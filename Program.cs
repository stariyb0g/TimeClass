using System;

namespace Demo_0002.TimeLib
{
    public class Time
    {
        private readonly uint seconds;

        public Time()
        {
            seconds = 0;
        }

        public Time(int hours, int minutes, int seconds)
        {
            this.seconds = (uint)((hours * 60 + minutes) * 60 + seconds);
        }

        public int Hours => (int)(seconds / 3600);
        public int Minutes => (int)(seconds / 60 % 60);
        public int Seconds => (int)(seconds % 60);

        public Time AddTime(Time other)
        {
            return new Time(0, 0, (int)(this.seconds + other.seconds));
        }

        public Time SubtractTime(Time other)
        {
            int resultSeconds = (int)this.seconds - (int)other.seconds;
            if (resultSeconds < 0)
            {
                resultSeconds = 0; 
            }
            return new Time(0, 0, resultSeconds);
        }

        public uint ToSeconds()
        {
            return seconds;
        }

        public static Time FromSeconds(uint seconds)
        {
            return new Time(0, 0, (int)seconds);
        }

        public override string ToString()
        {
            return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
        }

        public static Time FromFile(string filePath)
        {
            var lines = System.IO.File.ReadAllText(filePath).Split(':');
            int hours = int.Parse(lines[0]);
            int minutes = int.Parse(lines[1]);
            int seconds = int.Parse(lines[2]);
            return new Time(hours, minutes, seconds);
        }

        public void ToFile(string filePath)
        {
            System.IO.File.WriteAllText(filePath, ToString());
        }

        public static void Main(string[] args)
        {
            Time time1 = new Time(2, 45, 30);
            Time time2 = new Time(1, 20, 15);

            Time sum = time1.AddTime(time2);
            Time difference = time1.SubtractTime(time2);

            Console.WriteLine($"Час 1: {time1}");
            Console.WriteLine($"Час 2: {time2}");
            Console.WriteLine($"Сума: {sum}");
            Console.WriteLine($"Різниця: {difference}");

            uint seconds = time1.ToSeconds();
            Console.WriteLine($"Час 1 у секундах: {seconds}");

            Time fromSeconds = Time.FromSeconds(seconds);
            Console.WriteLine($"Час з секунд: {fromSeconds}");

            string filePath = "time.txt";
            time1.ToFile(filePath);
            Time readTime = Time.FromFile(filePath);
            Console.WriteLine($"Час з файлу: {readTime}");
        }
    }
}
