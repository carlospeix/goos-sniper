using System;
using NUnit.Framework;
using System.Collections.Concurrent;
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
        private readonly BlockingCollection<string> messages;

        internal FakeAuctionServer(string itemId)
        {
            this.itemId = itemId;

            messages = new BlockingCollection<string>();

            client = new ArtalkXmppClient(XMPP_HOSTNAME, String.Format(ITEM_ID_AS_LOGIN, itemId), AUCTION_PASSWORD);
            client.Message += OnNewMessage;
        }

        private void OnNewMessage(object sender, MessageEventArgs e)
        {
            messages.Add(e.Message.Body);
        }

        internal void StartSellingItem()
        {
            client.Connect();
        }

        internal void HasReceivedJoinRequestFromSniper()
        {
            string message;
            messages.TryTake(out message, 2000);
            Assert.NotNull(message, "Mensaje desde el cliente no recibido.");
        }

        internal void AnnounceClosed()
        {
            // TODO: Revisar el mecanismo de comunicación (algo parecido a un canal privado)
            client.SendMessage($"sniper@localhost", "Closed", null, null, MessageType.Chat);
        }

        internal string GetItemId()
        {
            return itemId;
        }

        internal void Stop()
        {
            client.Message -= OnNewMessage;
            client.Dispose();
        }
    }
}