namespace KeyboardTrainerModel.Interfaces
{
    public interface IUserService
    {
        User? Add(User user);
        User? GetUserById(int id);
        User? GetUserByLogin(string login);
        List<Score>? GetUserScores(int id);
        void Delete(Func<User, bool> filter, bool loadRalatedData = false);
    }
}
