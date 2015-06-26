"use strict";

app.controller("startmatchController",
    function ($scope, $q, $location, leaderboard, match, user) {
        $scope.playerlist = {};
        $scope.match1 = {};
        $scope.match2 = {};
        $scope.match3 = {};
        $scope.validationFailed = false;
        $scope.errorMessage = "";
        $scope.loading = true;
        $scope.Debug = "";
        
        $q.all([leaderboard.getLeaderboard(), user.getUsers()])
          .then(function (payload) {

              var users = payload[1];
              var leaderboard = payload[0];

              setupUsers(leaderboard, users);

              for (var i = 0; i < leaderboard.length; i++) {
                  leaderboard.Selected = false;
              }

              $scope.userList = leaderboard;
              $scope.loading = false;

          }, function (error) {
              $scope.loading = false;
              $scope.errorMessage = error;
              $scope.loadingFailed = true;
              console.log(error);
          });

        $scope.bestMatchup = new CalculateBestMatchup();

        $scope.playerClick = function (user) {
            $scope.bestMatchup.addPlayer(user);
            $scope.Debug = $scope.bestMatchup.Message;
        };

        $scope.sleep = function()
        {
            var start = new Date().getTime();
            for (var i = 0; i < 1e7; i++) {
                if ((new Date().getTime() - start) > 10) {
                    break;
                }
            }
            return;
        };

        $scope.doStuff = function () {
            
            var p1 = $scope.bestMatchup.autoMatchup[0];            
            var p2 = $scope.bestMatchup.autoMatchup[1];            
            var p3 = $scope.bestMatchup.autoMatchup[2];            
            var p4 = $scope.bestMatchup.autoMatchup[3];
            
            var matchList = [];
            if (typeof $scope.match1.MatchResult !== 'undefined') {
                if ($scope.match1.MatchResult.Team1Score > 0 || $scope.match1.MatchResult.Team2Score > 0) {
                    matchList.push($scope.match1);
                }
            }

            if (typeof $scope.match2.MatchResult !== 'undefined') {
                if ($scope.match2.MatchResult.Team1Score > 0 || $scope.match2.MatchResult.Team2Score > 0) {
                    matchList.push($scope.match2);
                }
            }

            if (typeof $scope.match3.MatchResult !== 'undefined') {
                if ($scope.match3.MatchResult.Team1Score > 0 || $scope.match3.MatchResult.Team2Score > 0) {
                    matchList.push($scope.match3);
                }
            }

            for (var i = 0; i < matchList.length; i++) {

                matchList[i].PlayerList = [p1, p2, p3, p4];

                var validationResult = match.validateMatch(matchList[i]);
                if (!validationResult.validated)
                {
                    $scope.validationFailed = true;
                    $scope.errorMessage = validationResult.errorMessage;
                    return;
                }
            }
            
            $scope.addMatches(matchList);
        };

        $scope.addMatches = function (m) {
            
            $scope.validationFailed = false;
            $scope.loading = true;
            var addMatchPromise = match.addMatches(m);

            addMatchPromise.then(function () {
                $scope.loading = false;
                console.log(m);
                $location.path("leaderboard");
            }, function () {
                $scope.loading = false;
                $scope.errorMessage = "Request failed ";
                $scope.validationFailed = true;
            });

            return;
        }
    });

var CalculateBestMatchup = function () {
    
    this.IsMatchReady = false;

    this.Message = "";

    this.playerlist = [];
    
    this.autoMatchup = [];

    this.orderedPlayerList = [];

    this.addPlayer = function (p) {

        for (var i = 0; i < this.playerlist.length; i++)
        {
            this.playerlist[i].Team1 = false;
            this.playerlist[i].Team2 = false;
        }

        var index = this.playerlist.indexOf(p);
        if (index > -1) {
            p.Selected = false;
            this.playerlist.splice(index, 1);
        }
        else {
            if (this.playerlist.length >= 4) {

                this.playerlist[0].Selected = false;
                this.playerlist.splice(0, 1);
            }

            this.playerlist[this.playerlist.length] = p;
            p.Selected = true;
        }

        this.IsMatchReady = this.validateGame()

        return;
    };

    this.validateGame = function () {
        if (this.playerlist.length !== 4) {
            this.Message = "Missing players!";
            return false;
        }

        this.Message = "All players selected!!";
        var sortedList = this.playerlist.slice().sort(function (a, b) { return b.EloRating - a.EloRating });
            
        sortedList[0].Team1 = true;
        sortedList[3].Team1 = true;
        
        sortedList[1].Team2 = true;
        sortedList[2].Team2 = true;

        this.autoMatchup = [sortedList[0].UserName, sortedList[3].UserName, sortedList[1].UserName, sortedList[2].UserName];

        this.Message = this.autoMatchup;

        return true;
    }
};
