﻿var app = angular.module("Foosball", [
        "leaderboardService", "matchService", "userService", "playerService", "ngRoute", "ngCookies", "achievementsService", "seasonsAdministrationService"
    ])
    .config([
        "$routeProvider", function($routeProvider) {
            $routeProvider
                //.when("/", {
                //    templateUrl: "views/home.html",
                //    controller: "homeController"
                //})
                .when("/leaderboard", {
                    templateUrl: "views/leaderboard.html",
                    controller: "leaderboardController"
                })
                .when("/", {
                    templateUrl: "views/leaderboard.html",
                    controller: "leaderboardController"
                })
                .when("/addmatch", {
                    templateUrl: "views/addmatch.html",
                    controller: "addmatchController"
                })
                .when("/startmatch", {
                    templateUrl: "views/startmatch.html",
                    controller: "startmatchController"
                })
                .when("/adduser", {
                    templateUrl: "views/adduser.html",
                    controller: "adduserController"
                })
                .when("/lastgames", {
                    templateUrl: "views/lastgames.html",
                    controller: "lastgamesController"
                })
                .when("/achievements", {
                    templateUrl: "views/achievements.html",
                    controller: "achievementsController"
                })
                .when("/seasonsAdministration", {
                    templateUrl: "views/seasonsAdministration.html",
                    controller: "seasonsAdministrationController"
                })
                .when("/player/:email", {
                    templateUrl: "views/player.html",
                    controller: "playerController"
                })
                .when("/login", {
                    templateUrl: "views/login.html",
                    controller: "loginController"
                })
                .otherwise({
                    redirectTo: "/404.png"
                });
        }
    ])
    .controller("mainController", function($scope) {
        $scope.message = "Main Content";
    });;