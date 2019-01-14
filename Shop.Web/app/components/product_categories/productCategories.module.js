/// <reference path="D:\Shop\Git\Shop.Web\Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('shop.product_categories', ['shop.common']).config(config);
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('product_categories', {
            url: '/product_categories',
            templateUrl: '/app/components/product_categories/productCategoryListView.html',
            controller: 'productCategoryListController'
        }).state('add_product_category', {
            url: '/add_product_category',
            templateUrl: '/app/components/product_categories/productCategoryAddView.html',
            controller: 'productCategoryAddController'
        }).state('edit_product_category', {
            url: '/edit_product_category/:id',
            templateUrl: '/app/components/product_categories/productCategoryEditView.html',
            controller: 'productCategoryEditController'
        });
    }
})();