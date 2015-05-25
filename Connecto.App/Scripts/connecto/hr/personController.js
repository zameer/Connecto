'use strict';
/* Controllers */
var cName = 'Person';
var contact = 'Contact';
var dataTable = null;
hrControllers.controller(cName + 'ListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.loadItems = function () {
          if (!$.fn.dataTable.isDataTable('#example')) {
              dataTable = $('#example').dataTable({
                  "serverSide": true,
                  "ordering": false,
                  "sAjaxSource": "/Person/Get",
                  "fnServerData": function (sSource, aoData, fnCallback) {
                      console.log(aoData);
                      AppCommonFunction.ShowWaitBlock();
                      $.get(sSource, aoData, function (json) {
                          fnCallback(json);
                      }).always(function () { AppCommonFunction.HideWaitBlock(); });
                  },
                  "columns": [
                      { "data": "PersonId" },
                      { "data": "LastName", "render": function (data, type, full) {
                              return full["FirstName"] + " " + full["LastName"];
                          }
                      },
                      { "render": function (data, type, full) {
                              return "<a class='btn btn-xs btn-info' href='#/Edit/" + full["PersonId"] + "'><i class='ace-icon fa fa-pencil bigger-120'></i></a>" +
                                  "<a class='btn btn-xs btn-warning'' href='#/Contacts/" + full["PersonId"] + "'><i class='ace-icon fa fa-eye bigger-120'></i></a>" +
                                  "<a class='btn btn-xs btn-danger delete-row' data-id='" + full["PersonId"] + "'><i class='ace-icon fa fa-trash-o bigger-120'></i></a>";
                          }
                      }
                  ]
              });
              mapSearchKeyup(dataTable);
              $('#example tbody').on('click', '.delete-row', function () {
                  $scope.delete($(this).data('id'));
              });
          }
      };
      $scope.loadItems();
      $scope.delete = function (itemId) {
          bootbox.confirm("Are you sure want to delete?", function (result) {
              if (result) {
                  $http.post('/' + cName + '/Delete/', { id: itemId }).success(function (data) {
                      showMessage(data);
                      if (data.Status != "Failure") dataTable.fnDraw();
                  });
              }
          });
      };
  }]);
hrControllers.controller(cName + 'NewCtrl', function ($scope, $location, $http) {
    $scope.add = function () {
        $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
            showMessage(data);
            if (data.Status != "Failure") $location.path('/');
        });
    };
});
hrControllers.controller(cName + 'EditCtrl', ['$scope', '$http', '$location', '$routeParams',
  function ($scope, $http, $location, $routeParams) {
      $http.get('/' + cName + '/GetItem/' + $routeParams.itemId).success(function (data) {
          data.PersonId = $routeParams.itemId;
          $scope.item = data;
      });
      $scope.edit = function () {
          AppCommonFunction.ShowWaitBlock();
          $http.post('/' + cName + '/Edit/', $scope.item).success(function (data) {
              showMessage(data);
              if (data.Status != "Failure") $location.path('/');
              AppCommonFunction.HideWaitBlock();
          });
      };
  }]);

hrControllers.controller(cName + 'ContactsCtrl', ['$scope', '$http', '$location', '$routeParams',
  function ($scope, $http, $location, $routeParams) {
      $scope.loadItems = function (id) {
          AppCommonFunction.ShowWaitBlock();
          $http.get('/' + contact + '/Get/' + id).success(function(data) {
              $scope.PersonId = id;
              $scope.items = data;
              AppCommonFunction.HideWaitBlock();
          });
      };
      $scope.loadItems($routeParams.itemId);
      
      $scope.addContact = function () {
          AppCommonFunction.ShowWaitBlock();
          if (!($scope.editing != undefined && $scope.editing)) {
              $scope.item.PersonId = $scope.PersonId;
              $http.post('/' + contact + '/Create/', $scope.item).success(function (data) {
                  if (data.Status == "Failure") showMessage(data);
                  $scope.loadItems($routeParams.itemId);
              });
          } else {
              $http.post('/' + contact + '/Edit/', $scope.item).success(function (data) {
                  if (data.Status == "Failure") showMessage(data);
                  $scope.loadItems($routeParams.itemId);
              });
          }
          
      };
      $scope.select = function (contactId) {
          $http.get('/' + contact + '/GetItem/' + contactId).success(function (data) {
              if (data.Status == "Failure") showMessage(data);
              $scope.item = data;
              $scope.editing = true;
          });
      };
      $scope.delete = function (itemId) {
          bootbox.confirm("Are you sure want to delete?", function (result) {
              if (result) {
                  $http.post('/' + contact + '/Delete/', { id: itemId }).success(function (data) {
                      if (data.Status == "Failure") showMessage(data);
                      $scope.loadItems($routeParams.itemId);
                  });
              }
          });
      };
      $scope.resetContact = function () {
          $scope.editing = false;
      };
  }]);