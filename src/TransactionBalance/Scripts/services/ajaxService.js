
define(['appConfig'], function (app) {

    app.register.service('ajaxService', ['$http', function ($http) {
        
        //$http.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
        $http.defaults.withCredentials = true;

        this.AjaxStatus = {
                ERROR: 0,
                SUCCESS: 1
        };

        this.AjaxConfig = function (url, showDisplay, loadMsg) {
            /// <summary>WebRequest configuration object</summary>
            /// <param name="role" type="Object">The role object</param>    
            var config = {
                url: url,
                contentType: "application/json", //application/x-www-form-urlencoded
                method: "GET",
                formId: "",
                callBack: "",
                showDisplay: (showDisplay === undefined) ? false : showDisplay,
                async: false,
                loadingMsg: (loadMsg === undefined) ? "Retrieving data" : loadMsg,
                data: ""
            }

            return config;
        };

        this.AjaxRequest = function (config) {
            /// <summary>AVZR Ajax function</summary>
            /// <param name="config" type="AVZR.AjaxConfig">The role object</param>
            /// <example>
            /// var config = new AVZR.AjaxConfig();
            /// config.url = url;
            /// config.showDisplay = true;
            /// config.loadingMsg = 'Retrieve User';
            /// config.method = "GET";
            /// var response = AVZR.AjaxRequest(config);
            /// </example>
            var response = {
                Status: 0,
                Data: ""
            };

            if (config.showDisplay === true) {
                $.iOSLoadingScreen(config.loadingMsg);
            }

            if (config.method === "POST" && config.data === "") {
                config.data = $('#' + config.formId).serialize();
            }

            $.ajax({
                url: config.url,
                type: config.method,
                async: config.async,
                cache: false,
                timeout: 30000,
                data: config.data,
                contentType: config.contentType,
                //dataType: 'json',
                beforeSend: function(jqXHR, settings ) {
                },
                success: function (result, textStatus, jqXHR) {
                    response.Status = 1;
                    response.Data = result;
                    if (typeof config.callBack === "function")
                        config.callBack(result);
                
                },
                complete: function (response) {
                    setTimeout(function () {
                        $.rmiOSLoadingScreen();
                    }, 100); //remove loading
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    //response.Status = AjaxStatus.ERROR;
                    console.log(jqXHR.responseText);
                    //alert("Error occurred while processing your request.");
                }
            });
        
            return response;
        }

        this.SerializeObject = function($form)
        {
            var o = {};
            var a = $form.serializeArray();
            $.each(a, function() {
                if (o[this.name] !== undefined) {
                    if (!o[this.name].push) {
                        o[this.name] = [o[this.name]];
                    }
                    o[this.name].push(this.value || '');
                } else {
                    o[this.name] = this.value || '';
                }
            });
            return o;
        };

        this.AddXsfToken = function () {
            var token = $('input[name="__RequestVerificationToken"]').attr('value');
            if (token !== undefined)
                $http.defaults.headers.common['X-XSRF-Token'] = $('input[name="__RequestVerificationToken"]').attr('value');
        }

        this.AjaxPost = function (data, route, successFunction, errorFunction) {
            $.iOSLoadingScreen('Loading');
            this.AddXsfToken();
            
            $http.post(route, data).success(function (response, status, headers, config) {
                $.rmiOSLoadingScreen();
                successFunction(response, status);
            }).error(function (response) {
                $.rmiOSLoadingScreen();
                if (response.IsAuthenicated == false) { window.location = "/#/"; }
                errorFunction(response);
            });
        }

        this.AjaxPostWithNoAuthenication = function (data, route, successFunction, errorFunction) {
            $.iOSLoadingScreen('Loading');
            this.AddXsfToken();
            $http.post(route, data).success(function (response, status, headers, config) {
                $.rmiOSLoadingScreen();
                successFunction(response, status);
            }).error(function (response) {
                $.rmiOSLoadingScreen();
                errorFunction(response);
            });
        }

        this.AjaxPut = function (data, route, successFunction, errorFunction) {
            $.iOSLoadingScreen('Loading');
            this.AddXsfToken();

            $http({ method: 'PUT', data: data, url: route }).success(function (response, status, headers, config) {
                $.rmiOSLoadingScreen();
                successFunction(response, status);
            }).error(function (response) {
                $.rmiOSLoadingScreen();
                if (response.IsAuthenicated == false) { window.location = "/#/"; }
                errorFunction(response);
            });
        }

        this.AjaxGet = function (route, successFunction, errorFunction) {
            $.iOSLoadingScreen('Loading');
            $http({ method: 'GET', url: route }).success(function (response, status, headers, config) {
                $.rmiOSLoadingScreen();
                successFunction(response, status);
            }).error(function (response) {
                $.rmiOSLoadingScreen();
                if (response.IsAuthenicated == false) { window.location = "/#/"; }
                errorFunction(response);
            });
        }

        this.AjaxGetWithData = function (data, route, successFunction, errorFunction) {
            $.iOSLoadingScreen('Loading')
            $http({ method: 'GET', url: route, params: data }).success(function (response, status, headers, config) {
                $.rmiOSLoadingScreen();
                successFunction(response, status);
            }).error(function (response) {
                $.rmiOSLoadingScreen();
                if (response.IsAuthenicated == false) { window.location = "/#/"; }
                errorFunction(response);
            });

        }

        this.AjaxGetWithNoBlock = function (data, route, successFunction, errorFunction) {
            $http({ method: 'GET', url: route, params: data }).success(function (response, status, headers, config) {
                successFunction(response, status);
            }).error(function (response) {;
                if (response.IsAuthenicated == false) { window.location = "/#/"; }
                errorFunction(response);
            });
        }
    }]);
});


