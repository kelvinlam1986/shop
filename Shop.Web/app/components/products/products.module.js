/// <reference path="D:\Shop\Git\Shop.Web\Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('shop.products', ['shop.common']).config(config);
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('products', {
                url: '/products',
                parent: 'base',
                templateUrl: '/app/components/products/productListView.html',
                controller: 'productListController'
            })
            .state('product_add', {
                url: '/product_add',
                parent: 'base',
                templateUrl: '/app/components/products/productAddView.html',
                controller: 'productAddController'
            })
            .state('product_edit', {
                url: '/product_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/products/productEditView.html',
                controller: 'productEditController'
            })
    }
})();