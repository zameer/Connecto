'use strict';
/* Controllers */
var cName = 'WriteOff';
var dataTable = null;
trControllers.controller(cName + 'Ctrl', ['$scope', '$filter', '$http', '$routeParams',
  function ($scope, $filter, $http) {
      $http.get('/CartIn/GetProductCodes/').success(function (data) {
          $scope.productCodes = data;
          $('#productCode').focus();
          AppCommonFunction.HideWaitBlock();
      });
      $('#date-timepicker1').datetimepicker().next().on(ace.click_event, function () {
          $(this).prev().focus();
      });
      $scope.starter = {};
      $http.get('/Home/GetStarter/').success(function (data) {
          $scope.starter = data;
      });

      $scope.employee = {};
      $scope.loadEmployees = function () {
          $http.get('/Employee/GetAll/').success(function (data) {
              $scope.employees = data;
              if ($scope.employee.selected == null) {
                  $scope.employee.selected = $filter('getById')(data, $scope.starter.EmployeeId, "EmployeeId");
              }
          });
      };
      $scope.writeoff = {};
      $scope.loadWriteoffs = function () {
          
          $http.get('/' + cName + '/GetWriteoffs/').success(function (data) {
              $scope.writeoffs = data;
              $scope.writeoff.selected = $scope.item != undefined ? $filter('getById')($scope.writeoffs, $scope.item.WriteoffId, "WriteoffId") : null;
          });
      };
      
      $scope.loadItems = function (writeoffId) {
          if (writeoffId != undefined) {
              $http.get('/' + cName + '/GetCart/' + writeoffId).success(function (data) {
                  $scope.items = data;
                  $scope.calculateGrossPrice();
              });
          } else $scope.items = [];
      };
      $scope.loadEmployees();
      $scope.loadWriteoffs();
      $scope.loadItems();
      $scope.filterProductSelection = function (productDetailId) {
          var arrItems = $scope.itemz;
          angular.forEach(arrItems, function (item) {
              if (item.ProductDetailId == productDetailId) {
                  setProductDetail(item);
                  $scope.productDetail.selected = item;
              }
          });
      };
      $scope.submit = function () {
          if ($scope.productCodeSelected != undefined && $scope.productCodeSelected.ProductCode == undefined) {
              $scope.setProduct();
          } else {

          }
      };
      $scope.onSelect = function () {
          $scope.setProduct();
      };
      $scope.setProduct = function () {
          if ($scope.productCodeSelected != undefined) {
              if ($scope.item == undefined) $scope.item = {};
              $scope.item.ProductCode = $scope.productCodeSelected.ProductCode == undefined ? $scope.productCodeSelected : $scope.productCodeSelected.ProductCode;
              $scope.filterProduct();
          }
      };
      $scope.productDetail = {};
      $scope.filterProduct = function (item) {
          if ($scope.item.ProductCode != undefined) {
              $http.get('/' + cName + '/GetWriteoffDetail/?productCode=' + $scope.item.ProductCode).success(function (data) {
                  if (data.length > 0) {
                      $scope.itemz = data;
                      if (item) {
                          $scope.setEditDetails(item);
                      }
                      else {
                          setProductDetail(data[0]);
                      }
                  } else {
                      $scope.reset();
                      showMessage({ Status: "Fail", Message: "No products found." });
                  }
              });
          }
      };
     
      
      $scope.setEditDetails = function (item) {
          $scope.filterProductSelection(item.ProductDetailId);
          $scope.item.WriteoffDetailId = item.WriteoffDetailId;
          $scope.item.Quantity = item.Quantity;
          $scope.item.QuantityActual = item.QuantityActual;
          $scope.item.QuantityLower = item.QuantityLower;
          $scope.item.NetPrice = item.NetPrice;
      };

       function setProductDetail(data) {
          var writeoffId = $scope.item.WriteoffId;
          $scope.productDetail.selected = data;
          $scope.item = data;
          if ($scope.item != undefined) $scope.item.WriteoffId = writeoffId;
          $scope.Measure = data.Measure;
       };

       $scope.calculatePrice = function () {

          var lowerUnitPrice = $scope.item.UnitPrice / ($scope.item.ContainsQty == 0 ? 1 : $scope.item.ContainsQty);
          var unitPrice = $scope.item.UnitPrice * $scope.item.Quantity;
          var actualPrice = lowerUnitPrice * $scope.item.QuantityActual;
          var lowerPrice = (lowerUnitPrice / $scope.item.Volume) * $scope.item.QuantityLower;
          $scope.item.NetPrice = Math.round(unitPrice + actualPrice + lowerPrice);

       };
       $scope.calculateGrossPrice = function () {
           $scope.GrossNetPrice = 0;
           angular.forEach($scope.items, function (item) {
               $scope.GrossNetPrice = $scope.GrossNetPrice + item.NetPrice;
           });
       };

       $scope.filterWriteoff = function (writeoffId) {
           console.log(writeoffId);
          $scope.reset();
          if (writeoffId > 0) $scope.loadItems(writeoffId);
          else {
              $scope.items = [];
              $scope.calculateGrossPrice();
          }
      };
      $scope.reset = function () {
          $scope.item = {};
          $scope.itemz = [];
          $scope.GrossNetPrice = 0;
          $scope.employee.selected = $scope.writeoff.selected != undefined ? $filter('getById')($scope.employees, $scope.writeoff.selected.EmployeeId, "EmployeeId")
              : $filter('getById')($scope.employees, $scope.starter.EmployeeId, "EmployeeId");
          $('#productCode').focus();
      };

      
      $scope.setHeader = function () {
          if ($scope.item == null) $scope.item = {};
          $scope.item.EmployeeId = $scope.employee.selected == null ? null : $scope.employee.selected.EmployeeId;
          if ($scope.writeoff.selected != null) {
              $scope.item.WriteoffId = $scope.writeoff.selected.WriteoffId;
              $scope.item.DateWriteoff = $scope.writeoff.selected.WriteoffDateDisplay;
          }
      };
      $scope.add = function () {
          $scope.setHeader();
          $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {

              showMessage(data);
              if ($scope.item.WriteoffId == undefined) $scope.loadWriteoffs();
              
              $scope.productCodeSelected = undefined;
              $scope.reset();
              $scope.item.WriteoffId = data.WriteoffId != undefined && data.WriteoffId > 0 ? data.WriteoffId : $scope.item.WriteoffId;
              
              if (data.Status != "Failure") $scope.loadItems($scope.item.WriteoffId);
          });

      };
      $scope.edit = function (item) {
          $scope.item = item;
          $scope.filterProduct(item);
      };
      $scope.delete = function (writeoffDetailId) {
          bootbox.confirm("Are you sure want to delete Writeoff Cart Item?", function (result) {
              if (result) {
                  $http.post('/' + cName + '/Delete/', { id: writeoffDetailId }).success(function (data) {
                      showMessage(data);
                      if (data.Status != "Failure") {
                          $scope.loadItems($scope.writeoff.selected.WriteoffId);
                      }
                  });
              }
          });
      };
      $scope.saveHeader = function () {
          $scope.setHeader();
          $http.post('/' + cName + '/EditHeader/', $scope.item).success(function (data) {
              showMessage(data);
          });
                                                                                                                                ``
      };
      $scope.complete = function () {
          $http.post('/' + cName + '/Complete/', { id: $scope.writeoff.selected.WriteoffId }).success(function (data) {
              showMessage(data);
              $scope.reset();
              if (data.Status != "Failure") {
                  $scope.loadWriteoffs();
                  $scope.loadItems();
              }
          });
      };
      $scope.print = function () {
          $http.post('/' + cName + '/Print/', { id: $scope.writeoff.selected.WriteoffId }).success(function (data) {
              showMessage(data);
              $scope.reset();
              if (data.Status != "Failure") {
                  $scope.loadWriteoffs();
                  $scope.loadItems();
              }
          });
      };
  }]);
