'use strict';
/* Controllers */
cSettingControllers.controller('MeasureListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      AppCommonFunction.ShowWaitBlock();
      $http.get('/Measure/Get/').success(function (data) {
          $scope.measures = data;
          AppCommonFunction.HideWaitBlock();
      });
      $scope.delete = function (measureId) {
          bootbox.confirm("Are you sure want to delete?", function (result) {
              if (result) {
                  $http.post('/Measure/Delete/', { id: measureId }).success(function (data) {
                      if (data.Status == "Failure") showMessage(data);
                      else $location.path('/');
                  });
              }
          });
      };
  }]);
cSettingControllers.controller('MeasureNewCtrl', function ($scope, $location, $http) {
    $scope.add = function () {
        $http.post('/Measure/Create/', $scope.Measure).success(function (data) {
            if(data.Status == "Failure")showMessage(data);
            else $location.path('/');
        });
    };
});
cSettingControllers.controller('MeasureDetailCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http, $routeParams) {
      $http.get('/Measure/GetItem/' + $routeParams.measureId).success(function (data) {
          $scope.measure = data;
      });
  }]);
cSettingControllers.controller('MeasureEditCtrl', ['$scope', '$http', '$location', '$routeParams',
  function ($scope, $http, $location, $routeParams) {
      $http.get('/Measure/GetItem/' + $routeParams.measureId).success(function (data) {
          $scope.measure = data;
      });
      $scope.edit = function () {
          $http.post('/Measure/Edit/', $scope.measure).success(function (data) {
              $location.path('/');
          });
      };
  }]);