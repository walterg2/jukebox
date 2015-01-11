
Jukebox.app.controller('ArtistController', ['$scope', '$http', function($scope, $http) {

    $scope.artists = [];

    $http.get('/api/artists')
      .success(function(data) {
          $scope.artists = data;
      });
}]);
