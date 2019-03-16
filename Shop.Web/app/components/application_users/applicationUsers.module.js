/// <reference path="D:\Shop\Git\Shop.Web\Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('shop.application_users', ['shop.common']).config(config);
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('application_users', {
                url: '/application_users',
                parent: 'base',
                templateUrl: '/app/components/application_users/applicationUsersListView.html',
                controller: 'applicationUsersListController'
            })
            .state('add_application_users', {
                url: '/add_application_users',
                parent: 'base',
                templateUrl: '/app/components/application_users/applicationUsersAddView.html',
                controller: 'applicationUsersAddController'
            })
            .state('edit_application_users', {
                url: '/edit_application_users/:id',
                parent: 'base',
                templateUrl: '/app/components/application_users/applicationUsersEditView.html',
                controller: 'applicationUsersEditController'
            });
    }
})();