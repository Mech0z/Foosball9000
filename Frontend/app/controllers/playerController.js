'use strict';

app.controller('playerController', function ($scope, $q, $routeParams, player, user) {

    $scope.errorMessage = "";
    $scope.loading = true;
    $scope.loadingFailed = false;
        
    $q.all([player.getPlayerMatches($routeParams.email), user.getUsers(), player.getPlayerPartnerWinPercent($routeParams.email)]).then(function (payload) {
        var matches = payload[0];
        var users = payload[1];
        $scope.partnerMatches = payload[2];

        setLocalTimeOnMatch(matches);
        setupUsersMatches(matches, users);

        $scope.lastgames = matches;

        $scope.loading = false;
        $scope.leaderboard = matches;

    }, function (error) {
        $scope.loading = false;
        $scope.errorMessage = error;
        $scope.loadingFailed = true;
        console.log(error);
    });
});