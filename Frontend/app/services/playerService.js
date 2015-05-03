(function () {
    'use strict';

    var playerServices = angular.module('playerService', []);

    playerServices.factory('player', ['$http', '$q', function ($http, $q) {

        return {
            getPlayerMatches: function (email) {
                var deferred = $q.defer();

                $http.get("http://localhost:44716/api/player/GetPlayerMatches?email=" + email)
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