'use strict';
/* Controllers */
var cName = 'CartOut';
trControllers.controller(cName + 'Ctrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $('#date-timepicker1').datetimepicker().next().on(ace.click_event, function () {
          $(this).prev().focus();
      });
      $scope.loadOrders = function () {
          $http.get('/' + cName + '/GetOrders/').success(function (data) {
              $scope.orders = data;
          });
      };
      $scope.loadCustomers = function () {
          $http.get('/Customer/GetAll/').success(function (data) {
              $scope.customers = data;
          });
      };

      $scope.loadItems = function (orderId) {
          if (orderId != undefined) {
              $http.get('/' + cName + '/GetCart/' + orderId).success(function (data) {
                  $scope.item.CustomerId = data[0] != undefined ? data[0].CustomerId : 0;
                  $scope.items = data;
                  $scope.calculateGrossPrice();
              });
          } else  $scope.items = [];
      };
      $scope.loadOrders();
      $scope.loadCustomers();
      $scope.loadItems();
      $scope.filterProductSelection = function (productDetailId) {
          var arrItems = $scope.itemz;
          angular.forEach(arrItems, function (item) {
              if (item.ProductDetailId == productDetailId) {
                  setProductDetail(item);
                  $scope.item.PrductDetailId = productDetailId;
              }
          });
      };
      $scope.filterProduct = function (item) {
          if ($scope.item.ProductCode != undefined) {
              $http.get('/' + cName + '/GetSalesDetail/?productCode=' + $scope.item.ProductCode).success(function (data) {
                  $scope.itemz = data;
                  if (item) $scope.setEditDetails(item);
                  else {
                      setProductDetail(data[0]);
                      $scope.decideSellingPrice();
                  }
              });
          }
      };
      $scope.setEditDetails = function (item) {
          $scope.filterProductSelection(item.ProductDetailId);
          $scope.item.CustomerId = item.CustomerId;
          $scope.item.SalesDetailId = item.SalesDetailId;
          $scope.item.Quantity = item.Quantity;
          $scope.item.QuantityActual = item.QuantityActual;
          $scope.item.QuantityLower = item.QuantityLower;
          $scope.item.SellingPrice = item.SellingPrice;
          $scope.item.Discount = item.Discount;
          $scope.item.Price = item.Price;
          $scope.DiscountBy = discountBy(item.DiscountBy);
          $scope.item.DiscountBy = $scope.DiscountBy;
          $scope.item.Discount = item.Discount;
          $scope.item.DiscountAs = item.DiscountAs;
          $scope.item.NetPrice = item.NetPrice;
          //$('#product-customer').trigger("change");
          //$('#productDetail').trigger("change");
      };

      function discountBy(enumber) {
          if (enumber == 1) return 'Rate';
          else if (enumber == 2) return 'Amount';
          else return '';
      }
      $scope.calculateBalance = function () { $scope.Balance = ($scope.Paid - $scope.GrossNetPrice + ($scope.Fluctuation * 1)); };
      function setProductDetail(data) {
          var orderId = $scope.item.OrderId;
          data.DateSold = '';
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
          }
      };
      $scope.filterOrder = function (orderId) {
          if (orderId.length > 0) $scope.loadItems(orderId);
          else {
              $scope.items = [];
              $scope.calculateGrossPrice();
          } 
      };
      $scope.DiscountBy = 'None';
      $scope.setDiscount = function (discountBy)
      {
          $scope.item.DiscountBy = discountBy;
          $scope.DiscountBy = discountBy;
          $scope.calculateDiscount();
      };
      $scope.calculateDiscount = function () {
          if ($scope.item.DiscountAs == undefined) $scope.item.DiscountAs = 0;
          if ($scope.DiscountBy == 'None') {
              $scope.item.DiscountAs = 0;
              $scope.item.Discount = 0;
          }
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
      
      $scope.calculateGrossPrice = function () {
          $scope.GrossNetPrice = 0; $scope.GrossPrice = 0; $scope.GrossDiscount = 0;
          $scope.Paid = 0; $scope.Balance = 0; $scope.Fluctuation = 0;
          angular.forEach($scope.items, function (item) {
              $scope.GrossNetPrice = $scope.GrossNetPrice + item.NetPrice;
              $scope.GrossPrice = $scope.GrossPrice + item.Price;
              $scope.GrossDiscount = $scope.GrossDiscount + item.Discount;
          });
      };
      $scope.add = function () {
          $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
              showMessage(data);
              $scope.item.OrderId = data.OrderId != undefined && data.OrderId > 0 ? data.OrderId : $scope.item.OrderId;
              if (data.Status != "Failure") $scope.loadItems($scope.item.OrderId);
              
          });
          
      };
      $scope.edit = function (item) {
          $scope.item = item;
          $scope.filterProduct(item);
      };
      $scope.delete = function (salesDetailId) {
          $http.post('/' + cName + '/Delete/', { id: salesDetailId }).success(function (data) {
              showMessage(data);
              if (data.Status != "Failure") {
                  $scope.loadItems($scope.item.OrderId);
              }
          });
      };
      $scope.complete = function () {
          $http.post('/' + cName + '/Complete/', { id: $scope.item.OrderId, fluctuation: $scope.Fluctuation }).success(function (data) {
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
