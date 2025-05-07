using GetWatch.Services;

public interface ICommentSection
{
    void AddItem(Comment item);
    List<Comment> GetItems();
}