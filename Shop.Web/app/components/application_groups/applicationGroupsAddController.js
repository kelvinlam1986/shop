(function (app) {
    app.controller('applicationGroupsAddController', applicationGroupsAddController);
    applicationGroupsAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function applicationGroupsAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.applicationGroup = {
            ID: 0,
            Roles: []
        };

        $scope.addApplicationGroup = addApplicationGroup;

        function addApplicationGroup() {
            apiService.post('/api/applicationgroup/add', $scope.applicationGroup,
                function (result) {
                    notificationService.displaySuccess($scope.applicationGroup.Name + ' đã được thêm thành công.');
                    $state.go('application_groups');
                }, function (err) {
                    notificationService.displayError('Thêm mới không thành công.')
                });
        }

        function loadRoles() {
            apiService.get('/api/applicationrole/getlistall', null, function (response) {
                $scope.roles = response.data;
            }, function (err) {
                notificationService.displayError('Không tải được danh sách quyền');
            });
        }

        loadRoles();
    }
})(angular.module('shop.application_groups'));