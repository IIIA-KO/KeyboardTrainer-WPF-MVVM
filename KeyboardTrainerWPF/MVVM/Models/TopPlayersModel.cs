using System;

namespace KeyboardTrainerWPF.MVVM.Models
{
    public class TopUsersModel
    {
        public TopUsersModel(int place, string? user, double speedUser, TimeSpan durationUser, int fails, double complexity)
        {
            Place = place;
            User = user;
            Speed = speedUser;
            Duration = durationUser;
            Fails = fails;
            Complexity = complexity;
        }

        public string? User { get; set; }
        public TimeSpan Duration { get; set; }
        public double Speed { get; set; }
        public int Place { get; set; }
        public int Fails { get; set; }
        public double Complexity { get; set; }
    }
}
