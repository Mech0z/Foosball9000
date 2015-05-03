"use strict";

app.controller("leaderboardController",
    function($scope, leaderboard) {

        $scope.loading = true;
        $scope.loadingFailed = false;
        $scope.leaderboardPromise = leaderboard.getLeaderboard();

        $scope.leaderboardPromise.then(function(payload) {
            $scope.loading = false;
            $scope.leaderboard = payload;
        }, function(error) {
            $scope.loading = false;
            $scope.errorMessage = error;
            $scope.loadingFailed = true;
            console.log(error);
        });
    });