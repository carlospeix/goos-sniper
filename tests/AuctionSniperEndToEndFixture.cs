using NUnit.Framework;

namespace GoosSniper.Tests
{
    public class AuctionSniperEndToEndFixture
    {
        private FakeAuctionServer auction;
        private ApplicationRunner application;

        [Test]
        public void SniperJoinsAutionUntilAuctionCloses()
        {
            auction.StartSellingItem();
            application.StartBiddingIn();
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
    }
}