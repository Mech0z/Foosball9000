"use strict";

app.controller("lastgamesController",
    function ($scope, $q, match, user) {

        $scope.errorMessage = "";
        $scope.loading = true;


        $q.all([match.getLatest(20), user.getUsers()]).then(function (payload) {
            var matches = payload[0];
            var users = payload[1];
            
            setLocalTimeOnMatch(matches);
            setupUsersMatches(matches, users);

            $scope.lastgames = matches

            $scope.loading = false;
            $scope.leaderboard = matches;

        }, function (error) {
            $scope.loading = false;
            $scope.errorMessage = error;
            $scope.loadingFailed = true;
            console.log(error);
        });
    });