(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);
    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state']
    function productCategoryAddController($scope, apiService, notificationService, $state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            CreatedBy: 'admin',
            UpdatedDate: new Date(),
            UpdatedBy: 'admin',
            Status: true
        };
        $scope.parentCategories = [];

        $scope.addProductCategory = addProductCategory;

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