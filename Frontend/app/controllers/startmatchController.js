"use strict";

app.controller("startmatchController",
    function ($scope, $q, $location, leaderboard, match, user) {

        $scope.match = {};
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

            if($scope.bestMatchup.IsMatchReady)
            {
                //save data for addmatch
            }
        };
    });

var CalculateBestMatchup = function () {
    
    this.IsMatchReady = false;

    this.Message = "";

    this.playerlist = [];
    
    this.autoMatchup = [];

    this.addPlayer = function (p) {

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
        this.Debug = "All players selected!!";

        var p1 = this.playerlist[0];
        var p2 = this.playerlist[1];
        var p3 = this.playerlist[2];
        var p4 = this.playerlist[3];

        var game = new Game();

        var m1 = new Matchup();
        m1.setTeamTwoPoints(p1.EloRating, p1.UserName, p3.EloRating, p3.UserName);
        m1.setTeamOnePoints(p4.EloRating, p4.UserName, p2.EloRating, p2.UserName);

        game.addMatchup(m1);

        var m2 = new Matchup();
        m1.setTeamTwoPoints(p1.EloRating, p1.UserName, p2.EloRating, p2.UserName);
        m1.setTeamOnePoints(p4.EloRating, p4.UserName, p3.EloRating, p3.UserName);

        game.addMatchup(m2);

        var m3 = new Matchup();
        m1.setTeamTwoPoints(p1.EloRating, p1.UserName, p4.EloRating, p4.UserName);
        m1.setTeamOnePoints(p2.EloRating, p2.UserName, p3.EloRating, p3.UserName);

        game.addMatchup(m3);

        var bestMatchup = game.getBestCombination();
        
        this.Message = bestMatchup.t1p1name + " and " + bestMatchup.t1p2name
        + " vs " + bestMatchup.t2p1name + " and " + bestMatchup.t2p2name;

        this.Debug = "points team 1: (" + bestMatchup.t1points + ") " + bestMatchup.t1p1name + " and " + bestMatchup.t1p2name
        + " vs team 2: (" + bestMatchup.t2points + ") " + bestMatchup.t2p1name + " and " + bestMatchup.t2p2name;
        
        this.autoMatchup = [bestMatchup.t1p1name, bestMatchup.t1p2name, bestMatchup.t2p1name, bestMatchup.t2p2name];

        return true;
    }
};

var Matchup = function () {
    var t1points = 0;
    var t1p1name = "";
    var t1p2name = "";

    var t2points = 0;
    var t2p1name = "";
    var t2p2name = "";


    this.setTeamOnePoints = function (p1, p1name, p2, p2name) {
        this.t1points = p1 + p2;
        this.t1p1name = p1name;
        this.t1p2name = p2name;
        return;
    };
    this.setTeamTwoPoints = function (p1, p1name, p2, p2name) {
        this.t2points = p1 + p2;
        this.t2p1name = p1name;
        this.t2p2name = p2name;
        return;
    };

    this.getDifference = function () {
        if (this.t2points > this.t1points) {
            return this.t2points - this.t1points;
        }

        return this.t1points - this.t2points
    };
};

var Game = function () {
    this.matchups = [];

    this.addMatchup = function (m) {
        this.matchups[this.matchups.length] = m;
        return;
    };
    this.getNumberOfCombinations = function () {
        return this.matchups.length;
    };
    this.getBestCombination = function () {
        var m = this.matchups[0];
        for (var i = 1; i < this.matchups.length; i++) {
            if (m.getDifference() > this.matchups[i].getDifference()) {
                m = this.matchups[i];
            }
        }
        return m;
    }
};

