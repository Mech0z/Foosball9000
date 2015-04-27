'use strict';

app.controller('leaderboardController',
    function ($scope, Leaderboard) {
    $scope.message = "Now viewing leaderboard!";

    $scope.leaderboard = Leaderboard.getLeaderboard();

});