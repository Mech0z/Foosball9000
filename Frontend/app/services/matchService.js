
(function () {
    'use strict';

    var matchServices = angular.module('matchService', []);

    matchServices.factory('match', ['$http', '$q', function ($http, $q) {

        return {
            addMatch: function (match) {
                $http.post("http://localhost:44716/api/match/SaveMatch", match)
                   .success(function (data, status, headers, config) {
                       console.log("success");
                   }).error(function (data, status, headers, config) {
                       console.log("fail");
                   });
            }
        };

    }]);
})();