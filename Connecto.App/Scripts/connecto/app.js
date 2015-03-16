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
  'trControllers'
]);
