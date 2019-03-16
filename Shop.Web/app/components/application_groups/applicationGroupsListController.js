(function (app) {
    app.controller('applicationGroupsListController', applicationGroupsListController);
    applicationGroupsListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter']
    function applicationGroupsListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.loading = true;
        $scope.applicationGroups = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.getApplicationGroups = getApplicationGroups;
        $scope.search = search;
        $scope.deleteItem = deleteItem;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            $ngBootbox.confirm('Bạn có chắc muốn xóa ?').then(function () {
                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.ID);
                });

                var config = {
                    params: {
                        checkedList: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/applicationgroup/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bảng ghi.');
                    search();
                }, function (err) {
                    notificationService.displayError('Xóa không thành công !');
                })
            })
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.applicationGroups, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.applicationGroups, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch('applicationGroups', function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteItem(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa ?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }

                apiService.del('/api/applicationgroup/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công.');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công.');
                })

            });
        }

        function search() {
            getApplicationGroups();
        }

        function getApplicationGroups(page) {
            page = page || 0;
            $scope.loading = true;
            var config = {
                params: {
                    filter: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }
            apiService.get('/api/applicationgroup/getlistpaging', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displaySuccess('Không có bảng ghi nào được tìm thấy');
                }

                $scope.applicationGroups = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function (error) {
                notificationService.displayError(error.data)
                $scope.loading = false;
            })
        }

        $scope.getApplicationGroups();
    }
})(angular.module('shop.application_groups'));