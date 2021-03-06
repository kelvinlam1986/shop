﻿(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);
    productCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']
    function productCategoryEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.productCategory = {
            Status: true
        };
        $scope.parentCategories = [];

        $scope.updateProductCategory = updateProductCategory;
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

        function loadProductCategoryDetail() {
            apiService.get('/api/productcategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function (err) {
                notificationService.displayError(err.data);
            });
        }

        function updateProductCategory() {
            apiService.put('/api/productcategory/update', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess($scope.productCategory.Name + ' đã được cập nhật thành công.');
                    $state.go('product_categories');
                }, function (err) {
                    notificationService.displayError('Cập nhật không thành công.')
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
        loadProductCategoryDetail();
    }
})(angular.module('shop.product_categories'));