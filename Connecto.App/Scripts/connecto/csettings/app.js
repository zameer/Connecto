'use strict';

/* App Module */

var cSettingControllers = angular.module('cSettingControllers', []);
var cSettingApp = angular.module('cSettingApp', [
  'ngRoute',
  'cSettingControllers'
]);

cSettingApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/', {
            templateUrl: '/Measure/List',
            controller: 'MeasureListCtrl'
        }).
        when('/Create/', {
            templateUrl: '/Measure/Create',
            controller: 'MeasureNewCtrl'
        }).
        when('/:measureId', {
            templateUrl: '/measure/Details',
            controller: 'MeasureDetailCtrl'
        }).
        when('/Edit/:measureId', {
            templateUrl: '/measure/Edit',
            controller: 'MeasureEditCtrl'
        }).
        otherwise({
            redirectTo: '/'
        });
  }]);