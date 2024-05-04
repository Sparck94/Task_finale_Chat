namespace task_chat.Services
{
    public interface IService<T>
    {
        IEnumerable<T> PrendiTutto();
    }
}
