using KeyboardTrainerDAL;
using KeyboardTrainerModel;
using KeyboardTrainerModel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KeyboardTrainerService
{
    public class UserService : IUserService
    {
        private readonly DataContext context;

        public UserService(DataContext context)
        {
            this.context = context;
        }

        public User? Add(User user)
        {
            User? added;
            try
            {
                added = this.context.Users.Add(user).Entity;
                this.context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                added = null;
            }
            return added;
        }

        public void Delete(Func<User, bool> filter, bool loadRalatedData = false)
        {
            var usersToDelete = (loadRalatedData) ? this.context.Users.Include(u => u.Scores).Where(filter).ToList() : this.context.Users.Where(filter).ToList();
            if (usersToDelete != null)
            {
                this.context.Users.RemoveRange(usersToDelete);
                this.context.SaveChanges();
            }
        }

        public User? GetUserById(int id)
        {
            return this.context.Users.Find(id);
        }

        public User? GetUserByLogin(string login)
        {
            return this.context.Users.FirstOrDefault(u => u.Login == login);
        }

        public List<Score>? GetUserScores(int id)
        {
            return this.context.Users.Include(u => u.Scores).FirstOrDefault(u => u.Id == id).Scores;
        }
    }
}
