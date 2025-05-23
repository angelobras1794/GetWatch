using GetWatch.Services;

public interface IComment
{
    public string User { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
}