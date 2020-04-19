using NUnit.Framework;

namespace GoosSniper.Tests
{
    // Configuración del servidor de XMPP
    //
    // User/pass: sniper/sniper (prosodyctl register sniper localhost sniper)
    // User/pass: auction-item-54321/auction (prosodyctl register auction-item-54321 localhost auction)
    // User/pass: auction-item-65432/auction (prosodyctl register auction-item-65432 localhost auction)
    //

    [TestFixture]
    public class AuctionSniperEndToEndFixture
    {
        private FakeAuctionServer auction;
        private ApplicationRunner application;

        [Test]
        public void SniperJoinsAutionUntilAuctionCloses()
        {
            auction.StartSellingItem();
            application.StartBiddingIn(auction);
            auction.HasReceivedJoinRequestFromSniper();
            auction.AnnounceClosed();
            application.ShowsSniperHasLostAuction();
        }

        [SetUp]
        public void Setup()
        {
            auction = new FakeAuctionServer("item-54321");
            application = new ApplicationRunner();
        }

        [TearDown]
        public void TearDown()
        {
            application.Stop();
            auction.Stop();
        }
    }
}