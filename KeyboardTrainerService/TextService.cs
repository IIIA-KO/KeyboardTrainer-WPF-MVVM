using KeyboardTrainerDAL;
using KeyboardTrainerModel;
using KeyboardTrainerModel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace KeyboardTrainerService
{
    public class TextService : ITextService
    {
        private readonly DataContext context;

        public TextService(DataContext context)
        {
            this.context = context;
        }

        public Text? Add(Text text)
        {
            Text? added;
            try
            {
                added = this.context.Texts.Add(text).Entity;
                this.context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                added = null;
            }
            return added;
        }

        public void Delete(Func<Text, bool> filter)
        {
            var textsToDelete = this.context.Texts.Where(filter).ToList();
            if (textsToDelete != null)
            {
                this.context.Texts.RemoveRange(textsToDelete);
                this.context.SaveChanges();
            }
        }

        public List<Text> GetTextByComplexity(int complexity)
        {
            return this.context.Texts.Where(t => t.Complexity == complexity).ToList();
        }

        public Text? GetTextById(int id)
        {
            return this.context.Texts.Find(id);
        }

        public List<Text> GetTextByLanguageCode(string code)
        {
            return this.context.Texts.Where(t => t.LanguageCode == code).ToList();
        }
    }
}
