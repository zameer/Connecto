'use strict';
/* Controllers */
var cName = 'Person';
var contact = 'Contact';
hrControllers.controller(cName + 'ListCtrl', ['$scope', '$http', '$routeParams',
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
hrControllers.controller(cName + 'NewCtrl', function ($scope, $location, $http) {
    $scope.add = function () {
        $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
            if(data.Status == "Failure")showMessage(data);
            else $location.path('/');
        });
    };
});
hrControllers.controller(cName + 'EditCtrl', ['$scope', '$http', '$location', '$routeParams',
  function ($scope, $http, $location, $routeParams) {
      $http.get('/' + cName + '/GetItem/' + $routeParams.itemId).success(function (data) {
          $scope.item = data;
      });
      $scope.edit = function () {
          $http.post('/' + cName + '/Edit/', $scope.item).success(function (data) {
              $location.path('/');
          });
      };
  }]);

hrControllers.controller(cName + 'ContactsCtrl', ['$scope', '$http', '$location', '$routeParams',
  function ($scope, $http, $location, $routeParams) {
      $http.get('/' + contact + '/Get/' + $routeParams.itemId).success(function (data) {
          $scope.PersonId = $routeParams.itemId;
          $scope.items = data;
          AppCommonFunction.HideWaitBlock();
      });
      
      $scope.addContact = function () {
          $scope.item.PersonId = $scope.PersonId;
          $http.post('/' + contact + '/Create/', $scope.item).success(function (data) {
              if (data.Status == "Failure") showMessage(data);
          });
      };
  }]);