'use strict';
/* Controllers */
var cName = 'Supplier';
var dataTable = null;
hrControllers.controller(cName + 'ListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.loadItems = function () {
          if (!$.fn.dataTable.isDataTable('#example')) {
              dataTable = $('#example').dataTable({
                  "serverSide": true,
                  "ordering": false,
                  "sAjaxSource": "/Supplier/Get",
                  "fnServerData": function (sSource, aoData, fnCallback) {
                      AppCommonFunction.ShowWaitBlock();
                      $.get(sSource, aoData, function (json) {
                          fnCallback(json);
                      }).always(function () { AppCommonFunction.HideWaitBlock(); });
                  },
                  "columns": [
                      { "data": "SupplierId" },
                      {
                          "data": "LastName", "render": function (data, type, full) {
                              var person = full["Person"];
                              return person["FirstName"] + " " + person["LastName"];
                          }
                      },
                      {
                          "render": function (data, type, full) {
                              return "<a class='btn btn-xs btn-danger delete-row' data-id='" + full["SupplierId"] + "'><i class='ace-icon fa fa-trash-o bigger-120'></i></a>";
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