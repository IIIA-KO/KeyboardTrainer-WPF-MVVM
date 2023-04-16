namespace KeyboardTrainerModel.Interfaces
{
    public interface ITextService
    {
        Text? Add(Text text);
        Text? GetTextById(int id);
        List<Text> GetTextByComplexity(int complexity);
        List<Text> GetTextByLanguageCode(string code);
        void Delete(Func<Text, bool> filter);
    }
}
