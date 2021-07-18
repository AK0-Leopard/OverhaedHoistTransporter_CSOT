using System;
using System.Threading;
using System.Windows.Forms;

namespace Mirle.BigDataCollection
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            bool isFirstOpen;
            Mutex mutex = new Mutex(false, Application.ProductName, out isFirstOpen);

            if (isFirstOpen)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new DataCollectionService());
            }
            else
            {
                MessageBox.Show("The application has started!");
            }
        }
    }
}