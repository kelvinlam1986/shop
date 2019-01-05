/// <reference path="D:\Shop\Git\Shop.Web\Assets/admin/libs/angular/angular.js" />
//(function () {
//    angular.module('shop', ['shop.products', 'shop.common']).config(config);

//    config.$inject = ['$stateProvider', '$urlRouterProvider'];
//    function config($stateProvider, $urlRouterProvider) {
//        $stateProvider.state('home', {
//            url: '/admin',
//            templateUrl: '/app/components/home/homeView.html',
//            controller: 'homeController'
//        });
//        $urlRouterProvider.otherwise('/admin');

//    }
//})();

(function () {
    angular.module('shop', ['ui.router', 'shop.products', 'shop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
    }
})();