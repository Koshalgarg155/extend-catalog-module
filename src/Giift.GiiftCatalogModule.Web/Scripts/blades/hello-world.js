angular.module('GiiftCatalogModule')
    .controller('GiiftCatalogModule.helloWorldController', ['$scope', 'GiiftCatalogModule.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'GiiftCatalogModule';

        blade.refresh = function () {
            api.get(function (data) {
                blade.title = 'GiiftCatalogModule.blades.hello-world.title';
                blade.data = data.result;
                blade.isLoading = false;
            });
        };

        blade.refresh();
    }]);
