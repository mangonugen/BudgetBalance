"use strict";

define(['appConfig', 'usersService'], function (app) {

    app.register.controller('loginController', ['$scope', '$rootScope', '$location', 'usersService',
        function ($scope, $rootScope, $location, usersService) {

        $scope.initializeController = function () {
            if ($rootScope.IsloggedIn === undefined) {
                usersService.isUserAuthenicated(function (response) {
                    if (response.IsAuthenticated)
                        $location.path('/');
                    $rootScope.IsloggedIn = response.IsAuthenticated;
                }, $scope.registerUserError);
            } else if ($rootScope.IsloggedIn) {
                $location.path('/');
            }
            console.log('loginController');
        };

        $scope.login = function () {
            var $form = $('#formLogin');
            //$.validator.unobtrusive.parse($form);
            if (!$form.valid()) {
                return false;
            }

            //var config = ajaxService.AjaxConfig('/api' + $('#formLogin').attr('action'), false, "message");
            //config.method = 'POST';
            //config.formId = 'formLogin';
            //config.data = JSON.stringify(ajaxService.SerializeObject($('#formLogin')));
            ////config.contentType = 'application/x-www-form-urlencoded';

            //var result = ajaxService.AjaxRequest(config);

            usersService.login($('#formLogin'), $scope.loginUserCompleted);
        };

        $scope.loginUserCompleted = function (response) {
            if ($rootScope.IsloggedIn === undefined) {
                $rootScope.IsloggedIn = response.IsAuthenticated;
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