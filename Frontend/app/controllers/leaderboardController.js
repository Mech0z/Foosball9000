"use strict";

app.controller("leaderboardController",
    function($scope, $q, leaderboard, user) {
        $scope.loading = true;
        $scope.loadingFailed = false;
        $scope.selectedSeason = null;

        $q.all([leaderboard.getLeaderboard(), user.getUsers()])
            .then(function(payload) {

                var users = payload[1];

                setupUsers(payload[0][0], users);

                $scope.leaderboard = payload[0][0];
                $scope.payloadBackup = payload;

                $scope.seasons = [];
                for (var i = 0; i < payload[0].length; i++) {
                    $scope.seasons.push(payload[0][i].SeasonName);
                };

                $scope.selectedSeason = payload[0][0].SeasonName;
                $scope.loading = false;

            }, function(error) {
                $scope.loading = false;
                $scope.errorMessage = error;
                $scope.loadingFailed = true;
                console.log(error);
            });

        $scope.changedSeason = function() {
            
            for (var i = 0; i < $scope.payloadBackup[0].length; i++) {
                if ($scope.payloadBackup[0][i].SeasonName === $scope.selectedSeason) {
                    setupUsers($scope.payloadBackup[0][i], $scope.payloadBackup[1]);
                    $scope.leaderboard = $scope.payloadBackup[0][i];
                }
            };
        };
    });

"use strict";