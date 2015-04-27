(function () {
    'use strict';

    var leaderboardServices = angular.module('leaderboardService', ['ngResource']);

    leaderboardServices.factory('Leaderboard', ['$resource',
      function ($resource) {
          return $resource('/api/leaderboard/', {}, {
              query: { method: 'GET', params: {}, isArray: true }
          });
      }]);
})();