"use strict";

define(['appConfig', 'accountsService', 'ajaxService'], function (app) {

    app.register.controller('registerController', ['$scope', '$rootScope', 'accountsService', 'ajaxService', function ($scope, $rootScope, accountsService, ajaxService) {
        //$rootScope.closeAlert = alertsService.closeAlert;

        $scope.initializeController = function () {
            //$rootScope.applicationModule = "Products";
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

            accountsService.registerUser(ajaxService.SerializeObject($('#formRegister')), $scope.registerUserCompleted, $scope.registerUserError);
            console.log('register');
        };

        $scope.registerUserCompleted = function (response) {
            window.location = "/#/";
        };

        $scope.registerUserError = function (response) {
            var $danger = $('.text-danger');
            if ($danger.hasClass('validation-summary-valid')) {
                $danger.removeClass('validation-summary-valid').addClass('validation-summary-errors');
            }
            var $errorUl = $(".validation-summary-errors ul");
            $errorUl.empty();
            for (var propertyName in response.ValidationErrors) {
                $errorUl.append("<li>" + response.ValidationErrors[propertyName] + "</li>");
                console.log(response.ValidationErrors[propertyName]);
            }
        }
    }]);
});