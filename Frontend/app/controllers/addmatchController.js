"use strict";

app.controller("addmatchController",
    function ($scope, $q, $location, $routeParams, match, user) {

        $scope.updateMatch = null;
        $scope.playerlist = {};
        $scope.match = {};
        $scope.match.Id = $routeParams.matchId;
        $scope.match.SomeId = $routeParams.matchId;
        $scope.validationFailed = false;
        $scope.errorMessage = "";
        $scope.loading = true;
        
        $q.all([user.getUsers(), match.getMatch($routeParams.matchId)]).then(function (payload) {

            $scope.userList = payload[0];
            var loadedMatch = payload[1];
            if (loadedMatch != null)
            {
                $scope.playerlist.Player1 = loadedMatch.PlayerList[0];
                $scope.playerlist.Player2 = loadedMatch.PlayerList[1];
                $scope.playerlist.Player3 = loadedMatch.PlayerList[2];
                $scope.playerlist.Player4 = loadedMatch.PlayerList[3];

                $scope.match.MatchResult = {};

                $scope.match.MatchResult.Team1Score = loadedMatch.MatchResult.Team1Score;
                $scope.match.MatchResult.Team2Score = loadedMatch.MatchResult.Team2Score;
            }            
            
            $scope.loading = false;
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
                $scope.loading = true;                
                var addMatchPromise = match.addMatches([$scope.match]);

                addMatchPromise.then(function () {
                    $scope.loading = false;
                    console.log($scope.match);
                    $location.path("leaderboard");
                }, function () {
                    $scope.loading = false;
                    $scope.errorMessage = "Request failed ";
                    $scope.validationFailed = true;
                });
                
            } else {
                $scope.validationFailed = true;
                $scope.errorMessage = validationResult.errorMessage;
            }
        };
    });