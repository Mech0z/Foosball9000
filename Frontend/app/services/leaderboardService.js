(function () {
    'use strict';

    var leaderboardServices = angular.module('leaderboardService', []);

    leaderboardServices.factory('Leaderboard', ['$http', '$q', function ($http, $q) {

        return {
            getLeaderboard: function () {
                var deferred = $q.defer();

                $http.get("http://localhost:44716/api/leaderboard")
                   .success(function (data, status, headers, config) {
                       deferred.resolve(data);
                   }).error(function (data, status, headers, config) {
                       deferred.reject(data);
                   });

                return deferred.promise;
            }
        };

    }]);
})();