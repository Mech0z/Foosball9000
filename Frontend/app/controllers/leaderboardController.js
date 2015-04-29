'use strict';

app.controller('leaderboardController',
    function ($scope, leaderboard) {

    $scope.leaderboardPromise = leaderboard.getLeaderboard();

    $scope.leaderboardPromise.then(function (payload) {
        $scope.leaderboard = payload;
    });
});