
app.controller("achievementsController",
    function ($scope, $q, achievements, user) {
        $scope.loading = true;
        $scope.loadingFailed = false;


        $q.all([achievements.getAchievements(), user.getUsers()])
            .then(function (payload) {

                var users = payload[1];

              //  setupUsers(payload[0], users);
                
                $scope.achievementsView = payload[0];

                $scope.loading = false;

            }, function (error) {
                $scope.loading = false;
                $scope.errorMessage = error;
                $scope.loadingFailed = true;
                console.log(error);
            });
    });