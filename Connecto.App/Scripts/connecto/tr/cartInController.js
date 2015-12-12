'use strict';
/* Controllers */
var cName = 'CartIn';
trControllers.controller(cName + 'Ctrl', ['$scope', '$filter', '$http', '$routeParams',
  function ($scope, $filter, $http) {
      $http.get('/' + cName + '/GetProductCodes/').success(function (data) {
          $scope.productCodes = data;
      });
     
      $('#date-timepicker1').datetimepicker().next().on(ace.click_event, function () {
          $(this).prev().focus();
      });
      $scope.starter = {};
      $http.get('/Home/GetStarter/').success(function (data) {
          $scope.starter = data;
      });

      $scope.employee = {};
      $scope.loadEmployees = function () {
          $http.get('/Employee/GetAll/').success(function (data) {
              $scope.employees = data;
              if ($scope.employee.selected == null) {
                  $scope.employee.selected = $filter('getById')(data, $scope.starter.EmployeeId, "EmployeeId");
              }
          });
      };
      $scope.order = {};
      $scope.loadOrders = function () {
          $http.get('/' + cName + '/GetOrders/').success(function (data) {
              $scope.orders = data;
              $scope.order.selected = $scope.item != undefined ? $filter('getById')($scope.orders, $scope.item.OrderId, "OrderId") : null;
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
      $scope.loadEmployees();
      $scope.loadOrders();
      $scope.loadItems();
      $scope.loadSelections();
      $scope.setProduct = function () {
          if ($scope.productCodeSelected != undefined) {
              $scope.product.selected = $filter('getById')($scope.products, $scope.productCodeSelected.ProductId, "ProductId");
              $scope.supplier.selected = $filter('getById')($scope.suppliers, $scope.productCodeSelected.SupplierId, "SupplierId");

              if ($scope.item == undefined) $scope.item = {};
              $scope.item.ProductCode = $scope.productCodeSelected.ProductCode == undefined ? $scope.productCodeSelected : $scope.productCodeSelected.ProductCode;
              $scope.item.Barcode = $scope.productCodeSelected.Barcode;
              $scope.filterProduct();
          }
      };
      $scope.filterProduct = function () {
          var productId = $scope.product.selected == undefined ? undefined : $scope.product.selected.ProductId;
          if (productId != undefined) {
              $http.get('/Product/GetItem/' + productId).success(function (data) {
                  $scope.info = data;
              });
          } else $scope.info = [];
      };
      $scope.filterOrder = function (orderId) {
          $scope.reset();
          if (orderId > 0) $scope.loadItems(orderId);
          else $scope.items = [];
      };
      $scope.reset = function () {
          $scope.item = {};
          $scope.itemz = [];
          $scope.supplier = {};
          $scope.product = {};
          $scope.info = [];
          $scope.employee.selected = $scope.order.selected != undefined ? $filter('getById')($scope.employees, $scope.order.selected.EmployeeId, "EmployeeId")
              : $filter('getById')($scope.employees, $scope.starter.EmployeeId, "EmployeeId");
      };
      $scope.setHeader = function () {
          if ($scope.item == null) $scope.item = {};
          $scope.item.ProductId = $scope.product.selected == null ? null : $scope.product.selected.ProductId;
          $scope.item.SupplierId = $scope.supplier.selected == null ? null : $scope.supplier.selected.SupplierId;
          $scope.item.EmployeeId = $scope.employee.selected == null ? null : $scope.employee.selected.EmployeeId;
          
          if ($scope.order.selected != null) {
              $scope.item.OrderId = $scope.order.selected.OrderId;
              $scope.item.ReferenceCode = $scope.order.selected.ReferenceCode;
              $scope.item.DateReceived = $scope.order.selected.OrderDateDisplay;
          }
      };
      $scope.add = function () {
          $scope.setHeader();
          $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
              showMessage(data);
              if ($scope.item.OrderId == undefined) $scope.loadOrders();
              $scope.reset();
              $scope.item.OrderId = data.OrderId != undefined && data.OrderId > 0 ? data.OrderId : $scope.item.OrderId;
              if (data.Status != "Failure") $scope.loadItems($scope.item.OrderId);
          });
      };
      $scope.edit = function (item) {
          $scope.product.selected = $filter('getById')($scope.products, item.ProductId, "ProductId");
          $scope.supplier.selected = $filter('getById')($scope.suppliers, item.SupplierId, "SupplierId");
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
      $scope.saveHeader = function () {
          $scope.setHeader();
          $http.post('/' + cName + '/EditHeader/', $scope.item).success(function (data) {
              showMessage(data);
          });

      };
      $scope.complete = function () {
          $http.post('/' + cName + '/Complete/', { id: $scope.order.selected.OrderId }).success(function (data) {
              showMessage(data);
              if (data.Status != "Failure") {
                  $scope.loadOrders();
                  $scope.loadItems();
              }
          });
      };
  }]);
