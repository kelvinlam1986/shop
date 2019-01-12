/// <reference path="D:\Shop\Git\Shop.Web\Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', 'apiService']
    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;

        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/productcategory/getall', config, function (result) {
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function (error) {
                console.log('Load product categories failed.', error);
            })
        }

        $scope.getProductCategories();
    }
})(angular.module('shop.product_categories'));