namespace Yburn.Interfaces
{
    public interface WorkerStarter
    {
        void Run();

        string Title
        {
            get;
            set;
        }

        JobOrganizer JobOrganizer
        {
            get;
            set;
        }
    }
}