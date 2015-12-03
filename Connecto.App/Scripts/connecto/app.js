'use strict';
/* App Module */
var cSettingControllers = angular.module('cSettingControllers', []);
var cSettingApp = angular.module('cSettingApp', [
  'ngRoute',
  'cSettingControllers',
  'ui.select',
  'ngSanitize'
]);

var hrControllers = angular.module('hrControllers', []);
var hrApp = angular.module('hrApp', [
  'ngRoute',
  'hrControllers'
]);

var trControllers = angular.module('trControllers', []);
var trApp = angular.module('trApp', [
  'ngRoute',
  'trControllers',
  'ui.bootstrap',
  'ui.select',
  'ngSanitize'
]);
trApp.filter('getById', function () {
    return function (input, id, propName) {
        var i = 0, len = input.length;
        for (; i < len; i++) {
            if (propName == 'ProductId') 
                if (+input[i].ProductId == +id) return input[i];
            
            if (propName == 'SupplierId') 
                if (+input[i].SupplierId == +id) return input[i];
            
            if (propName == 'OrderId')
                if (+input[i].OrderId == +id) return input[i];
            
            if (propName == 'InvoiceId')
                if (+input[i].InvoiceId == +id) return input[i];
            
            if (propName == 'EmployeeId')
                if (+input[i].EmployeeId == +id) return input[i];
        }
        return null;
    };
});
cSettingApp.filter('getById', function () {
    return function (input, id, propName) {
        var i = 0, len = input.length;
        for (; i < len; i++) {
            if (propName == 'MeasureId')
                if (+input[i].MeasureId == +id) return input[i];
        }
        return null;
    };
});

