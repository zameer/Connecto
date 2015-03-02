'use strict';
/* Controllers */
cSettingControllers.controller('MeasureListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      //showMessage({ Messages: [{ Message: 'Hi' }, { Message: 'Hi' }], Status: 'Failure' });
      $http.get('/Measure/Get/').success(function (data) {
          $scope.measures = data;
      });
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
