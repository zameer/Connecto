'use strict';
/* Controllers */
var cName = 'Vendor';
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
    $scope.add = function () {
        $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
            if(data.Status == "Failure")showMessage(data);
            else $location.path('/');
        });
    };
});
cSettingControllers.controller(cName + 'EditCtrl', ['$scope', '$http', '$location', '$routeParams',
  function ($scope, $http, $location, $routeParams) {
      $http.get('/' + cName + '/GetItem/' + $routeParams.itemId).success(function (data) {
          $scope.item = data;
      });
      $scope.edit = function () {
          $http.post('/' + cName + '/Edit/', $scope.item).success(function (data) {
              if (data.Status == "Failure") showMessage(data);
              else $location.path('/');
          });
      };
  }]);