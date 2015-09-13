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
                  if ($scope.SalesDetails != '') $scope.SalesDetails.OrderId = orderId;
                  $scope.Measure = data.Measure;
                  //setProductDetail(data[0]);
              });
          }
      };
      $scope.filterSalesSelection = function (productDetailId) {
          var arrItems = $scope.SalesDetails;
          angular.forEach(arrItems, function (item) {
              if (item.ProductDetailId == $scope.item.ProductDetailId) {

                  $scope.details = item;
                  console.log(item);
                  //setProductDetail(item);
                  //$scope.item.PrductDetailId = productDetailId;
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
          var lowerUnitPrice = $scope.item.SellingPrice / ($scope.details.ContainsQty == 0 ? 1 : $scope.item.ContainsQty);
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
          //var SoldQuantity = ($Scope.item.Quantity * ($scope.item.ContainsQty == 0 ? 1 : $scope.item.ContainsQty)) * ($scope.item.Volume == 0 ? 1 : $scope.item.Volume);
          var SoldQuantityActual = $scope.item.QuantityActual * ($scope.item.Volume == 0 ? 1 : $scope.item.Volume);
          var SoldQuantityLower = $scope.item.QuantityLower;
          //var TotalSoldQuantity = SoldQuantity + SoldQuantityActual + SoldQuantityLower;
          var TotalSoldQuantity = SoldQuantityActual + SoldQuantityLower;

          //var ReturnBagQuantity = $Scope.item.ReturnQuantity * ($scope.item.ContainsQty == 0 ? 1 : $scope.item.ContainsQty) * ($scope.item.Volume == 0 ? 1 : $scope.item.Volume);
          var ReturnKgQuantity = $scope.item.ReturnQuantityActual * $scope.item.Volume;
          var ReturngQuantity = $scope.item.ReturnQuantityLower;
         // var TotalReturnQuantity = ReturnBagQuantity + ReturnKgQuantity + ReturngQuantity;
          var TotalReturnQuantity = ReturnKgQuantity + ReturngQuantity;
          
          var unitDiscount = $scope.item.Discount / TotalSoldQuantity;

          $scope.item.returnDiscountPrice = unitDiscount * TotalReturnQuantity;

          if ($scope.item.Price == $scope.item.returnPrice) {
              $scope.item.returnNetPrice = $scope.item.NetPrice;
          }
          else {
              $scope.item.returnNetPrice = Math.round($scope.item.returnPrice - $scope.item.returnDiscountPrice);
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