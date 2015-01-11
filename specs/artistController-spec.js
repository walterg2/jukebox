'use strict';

describe('ArtistController', function() {
    var subject,
        $httpBackend;

    beforeEach(function() {
        module('jukebox');

        inject(function($rootScope, $controller, _$httpBackend_) {
            subject = $rootScope;
            $httpBackend = _$httpBackend_;

            $controller('ArtistController', {$scope: $rootScope});
        });
    });

    afterEach(function() {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    it('loads the albums initially', function() {
        var expectedArtists = [
          {Name: '2Pac'}, {Name: 'The Who'}
        ];

        $httpBackend.expectGET('/api/artists')
          .respond(expectedArtists);
        $httpBackend.flush();

        expect(subject.artists).toEqual(expectedArtists);
    });
});
