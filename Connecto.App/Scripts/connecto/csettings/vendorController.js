'use strict';
/* Controllers */
var cName = 'Vendor';
var dataTable = null;
cSettingControllers.controller(cName + 'ListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
       $scope.loadItems = function () {
          if (!$.fn.dataTable.isDataTable('#example')) {
              dataTable = $('#example').dataTable({
                  "serverSide": true,
                  "ordering": false,
                  "sAjaxSource": "/Vendor/Get",
                  "fnServerData": function (sSource, aoData, fnCallback) {
                      AppCommonFunction.ShowWaitBlock();
                      $.get(sSource, aoData, function (json) {
                          fnCallback(json);
                      }).always(function () { AppCommonFunction.HideWaitBlock(); });
                  },
                  "columns": [
                      { "data": "VendorId" },
                      { "data": "Name", "render": function (data, type, full) {
                              return full["Name"];
                          }
                      },
                      { "render": function (data, type, full) {
                              return "<a class='btn btn-xs btn-info' href='#/Edit/" + full["VendorId"] + "'><i class='ace-icon fa fa-pencil bigger-120'></i></a>" +
                                  "<a class='btn btn-xs btn-danger delete-row' data-id='" + full["VendorId"] + "'><i class='ace-icon fa fa-trash-o bigger-120'></i></a>";
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