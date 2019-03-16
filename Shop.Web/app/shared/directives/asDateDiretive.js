(function (app) {
    'use strict';

    app.directive('asDate', asDate);

    function asDate(dateFilter) {
        return {
            require:'ngModel',
            link:function (scope, elm, attrs, ctrl) {
                var dateFormat = attrs['date'] || 'dd/MM/yyyy';
                ctrl.$formatters.unshift(function (modelValue) {
                    if (!modelValue) {
                        return;
                    }
                    return dateFilter(modelValue, dateFormat);
                });
                ctrl.$parsers.push(function (modelValue) {
                    return modelValue;
                });
            }
        };
    }
})(angular.module('shop.common'));