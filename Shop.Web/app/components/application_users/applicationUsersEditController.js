(function (app) {
    app.controller('applicationUsersEditController', applicationUsersEditController);
    applicationUsersEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']
    function applicationUsersEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.applicationUser = {
        };
      
        $scope.updateApplicationUser = updateApplicationUser;
      
        function loadApplicationUserDetail() {
            apiService.get('/api/applicationuser/getbyid/' + $stateParams.id, null, function (result) {
                $scope.applicationUser = result.data;
            }, function (err) {
                notificationService.displayError(err.data);
            });
        }

        function updateApplicationUser() {
            $scope.applicationUser.BirthDate = moment($scope.applicationUser.BirthDate, "DD/MM/YYYY").add(1, "days");
            apiService.put('/api/applicationuser/update', $scope.applicationUser,
                function (result) {
                    notificationService.displaySuccess($scope.applicationUser.FullName + ' đã được cập nhật thành công.');
                    $state.go('application_users');
                }, function (err) {
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
        loadApplicationUserDetail();
    }
})(angular.module('shop.application_users'));