//define(['application-configuration', 'mainService', 'alertsService'], function (app) {

//    app.register.controller('defaultController', ['$scope',  '$rootScope', 'mainService', 'alertsService', function ($scope,  $rootScope, mainService, alertsService) {
define(['appConfig', 'usersService'], function (app) {

    app.register.controller('defaultController', ['$scope', '$rootScope', 'usersService', function ($scope, $rootScope, usersService) {
        //$rootScope.closeAlert = alertsService.closeAlert;

        $scope.initializeController = function () {
            usersService.isUserAuthenicated($scope.initializeApplicationComplete, $scope.initializeApplicationError);
            console.log('default');
        }

        $scope.initializeApplicationComplete = function (response)
        {     
            if (response.IsAuthenticated === false) {
                // set timeout needed to prevent AngularJS from raising a digest error
                setTimeout(function () {
                    if ($('#home').children().length > 0)
                        location.reload();
                    window.location = "#/User/Login";
                }, 10);
            }
            else if (!$('.navbar-collapse ul').hasClass('isLogin')) {
                if ($rootScope.IsloggedIn === undefined)
                    $rootScope.IsloggedIn = response.IsAuthenticated;
                location.reload();
            }
        }

        $scope.initializeApplicationError = function (response)
        {         
            //alertsService.RenderErrorMessage(response.ReturnMessage);
            console.log(JSON.stringify(response));
        }
    }]);
});