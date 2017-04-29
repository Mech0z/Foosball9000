using System;
using System.Collections.Generic;
using Logic;
using MongoDBRepository;
using Moq;
using NUnit.Framework;
using Match = Models.Match;

namespace LogicTests
{ 
    [TestFixture]
    public class LeaderboardServiceTests
    {
        [Test]
        public void RecalculateLeaderboardTest()
        {
            //Arrange
            var season = "season1";
            var matchRepoMock = new Mock<IMatchRepository>();
            matchRepoMock.Setup(x => x.GetMatches(season)).Returns(new List<Match>
            {
            });
            var leaderboardViewRepositoryMock= new Mock<ILeaderboardViewRepository>();
            var seasonLockMock = new Mock<ISeasonLogic>();

            var cut = new LeaderboardService(leaderboardViewRepositoryMock.Object, matchRepoMock.Object, seasonLockMock.Object);

            //Act
            
            var result = cut.RecalculateLeaderboard(season);

            //Assert
        }
    }
}