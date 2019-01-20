(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService', 'authenticationService'];

    function apiService($http, notificationService, authenticationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }

        function del(url, data, success, failed) {
            authenticationService.setHeader();
            $http.delete(url, data).then(function (result) {
                success(result);
            }, function (err) {
                if (err.statusCode === '401') {
                    notificationService.displayError('Authentication is required.');
                    failed(err);
                } else {
                    failed(err);
                }
            });
        }

        function post(url, data, success, failed) {
            authenticationService.setHeader();
            $http.post(url, data).then(function (result) {
                success(result);
            }, function (err) {
                if (err.statusCode === '401') {
                    notificationService.displayError('Authentication is required.');
                    failed(err);
                } else {
                    failed(err);
                }
            });
        }

        function put(url, data, success, failed) {
            authenticationService.setHeader();
            $http.put(url, data).then(function (result) {
                success(result);
            }, function (err) {
                if (err.statusCode === '401') {
                    notificationService.displayError('Authentication is required.');
                    failed(err);
                } else {
                    failed(err);
                }
            });
        }

        function get(url, params, success, failed) {
            authenticationService.setHeader();
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {
                failed(error);
            });
        }
    }
})(angular.module('shop.common'))