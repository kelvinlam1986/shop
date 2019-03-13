(function (app) {
    app.controller('applicationGroupsEditController', applicationGroupsEditController);
    applicationGroupsEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']
    function applicationGroupsEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.applicationGroup = {
        };
      
        $scope.updateApplicationGroup = updateApplicationGroup;
      
        function loadApplicationGroupDetail() {
            apiService.get('/api/applicationgroup/getbyid/' + $stateParams.id, null, function (result) {
                $scope.applicationGroup = result.data;
            }, function (err) {
                notificationService.displayError(err.data);
            });
        }

        function updateApplicationGroup() {
            apiService.put('/api/applicationgroup/update', $scope.applicationGroup,
                function (result) {
                    notificationService.displaySuccess($scope.applicationGroup.Name + ' đã được cập nhật thành công.');
                    $state.go('application_groups');
                }, function (err) {
                    notificationService.displayError('Cập nhật không thành công.')
                });
        }

        loadApplicationGroupDetail();
    }
})(angular.module('shop.application_groups'));