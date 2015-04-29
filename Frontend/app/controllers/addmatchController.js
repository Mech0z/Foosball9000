"use strict";

app.controller("addmatchController",
    function($scope, match, user) {

        user.getUsers().then(function(payload) {
            $scope.userList= payload;
        });
    });