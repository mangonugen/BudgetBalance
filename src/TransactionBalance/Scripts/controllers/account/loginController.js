"use strict";

define(['appConfig', 'accountsService'], function (app) {

    app.register.controller('loginController', ['$scope', '$rootScope', '$location', 'accountsService',
        function ($scope, $rootScope, $location, accountsService) {

        $scope.initializeController = function () {
            if ($rootScope.isAuthenicated === undefined) {
                accountsService.isUserAuthenicated(function (response) {
                    if (response.IsAuthenicated)
                        $location.path('/');
                    $rootScope.isAuthenicated = response.IsAuthenicated;
                }, $scope.registerUserError);
            } else if ($rootScope.isAuthenicated) {
                $location.path('/');
            }
        };

        $scope.login = function () {
            var $form = $('#formLogin');
            $.validator.unobtrusive.parse($form);
            if (!$form.valid()) {
                return false;
            }

            //var config = ajaxService.AjaxConfig('/api' + $('#formLogin').attr('action'), false, "message");
            //config.method = 'POST';
            //config.formId = 'formLogin';
            //config.data = JSON.stringify(ajaxService.SerializeObject($('#formLogin')));
            ////config.contentType = 'application/x-www-form-urlencoded';

            //var result = ajaxService.AjaxRequest(config);

            accountsService.login($('#formLogin'), $scope.loginUserCompleted, $scope.loginUserError);
            console.log('login');
        };

        $scope.loginUserCompleted = function (response) {
            if ($rootScope.isAuthenicated === undefined) {
                $rootScope.isAuthenticated = response.IsAuthenicated;
            }
            $location.path('/');
        };

        $scope.loginUserError = function (response) {
            var $danger = $('.text-danger');
            if ($danger.hasClass('validation-summary-valid')) {
                $danger.removeClass('validation-summary-valid').addClass('validation-summary-errors');
            }
            var $errorUl = $(".validation-summary-errors ul");
            $errorUl.empty();
            for (var propertyName in response.ValidationErrors) {
                $errorUl.append("<li>" + response.ValidationErrors[propertyName] + "</li>");
            }

            $.each(response.ReturnMessage, function (index, value) {
                $errorUl.append("<li>" + value + "</li>");
            });
        }
    }]);
});