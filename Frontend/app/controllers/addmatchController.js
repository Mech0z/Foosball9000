"use strict";

app.controller("addmatchController",
    function($scope, $location, match, user) {

        $scope.playerlist = {};
        $scope.match = {};
        $scope.validationFailed = false;
        $scope.errorMessage = "";
        user.getUsers().then(function(payload) {
            $scope.userList = payload;
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
                match.addMatch($scope.match);
                console.log($scope.match);
                $location.path("leaderboard");
            } else {
                $scope.validationFailed = true;
                $scope.errorMessage = validationResult.errorMessage;
            }
        };
    });