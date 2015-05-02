"use strict";

app.controller("lastgamesController",
    function ($scope, match) {
                
        $scope.lastgamesPromise = match.getLatest(5);

        $scope.lastgamesPromise.then(function (payload) {
            $scope.lastgames = payload;
        });
    });