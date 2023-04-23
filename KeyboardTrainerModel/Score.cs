namespace KeyboardTrainerModel
{
    public class Score
    {
        public int Id { get; set; }
        public User User { get; set; } = null!;
        public Text Text { get; set; } = null!;
        public DateTime SessionBeginning { get; set; }
        public TimeSpan Duration { get; set; }
        public int Fails { get; set; }
        public double Speed { get; set; }
        public double Complexity { get; set; }
    }
}