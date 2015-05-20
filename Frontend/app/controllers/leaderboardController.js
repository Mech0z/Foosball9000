"use strict";

app.controller("leaderboardController",
    function($scope, $q, leaderboard, user) {
        $scope.loading = true;
        $scope.loadingFailed = false;

        $q.all([leaderboard.getLeaderboard(), user.getUsers()])
            .then(function(payload) {

                var users = payload[1];

                setupUsers(payload[0], users);

                $scope.loading = false;
                $scope.leaderboard = payload[0];

            }, function(error) {
                $scope.loading = false;
                $scope.errorMessage = error;
                $scope.loadingFailed = true;
                console.log(error);
            });
    });

"use strict";