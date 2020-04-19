using System.Windows.Forms;
using Artalk.Xmpp;
using Artalk.Xmpp.Im;
using Artalk.Xmpp.Client;

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
            Jid auctionId = $"auction-{itemId}@localhost";
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