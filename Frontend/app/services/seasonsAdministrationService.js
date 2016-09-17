(function () {
    'use strict';

    var seasonsAdministration = angular.module('seasonsAdministrationService', []);

    seasonsAdministration.factory('seasonsAdministration', ['$http', '$q', "$cookieStore", function ($http, $q, $cookieStore) {

        return {
            startNewSeason: function (email) {
                var deferred = $q.defer();

                var voidRequest = {
                    Email: $cookieStore.get("email"),
                    Password: $cookieStore.get("password")
                };

                $http.post("http://localhost:44716/api/SeasonsAdministration/StartNewSeason", voidRequest)
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

                $http.post("http://localhost:44716/api/SeasonsAdministration/GetSeasons", voidRequest)
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