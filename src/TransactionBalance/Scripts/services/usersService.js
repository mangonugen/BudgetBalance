define(['appConfig', 'ajaxService'], function (app) {

    app.register.service('usersService', ['ajaxService', function (ajaxService) {

        this.registerUser = function ($user, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication($user, "/api/user/RegisterUser", successFunction, errorFunction);
        };

        this.login = function ($user, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication($user, "/api/user/Login", successFunction, errorFunction);
        };

        this.getUser = function (successFunction, errorFunction) {
            ajaxService.AjaxGet("/api/user/GetUser", successFunction, errorFunction);
        };        

        this.updateUser = function ($user, successFunction, errorFunction) {
            ajaxService.AjaxPut($user, "/api/user/UpdateUser", successFunction, errorFunction);
        };

        this.isUserAuthenicated = function (successFunction, errorFunction) {
            ajaxService.AjaxGet("/api/user/IsUserAuthenicated", successFunction, errorFunction);
        };

        this.logout = function (successFunction, errorFunction) {
            ajaxService.AjaxGet("/api/user/Logout", successFunction, errorFunction);
        };
    }]);
});