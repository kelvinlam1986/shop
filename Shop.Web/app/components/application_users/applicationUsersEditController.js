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
            apiService.put('/api/applicationuser/update', $scope.applicationUser,
                function (result) {
                    notificationService.displaySuccess($scope.applicationUser.Name + ' đã được cập nhật thành công.');
                    $state.go('application_users');
                }, function (err) {
                    notificationService.displayError(err.data.Message)
                });
        }

        loadApplicationUserDetail();
    }
})(angular.module('shop.application_users'));