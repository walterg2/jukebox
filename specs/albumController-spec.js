describe('AlbumController', function() {
    var subject,
        $httpBackend,
        init,
        route = {};

    beforeEach(function() {
        module('jukebox', function($provide) {
            $provide.value('$routeParams', route);
        });

        inject(function($rootScope, $controller, _$httpBackend_) {
            subject = $rootScope;
            $httpBackend = _$httpBackend_;

            init = function() {
                $controller('AlbumController', {$scope: $rootScope});
            };
        });
    });

    it('saves the artist', function() {
      route.id = 'The Who';
      init();

      expect(subject.artist).toEqual('The Who');
    });

    it('uses base64 to encode the artist name for the request', function() {
        var expectedAlbums = [
            {Name: 'Notorious'}
        ];
        route.id = 'Notorious B.I.G.';

        init();

        $httpBackend.expectGET('/api/albums/' + btoa(route.id) + '/')
            .respond(expectedAlbums);
        $httpBackend.flush();

        expect(subject.albums).toEqual(expectedAlbums);
    });
});
