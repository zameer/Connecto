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
      $scope.loadReturns = function () {
          $http.get('/' + cName + '/Get/').success(function (data) {
              $scope.customerReturns = data;
          });
      };
      $scope.loadReturns();
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

      $scope.calculateReturnPrice = function () {
          var lowerUnitPrice = $scope.item.SellingPrice / ($scope.item.ContainsQty == 0 ? 1 : $scope.item.ContainsQty);
          if ($scope.item.ReturnQuantity == null) $scope.item.ReturnQuantity = 0;
          if ($scope.item.ReturnQuantityActual == null) $scope.item.ReturnQuantityActual = 0;
          if($scope.item.ReturnQuantityLower == null) $scope.item.ReturnQuantityLower = 0;
          var unitPrice = $scope.item.SellingPrice * $scope.item.ReturnQuantity;
          var actualPrice = lowerUnitPrice * $scope.item.ReturnQuantityActual;
          var lowerPrice = (lowerUnitPrice / $scope.item.Volume) * $scope.item.ReturnQuantityLower;
          $scope.item.returnPrice = Math.round(unitPrice + actualPrice + lowerPrice);
          $scope.calculateReturnNetPrice();
      };
      $scope.calculateReturnNetPrice = function () {
          var lowerUnitDiscountPrice = $scope.item.Discount / ($scope.item.ContainsQty == 0 ? 1 : $scope.item.ContainsQty);
          var unitDiscountPrice = $scope.item.Discount * $scope.item.ReturnQuantity;
          var actualDiscountPrice = lowerUnitDiscountPrice * $scope.item.ReturnQuantityActual;
          var lowerDiscountPrice = (lowerUnitDiscountPrice / $scope.item.Volume) * $scope.item.ReturnQuantityLower;
          var returnDiscountPrice = unitDiscountPrice + actualDiscountPrice + lowerDiscountPrice;

          if ($scope.item.Price == $scope.item.returnPrice) {
              $scope.item.returnNetPrice = $scope.item.NetPrice;
          }
          else {
              $scope.item.returnNetPrice = $scope.item.returnPrice - returnDiscountPrice;
          }
          $scope.calculateReturnChangeAmount();
      };
      $scope.calculateReturnChangeAmount = function () {
          $scope.item.returnChangeAmount = $scope.item.returnAmountPaid - $scope.item.returnNetPrice;
      };
      $scope.filterOrder = function (orderId) {
          if (orderId.length > 0) $scope.loadItems(orderId);
          else $scope.items = [];
      };
      $scope.setReturn = function (returnBy) {
          $scope.ReturnBy = returnBy;
      }
      $scope.complete = function () {
          $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
              showMessage(data);
              $scope.item.OrderId = data.OrderId != undefined && data.OrderId > 0 ? data.OrderId : $scope.item.OrderId;
              if (data.Status != "Failure") $scope.loadItems($scope.item.OrderId);
          });
      };
      $scope.completeA = function () {
          $http.post('/' + cName + '/Complete/', { id: $scope.item.OrderId }).success(function (data) {
              showMessage(data);
              if (data.Status != "Failure") {
                  $scope.loadReturns();
              }
          });
      };
      $scope.print = function () {
          $http.post('/' + cName + '/Print/', { orderId: 64 }).success(function (data) {
              showMessage(data);
          });
      };
  }]);
