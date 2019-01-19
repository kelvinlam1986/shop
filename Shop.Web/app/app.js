(function () {
    angular.module('shop',
        ['shop.product_categories',
         'shop.products',
         'shop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider', '$qProvider'];

    function config($stateProvider, $urlRouterProvider, $qProvider) {
        $qProvider.errorOnUnhandledRejections(false);
        $stateProvider
            .state('base', {
                url: '',
                templateUrl: '/app/shared/views/baseView.html',
                abstract: true
            })
            .state('login', {
                url: '/login',
                templateUrl: '/app/components/login/loginView.html',
                controller: 'loginController'
            })
            .state('home', {
                url: '/admin',
                parent: 'base',
                templateUrl: '/app/components/home/homeView.html',
                controller: 'homeController'
            });

        $urlRouterProvider.otherwise('/login');
    }
})();