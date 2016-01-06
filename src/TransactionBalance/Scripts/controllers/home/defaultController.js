//define(['application-configuration', 'mainService', 'alertsService'], function (app) {

//    app.register.controller('defaultController', ['$scope',  '$rootScope', 'mainService', 'alertsService', function ($scope,  $rootScope, mainService, alertsService) {
define(['appConfig', 'accountsService'], function (app) {

    app.register.controller('defaultController', ['$scope', '$rootScope', 'accountsService', function ($scope, $rootScope, accountsService) {
        //$rootScope.closeAlert = alertsService.closeAlert;

        $scope.initializeController = function () {
            accountsService.isUserAuthenicated($scope.initializeApplicationComplete, $scope.initializeApplicationError);
        }

        $scope.initializeApplicationComplete = function (response)
        {     
            if (response.IsAuthenicated === false) {
                // set timeout needed to prevent AngularJS from raising a digest error
                setTimeout(function () {
                    window.location = "#/Account/Login";
                }, 10);
            }
            else if ($('#home').children().length === 0) {
                if ($rootScope.isAuthenicated === undefined)
                    $rootScope.isAuthenicated = response.IsAuthenicated;
                location.reload();
            }
        }

        $scope.initializeApplicationError = function (response)
        {         
            //alertsService.RenderErrorMessage(response.ReturnMessage);
        }
    }]);
});