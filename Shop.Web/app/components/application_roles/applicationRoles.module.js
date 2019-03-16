/// <reference path="D:\Shop\Git\Shop.Web\Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('shop.application_roles', ['shop.common']).config(config);
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('application_roles', {
                url: '/application_roles',
                parent: 'base',
                templateUrl: '/app/components/application_roles/applicationRolesListView.html',
                controller: 'applicationRolesListController'
            })
            .state('add_application_roles', {
                url: '/add_application_roles',
                parent: 'base',
                templateUrl: '/app/components/application_roles/applicationRolesAddView.html',
                controller: 'applicationRolesAddController'
            })
            .state('edit_application_roles', {
                url: '/edit_application_roles/:id',
                parent: 'base',
                templateUrl: '/app/components/application_roles/applicationRolesEditView.html',
                controller: 'applicationRolesEditController'
            });
    }
})();