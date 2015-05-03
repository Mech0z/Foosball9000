"use strict";

app.controller("lastgamesController",
    function($scope, match) {

        $scope.errorMessage = "";
        $scope.loading = true;

        $scope.lastgamesPromise = match.getLatest(5);

        $scope.lastgamesPromise.then(function(payload) {
            $scope.lastgames = payload;
            $scope.loading = false;
        }, function(error) {
            $scope.loading = false;
            $scope.errorMessage = error;
            $scope.loadingFailed = true;
            console.log(error);
        });
    });