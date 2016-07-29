using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Yburn.Interfaces;

namespace Yburn
{
    public static class YburnStarter
    {
        /********************************************************************************************
		 * Public members, functions and properties
		 ********************************************************************************************/

        public static void Execute(
            WorkerStarter workerStarter,
            string workerName
            )
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

                FullNameVersion = NameVersion + " - " + workerName;

                workerStarter.Title = FullNameVersion;
                workerStarter.JobOrganizer = CreateBackgroundService(workerName);
                workerStarter.Run();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.ToString(), exception.GetType().Name,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /********************************************************************************************
		 * Private/protected static members, functions and properties
		 ********************************************************************************************/

        private static readonly string NameVersion = "Yburn 6.0";

        private static string FullNameVersion;

        private static JobOrganizer CreateBackgroundService(
            string workerName
            )
        {
            BackgroundService backgroundService = new BackgroundService();

            Worker worker = WorkerLoader.CreateInstance(workerName);
            worker.NameVersion = FullNameVersion;
            backgroundService.SetWorker(worker);

            backgroundService.ProcessParameterFile(YburnConfigFile.LastParaFile);

            return backgroundService;
        }
    }
}