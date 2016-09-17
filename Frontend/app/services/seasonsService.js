(function () {
    'use strict';

    var playerServices = angular.module('playerService', []);

    playerServices.factory('player', ['$http', '$q', "$cookieStore", function ($http, $q, $cookieStore) {

        return {
            startNewSeason: function (email) {
                var deferred = $q.defer();

                var voidRequest = {
                    Email: $cookieStore.get("email"),
                    Password: $cookieStore.get("password")
                };

                $http.get("http://localhost:44716/api/Season/StartNewSeason", voidRequest)
                .success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).error(function (data, status, headers, config) {
                    deferred.reject("Error loading data");
                });
                return deferred.promise;
            },
            getSeasons: function (email) {
                var deferred = $q.defer();
                
                var voidRequest = {
                    Email: $cookieStore.get("email"),
                    Password: $cookieStore.get("password")
                };

                $http.get("http://localhost:44716/api/Season/GetSeasons", voidRequest)
                .success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).error(function (data, status, headers, config) {
                    deferred.reject("Error loading data");
                });
                return deferred.promise;
            }
        };

    }]);
})();