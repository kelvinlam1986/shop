/// <reference path="D:\Shop\Git\Shop.Web\Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function productAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.categories = [];
        $scope.moreImages = [];
        $scope.product = {
            CreatedDate: new Date(),
            CreatedBy: 'admin',
            UpdatedDate: new Date(),
            UpdatedBy: 'admin',
            Status: true
        };

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.addProduct = addProduct;
        $scope.getSeoTitle = getSeoTitle;
        $scope.chooseImage = chooseImage;
        $scope.chooseMoreImage = chooseMoreImage;

        function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl, data) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
            };

            finder.popup();
        }

        function addProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('/api/product/create', $scope.product,
                function (result) {
                    notificationService.displaySuccess($scope.product.Name + ' đã được thêm thành công.');
                    $state.go('products');
                }, function (err) {
                    notificationService.displayError('Thêm mới không thành công.')
                });
        }

        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function loadProductCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.categories = result.data;
            }, function (err) {
                console.log('Can not load product category list');
            })
        }

        function chooseMoreImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl, data) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            };

            finder.popup();
        }

        loadProductCategories();
    }
})(angular.module('shop.products'));