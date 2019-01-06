/// <reference path="D:\Shop\Git\Shop.Web\Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService']
    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.getProductCategories = getProductCategories;

        function getProductCategories() {
            apiService.get('/api/productcategory/getall', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                console.log('Load product categories failed.', error);
            })
        }

        $scope.getProductCategories();
    }
})(angular.module('shop.product_categories'));