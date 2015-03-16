'use strict';
/* Controllers */
var cName = 'Transaction';
trControllers.controller(cName + 'CartInCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.loadItems = function () {
          $http.get('/Product/Get/').success(function (data) {
              $scope.products = data;
          });
          $http.get('/Supplier/Get/').success(function (data) {
              $scope.suppliers = data;
          });
      };
      $scope.loadItems();
  }]);
