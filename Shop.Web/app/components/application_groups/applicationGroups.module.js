/// <reference path="D:\Shop\Git\Shop.Web\Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('shop.application_groups', ['shop.common']).config(config);
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('application_groups', {
                url: '/application_groups',
                parent: 'base',
                templateUrl: '/app/components/application_groups/applicationGroupsListView.html',
                controller: 'applicationGroupsListController'
            })
            .state('add_application_groups', {
                url: '/add_application_groups',
                parent: 'base',
                templateUrl: '/app/components/application_groups/applicationGroupsAddView.html',
                controller: 'applicationGroupsAddController'
            })
            .state('edit_application_groups', {
                url: '/edit_application_groups/:id',
                parent: 'base',
                templateUrl: '/app/components/application_groups/applicationGroupsEditView.html',
                controller: 'applicationGroupsEditController'
            });
    }
})();