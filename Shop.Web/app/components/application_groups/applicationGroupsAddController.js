(function (app) {
    app.controller('applicationGroupsAddController', applicationGroupsAddController);
    applicationGroupsAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']
    function applicationGroupsAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.applicationGroup = {
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
    }
})(angular.module('shop.application_groups'));