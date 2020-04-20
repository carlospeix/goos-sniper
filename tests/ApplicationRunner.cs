using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using NUnit.Framework;
using System.Diagnostics;

namespace GoosSniper.Tests
{
    internal class ApplicationRunner
    {
        private const string XMPP_HOSTNAME = "localhost";
        private const string SNIPER_ID = "sniper";
        private const string SNIPER_PASSWORD = "sniper";
        private const string appPath = @"C:\Users\carlos\Repositories\goos-sniper\sniper\bin\Debug\sniper.exe";

        private Application app;

        internal void StartBiddingIn(FakeAuctionServer auction)
        {
            var startInfo = new ProcessStartInfo(appPath, $"{XMPP_HOSTNAME} {SNIPER_ID} {SNIPER_PASSWORD} {auction.GetItemId()}");
            app = Application.Launch(startInfo);

            using (var automation = new UIA3Automation())
            {
                Assert.AreEqual("Auction Sniper 1.0", app.GetMainWindow(automation).Title);
            }

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

                var statusLabel = window.FindFirstDescendant(cf => cf.ByAutomationId("lblStatus")).AsLabel();
                Assert.NotNull(statusLabel, "No encontrada la etiqueta de estado con nombre lblStatus");

                Assert.AreEqual(statusText, statusLabel.Text);
            }
        }

        internal void Stop()
        {
            app.Close();
            app.Dispose();
        }
    }
}