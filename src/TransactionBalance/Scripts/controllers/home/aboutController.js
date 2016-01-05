"use strict";

define(['appConfig'], function (app) {

    app.register.controller('aboutController', ['$scope', '$rootScope', function ($scope, $rootScope) {
        $rootScope.closeAlert = alertsService.closeAlert;

        $scope.initializeController = function () {
            $rootScope.applicationModule = "Products";
        }
    }]);
});