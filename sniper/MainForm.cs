using System;
using System.Windows.Forms;
using Artalk.Xmpp.Client;
using Artalk.Xmpp.Im;

namespace GoosSniper.Sniper
{
    public partial class MainForm : Form
    {
        private ArtalkXmppClient client;

        public MainForm(string xmppHostname, string sniperId, string sniperPassword, string itemId)
        {
            InitializeComponent();

            SetStatus("Joining");
            ConnectToServer(xmppHostname, sniperId, sniperPassword);
            JoinAuction(itemId);
        }

        private void ConnectToServer(string xmppHostname, string sniperId, string sniperPassword)
        {
            client = new ArtalkXmppClient(xmppHostname, sniperId, sniperPassword);
            client.Message += OnNewMessage;
            client.Connect();
        }
        private void JoinAuction(string itemId)
        {
            string auctionId = $"auction-{itemId}@localhost";
            client.SendMessage(auctionId, "Empty", null, null, MessageType.Chat);
        }

        private void OnNewMessage(object sender, MessageEventArgs e)
        {
            SetStatus("Lost");
        }

        private void SetStatus(string statusText)
        {
            lblStatus.Text = statusText;
        }
    }
}