trControllers.controller(cName + 'ListCtrl', ['$scope', '$http', '$routeParams',
  function ($scope, $http) {
      $scope.loadItems = function () {
          //AppCommonFunction.ShowWaitBlock();
          //$http.get('/' + cName + '/GetWriteoffDetailList/').success(function (data) {
          //    $scope.items = data;
          //    AppCommonFunction.HideWaitBlock();
          //});
          //};
          if (!$.fn.dataTable.isDataTable('#example')) {
              dataTable = $('#example').dataTable({
                  "serverSide": true,
                  "ordering": false,
                  "filter" : false, 
                  "sAjaxSource": "/WriteOff/GetWriteoffDetailList",
                  "fnServerData": function (sSource, aoData, fnCallback) {
                      AppCommonFunction.ShowWaitBlock();
                      $.get(sSource, aoData, function (json) {
                          fnCallback(json);
                      }).always(function () { AppCommonFunction.HideWaitBlock(); });
                  },
                  "columns": [
                      { "data": "WriteoffId" },
                       { "data": "ItemCount" },
                        { "data": "DisplayDate" },
                         { "data": "GrossPrice" },
                      {
                          "render": function (data, type, full) {

                              return "<a class='btn btn-xs btn-warning'' href='#/'><i class='ace-icon fa fa-eye bigger-120'></i></a>" +
                                     "<a class='btn btn-xs btn-danger delete-row' data-id='" + full["WriteoffId"] + "'><i class='ace-icon fa fa-trash-o bigger-120'></i></a>";
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
      //$scope.sortColumn = "WriteoffId";
      //$scope.reverseSort = false;

      //$scope.sortData = function (column) {
      //    $scope.reverseSort = ($scope.sortColumn == column) ? !$scope.reverseSort : false;
      //    $scope.sortColumn = column;

      //}
      //$scope.getSortClass = function (column) {
      //    if ($scope.sortColumn == column) {
      //        return $scope.reverseSort ? 'arrow-down' : 'arrow-up';
      //    }
      //    return "";
      //}

      $scope.delete = function (itemId) {
          bootbox.confirm("Are you sure want to delete Writeoff Detail?", function (result) {
              if (result) {
                  $http.post('/' + cName + '/DeleteWriteoffDetail/', { id: itemId }).success(function (data) {
                      if (data.Status == "Failure") showMessage(data);
                      else dataTable.fnDraw();
                  });
              }
          });
      };


  }]);
