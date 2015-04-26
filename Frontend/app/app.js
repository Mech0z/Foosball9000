var app = angular.module('myApp', ['ngRoute'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
      .when('/', {
          templateUrl: 'views/home.html',
          controller: 'homeController'
      })
      .when('/leaderboard', {
          templateUrl: 'views/leaderboard.html',
          controller: 'leaderboardController'
      })
      .otherwise({
          redirectTo: '/'
      });
}])
.controller('mainController', function ($scope) {
    $scope.message = "Main Content";
});;