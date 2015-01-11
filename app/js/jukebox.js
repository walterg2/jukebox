'use strict';

var Jukebox = Jukebox || {};

Jukebox.app = angular.module('jukebox', ['ngRoute'])
    .config(['$routeProvider', function($routeProvider) {
        $routeProvider
            .when('/artists', {
                templateUrl: 'partials/artists.html',
                controller: 'ArtistController'
            })
            .when('/artist/:id/albums', {
                templateUrl: 'partials/albums.html',
                controller: 'AlbumController'
            })
            .otherwise({redirectTo: '/artists'});
    }]);
