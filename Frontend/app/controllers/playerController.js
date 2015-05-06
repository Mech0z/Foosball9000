'use strict';

app.controller('playerController', function ($scope, $routeParams, player) {

    $scope.errorMessage = "";
    $scope.loading = true;
    $scope.loadingFailed = false;

    var email = $routeParams.email;

    $scope.playerMatchesPromies = player.getPlayerMatches(email);

    $scope.playerMatchesPromies.then(function (payload) {

        var matches = payload;

        setLocalTimeOnMatch(matches);
        
        $scope.lastgames = matches;
        $scope.loading = false;
    }, function (error) {
        $scope.loading = false;
        $scope.errorMessage = error;
        $scope.loadingFailed = true;
        console.log(error);
    });
});