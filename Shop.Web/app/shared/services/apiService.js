(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService'];

    function apiService($http, notificationService) {
        return {
            get: get,
            post: post,
            put: put
        }

        function post(url, data, success, failed) {
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
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {
                failed(error);
            });
        }
    }
})(angular.module('shop.common'))