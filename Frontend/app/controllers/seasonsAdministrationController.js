
app.controller("seasonsAdministrationController",
    function ($scope, $q, seasonsAdministration) {
        $scope.loading = true;

        $q.all([seasonsAdministration.getSeasons()])
          .then(function (payload) {
              


              $scope.loading = false;
            }, function (error) {
              $scope.loading = false;
              $scope.errorMessage = error;
              $scope.loadingFailed = true;
              console.log(error);
          });
    });