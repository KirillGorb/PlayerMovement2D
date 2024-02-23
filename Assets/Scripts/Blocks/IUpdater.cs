namespace Play.Block
{
    public interface IUpdater
    {
        public void Update();
    }
    
    public interface IUpdater<T>
    {
        public T Update();
    }
}