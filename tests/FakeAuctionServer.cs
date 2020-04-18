using System;
using Artalk.Xmpp.Im;
using Artalk.Xmpp.Client;

namespace GoosSniper.Tests
{
    internal class FakeAuctionServer
    {
        private const string XMPP_HOSTNAME = "localhost";
        private const string ITEM_ID_AS_LOGIN = "auction-{0}";
        private const string AUCTION_PASSWORD = "auction";

        private readonly string itemId;
        private readonly ArtalkXmppClient client;

        internal FakeAuctionServer(string itemId)
        {
            this.itemId = itemId;

            client = new ArtalkXmppClient(XMPP_HOSTNAME, String.Format(ITEM_ID_AS_LOGIN, itemId), AUCTION_PASSWORD);
            client.Message += ClientMessage;
        }

        private void ClientMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Message from <" + e.Jid + ">: " + e.Message.Body);
        }

        internal void StartSellingItem()
        {
            client.Connect();
            // client.SendMessage("", "");
        }

        internal void HasReceivedJoinRequestFromSniper()
        {
        }

        internal void AnnounceClosed()
        {
        }

        internal string GetItemId()
        {
            return itemId;
        }

        internal void Stop()
        {
            client.Message -= ClientMessage;
            client.Dispose();
        }
    }
}