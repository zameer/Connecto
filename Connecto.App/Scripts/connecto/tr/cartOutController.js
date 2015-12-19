'use strict';
/* Controllers */
var cName = 'CartOut';
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
          AppCommonFunction.ShowWaitBlock();
          $http.get('/Employee/GetAll/').success(function (data) {
              $scope.employees = data;
              if ($scope.employee.selected == null) {
                  $scope.employee.selected = $filter('getById')(data, $scope.starter.EmployeeId, "EmployeeId");
              }
          });
      };
      $scope.invoice = {};
      $scope.loadInvoices = function () {
          $http.get('/' + cName + '/GetInvoices/').success(function (data) {
              $scope.invoices = data;
              $scope.invoice.selected = $scope.item != undefined ? $filter('getById')($scope.invoices, $scope.item.InvoiceId, "InvoiceId") : null;
          });
      };
      $scope.customer = {};
      $scope.loadCustomers = function () {
          $http.get('/Customer/GetAll/').success(function (data) {
              $scope.customers = data;
          });
      };

      $scope.loadItems = function (invoiceId) {
          if (invoiceId != undefined) {
              $http.get('/' + cName + '/GetCart/' + invoiceId).success(function (data) {
                  $scope.items = data;
                  $scope.calculateGrossPrice();
              });
          } else  $scope.items = [];
      };
      $scope.loadEmployees();
      $scope.loadInvoices();
      $scope.loadCustomers();
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
              $http.get('/' + cName + '/GetSalesDetail/?productCode=' + $scope.item.ProductCode).success(function (data) {
                  if (data.length > 0) {
                      $scope.itemz = data;
                      if (item) {
                          $scope.setEditDetails(item);
                      }
                      else {
                          setProductDetail(data[0]);
                          if ($scope.item.AutoSelling) {
                              $scope.item.Quantity = $scope.item.AutoSellingQty;
                              $scope.calculatePrice();
                          }
                          $scope.decideSellingPrice();
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
          $scope.item.SalesDetailId = item.SalesDetailId;
          $scope.item.Quantity = item.Quantity;
          $scope.item.QuantityActual = item.QuantityActual;
          $scope.item.QuantityLower = item.QuantityLower;
          $scope.item.SellingPrice = item.SellingPrice;
          $scope.item.Discount = item.Discount;
          $scope.item.Price = item.Price;
          $scope.DiscountBy = discountBy(item.DiscountBy);
          $scope.item.DiscountBy = $scope.DiscountBy;
          $scope.item.Discount = item.Discount;
          $scope.item.DiscountAs = item.DiscountAs;
          $scope.item.NetPrice = item.NetPrice;
      };

      function discountBy(enumber) {
          if (enumber == 1) return 'Rate';
          else if (enumber == 2) return 'Amount';
          else return '';
      }
      $scope.calculateBalance = function () { $scope.Balance = ($scope.Paid - $scope.GrossNetPrice + ($scope.Fluctuation * 1)); };
      function setProductDetail(data) {
          var invoiceId = $scope.item.InvoiceId;
          $scope.productDetail.selected = data;
          $scope.calculateAveragePrice();
          $scope.item = data;
          if ($scope.item != undefined) $scope.item.InvoiceId = invoiceId;
          $scope.Measure = data.Measure;
      };

      $scope.calculatePrice = function () {
          $scope.decideSellingPrice();
          var lowerUnitPrice = $scope.item.SellingPrice / ($scope.item.ContainsQty == 0 ? 1 : $scope.item.ContainsQty);
          var unitPrice = $scope.item.SellingPrice * $scope.item.Quantity;
          var actualPrice = lowerUnitPrice * $scope.item.QuantityActual;
          var lowerPrice = (lowerUnitPrice / $scope.item.Volume) * $scope.item.QuantityLower;
          $scope.item.Price = Math.round(unitPrice + actualPrice + lowerPrice);
          $scope.calculateDiscount();
      };

      $scope.decideSellingPrice = function () {
          if ($scope.item.SellingMargin && $scope.item.Quantity == 0) {
              if ($scope.item.SellingPrice == $scope.item.SellingPriceActual) $scope.item.SellingPrice += $scope.item.MarginAmount;
          }
      };
      $scope.filterInvoice = function (invoiceId) {
          $scope.reset();
          if (invoiceId > 0) $scope.loadItems(invoiceId);
          else {
              $scope.items = [];
              $scope.calculateGrossPrice();
          } 
      };
      $scope.reset = function () {
          $scope.item = {};
          $scope.itemz = [];
          $scope.Paid = 0;
          $scope.Balance = 0;
          $scope.GrossNetPrice = 0;
          $scope.customer.selected = $scope.invoice.selected != undefined ? $scope.invoice.selected.Customer : null;
          $scope.employee.selected = $scope.invoice.selected != undefined ? $filter('getById')($scope.employees, $scope.invoice.selected.EmployeeId, "EmployeeId")
              : $filter('getById')($scope.employees, $scope.starter.EmployeeId, "EmployeeId");
          $('#productCode').focus();
      };

      $scope.DiscountBy = 'None';
      $scope.setDiscount = function (discountBy)
      {
          $scope.item.DiscountBy = discountBy;
          $scope.DiscountBy = discountBy;
          $scope.calculateDiscount();
      };
      $scope.calculateDiscount = function () {
          if ($scope.item.DiscountAs == undefined) $scope.item.DiscountAs = 0;
          if ($scope.DiscountBy == 'None') {
              $scope.item.DiscountAs = 0;
              $scope.item.Discount = 0;
          }
          if ($scope.DiscountBy == "Amount")
              $scope.item.Discount = Math.round($scope.item.DiscountAs);
          if ($scope.DiscountBy == "Rate") {
              $scope.item.DiscountBy = $scope.DiscountBy;
              $scope.item.Discount = Math.round(($scope.item.DiscountAs / 100) * $scope.item.Price);
          }
          $scope.calculateNetPrice();
      };
      $scope.calculateNetPrice = function () {
          $scope.item.NetPrice = $scope.item.Price - ($scope.item.Discount != undefined ? $scope.item.Discount : 0);
          if ($scope.item.SalesDetailId == 0 && $scope.item.AutoSelling) $scope.add();
      };
      
      $scope.calculateGrossPrice = function () {
          $scope.GrossNetPrice = 0; $scope.GrossPrice = 0; $scope.GrossDiscount = 0;
          $scope.Paid = 0; $scope.Balance = 0; $scope.Fluctuation = 0;
          angular.forEach($scope.items, function (item) {
              $scope.GrossNetPrice = $scope.GrossNetPrice + item.NetPrice;
              $scope.GrossPrice = $scope.GrossPrice + item.Price;
              $scope.GrossDiscount = $scope.GrossDiscount + item.Discount;
          });
      };
      $scope.calculateAveragePrice = function () {
          var priceSum = 0;
          angular.forEach($scope.itemz, function (item) { priceSum = priceSum + item.SellingPriceActual;});
          $scope.AveragePrice = (priceSum / $scope.itemz.length).toFixed(2);
      };
      $scope.setHeader = function() {
          if ($scope.item == null) $scope.item = {};
          $scope.item.CustomerId = $scope.customer.selected == null ? null : $scope.customer.selected.CustomerId;
          $scope.item.EmployeeId = $scope.employee.selected == null ? null : $scope.employee.selected.EmployeeId;
          if ($scope.invoice.selected != null) {
              $scope.item.InvoiceId = $scope.invoice.selected.InvoiceId;
              $scope.item.ReferenceCode = $scope.invoice.selected.ReferenceCode;
              $scope.item.DateSold = $scope.invoice.selected.InvoiceDateDisplay;
          }
      };
      $scope.add = function () {
          $scope.setHeader();
          $http.post('/' + cName + '/Create/', $scope.item).success(function (data) {
              showMessage(data);
              if ($scope.item.InvoiceId == undefined) $scope.loadInvoices();

              $scope.productCodeSelected = undefined;
              $scope.reset();
              $scope.item.InvoiceId = data.InvoiceId != undefined && data.InvoiceId > 0 ? data.InvoiceId : $scope.item.InvoiceId;

              if (data.Status != "Failure") $scope.loadItems($scope.item.InvoiceId);
          });
          
      };
      $scope.edit = function (item) {
          $scope.item = item;
          $scope.filterProduct(item);
      };
      $scope.delete = function (salesDetailId) {
          $http.post('/' + cName + '/Delete/', { id: salesDetailId }).success(function (data) {
              showMessage(data);
              if (data.Status != "Failure") {
                  $scope.loadItems($scope.invoice.selected.InvoiceId);
              }
          });
      };
      $scope.saveHeader = function () {
          $scope.setHeader();
          $http.post('/' + cName + '/EditHeader/', $scope.item).success(function (data) {
              showMessage(data);
          });
      };
      $scope.complete = function () {
          $http.post('/' + cName + '/Complete/', { id: $scope.invoice.selected.InvoiceId, fluctuation: $scope.Fluctuation }).success(function (data) {
              showMessage(data);
              $scope.reset();
              if (data.Status != "Failure") {
                  $scope.loadInvoices();
                  $scope.loadItems();
              }
          });
      };
      $scope.print = function () {
          $http.post('/' + cName + '/Print/', { id: $scope.invoice.selected.InvoiceId, fluctuation: $scope.Fluctuation, paid: $scope.Paid, balance: $scope.Balance }).success(function (data) {
              showMessage(data);
              $scope.reset();
              if (data.Status != "Failure") {
                  $scope.loadInvoices();
                  $scope.loadItems();
              }
          });
      };
  }]);
