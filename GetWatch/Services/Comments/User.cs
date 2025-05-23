using GetWatch.Services; // Adjusted to match the correct namespace if CommentSection is within Services

public class User : IObserver<Comment>
{
    public string Name { get; set; }
    public List<Comment> Comments { get; set; }

    public User(string name)
    {
        Name = name;
        Comments = new List<Comment>();
    }

    public void SubscribeTo(CommentSection section)
    {
        section.Subscribe(this);
        Console.WriteLine($"{Name} subscribed to {section.Name}");
    }

    public void UnsubscribeFrom(CommentSection section)
    {
        section.Unsubscribe(this);
        Console.WriteLine($"{Name} unsubscribed from {section.Name}");
    }

    public void onNotified(Comment comment)
    {
        Comments.Add(comment);
        Console.WriteLine($"{Name} received a new notification: {comment}");
    }
}