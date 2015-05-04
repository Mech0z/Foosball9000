"use strict";

app.controller("leaderboardController",
    function($scope, $q, leaderboard, user) {
        $scope.loading = true;
        $scope.loadingFailed = false;

        $q.all([leaderboard.getLeaderboard(), user.getUsers()])
            .then(function(payload) {

                $scope.loading = false;
                $scope.leaderboard = payload[0];

                var users = payload[1];

                for (var i = 0; i < $scope.leaderboard.length; i++) {
                    for (var j = 0; j < users.length; j++) {
                        if ($scope.leaderboard[i].UserName === users[j].Email) {
                            $scope.leaderboard[i].displayName = users[j].Username;
                        }
                    }
                }
            }, function(error) {
                $scope.loading = false;
                $scope.errorMessage = error;
                $scope.loadingFailed = true;
                console.log(error);
            });
    });