'use strict';
/* Controllers */
var cName = 'Product';
var dataTable = null;
cSettingControllers.controller(cName + 'ListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.loadItems = function () {
          if (!$.fn.dataTable.isDataTable('#example')) {
              dataTable = $('#example').dataTable({
                  "serverSide": true,
                  "ordering": false,
                  "sAjaxSource": "/Product/GetSearch",
                  "fnServerData": function (sSource, aoData, fnCallback) {
                      AppCommonFunction.ShowWaitBlock();
                      $.get(sSource, aoData, function (json) {
                          fnCallback(json);
                      }).always(function () { AppCommonFunction.HideWaitBlock(); });
                  },
                  "columns": [
                      { "data": "ProductId" },
                      { "data": "Name" },
                      {
                          "data": "ProductTypeId", "render": function (data, type, full) {
                              var ptype = full["ProductType"];
                              return ptype.Type + " " + ptype.StockAs;
                      }
                      },
                      {
                          "data": "VendorId", "render": function (data, type, full) {
                              return full["Vendor"].Name;
                              
                          }
                      },
                      {
                          "data": "VendorId", "render": function (data, type, full) {
                              return full["Vendor"].Name;
                          }
                      },
                      {
                          "render": function (data, type, full) {
                              return "<a class='btn btn-xs btn-info' href='#/Edit/" + full["ProductId"] + "'><i class='ace-icon fa fa-pencil bigger-120'></i></a>" +
                                  "<a class='btn btn-xs btn-danger delete-row' data-id='" + full["ProductId"] + "'><i class='ace-icon fa fa-trash-o bigger-120'></i></a>";
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
                      else $location.path('/');
                  });
              }
          });
      };
  }]);
cSettingControllers.controller(cName + 'NewCtrl', function ($scope, $filter, $location, $http) {
    $http.get('/Vendor/Get/').success(function (data) {
        $scope.vendors = data;
    });
    $http.get('/ProductType/Get/').success(function (data) {
        $scope.productTypes = data;
    });
    $scope.add = function () {
        $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
            if(data.Status == "Failure")showMessage(data);
            else $location.path('/');
        });
    };
    $scope.filterProductType = function () {
        var ptype = $filter('filter')($scope.productTypes, { ProductTypeId: $scope.item.ProductTypeId })[0];
        if (ptype != undefined && $scope.item.SellingLower)
            $scope.ContainsQtyDesc = ptype.Measure.Actual + '(s) in a ' + ptype.StockAs;
        else $scope.ContainsQtyDesc = undefined;
    };
    $scope.filterSellingMargin = function () {
        console.log($scope.item.SellingMargin);
        if (!$scope.item.SellingMargin)
            $scope.MarginAmount = undefined;
    };
});
cSettingControllers.controller(cName + 'EditCtrl', ['$scope', '$filter','$http', '$location', '$routeParams',
  function ($scope, $filter, $http, $location, $routeParams) {
      $http.get('/' + cName + '/GetItem/' + $routeParams.itemId).success(function (data) {
          $scope.item = data;
          $http.get('/Vendor/Get/').success(function (data1) {
              $scope.vendors = data1;
              $http.get('/ProductType/Get/').success(function (data2) {
                  $scope.productTypes = data2;
                  $('.select2').select2();
                  $('.select2').css('width', '200px').select2();
                  $scope.filterProductType();
              });
          });
      });

      $scope.edit = function () {
          $http.post('/' + cName + '/Edit/', $scope.item).success(function (data) {
              $location.path('/');
          });
      };
      $scope.filterProductType = function () {
          var ptype = $filter('filter')($scope.productTypes, { ProductTypeId: $scope.item.ProductTypeId })[0];
          if (ptype != undefined && $scope.item.SellingLower)
              $scope.ContainsQtyDesc = ptype.Measure.Actual + '(s) in a ' + ptype.StockAs;
          else $scope.ContainsQtyDesc = undefined;
      };
  }]);