'use strict';
/* Controllers */
var cName = 'CartIn';
trControllers.controller(cName + 'Ctrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.order = {};
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
      
      $scope.product = {};
      $scope.supplier = {};
      $scope.loadSelections = function () {
          $http.get('/Product/Get/').success(function (data) {
              $scope.products = data;
          });
          $http.get('/Supplier/Get/').success(function (data) {
              $scope.suppliers = data;
          });
      };
      $scope.loadOrders();
      $scope.loadItems();
      $scope.loadSelections();
      $scope.filterProduct = function () {
          var productId = $scope.product.selected == undefined ? undefined : $scope.product.selected.ProductId;
          if (productId != undefined) {
              $http.get('/Product/GetItem/' + productId).success(function (data) {
                  $scope.info = data;
              });
          } else $scope.info = [];
      };
      $scope.filterOrder = function (orderId) {
          if (orderId > 0) $scope.loadItems(orderId);
          else $scope.items = [];
      };
      $scope.add = function () {
          $scope.item.OrderId = $scope.order.selected == null ? null : $scope.order.selected.OrderId;
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
      $scope.delete = function (productDetailId) {
          $http.post('/' + cName + '/Delete/', { id: productDetailId }).success(function (data) {
              showMessage(data);
              if (data.Status != "Failure") {
                  $scope.loadItems($scope.item.OrderId);
              }
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
