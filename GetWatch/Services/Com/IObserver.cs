using GetWatch.Services;

public interface IObserver<T>
{
    void onNotified(T data);
}
