'use strict';

/* App Module */

var productApp = angular.module('productApp', [
  'ngRoute',
  'productControllers'
]);

productApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/', {
            templateUrl: '/products/List',
            controller: 'ProductListCtrl'
        }).
        when('/Create/', {
            templateUrl: '/products/Create',
            controller: 'ProductNewCtrl'
        }).
        when('/:productId', {
            templateUrl: '/products/Details',
            controller: 'ProductDetailCtrl'
        }).
        when('/Edit/:productId', {
            templateUrl: '/products/Edit',
            controller: 'ProductEditCtrl'
        }).
        otherwise({
            redirectTo: '/'
        });
  }]);