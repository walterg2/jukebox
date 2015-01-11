'use strict';

var Jukebox = Jukebox || {};

Jukebox.app = angular.module('jukebox', ['ngRoute'])
    .config(['$routeProvider', function($routeProvider) {
        $routeProvider
            .when('/artists', {
                templateUrl: 'partials/artists.html',
                controller: 'ArtistController'
            })
            .otherwise({redirectTo: '/artists'});

    }]);
