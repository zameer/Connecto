'use strict';
/* Controllers */
var cName = 'CartOut';
trControllers.controller(cName + 'Ctrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.loadOrders = function () {
          $http.get('/' + cName + '/GetOrders/').success(function (data) {
              $scope.orders = data;
          });
      };
      $scope.loadItems = function (orderId) {
          if (orderId != undefined) {
              $http.get('/' + cName + '/GetCart/' + orderId).success(function (data) {
                  $scope.items = data;
              });
          } else $scope.items = [];
      };
      $scope.loadOrders();
      $scope.loadItems();
      $scope.filterProduct = function () {
          if ($scope.item.ProductCode != undefined) {
              $http.get('/' + cName + '/GetSalesDetail/?productCode=' + $scope.item.ProductCode).success(function (data) {
                  $scope.item = data;
                  $scope.Measure = data.Measure;
              });
          }
      };
      $scope.calculatePrice = function () {
          var lowerPrice = ($scope.item.Volume / $scope.item.SellingPrice) * ($scope.item.Lower != undefined ? $scope.item.Lower : 0);
          $scope.item.Price = Math.round(($scope.item.Quantity * $scope.item.SellingPrice) + lowerPrice);
      };
      $scope.filterOrder = function (orderId) {
          if (orderId.length > 0) $scope.loadItems(orderId);
          else $scope.items = [];
      };
      $scope.add = function () {
          $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
              showMessage(data);
              if (data.Status != "Failure") $scope.loadItems($scope.item.OrderId);
          });
      };
      $scope.complete = function () {
          $http.post('/' + cName + '/Complete/', { id: $scope.item.OrderId }).success(function (data) {
              showMessage(data);
              if (data.Status != "Failure") {
                  $scope.loadOrders();
                  $scope.loadItems();
              }
          });
      };
  }]);