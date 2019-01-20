(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams']
    function productEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.categories = [];
        $scope.moreImages = [];
        $scope.product = {
        };

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.updateProduct = updateProduct;
        $scope.getSeoTitle = getSeoTitle;
        $scope.chooseImage = chooseImage;
        $scope.chooseMoreImage = chooseMoreImage;

        function chooseMoreImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl, data) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            };

            finder.popup();
        }

        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
                if ($scope.moreImages === null) {
                    $scope.moreImages = [];
                }
            }, function (err) {
                notificationService.displayError(err.data);
            });
        }

        function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl, data) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
            };

            finder.popup();
        }

        function updateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('/api/product/update', $scope.product,
                function (result) {
                    notificationService.displaySuccess($scope.product.Name + ' đã cập nhật thành công.');
                    $state.go('products');
                }, function (err) {
                    notificationService.displayError('Cập nhật không thành công.')
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

        loadProductCategories();
        loadProductDetail();
    }
})(angular.module('shop.products'));