"use strict";

app.controller("addmatchController",
    function($scope, match, user) {

        $scope.match = {};

        user.getUsers().then(function(payload) {
            $scope.userList = payload;

        });

        $scope.submit = function () {
            match.addMatch($scope.match);
            console.log($scope.match);
        };
    });