'use strict';

/* Controllers */

var productControllers = angular.module('productControllers', []);
productControllers.controller('ProductListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $http.get('/Products/GetProducts/').success(function (data) {
          $scope.products = data;
      });
  }]);
productControllers.controller('ProductNewCtrl', function ($scope, $http) {
    $http.get('/Products/Vendors/').success(function (data) {
        $scope.vendors = data;
    });
    $http.get('/Products/ProductTypes/').success(function (data) {
        $scope.productTypes = data;
    });
    $scope.addProduct = function () {
        $http.post('/Products/Create/', $scope.Product).success(function (data) {
            $scope.Product = {};
            $(".select2").select2('val', '');
        });
    };
});
productControllers.controller('ProductDetailCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http, $routeParams) {
      $http.get('/Products/Get/' + $routeParams.productId).success(function (data) {
          $scope.product = data;
      });
  }]);
productControllers.controller('ProductEditCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http, $routeParams) {
      $http.get('/Products/Get/' + $routeParams.productId).success(function (data) {
          $scope.product = data;
      });
  }]);