Jukebox.app
    .controller('ArtistController', ['$scope', '$http', function($scope, $http) {

        $scope.artists = [];

        $http.get('/api/artists')
            .success(function(data) {
                $scope.artists = data;
            });
    }])
    .controller('AlbumController', ['$scope', '$http', '$routeParams', function($scope, $http, $routeParams) {
        $scope.albums = [];

        $scope.artist = $routeParams.id;

        $http.get('/api/albums/' + btoa($scope.artist) + '/')
            .success(function(data) {
                $scope.albums = data;
            });
    }]);
