
(function () {
    'use strict';

    var matchServices = angular.module('matchService', []);

    matchServices.factory('match', ['$http', '$q', function ($http, $q) {

        return {
            addMatch: function (match) {
                var deferred = $q.defer();

                $http.post("http://localhost:44716/api/match/SaveMatch", match)
                   .success(function (data, status, headers, config) {
                       console.log("success sending add match request");
                       deferred.resolve(data);
                   }).error(function (data, status, headers, config) {
                       console.log("failed sending add match request");
                       deferred.reject(data);
                   });
                return deferred.promise;
            },
            getLatest(number) {
                var deferred = $q.defer();

                $http.get("http://localhost:44716/api/match/lastgames?numberofmatches="+number)
                .success(function (data, status, headers, config){
                    deferred.resolve(data);
                }).error(function (data, status, headers, config) {
                    deferred.reject(data);
                });
                return deferred.promise;
            }
        };

    }]);
})();