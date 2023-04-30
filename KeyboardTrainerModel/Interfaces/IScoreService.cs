namespace KeyboardTrainerModel.Interfaces
{
    public interface IScoreService
    {
        Score? Add(Score score);
        Score? GetScoreById(int id);
        List<Score> Search(Func<Score, bool> filter, bool loadRalatedData = false);
        void Delete(Func<Score, bool> filter, bool loadRalatedData = false);
    }
}