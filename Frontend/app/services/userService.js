(function () {
    'use strict';

    var userServices = angular.module('userService', []);

    userServices.factory('user', ['$http', '$q', function ($http, $q) {

        return {
            addUser: function (user) {
                var deferred = $q.defer();

                $http.post("http://localhost:44716/api/player/CreateUser", user)
                   .success(function (data, status, headers, config) {
                       console.log("success adding user");
                       deferred.resolve(data);
                    }).error(function (data, status, headers, config) {
                        if (status == 409) {
                            deferred.reject("User with that email already exists");
                        } else {
                            deferred.reject("Server error");
                        }
                    });
                return deferred.promise;
            },
            getUsers: function() {
                var deferred = $q.defer();

                $http.get("http://localhost:44716/api/player/GetUsers")
                   .success(function (data, status, headers, config) {
                       console.log(data);
                       deferred.resolve(data);
                   }).error(function (data, status, headers, config) {
                           deferred.reject(data);
                   });

                return deferred.promise;
            }
        };

    }]);
})();