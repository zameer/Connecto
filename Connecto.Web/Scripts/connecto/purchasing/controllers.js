'use strict';

/* Controllers */

var phonecatApp = angular.module('phonecatApp', []);

phonecatApp.controller('PhoneListCtrl', function ($scope, $http) {
    $http.get('/Product/Vendors/').success(function (data) {
        $scope.vendors = data;
    });
    $http.get('/Product/ProductTypes/').success(function (data) {
        console.log(data);
        $scope.productTypes = data;
    });
    $scope.addProduct = function () {
        $http.post('/Product/Create/', $scope.Product).success(function (data) {
            $scope.Product = {};
            $(".select2").select2('val', '');
        });
    };
});
