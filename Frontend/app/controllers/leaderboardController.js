"use strict";

app.controller("leaderboardController",
    function($scope, leaderboard) {

        $scope.loading = true;
        $scope.leaderboardPromise = leaderboard.getLeaderboard();

        $scope.leaderboardPromise.then(function(payload) {
            $scope.loading = false;
            $scope.leaderboard = payload;
        });
    });