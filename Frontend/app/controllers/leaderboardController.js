'use strict';

app.controller('leaderboardController',
    function ($scope, leaderboard) {
    $scope.message = "Now viewing leaderboard!";


    $scope.leaderboardPromise = leaderboard.getLeaderboard();

    $scope.leaderboardPromise.then(function (payload) {
        $scope.leaderboard = payload;
    });
});