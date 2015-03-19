'use strict';
/* Controllers */
var cName = 'Transaction';
trControllers.controller(cName + 'CartInCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.loadInvoices = function () {
          $http.get('/' + cName + '/GetInvoices/').success(function (data) {
              $scope.invoices = data;
          });
      };
      $scope.loadItems = function (invoiceId) {
          if (invoiceId != undefined) {
              $http.get('/' + cName + '/GetCart/' + invoiceId).success(function(data) {
                  $scope.items = data;
              });
          } else $scope.items = [];
      };
      $scope.loadSelections = function () {
          $http.get('/Product/Get/').success(function (data) {
              $scope.products = data;
          });
          $http.get('/Supplier/Get/').success(function (data) {
              $scope.suppliers = data;
          });
      };
      $scope.loadInvoices();
      $scope.loadItems();
      $scope.loadSelections();
      $scope.filterInvoice = function (invoiceId) {
          if (invoiceId.length > 0) $scope.loadItems(invoiceId);
          else $scope.items = [];
      };
      $scope.add = function () {
          $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
              showMessage(data);
              if (data.Status != "Failure") $scope.loadItems($scope.item.InvoiceId);
          });
      };
  }]);
