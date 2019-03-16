(function (app) {
    app.controller('applicationRolesAddController', applicationRolesAddController);
    applicationRolesAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function applicationRolesAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.applicationRole = {
            ID: 0
        };

        $scope.addApplicationRole = addApplicationRole;

        function addApplicationRole() {
            apiService.post('/api/applicationrole/add', $scope.applicationRole,
                function (result) {
                    notificationService.displaySuccess($scope.applicationRole.Name + ' đã được thêm thành công.');
                    $state.go('application_roles');
                }, function (err) {
                    console.log('err', err);
                    notificationService.displayError(err.data.Message)
                });
        }
    }
})(angular.module('shop.application_roles'));