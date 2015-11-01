'use strict';
/* App Module */
var cSettingControllers = angular.module('cSettingControllers', []);
var cSettingApp = angular.module('cSettingApp', [
  'ngRoute',
  'cSettingControllers'
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
  'ui.select',
  'ngSanitize'
]);
trApp.filter('getById', function () {
    return function(input, id, propName) {
        var i = 0, len = input.length;
        for (; i < len; i++) {
            if (propName == 'ProductId') 
                if (+input[i].ProductId == +id) return input[i];
            
            if (propName == 'SupplierId') 
                if (+input[i].SupplierId == +id) return input[i];
            
            if (propName == 'OrderId')
                if (+input[i].OrderId == +id) return input[i];
            
        }
        return null;
    };
});
