define(['appConfig', 'ajaxService'], function (app) {

    app.register.service('accountsService', ['ajaxService', function (ajaxService) {

        this.registerUser = function (user, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication(user, "/api/account/RegisterUser", successFunction, errorFunction);
        };

        this.login = function (user, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication(user, "/api/account/Login", successFunction, errorFunction);
        };

        this.getUser = function (successFunction, errorFunction) {
            ajaxService.AjaxGet("/api/account/GetUser", successFunction, errorFunction);
        };        

        this.updateUser = function (user, successFunction, errorFunction) {
            ajaxService.AjaxPut(user, "/api/account/UpdateUser", successFunction, errorFunction);
        };

    }]);
});