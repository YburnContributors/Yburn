using System;
using System.Windows.Forms;
using Yburn.Interfaces;

namespace Yburn.QQonFire.UI
{
	public class QQonFireStarter : YburnUI
	{
		/********************************************************************************************
		 * Constructors
		 ********************************************************************************************/

		public QQonFireStarter()
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
				YburnStarter.Execute(new QQonFireStarter(), "QQonFire");
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
			Application.Run(new QQonFireMainWindow(Title, JobOrganizer));
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