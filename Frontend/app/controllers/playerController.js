'use strict';

app.controller('playerController', function ($scope, $routeParams, player) {

    $scope.errorMessage = "";
    $scope.loading = true;
    var email = $routeParams.email;
    $scope.playerMatchesPromies = player.getPlayerMatches(email);

    $scope.playerMatchesPromies.then(function(payload) {
        $scope.lastgames = payload;
        $scope.loading = false;
    }, function (error) {
        $scope.loading = false;
        $scope.errorMessage = error;
        $scope.loadingFailed = true;
        console.log(error);
    });
});