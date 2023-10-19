angular.module('GiiftCatalogModule')
    .factory('GiiftCatalogModule.webApi', ['$resource', function ($resource) {
        return $resource('api/giift-catalog-module');
    }]);
