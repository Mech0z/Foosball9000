
app.controller("seasonsAdministrationController",
    function ($scope, $q, seasonsAdministration) {
        $scope.loading = true;


        $q.all([seasonsAdministration.getSeasons()])
          .then(function (payload) {

                $scope.seasons = payload[0];

                $scope.loading = false;
            }, function (error) {
              $scope.loading = false;
              $scope.errorMessage = error;
              $scope.loadingFailed = true;
              console.log(error);
            });

        $scope.startNewSeason = function () {
            $scope.loading = true;
            var startNewSeasonPromise = seasonsAdministration.startNewSeason();

            startNewSeasonPromise.then(function () {
                $scope.loadSeasons();
            }, function (error) {
                $scope.loading = false;
                $scope.errorMessage = error;
                $scope.validationFailed = true;
            });
        };

        $scope.loadSeasons = function () {
            var getSeasonsPromise = seasonsAdministration.getSeasons();

            getSeasonsPromise.then(function (seasons) {

                $scope.seasons = seasons;

                $scope.loading = false;
            }, function (error) {
                $scope.loading = false;
                $scope.errorMessage = error;
                $scope.validationFailed = true;
            });
        };
    });