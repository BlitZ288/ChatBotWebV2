namespace Coman
{
    public class BotAnswer
    {
        public string Content { get; set; }

        public string OwnerAnswer { get; set; }

        public BotAnswer(string content, string ownerAnswer)
        {
            this.Content = content;
            this.OwnerAnswer = ownerAnswer;
        }

    }
}
