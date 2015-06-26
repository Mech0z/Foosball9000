"use strict";

app.controller("addmatchController",
    function($scope, $location, match, user) {

        $scope.playerlist = {};
        $scope.match = {};
        $scope.validationFailed = false;
        $scope.errorMessage = "";
        $scope.loading = true;
        user.getUsers().then(function (payload) {
            $scope.userList = payload;
            $scope.loading = false;           

        });

        $scope.submit = function() {
            $scope.match.PlayerList = [];
            $scope.match.PlayerList.push($scope.playerlist.Player1);
            $scope.match.PlayerList.push($scope.playerlist.Player2);
            $scope.match.PlayerList.push($scope.playerlist.Player3);
            $scope.match.PlayerList.push($scope.playerlist.Player4);

            var validationResult = match.validateMatch($scope.match);
            if (validationResult.validated) {
                $scope.validationFailed = false;
                $scope.loading = true;
                var addMatchPromise = match.addMatches([$scope.match]);

                addMatchPromise.then(function () {
                    $scope.loading = false;
                    console.log($scope.match);
                    $location.path("leaderboard");
                }, function () {
                    $scope.loading = false;
                    $scope.errorMessage = "Request failed ";
                    $scope.validationFailed = true;
                });
                
            } else {
                $scope.validationFailed = true;
                $scope.errorMessage = validationResult.errorMessage;
            }
        };
    });