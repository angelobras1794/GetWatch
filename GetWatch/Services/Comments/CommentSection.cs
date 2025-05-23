using GetWatch.Services;

public class CommentSection : IObservable<Comment>,ICommentSection
{

    public string Name { get; set; }
    private readonly List<Comment> _items;
    private readonly List<IObserver<Comment>> _observers = new();

    public CommentSection(string name)
    {
        Name = name;
        _items = new List<Comment>();
    }

    public void Subscribe(IObserver<Comment> observer)
    {
        _observers.Add(observer);
    }

    public void Unsubscribe(IObserver<Comment> observer)
    {
        _observers.Remove(observer);
    }


    public void Notify(Comment data)
    {
        foreach (var observer in _observers)
        {
            observer.onNotified(data);
        }
    }

    public void AddItem(Comment item)
    {
        _items.Add(item);
        Notify(item);
    }

    public List<Comment> GetItems()
    {
        return _items;
    }

}