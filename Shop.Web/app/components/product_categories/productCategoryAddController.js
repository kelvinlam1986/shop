﻿(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);
    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function productCategoryAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.productCategory = {
            Status: true
        };

        $scope.parentCategories = [];
        $scope.addProductCategory = addProductCategory;
        $scope.getSeoTitle = getSeoTitle;
        $scope.chooseImage = chooseImage;

        function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl, data) {
                $scope.$apply(function () {
                    $scope.productCategory.Image = fileUrl;
                });
            };

            finder.popup();
        }

        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function addProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess($scope.productCategory.Name + ' đã được thêm thành công.');
                    $state.go('product_categories');
                }, function (err) {
                    notificationService.displayError('Thêm mới không thành công.')
                });
        }

        function loadParentCategory() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;
            }, function (err) {
                console.log('Cannot get list parents');
            });
        }

        loadParentCategory();
    }
})(angular.module('shop.product_categories'));