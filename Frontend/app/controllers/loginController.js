'use strict';

app.controller('loginController', function ($scope, $q, $routeParams, $cookieStore, $location, user) {

    $scope.errorMessage = "";
    $scope.loading = false;
    $scope.validationFailed = false;
    $scope.user = {};


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

        }, function (error, status) {
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