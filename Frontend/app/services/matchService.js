
(function() {
    "use strict";

    var matchServices = angular.module("matchService", []);

    matchServices.factory("match", [
        "$http", "$q", function($http, $q) {

            return {
                addMatches: function (matches) {
                    var deferred = $q.defer();

                    for (var i = 0; i < matches.length; i++) {
                        matches[i].TimeStampUtc = new Date().toJSON();
                    }

                    $http.post("http://localhost:44716/api/match/SaveMatch", matches)
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
                        

                    if (match.MatchResult.Team1Score >= 10 ||
                        match.MatchResult.Team2Score >= 10) {
                        //valid
                    } else {
                        return { validated: false, errorMessage: "One team must have a score of 10 or greater!" }
                    }

                    return { validated: true, errorMessage: "" }
                }
            };

        }
    ]);
})();