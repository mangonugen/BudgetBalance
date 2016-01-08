define(['appConfig', 'ajaxService'], function (app) {

    app.register.service('accountsService', ['ajaxService', function (ajaxService) {

        this.createAccount = function (user, successFunction, errorFunction) {
            ajaxService.AjaxPostWithNoAuthenication(user, "/api/account/RegisterUser", successFunction, errorFunction);
        };

        this.getAccount = function (successFunction, errorFunction) {
            ajaxService.AjaxGet("/api/account/GetUser", successFunction, errorFunction);
        };        

        this.updateAccount = function (user, successFunction, errorFunction) {
            ajaxService.AjaxPut(user, "/api/account/UpdateUser", successFunction, errorFunction);
        };

        this.deleteAccount = function (successFunction, errorFunction) {
            ajaxService.AjaxGet("/api/account/GetUser", successFunction, errorFunction);
        };
    }]);
});