/// <reference path="D:\Shop\Git\Shop.Web\Assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('shop.product_categories', ['shop.common']).config(config);
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('product_categories', {
            url: '/product_categories',
            templateUrl: '/app/components/product_categories/productCategoryListView.html',
            controller: 'productCategoryListController'
        });
    }
})();