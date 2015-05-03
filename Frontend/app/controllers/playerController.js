'use strict';

app.controller('playerController', function ($scope, $routeParams, player) {

    var email = $routeParams.email;
    $scope.playerMatchesPromies = player.getPlayerMatches(email);

    $scope.playerMatchesPromies.then(function(payload) {
        $scope.lastgames = payload;
    });

});