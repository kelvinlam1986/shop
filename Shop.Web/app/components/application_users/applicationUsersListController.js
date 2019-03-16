(function (app) {
    app.controller('applicationUsersListController', applicationUsersListController);
    applicationUsersListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter']
    function applicationUsersListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.loading = true;
        $scope.applicationUsers = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.getApplicationUsers = getApplicationUsers;
        $scope.search = search;
        $scope.deleteItem = deleteItem;
        $scope.isAll = false;
     
        function deleteItem(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }

                apiService.del('/api/applicationuser/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công.');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công.');
                })

            });
        }

        function search() {
            getApplicationUsers();
        }

        function getApplicationUsers(page) {
            page = page || 0;
            $scope.loading = true;
            var config = {
                params: {
                    filter: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }
            apiService.get('/api/applicationuser/getlistpaging', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displaySuccess('Không có bảng ghi nào được tìm thấy');
                }

                $scope.applicationUsers = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function (error) {
                $scope.loading = false;
                notificationService.displayError(error.data)
            })
        }

        $scope.getApplicationUsers();
    }
})(angular.module('shop.application_users'));