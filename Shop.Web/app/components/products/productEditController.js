(function (app) {
    app.controller('productEditController', productEditController);
    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams']
    function productEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {

        $scope.categories = [];
        $scope.product = {
            UpdatedDate: new Date(),
            UpdatedBy: 'admin'
        };

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.updateProduct = updateProduct;
        $scope.getSeoTitle = getSeoTitle;
        $scope.chooseImage = chooseImage;

        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
            }, function (err) {
                notificationService.displayError(err.data);
            });
        }

        function chooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl, data) {
                $scope.product.Image = fileUrl;
            };

            finder.popup();
        }

        function updateProduct() {
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