
(function() {
    "use strict";

    var matchServices = angular.module("matchService", []);

    matchServices.factory("match", [
        "$http", "$q", "$cookieStore", function ($http, $q, $cookieStore) {

            return {
                addMatches: function (matches) {
                    var deferred = $q.defer();

                    for (var i = 0; i < matches.length; i++) {
                        var date = new Date();

                        date = new Date(date.getTime() -60000 + (5 * 60000 * i));

                        matches[i].TimeStampUtc = date.toJSON();
                    }

                    var user = {
                        Email: $cookieStore.get("email"),
                        Password: $cookieStore.get("password")
                    };

                    var request = {
                        User: user,
                        Matches: matches
                    };

                    $http.post("http://localhost:44716/api/match/SaveMatch", request)
                        .success(function (data, status, headers, config) {
                            console.log("success sending add match request");
                            deferred.resolve(data);
                        }).error(function (data, status, headers, config) {
                            console.log("failed sending add match request");
                            deferred.reject(data);
                        });
                    return deferred.promise;
                },
                getLatest: function(number) {
                    var deferred = $q.defer();

                    $http.get("http://localhost:44716/api/match/lastgames?numberofmatches=" + number)
                        .success(function(data, status, headers, config) {
                            deferred.resolve(data);
                        }).error(function(data, status, headers, config) {
                            deferred.reject(data);
                        });
                    return deferred.promise;
                },
                validateMatch: function(match) {
                    if (match.MatchResult.Team1Score >= match.MatchResult.Team2Score + 2||
                        match.MatchResult.Team2Score >= match.MatchResult.Team1Score + 2) {
                        //valid
                    } else {
                        return { validated: false, errorMessage :"One team must win with 2 or more points" }
                    }
                    function onlyUnique(value, index, self) {
                        return self.indexOf(value) === index;
                    }

                    if (match.PlayerList.filter(onlyUnique).length < 4) {
                        return { validated: false, errorMessage: "Please select 4 unique players!" }
                    }
                        

                    if (match.MatchResult.Team1Score >= 8 ||
                        match.MatchResult.Team2Score >= 8) {
                        //valid
                    } else {
                        return { validated: false, errorMessage: "One team must have a score of 8 or greater!" }
                    }

                    return { validated: true, errorMessage: "" }
                }
            };

        }
    ]);
})();
