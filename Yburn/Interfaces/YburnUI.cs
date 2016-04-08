namespace Yburn.Interfaces
{
	public interface YburnUI
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