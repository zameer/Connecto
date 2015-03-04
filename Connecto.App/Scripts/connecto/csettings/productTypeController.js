'use strict';
/* Routing */

/* Controllers */
cSettingControllers.controller('ProductTypeListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      AppCommonFunction.ShowWaitBlock();
      $http.get('/ProductType/Get/').success(function (data) {
          $scope.productTypes = data;
          AppCommonFunction.HideWaitBlock();
      });
      $scope.delete = function (productTypeId) {
          bootbox.confirm("Are you sure want to delete?", function (result) {
              if (result) {
                  $http.post('/ProductType/Delete/', { id: productTypeId }).success(function (data) {
                      if (data.Status == "Failure") showMessage(data);
                      else $location.path('/');
                  });
              }
          });
      };
  }]);
cSettingControllers.controller('ProductTypeNewCtrl', function ($scope, $location, $http) {
    $http.get('/Measure/Get/').success(function (data) {
        $scope.measures = data;
    });

    $scope.add = function () {
        $http.post('/ProductType/Create/', $scope.ProductType).success(function (data) {
            if(data.Status == "Failure")showMessage(data);
            else $location.path('/');
        });
    };
});
cSettingControllers.controller('ProductTypeEditCtrl', ['$scope', '$http', '$location', '$routeParams',
  function ($scope, $http, $location, $routeParams) {
      $http.get('/ProductType/GetItem/' + $routeParams.productTypeId).success(function (data) {
          $scope.productType = data;
          
          $http.get('/Measure/Get/').success(function (measures) {
              $scope.measures = measures;
              $('.select2').select2();
              $('.select2').css('width', '200px').select2();
          });
      });

      $scope.edit = function () {
          $http.post('/ProductType/Edit/', $scope.productType).success(function (data) {
              $location.path('/');
          });
      };
  }]);