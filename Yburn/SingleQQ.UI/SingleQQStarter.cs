using System;
using System.Windows.Forms;
using Yburn.Interfaces;

namespace Yburn.SingleQQ.UI
{
	public class SingleQQStarter : YburnUI
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public SingleQQStarter()
		{
		}

		/********************************************************************************************
		 * Public static members, functions and properties
		 ********************************************************************************************/

		[STAThread]
		public static void Main()
		{
			try
			{
				YburnStarter.Execute(new SingleQQStarter(), "SingleQQ");
			}
			catch(Exception exception)
			{
				MessageBox.Show(exception.ToString(), exception.GetType().Name,
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

		public void Run()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new SingleQQMainWindow(Title, JobOrganizer));
		}

		public string Title
		{
			get;
			set;
		}

		public JobOrganizer JobOrganizer
		{
			get;
			set;
		}
	}
}