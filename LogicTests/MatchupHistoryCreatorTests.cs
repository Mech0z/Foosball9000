using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Logic;
using Models;
using MongoDBRepository;
using Moq;
using NUnit.Framework;
using Match = Models.Match;

namespace LogicTests
{
    [TestFixture]
    public partial class MatchupHistoryCreatorTests
    {
        [Test]
        public void GetPartnerWinPercent()
        {
            //Arrange
            string p1Email = "p1";
            string p2Email = "p2";
            string p3Email = "p3";
            string p4Email = "p4";

            string seasonName = "1";

            var matchupResultRepository = new Mock<IMatchupResultRepository>();
            var matchRepoMock = new Mock<IMatchRepository>();

            var matches = new List<Match>
            {
                new MatchBuilder().SetPlayers(p1Email, p2Email, p3Email, p4Email).SetResult(8, 6).Build(),
                new MatchBuilder().SetPlayers(p1Email, p2Email, p3Email, p4Email).SetResult(6, 8).Build(),
                new MatchBuilder().SetPlayers(p2Email, p3Email, p4Email,p1Email).SetResult(6, 8).Build(),
                new MatchBuilder().SetPlayers(p2Email, p3Email, p4Email,p1Email).SetResult(8, 6).Build(),
            };

            matchRepoMock.Setup(x => x.GetMatches(seasonName)).Returns(matches);

            var userRepomock = new Mock<IUserRepository>();
            userRepomock.Setup(x => x.GetUsers()).Returns(new List<User>
            {
                new User{Email = p1Email, Username = "p1"},
                new User{Email = p2Email, Username = "p2"},
                new User{Email = p3Email, Username = "p3"},
                new User{Email = p4Email, Username = "p4"},
            });

            var leaderboardViewRepositoryMock = new Mock<ILeaderboardViewRepository>();
            //leaderboardViewRepositoryMock.Setup(x => x.GetLeaderboardView(seasonName)).Returns(new LeaderboardView());

            var cut = new MatchupHistoryCreator(matchupResultRepository.Object, matchRepoMock.Object,
                userRepomock.Object, leaderboardViewRepositoryMock.Object);

            //Act
            List<PartnerPercentResult> result = cut.GetPartnerWinPercent(p1Email, seasonName);

            //Assert
            var p2 = result.Single(x => x.Email == p2Email);
            p2.MatchesAgainst.Should().Be(2);
            p2.WinsAgainst.Should().Be(1);
            p2.MatchesTogether.Should().Be(2);
            p2.WinsTogether.Should().Be(1);

            var p3 = result.Single(x => x.Email == p3Email);
            p3.MatchesAgainst.Should().Be(4);
            p3.WinsAgainst.Should().Be(2);
            p3.MatchesTogether.Should().Be(0);
            p3.WinsTogether.Should().Be(0);

            var p4 = result.Single(x => x.Email == p4Email);
            p4.MatchesAgainst.Should().Be(2);
            p4.WinsAgainst.Should().Be(1);
            p4.MatchesTogether.Should().Be(2);
            p4.WinsTogether.Should().Be(1);
        }
    }
}