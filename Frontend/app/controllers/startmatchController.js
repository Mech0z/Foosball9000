"use strict";

app.controller("startmatchController",
    function ($scope, $location, match, user) {

        $scope.playerlist = {};
        $scope.match = {};
        $scope.validationFailed = false;
        $scope.errorMessage = "";
        $scope.loading = true;
        $scope.Debug = "";

        user.getUsers().then(function (payload) {
            $scope.userList = payload;
            $scope.loading = false;
        });

        var game = new Game();

        var m1 = new Matchup();
        m1.setTeamTwoPoints(1, "p1name", 3, "p3name");
        m1.setTeamOnePoints(4, "p4name", 2, "p2name");

        game.addMatchup(m1);

        var m2 = new Matchup();
        m2.setTeamTwoPoints(1, "p1name", 2, "p2name");
        m2.setTeamOnePoints(4, "p4name", 3, "p3name");

        game.addMatchup(m2);
        
        var m3 = new Matchup();
        m3.setTeamTwoPoints(1, "p1name", 4, "p4name");
        m3.setTeamOnePoints(2, "p2name", 3, "p3name");

        game.addMatchup(m3);

        var bestMatchup = game.getBestCombination();

        $scope.Debug = "points team 1: (" + bestMatchup.t1points + ") " + bestMatchup.t1p1name + " and " + bestMatchup.t1p2name
        + " vs team 2: (" + bestMatchup.t2points + ") " + bestMatchup.t2p1name + " and " + bestMatchup.t2p2name
    });


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