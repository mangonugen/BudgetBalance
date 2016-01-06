"use strict";

define(['appConfig', 'accountsService'], function (app) {

    app.register.controller('registerController', ['$scope', '$rootScope', 'accountsService', function ($scope, $rootScope, accountsService) {

        $scope.initializeController = function () {
            if ($rootScope.isAuthenicated === undefined) {
                accountsService.isUserAuthenicated(function (response) {
                    if (response.IsAuthenicated)
                        window.location = "/#/";
                    $rootScope.isAuthenicated = response.IsAuthenicated;
                }, $scope.registerUserError);
            } else if ($rootScope.isAuthenicated) {
                window.location = "/#/";
            }
        };

        $scope.register = function () {
            var $form = $('#formRegister');
            $.validator.unobtrusive.parse($form);            
            if (!$form.valid()) {
                return false;
            }

            //var config = ajaxService.AjaxConfig('/api' + $('#formRegister').attr('action'), false, "message");
            //config.method = 'POST';
            //config.formId = 'formRegister';
            //config.data = JSON.stringify(ajaxService.SerializeObject($('#formRegister')));
            ////config.contentType = 'application/x-www-form-urlencoded';

            //var result = ajaxService.AjaxRequest(config)

            accountsService.registerUser($('#formRegister'), $scope.registerUserCompleted, $scope.registerUserError);
            console.log('register');
        };

        $scope.registerUserCompleted = function (response) {
            if ($rootScope.isAuthenicated === undefined) {
                $rootScope.isAuthenticated = response.IsAuthenicated;
            }
            window.location = "/#/";
        };

        $scope.registerUserError = function (response) {
            var $danger = $('.text-danger');
            if ($danger.hasClass('validation-summary-valid')) {
                $danger.removeClass('validation-summary-valid').addClass('validation-summary-errors');
            }
            var $errorUl = $(".validation-summary-errors ul");
            $errorUl.empty();
            $.each(response.ValidationErrors, function (key, value) {
                $errorUl.append("<li>" + value + "</li>");
            });
            $.each(response.ReturnMessage, function (index, value) {
                $errorUl.append("<li>" + value + "</li>");
            });
        }
    }]);
});