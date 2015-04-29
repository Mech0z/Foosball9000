"use strict";

app.controller("addmatchController",
    function($scope, match, user) {

        $scope.playerlist = {};
        $scope.match = {};

        user.getUsers().then(function(payload) {
            $scope.userList = payload;

        });

        $scope.submit = function () {
            match.PlayerList = {};
            match.PlayerList.push($scope.playerlist.Player1);
            match.PlayerList.push($scope.playerlist.Player2);
            match.PlayerList.push($scope.playerlist.Player3);
            match.PlayerList.push($scope.playerlist.Player4);
            match.addMatch($scope.match);
            console.log($scope.match);
        };
    });