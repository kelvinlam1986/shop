(function (app) {
    app.controller('applicationRolesEditController', applicationRolesEditController);
    applicationRolesEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']
    function applicationRolesEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.applicationRole = {
        };
      
        $scope.updateApplicationRole = updateApplicationRole;
      
        function loadApplicationRoleDetail() {
            apiService.get('/api/applicationrole/getbyid/' + $stateParams.id, null, function (result) {
                $scope.applicationRole = result.data;
            }, function (err) {
                notificationService.displayError(err.data);
            });
        }

        function updateApplicationRole() {
            apiService.put('/api/applicationrole/update', $scope.applicationRole,
                function (result) {
                    notificationService.displaySuccess($scope.applicationRole.Name + ' đã được cập nhật thành công.');
                    $state.go('application_roles');
                }, function (err) {
                    notificationService.displayError(err.data.Message)
                });
        }

        loadApplicationRoleDetail();
    }
})(angular.module('shop.application_roles'));