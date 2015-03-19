'use strict';
/* Controllers */
var cName = 'Transaction';
trControllers.controller(cName + 'CartInCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.loadItems = function (invoiceId) {
          if (invoiceId != undefined) {
              $http.get('/' + cName + '/GetCart/', { id: invoiceId }).success(function (data) {
                  $scope.items = data;
              });
          }
      };
      $scope.loadSelections = function () {
          $http.get('/Product/Get/').success(function (data) {
              $scope.products = data;
          });
          $http.get('/Supplier/Get/').success(function (data) {
              $scope.suppliers = data;
          });
      };
      $scope.loadItems();
      $scope.loadSelections();
      $scope.add = function () {
          $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
              showMessage(data);
              if (data.Status != "Failure") $scope.loadItems();
          });
      };
  }]);
