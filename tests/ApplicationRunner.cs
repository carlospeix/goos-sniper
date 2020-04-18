using System;
using System.IO;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace GoosSniper.Tests
{
    internal class ApplicationRunner
    {
        private const string XMPP_HOSTNAME = "localhost";
        private const string SNIPER_ID = "sniper";
        private const string SNIPER_PASSWORD = "sniper";

        private const string appPath = @"C:\Users\carlos\Repositories\goos-sniper\sniper\bin\Debug\sniper.exe";

        private Application app;

        internal ApplicationRunner()
        {
            // Directory.GetCurrentDirectory= C:\\Users\\carlos\\Repositories\\goos-sniper\\tests\\bin\\Debug\\netcoreapp3.1
            var command = $"{appPath}"; // {XMPP_HOSTNAME} {SNIPER_ID} {SNIPER_PASSWORD} {auction.GetItemId()}";
            app = Application.Launch(command);

            using (var automation = new UIA3Automation())
            {
                if (app.GetMainWindow(automation).Title != "Auction Sniper 1.0")
                    throw new Exception("No encontrada la ventana con título correcto.");
            }
        }

        internal void StartBiddingIn(FakeAuctionServer auction)
        {
            ShowsSniperStatus("Joining");
        }

        internal void ShowsSniperHasLostAuction()
        {
            ShowsSniperStatus("Lost");
        }

        internal void ShowsSniperStatus(string statusText)
        {
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                var statusLabel = window.FindFirstDescendant(cf => cf.ByName("lblStatus")).AsLabel();
                if (statusLabel == null)
                    throw new Exception("No encontrada la etiqueta de estado con nombre lblStatus");

                if (!statusLabel.Text.Equals(statusText))
                    throw new Exception("Etiquera lblStatus no contiene el texto: " + statusText);
            }
        }

        internal void Stop()
        {
            app.Kill();
            app.Dispose();
        }
    }
}