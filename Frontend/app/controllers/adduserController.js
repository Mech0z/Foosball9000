"use strict";

app.controller("adduserController",
    function($scope, user) {

        $scope.User = {};
        $scope.errorMessage = false;
        $scope.loading = false;

        $scope.submit = function() {
            console.log($scope.User);
            $scope.loading = true;
            $scope.errorMessage = false;
            var addUserPromise = user.addUser($scope.User);

            addUserPromise.then(function() {
                $scope.loading = false;
                $scope.errorMessage = "Success adding " + $scope.User.Username + " as a user!";
                $scope.User = {};
            }, function(error) {
                $scope.loading = false;
                $scope.errorMessage = error;
            });

        };
    });