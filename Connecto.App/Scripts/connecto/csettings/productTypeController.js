'use strict';
/* Controllers */
var cName = 'ProductType';
cSettingControllers.controller(cName + 'ListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.loadItems = function () {
          AppCommonFunction.ShowWaitBlock();
          $http.get('/' + cName + '/Get/').success(function (data) {
              $scope.items = data;
              AppCommonFunction.HideWaitBlock();
          });
      };
      $scope.loadItems();
      $scope.delete = function (itemId) {
          bootbox.confirm("Are you sure want to delete?", function (result) {
              if (result) {
                  $http.post('/' + cName + '/Delete/', { id: itemId }).success(function (data) {
                      if (data.Status == "Failure") showMessage(data);
                      else $scope.loadItems();
                  });
              }
          });
      };
  }]);
cSettingControllers.controller(cName + 'NewCtrl', function ($scope, $location, $http) {
    $scope.measure = {};
    $http.get('/Measure/Get/').success(function (measures) {
        $scope.measures = measures;
    });
    $scope.add = function () {
        $scope.item.MeasureId = $scope.measure.selected == null ? null : $scope.measure.selected.MeasureId;
        $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
            if(data.Status == "Failure")showMessage(data);
            else $location.path('/');
        });
    };
});
cSettingControllers.controller(cName + 'EditCtrl', ['$scope', '$filter', '$http', '$location', '$routeParams',
  function ($scope, $filter, $http, $location, $routeParams) {
      $scope.measure = {};
      $http.get('/Measure/Get/').success(function (measures) {
          $scope.measures = measures;
      });
      
      $http.get('/' + cName + '/GetItem/' + $routeParams.itemId).success(function (data) {
          $scope.item = data;
          $scope.measure.selected = $scope.item != undefined ? $filter('getById')($scope.measures, data.MeasureId, "MeasureId") : null;
      });

      $scope.edit = function () {
          $scope.item.MeasureId = $scope.measure.selected == null ? null : $scope.measure.selected.MeasureId;
          $http.post('/' + cName + '/Edit/', $scope.item).success(function (data) {
              if (data.Status == "Failure") showMessage(data);
              else $location.path('/');
          });
      };
  }]);