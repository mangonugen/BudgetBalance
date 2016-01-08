"use strict";

define(['appConfig', 'usersService'], function (app) {

    app.register.controller('logoutController', ['$scope', '$rootScope', '$location', 'usersService',
        function ($scope, $rootScope, $location, usersService) {

        $scope.initializeController = function () {
            usersService.logout(function(response) {
                $rootScope.IsloggedIn = response.IsAuthenticated;
                $location.path('/');
            }, function(response) {
                //todo display popup directive
            });
        };
    }]);
});