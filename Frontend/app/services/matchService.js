(function () {
    'use strict';

    var matchServices = angular.module('matchService', []);

    matchServices.factory('match', ['$http', '$q', function ($http, $q) {

        return {
            addMatch: function (match) {
                //var deferred = $q.defer();

                //$http.get("http://localhost:44716/api/leaderboard")
                //   .success(function (data, status, headers, config) {
                //       console.log(data);
                //       deferred.resolve(data);
                //   }).error(function (data, status, headers, config) {
                //       deferred.reject(data);
                //   });

                //return deferred.promise;
            }
        };

    }]);
})();