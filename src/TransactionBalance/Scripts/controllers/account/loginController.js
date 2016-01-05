"use strict";

define(['appConfig', 'accountsService', 'ajaxService'], function (app) {

    app.register.controller('loginController', ['$scope', '$rootScope', '$location', 'accountsService', 'ajaxService',
        function ($scope, $rootScope, $location, accountsService, ajaxService) {
        //$rootScope.closeAlert = alertsService.closeAlert;

        $scope.initializeController = function () {
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

            accountsService.login(ajaxService.SerializeObject($('#formLogin')), $scope.loginUserCompleted, $scope.loginUserError);
            console.log('login');
        };

        $scope.loginUserCompleted = function (response) {
            //window.location = "/#/";
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
                console.log(response.ValidationErrors[propertyName]);
            }
        }
    }]);
});