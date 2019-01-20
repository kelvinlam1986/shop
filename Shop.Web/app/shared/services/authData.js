﻿(function (app) {
    'use strict';
    app.factory('authData', [
        function () {
            var authDataFactory = {};
            var authentication = {
                IsAutheticated: false,
                userName: ''
            };

            authDataFactory.authenticationData = authentication;
            return authDataFactory;
        }
    ])
})(angular.module('shop.common'));