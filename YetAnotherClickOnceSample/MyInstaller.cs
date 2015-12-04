using System;
using System.Text;
using System.Deployment.Application;

namespace YetAnotherClickOnceSample
{
    // ref. from https://msdn.microsoft.com/ja-jp/library/dd997001.aspx
    public class MyInstaller
    {
        private InPlaceHostingManager iphm;
        private IPrinter printer;

        public MyInstaller(IPrinter printer)
        {
            this.printer = printer;
        }

        public void InstallApplication(string deployManifestUriStr)
        {
            try
            {
                var deploymentUri = new Uri(deployManifestUriStr);
                iphm = new InPlaceHostingManager(deploymentUri, false);
            }
            catch (UriFormatException uriEx)
            {
                printer.PrintError("Cannot install the application: " +
                    "The deployment manifest URL supplied is not a valid URL. " +
                    "Error: {0}", uriEx.Message);
            }
            catch (PlatformNotSupportedException pfEx)
            {
                printer.PrintError("Cannot install the application: " +
                    "This program requires adequate Windows version. " +
                    "Error: {0}", pfEx.Message);
            }
            catch (ArgumentException aEx)
            {
                printer.PrintError("Cannot install the application: " +
                    "The deployment manifest URL supplied is not a valid URL. " +
                    "Error: {0}", aEx.Message);
            }

            iphm.GetManifestCompleted += new EventHandler<GetManifestCompletedEventArgs>(iphm_GetManifestCompleted);
            iphm.GetManifestAsync();

            printer.PrintDebug("Installing...");
        }

        private void iphm_GetManifestCompleted(object sender, GetManifestCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                printer.PrintError("Could not download manifest. Error: {0}", e.Error.Message);
                return;
            }

            try
            {
                iphm.AssertApplicationRequirements(true);
            }
            catch (Exception ex)
            {
                printer.PrintError("An error occurred while verifying the application. " +
                    "Error: {0}", ex.Message);
                return;
            }

            var appInfo = new StringBuilder();
            appInfo.Append("Application Name: ");
            appInfo.Append(e.ProductName); ;
            appInfo.Append("\nVersion: ");
            appInfo.Append(e.Version);
            appInfo.Append("\nSupport/Help Requests: ");
            appInfo.Append(e.SupportUri != null ? e.SupportUri.ToString() : "N/A");
            appInfo.Append("\n\nConfirmed that this application can run with its requested permissions.");
            printer.Print(appInfo.ToString());

            iphm.DownloadProgressChanged += new EventHandler<DownloadProgressChangedEventArgs>(iphm_DownloadProgressChanged);
            iphm.DownloadApplicationCompleted += new EventHandler<DownloadApplicationCompletedEventArgs>(iphm_DownloadApplicationCompleted);

            try
            {
                iphm.DownloadApplicationAsync();

                printer.PrintDebug("Downloading...");
            }
            catch (Exception ex)
            {
                printer.PrintError("Cannot initiate download of application. Error: {0}", ex.Message);
                return;
            }

            printer.PrintDebug("End GetManifestCompleted callback");
        }

        private void iphm_DownloadApplicationCompleted(object sender, DownloadApplicationCompletedEventArgs e)
        {
            printer.PrintDebug("Begin DownloadApplicationCompleted callback");

            if (e.Error != null)
            {
                printer.PrintError("Could not download and install application. Error: {0}", e.Error.Message);
                return;
            }

            printer.Print("Application installed! You may now run it from the Start menu.");
        }

        private void iphm_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            printer.PrintDebug("Begin DownloadProgressChanged callback");

            printer.PrintDebug("State: {0}, All: {1}bytes, Now: {2}bytes, Progress: {3}%",
                e.State, e.BytesDownloaded, e.TotalBytesToDownload, e.ProgressPercentage);
        }
    }
}
