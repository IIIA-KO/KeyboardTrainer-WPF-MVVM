namespace KeyboardTrainerModel
{
    public class Text
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; } = null!;
        public string TextContent { get; set; } = null!;
        public int Complexity { get; set; }
    }
}