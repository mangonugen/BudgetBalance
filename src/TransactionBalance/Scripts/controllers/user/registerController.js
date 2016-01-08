"use strict";

define(['appConfig', 'usersService'], function (app) {

    app.register.controller('registerController', ['$scope', '$rootScope', 'usersService', function ($scope, $rootScope, usersService) {

        $scope.initializeController = function () {
            if ($rootScope.IsloggedIn === undefined) {
                usersService.isUserAuthenicated(function (response) {
                    if (response.IsAuthenicated)
                        window.location = "/#/";
                    $rootScope.IsloggedIn = response.IsAuthenicated;
                }, $scope.registerUserError);
            } else if ($rootScope.IsloggedIn) {
                window.location = "/#/";
            }
        };

        $scope.register = function () {
            var $form = $('#formRegister');
            //$.validator.unobtrusive.parse($form);            
            if (!$form.valid()) {
                return false;
            }

            //var config = ajaxService.AjaxConfig('/api' + $('#formRegister').attr('action'), false, "message");
            //config.method = 'POST';
            //config.formId = 'formRegister';
            //config.data = JSON.stringify(ajaxService.SerializeObject($('#formRegister')));
            ////config.contentType = 'application/x-www-form-urlencoded';

            //var result = ajaxService.AjaxRequest(config)

            usersService.registerUser($('#formRegister'), $scope.registerUserCompleted, $scope.registerUserError);
            console.log('register');
        };

        $scope.registerUserCompleted = function (response) {
            if ($rootScope.IsloggedIn === undefined) {
                $rootScope.IsloggedIn = response.IsAuthenicated;
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