using System;

namespace KeyboardTrainerWPF.MVVM.Models
{
    public class UserScoreModel
    {
        public UserScoreModel(string? user, DateTime sessionBeginning, TimeSpan duration, int fails, double speed, double complexity)
        {
            User = user;
            SessionBeginning = sessionBeginning;
            Duration = duration;
            Fails = fails;
            Speed = speed;
            Complexity = complexity;
        }

        public string? User { get; set; }
        public DateTime SessionBeginning { get; set; }
        public TimeSpan Duration { get; set; }
        public int Fails { get; set; }
        public double Speed { get; set; }
        public double Complexity { get; set; }

        public override string ToString() 
            => $"{User}|{SessionBeginning}|{Duration}|{Fails}|{Speed}|{Complexity}\n";
    }
}
