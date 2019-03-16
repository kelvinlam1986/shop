(function (app) {
    app.controller('applicationUsersAddController', applicationUsersAddController);
    applicationUsersAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function applicationUsersAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.applicationUser = {
            ID: 0
        };

        $scope.addApplicationUser = addApplicationUser;

        function addApplicationUser() {
            apiService.post('/api/applicationuser/add', $scope.applicationUser,
                function (result) {
                    notificationService.displaySuccess($scope.applicationUser.Name + ' đã được thêm thành công.');
                    $state.go('application_users');
                }, function (err) {
                    console.log('err', err);
                    notificationService.displayError(err.data.Message)
                });
        }
    }
})(angular.module('shop.application_users'));