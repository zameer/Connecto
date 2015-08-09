'use strict';
/* Controllers */
var cName = 'CustomerReturn';
trControllers.controller(cName + 'Ctrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.getReasons = function () {
          $http.get('/ReturnReason/Get/').success(function (data) {
              $scope.reasons = data;
              console.log($scope.reasons)
          });
      };
      $scope.getReasons();

      $scope.filterProductSelection = function (productDetailId) {
          var arrItems = $scope.itemz;
          angular.forEach(arrItems, function (item) {
              if (item.ProductDetailId == productDetailId) {
                  setProductDetail(item);
              }
          });
      };
      $scope.filterProduct = function () {
          if ($scope.item.OrderId != undefined) {
              $http.get('/CartOut/Get/?orderId=' + $scope.item.OrderId).success(function (data) {
                  $scope.SalesDetails = data;
                  //setProductDetail(data[0]);
              });
          }
      };
      $scope.filterSalesSelection = function () {
          var arrItems = $scope.SalesDetails;
          console.log($scope.item.ProductDetailId);
          angular.forEach(arrItems, function (item) {
              if (item.ProductDetailId == $scope.item.ProductDetailId) {
                  setProductDetail(item);
                  console.log(item);
              }
          });
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
      $scope.setReturn = function (returnBy) {
          $scope.ReturnBy = returnBy;
      }
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
      $scope.add = function () {
          $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
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
