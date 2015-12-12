'use strict';
/* Controllers */
var cName = 'Product';
var dataTable = null;
cSettingControllers.controller(cName + 'ListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.loadItems = function () {
          if (!$.fn.dataTable.isDataTable('#product-list')) {
              dataTable = $('#product-list').dataTable({
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
    $scope.vendor = {};
    $scope.producttype = {};

    $http.get('/Vendor/Get/').success(function (data) {
        $scope.vendors = data;
    });
    $http.get('/ProductType/Get/').success(function (data) {
        $scope.productTypes = data;
    });
    $scope.add = function () {
        $scope.item.VendorId = $scope.vendor.selected == null ? null : $scope.vendor.selected.VendorId;
        $scope.item.ProductTypeId = $scope.producttype.selected == null ? null : $scope.producttype.selected.ProductTypeId;
        $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
            if(data.Status == "Failure")showMessage(data);
            else $location.path('/');
        });
    };
    $scope.filterProductType = function () {
        var ptype = $filter('filter')($scope.productTypes, { ProductTypeId: $scope.producttype.selected.ProductTypeId })[0];
        if (ptype != undefined && $scope.item.SellingLower)
            $scope.ContainsQtyDesc = ptype.Measure.Actual + '(s) in a ' + ptype.StockAs;
        else $scope.ContainsQtyDesc = undefined;
    };
    $scope.filterSellingMargin = function () {
        if (!$scope.item.SellingMargin)
            $scope.MarginAmount = undefined;
    };
});
cSettingControllers.controller(cName + 'EditCtrl', ['$scope', '$filter','$http', '$location', '$routeParams',
  function ($scope, $filter, $http, $location, $routeParams) {
      $scope.vendor = {};
      $scope.producttype = {};

      $scope.loadVendors = function (id) {
          $http.get('/Vendor/Get/').success(function (vendors) {
              $scope.vendors = vendors;
              $scope.vendor.selected = $filter('filter')($scope.vendors, { VendorId: id })[0];
          });
      };
      $scope.loadProductTypes = function (id) {
          $http.get('/ProductType/Get/').success(function (producttypes) {
              $scope.productTypes = producttypes;
              $scope.producttype.selected = $filter('filter')($scope.productTypes, { ProductTypeId: id })[0];
              $scope.filterProductType();
          });
      };
      $http.get('/' + cName + '/GetItem/' + $routeParams.itemId).success(function (product) {
          $scope.item = product;
          $scope.loadVendors(product.VendorId);
          $scope.loadProductTypes(product.ProductTypeId);
      });

      $scope.edit = function () {
          $scope.item.VendorId = $scope.vendor.selected == null ? null : $scope.vendor.selected.VendorId;
          $scope.item.ProductTypeId = $scope.producttype.selected == null ? null : $scope.producttype.selected.ProductTypeId;
          $http.post('/' + cName + '/Edit/', $scope.item).success(function (data) {
              $location.path('/');
          });
      };
      $scope.filterProductType = function () {
          var ptype = $filter('filter')($scope.productTypes, { ProductTypeId: $scope.producttype.selected.ProductTypeId })[0];
          if (ptype != undefined && $scope.item.SellingLower)
              $scope.ContainsQtyDesc = ptype.Measure.Actual + '(s) in a ' + ptype.StockAs;
          else $scope.ContainsQtyDesc = undefined;
      };
      $scope.allowAutoSelling = function () {
          if ($scope.item.AutoSelling)
              $scope.item.AutoSellingQty = 1;
          else $scope.item.AutoSellingQty = undefined;
      };
  }]);