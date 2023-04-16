namespace KeyboardTrainerModel.Interfaces
{
    public interface IScoreService
    {
        Score? Add(Score score);
        Score? GetScoreById(int id);
        void Delete(Func<Score, bool> filter, bool loadRalatedData = false);
    }
}