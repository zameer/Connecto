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
                  $scope.calculateGrossPrice();
              });
          } else $scope.items = [];
      };
      $scope.loadOrders();
      $scope.loadItems();
      $scope.filterProductSelection = function (productDetailId) {
          var arrItems = $scope.itemz;
          angular.forEach(arrItems, function (item) {
              if (item.ProductDetailId == productDetailId) {
                  setProductDetail(item);
              }
          });
      };
      $scope.filterProduct = function () {
          if ($scope.item.ProductCode != undefined) {
              $http.get('/' + cName + '/GetSalesDetail/?productCode=' + $scope.item.ProductCode).success(function (data) {
                  $scope.itemz = data;
                  setProductDetail(data[0]);
              });
          }
      };
      function setProductDetail(data) {
          var orderId = $scope.item.OrderId;
          $scope.item = data;
          if ($scope.item != '') $scope.item.OrderId = orderId;
          $scope.Measure = data.Measure;
      };

      $scope.calculatePrice = function () {
          $scope.decideSellingPrice();
          var lowerUnitPrice = $scope.item.SellingPrice / ($scope.item.ContainsQty == 0 ? 1 : $scope.item.ContainsQty);
          var unitPrice = $scope.item.SellingPrice * $scope.item.Quantity;
          var actualPrice = lowerUnitPrice * $scope.item.QuantityActual;
          var lowerPrice = (lowerUnitPrice / $scope.item.Volume) * $scope.item.QuantityLower;
          $scope.item.Price = Math.round(unitPrice + actualPrice + lowerPrice);
          $scope.calculateDiscount();
          
      };
      $scope.decideSellingPrice = function () {
          if ($scope.item.SellingMargin && $scope.item.Quantity == 0) {
              if ($scope.item.SellingPrice == $scope.item.SellingPriceActual) $scope.item.SellingPrice += $scope.item.MarginAmount;
          } else $scope.item.SellingPrice = $scope.item.SellingPriceActual;
      };
      $scope.filterOrder = function (orderId) {
          if (orderId.length > 0) $scope.loadItems(orderId);
          else $scope.items = [];
      };
      $scope.setDiscount = function (discountBy) {
          $scope.DiscountBy = discountBy;
          $scope.calculateDiscount();
      };
      $scope.calculateDiscount = function () {
          if ($scope.item.DiscountAs == undefined) $scope.item.DiscountAs = 0;
          if ($scope.DiscountBy == '')
              $scope.item.Discount = 0;
          if ($scope.DiscountBy == "Amount")
              $scope.item.Discount = Math.round($scope.item.DiscountAs);
          if ($scope.DiscountBy == "Rate") {
              $scope.item.DiscountBy = $scope.DiscountBy;
              $scope.item.Discount = Math.round(($scope.item.DiscountAs / 100) * $scope.item.Price);
          }
          $scope.calculateNetPrice();
      };
      $scope.calculateNetPrice = function () {
          $scope.item.NetPrice = $scope.item.Price - ($scope.item.Discount != undefined ? $scope.item.Discount : 0);
          
      };
<<<<<<< HEAD

      $scope.calculateGrossPrice = function () {
          $scope.GrossNetPrice = 0; $scope.GrossPrice = 0; $scope.GrossDiscount = 0;
          angular.forEach($scope.items, function (item) {
              $scope.GrossNetPrice = $scope.GrossNetPrice + item.NetPrice;
              $scope.GrossPrice = $scope.GrossPrice + item.Price;
              $scope.GrossDiscount = $scope.GrossDiscount + item.Discount;
          });
      };
=======
      //$scope.calculateTotalNetPrice = function () {
      //    $scope.item.TotalNetPrice = $scope.item.TotalNetPrice + $scope.item.NetPrice;

      //};
>>>>>>> 382d6209ca2de7d35d1a2a172c6e342c72e3829f
      $scope.add = function () {
          $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
              //$scope.calculateTotalNetPrice();
              showMessage(data);
              $scope.item.OrderId = data.OrderId != undefined && data.OrderId > 0 ? data.OrderId : $scope.item.OrderId;
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
      $scope.print = function () {
          $http.post('/' + cName + '/Print/', { orderId: 64 }).success(function (data) {
              showMessage(data);
          });
      };
  }]);
