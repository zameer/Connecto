'use strict';
/* Controllers */
var cName = 'Supplier';
hrControllers.controller(cName + 'ListCtrl', ['$scope', '$http', '$routeParams',
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
                      $scope.loadItems();
                  });
              }
          });
      };
  }]);
hrControllers.controller(cName + 'PeopleCtrl', function ($scope, $location, $http) {
    AppCommonFunction.ShowWaitBlock();
    $scope.loadItems = function () {
        AppCommonFunction.ShowWaitBlock();
        $http.get('/' + cName + '/GetPeople/').success(function (data) {
            $scope.items = data;
            AppCommonFunction.HideWaitBlock();
        });
    };
    $scope.loadItems();
   
    $scope.assign = function (id) {
        $http.post('/' + cName + '/Create/' + id).success(function (data) {
            if (data.Status == "Failure") showMessage(data);
            $scope.loadItems();
        });
    };
});