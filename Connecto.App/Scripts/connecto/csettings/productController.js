'use strict';
/* Controllers */
var cName = 'Product';
cSettingControllers.controller(cName + 'ListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      AppCommonFunction.ShowWaitBlock();
      $http.get('/' + cName + '/Get/').success(function (data) {
          $scope.items = data;
          AppCommonFunction.HideWaitBlock();
      });
      $scope.delete = function (itemId) {
          bootbox.confirm("Are you sure want to delete?", function (result) {
              if (result) {
                  $http.post('/' + cName + '/Delete/', { id: itemId }).success(function (data) {
                      if (data.Status == "Failure") showMessage(data);
                      else $location.path('/');
                  });
              }
          });
      };
  }]);
cSettingControllers.controller(cName + 'NewCtrl', function ($scope, $filter, $location, $http) {
    $http.get('/Vendor/Get/').success(function (data) {
        $scope.vendors = data;
    });
    $http.get('/ProductType/Get/').success(function (data) {
        $scope.productTypes = data;
    });
    $scope.add = function () {
        $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
            if(data.Status == "Failure")showMessage(data);
            else $location.path('/');
        });
    };
    $scope.filterProductType = function (productTypeId) {
        var ptype = $filter('filter')($scope.productTypes, { ProductTypeId: productTypeId })[0];
        if (ptype != undefined && (ptype.Measure.Actual != ptype.StockAs))
            $scope.ContainsQtyDesc = ptype.Measure.Actual + '(s) in a ' + ptype.StockAs;
        else $scope.ContainsQtyDesc = undefined;
    };
});
cSettingControllers.controller(cName + 'EditCtrl', ['$scope', '$filter','$http', '$location', '$routeParams',
  function ($scope, $filter, $http, $location, $routeParams) {
      $http.get('/' + cName + '/GetItem/' + $routeParams.itemId).success(function (data) {
          $scope.item = data;
          $http.get('/Vendor/Get/').success(function (data1) {
              $scope.vendors = data1;
              $http.get('/ProductType/Get/').success(function (data2) {
                  $scope.productTypes = data2;
                  $('.select2').select2();
                  $('.select2').css('width', '200px').select2();
                  $scope.filterProductTypes(data.ProductTypeId);
              });
          });
      });

      $scope.edit = function () {
          $http.post('/' + cName + '/Edit/', $scope.item).success(function (data) {
              $location.path('/');
          });
      };
      $scope.filterProductTypes = function (productTypeId) {
          var ptype = $filter('filter')($scope.productTypes, { ProductTypeId: productTypeId })[0];
          if (ptype != undefined && (ptype.Measure.Actual != ptype.StockAs))
              $scope.ContainsQtyDesc = ptype.Measure.Actual + '(s) in a ' + ptype.StockAs;
          else $scope.ContainsQtyDesc = undefined;
      };
  }]);