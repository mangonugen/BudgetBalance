"use strict";

define(['appConfig'], function (app) {

    app.register.controller('accountIndexController', ['$scope', '$rootScope', function ($scope, $rootScope) {

        $scope.initializeController = function () {
            $rootScope.applicationModule = "Products";
            console.log('accountIndexController');
        }
    }]);
});