'use strict';

app.controller('loginController', function ($scope, $q, $routeParams, $cookieStore, $location, user) {

    $scope.errorMessage = "";
    $scope.loading = false;
    $scope.validationFailed = false;
    $scope.user = {};
    $scope.newPassword = "";
    $scope.oldPassword = "";


    if ($cookieStore.get("email") == null) {
        $scope.loggedIn = false;
    } else {
        $scope.loggedIn = true;
    }

    $scope.cookieEmail = $cookieStore.get("email");
    $scope.cookiePasswordHash = $cookieStore.get("password");

    $scope.login = function () {
        
        $scope.loading = true;

        var loginPromise = user.login($scope.user);

        loginPromise.then(function (data) {
            $scope.loading = false;
            $scope.validationFailed = false;
            $scope.errorMessage = "Successful login";

            $cookieStore.put("email", $scope.user.Email);
            $cookieStore.put("password", data);

            $location.path("leaderboard");

        }, function (error) {
            $scope.validationFailed = true;
            $scope.loading = false;
            $scope.errorMessage = error;
        });
    }

    $scope.changePassword = function () {
        $scope.validationFailed = false;
        $scope.loading = true;

        $scope.cookieEmail = $cookieStore.get("email");

        var changePasswordPromise = user.changePassword($scope.cookieEmail, $scope.oldPassword, $scope.newPassword);

        changePasswordPromise.then(function(data) {
            $scope.loading = false;
            $cookieStore.put("password", data);
            $scope.cookiePasswordHash = $cookieStore.get("password");

            $scope.newPassword = "";
            $scope.oldPassword = "";

            $scope.validationFailed = true;
            $scope.errorMessage = "Password changed!";
        }, function (error) {
            $scope.validationFailed = true;
            $scope.loading = false;
            $scope.errorMessage = error;
        });
    }

    $scope.logout = function () {
        
        $cookieStore.put("email", null);
        $cookieStore.put("password", null);
        $scope.loggedIn = false;
    }
});