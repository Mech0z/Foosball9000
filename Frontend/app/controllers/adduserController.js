"use strict";

app.controller("adduserController",
    function($scope, user) {

        $scope.User = {};

        $scope.submit = function() {
            console.log($scope.User);
            user.addUser($scope.User);
        };
    });