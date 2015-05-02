"use strict";

app.controller("addmatchController",
    function($scope, $location, match, user) {

        $scope.playerlist = {};
        $scope.matchResult = {};

        user.getUsers().then(function(payload) {
            $scope.userList = payload;

        });

        $scope.submit = function () {
            $scope.matchResult.PlayerList = [];
            $scope.matchResult.PlayerList.push($scope.playerlist.Player1);
            $scope.matchResult.PlayerList.push($scope.playerlist.Player2);
            $scope.matchResult.PlayerList.push($scope.playerlist.Player3);
            $scope.matchResult.PlayerList.push($scope.playerlist.Player4);
            match.addMatch($scope.matchResult);
            console.log($scope.matchResult);
            $location.path("leaderboard");
        };
    });