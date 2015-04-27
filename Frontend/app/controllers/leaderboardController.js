'use strict';

app.controller('leaderboardController',
    function ($scope, leaderboard) {
    $scope.message = "Now viewing leaderboard!";

    $scope.movies = leaderboard.query();

});