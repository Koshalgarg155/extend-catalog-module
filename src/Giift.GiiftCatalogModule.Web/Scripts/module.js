// Call this to register your module to main application
var moduleName = 'GiiftCatalogModule';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider',
        function ($stateProvider) {
            $stateProvider
                .state('workspace.GiiftCatalogModuleState', {
                    url: '/GiiftCatalogModule',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        'platformWebApp.bladeNavigationService',
                        function (bladeNavigationService) {
                            var newBlade = {
                                id: 'blade1',
                                controller: 'GiiftCatalogModule.helloWorldController',
                                template: 'Modules/$(Giift.GiiftCatalogModule)/Scripts/blades/hello-world.html',
                                isClosingDisabled: true,
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['platformWebApp.mainMenuService', '$state',
        function (mainMenuService, $state) {
            //Register module in main menu
            var menuItem = {
                path: 'browse/GiiftCatalogModule',
                icon: 'fa fa-cube',
                title: 'GiiftCatalogModule',
                priority: 100,
                action: function () { $state.go('workspace.GiiftCatalogModuleState'); },
                permission: 'GiiftCatalogModule:access',
            };
            mainMenuService.addMenuItem(menuItem);
        }
    ]);
