(function () {
    'use strict';

    var userServices = angular.module('userService', []);

    userServices.factory('user', ['$http', '$q', '$cookieStore', function ($http, $q, $cookieStore) {

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
            },
            login: function (user) {
                var deferred = $q.defer();

                $http.post("http://localhost:44716/api/player/Login", user)
                   .success(function (data, status, headers, config) {
                       console.log(data);
                       deferred.resolve(data);
                   }).error(function (data, status, headers, config) {
                       if (status === 401) {
                           deferred.reject("Wrong email or password!");
                       }

                       deferred.reject("Unknown login failure");
                   });

                return deferred.promise;
            },
            changePassword: function (email, oldPassword, newPassword) {
                var deferred = $q.defer();

                var changePasswordRequest = {};

                changePasswordRequest.Email = email;
                changePasswordRequest.OldPassword = oldPassword;
                changePasswordRequest.NewPassword = newPassword;

                $http.post("http://localhost:44716/api/player/ChangePassword", changePasswordRequest)
                   .success(function (data, status, headers, config) {
                       console.log(data);
                       deferred.resolve(data);  
                   }).error(function (data, status, headers, config) {
                       if (status === 401) {
                           deferred.reject("Wrong email or password!");
                       }

                       deferred.reject("Unknown login failure");
                   });

                return deferred.promise;
            }
        };

    }]);
})();