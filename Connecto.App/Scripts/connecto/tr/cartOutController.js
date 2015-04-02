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
          var lowerUnitPrice = $scope.item.SellingPrice / $scope.item.ContainsQty;
          var unitPrice = $scope.item.SellingPrice * $scope.item.Quantity;
          var actualPrice = lowerUnitPrice * $scope.item.QuantityActual;
          var lowerPrice = (lowerUnitPrice / $scope.item.Volume) * $scope.item.QuantityLower;
          $scope.item.Price = Math.round(unitPrice + actualPrice + lowerPrice);
          $scope.calculateDiscount();
      };
      $scope.filterOrder = function (orderId) {
          if (orderId.length > 0) $scope.loadItems(orderId);
          else $scope.items = [];
      };
      $scope.setDiscount = function (discountBy) {
          $scope.discountBy = discountBy;
          $scope.calculateDiscount();
      };
      $scope.calculateDiscount = function () {
          if ($scope.item.DiscountValue == undefined) $scope.item.DiscountValue = 0;
          if ($scope.discountBy == "Amount")
              $scope.item.Discount = Math.round($scope.item.DiscountValue);
          if ($scope.discountBy == "Rate")
              $scope.item.Discount = Math.round(($scope.item.DiscountValue / 100) * $scope.item.Price);
          $scope.calculateNetPrice();
      };
      $scope.calculateNetPrice = function () {
          $scope.item.NetPrice = $scope.item.Price - ($scope.item.Discount != undefined ? $scope.item.Discount : 0);
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
