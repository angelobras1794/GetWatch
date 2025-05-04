using GetWatch.Services;
public class Comment : IComment{

    public string User { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }

    public Comment(string user, string text)
    {
        User = user;
        Text = text;
        Timestamp = DateTime.Now;
    }
    public override string ToString()
    {
        return $"{Timestamp}: {User} - {Text}";
    }
}