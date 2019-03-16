(function (app) {
    app.controller('applicationUsersAddController', applicationUsersAddController);
    applicationUsersAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function applicationUsersAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.applicationUser = {
            ID: 0,
            Groups: []
        };

        $scope.addApplicationUser = addApplicationUser;

        function addApplicationUser() {
            apiService.post('/api/applicationuser/add', $scope.applicationUser,
                function (result) {
                    notificationService.displaySuccess($scope.applicationUser.FullName + ' đã được thêm thành công.');
                    $state.go('application_users');
                }, function (err) {
                    console.log('err', err);
                    notificationService.displayError(err.data.Message)
                });
        }

        function loadGroups() {
            apiService.get('/api/applicationgroup/getlistall',
                null,
                function (res) {
                    $scope.groups = res.data;
                }, function (err) {
                    notificationService.displayError('Không tải được danh sách nhóm');
                });
        }

        loadGroups();
    }
})(angular.module('shop.application_users'));