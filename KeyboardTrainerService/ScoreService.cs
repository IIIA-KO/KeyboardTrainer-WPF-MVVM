using KeyboardTrainerDAL;
using KeyboardTrainerModel;
using KeyboardTrainerModel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KeyboardTrainerService
{
    public class ScoreService : IScoreService
    {
        private readonly DataContext context;

        public ScoreService(DataContext context)
        {
            this.context = context;
        }

        public Score? Add(Score score)
        {
            Score? added;
            try
            {
                added = this.context.Scores.Add(score).Entity;
                this.context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                added = null;
            }
            return added;
        }

        public void Delete(Func<Score, bool> filter, bool loadRalatedData = false)
        {
            var scoresToDelete = (loadRalatedData) ? this.context.Scores.Include(s => s.User).Where(filter).ToList() : this.context.Scores.Where(filter).ToList();
            if (scoresToDelete != null)
            {
                this.context.Scores.RemoveRange(scoresToDelete);
                this.context.SaveChanges();
            }
        }

        public Score? GetScoreById(int id)
        {
            return this.context.Scores.Find(id);
        }

        public List<Score> Search(Func<Score, bool> filter, bool loadRalatedData = false)
            => (loadRalatedData) ? 
            this.context.Scores.Include(s => s.User).Include(s => s.Text).Where(filter).ToList() 
            : this.context.Scores.Where(filter).ToList();
    }
}
