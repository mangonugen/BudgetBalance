"use strict";

define(['angularAMD', 'angular', 'ui-bootstrap', "jquery", 'jqueryAddon', 'jquery.bootstrap', "jquery.menu", "modernizr", 'respond'], function (angularAMD) {
    var app = angular.module("mainModule", ['ngRoute', 'ngSanitize', 'ui.bootstrap']);

    app.config(function ($httpProvider) {
        $httpProvider.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';
        $httpProvider.defaults.withCredentials = true;
    });

    app.config(['$routeProvider', function ($routeProvider) {
   
        $routeProvider
        .when("/", angularAMD.route({
            templateUrl: function (rp) {
                //return 'Views/Main/default.html';
                return '/home/default';
            },
            controllerUrl: "controllers/home/defaultController"
        }))

        .when("/:section/:tree", angularAMD.route({

            templateUrl: function (rp) {
                //return 'views/' + rp.section + '/' + rp.tree + '.html';
                return '/' + rp.section + '/' + rp.tree;
            },

            resolve: {

                load: ['$q', '$rootScope', '$location', function ($q, $rootScope, $location) {

                    var path = $location.path();
                    var parsePath = path.split("/");
                    var parentPath = parsePath[1];
                    var controllerName = parsePath[2];

                    //var loadController = "Views/" + parentPath + "/" + controllerName + "Controller";
                    var loadController = "controllers/" + parentPath + "/" + controllerName + "Controller";

                    var deferred = $q.defer();
                    require([loadController], function () {
                        $rootScope.$apply(function () {
                            deferred.resolve();
                        });
                    });
                    return deferred.promise;
                }]
            }
        }))
            
        .when("/:section/:tree/:id", angularAMD.route({

            templateUrl: function (rp) {
                return 'views/' + rp.section + '/' + rp.tree + '.html';
            },

            resolve: {

                load: ['$q', '$rootScope', '$location', function ($q, $rootScope, $location) {

                    var path = $location.path();
                    var parsePath = path.split("/");
                    var parentPath = parsePath[1];
                    var controllerName = parsePath[2];

                    //var loadController = "Views/" + parentPath + "/" + controllerName + "Controller";
                    var loadController = "controllers/" + parentPath + "/" + controllerName + "Controller";

                    var deferred = $q.defer();
                    require([loadController], function () {
                        $rootScope.$apply(function () {
                            deferred.resolve();
                        });
                    });
                    return deferred.promise;
                }]
            }
        }))

        .otherwise({ 
            redirectTo: '/' 
        });
    }]);

    var indexController = function ($scope, $rootScope, $http, $location, $browser) {

        $scope.$on('$locationChangeStart', function (event) {
            console.log($location.path());
            //if ($scope.form.$invalid) {
            //    event.preventDefault();
            //}
        });
             
        $scope.$on('$routeChangeStart', function (scope, next, current) {
            $.iOSLoadingScreen('Loading');
            console.log('$routeChangeStart');
            if ($rootScope.IsloggedIn === true)
            {               
                $scope.authenicateUser($location.path(),$scope.authenicateUserComplete, function() {
                    console.log('Authenicate error');
                });
            }         
        });

        $scope.$on('$routeChangeSuccess', function (scope, next, current) {
         
            setTimeout(function () {
                //if ($scope.isCollapsed == true) {                   
                //    set95PercentWidth();
                //}              
            }, 1000);
        });

        $scope.$on('$viewContentLoaded', function () {
            setTimeout(function () {
                //remove loading screen
                if ($('.ui-ios-overlay').hasClass('ios-overlay-show'))
                    $.rmiOSLoadingScreen();
                //collapse menu if open
                if ($(".navbar-collapse").hasClass("collapse in"))
                    $('.navbar-toggle').click();

                $("input[type=button]").each($scope.setUnobtrusive);
            }, 10);
        });

        $scope.setUnobtrusive = function () {
            $.validator.unobtrusive.parse($(this).closest('form'));
        }

        $scope.initializeController = function() {
            $rootScope.displayContent = false;
            if ($location.path() != "") {
                $scope.initializeApplication($scope.initializeApplicationComplete, $scope.initializeApplicationError);
            }
        };

        $scope.initializeApplication = function (successFunction, errorFunction) {
            $scope.AjaxGet("/api/user/IsUserAuthenicated", successFunction, errorFunction);
        };

        $scope.initializeApplicationComplete = function(response) {
            $rootScope.IsloggedIn = response.IsAuthenticated;
            if (!$rootScope.IsloggedIn) {
                $location.path('/User/Login');
            }
        };

        $scope.authenicateUser = function (route, successFunction, errorFunction) {
            //var authenication = new Object();
            //authenication.route = route;
            //$scope.AjaxGet(authenication, "/api/user/IsUserAuthenicated", successFunction, errorFunction);
            $scope.AjaxGet("/api/user/IsUserAuthenicated", successFunction, errorFunction);
        };

        $scope.authenicateUserComplete = function (response) {
            if (response.IsAuthenticated == false)
                $location.path('/');
        }



        $browser.onUrlChange(function (newUrl, newState) {
            $rootScope.$evalAsync(function () {
                var oldUrl = $location.absUrl();
                var oldState = $location.$$state;

                $location.$$parse(newUrl);
                $location.$$state = newState;
                if ($rootScope.$broadcast('$locationChangeStart', newUrl, oldUrl,
                    newState, oldState).defaultPrevented) {
                    $location.$$parse(oldUrl);
                    $location.$$state = oldState;
                    //setBrowserUrlWithFallback(oldUrl, false, oldState);
                } else {
                    //initializing = false;
                    //afterLocationChange(oldUrl, oldState);
                }
            });
            if (!$rootScope.$$phase) $rootScope.$digest();
        });

        $scope.AjaxGet = function (route, successFunction, errorFunction) {
            $.iOSLoadingScreen('Loading');
            console.log('$scope.AjaxGet');
            $http({ method: 'GET', url: route }).success(function (response, status, headers, config) {
                $.rmiOSLoadingScreen();
                successFunction(response, status);
            }).error(function (response) {
                $.rmiOSLoadingScreen();
                errorFunction(response);
            });

        }

        //$scope.AjaxGetWithData = function (data, route, successFunction, errorFunction) {
        //    $.iOSLoadingScreen('Loading');
        //    $http({ method: 'GET', url: route, params: data }).success(function (response, status, headers, config) {
        //        $.rmiOSLoadingScreen();
        //        successFunction(response, status);
        //    }).error(function (response) {
        //        $.rmiOSLoadingScreen();
        //        errorFunction(response);
        //    });

        //}
    };

    indexController.$inject = ['$scope', '$rootScope', '$http', '$location', '$browser'];
    app.controller("indexController", indexController);
  
    // Bootstrap Angular when DOM is ready
    angularAMD.bootstrap(app);
    
    $(function () {
        //	The menu
        //$('#menu').show().mmenu({
        //    extensions: ['theme-light', 'effect-menu-slide', 'pageshadow'],
        //    counters: true,
        //    dividers: {
        //        fixed: true
        //    },
        //    navbar: {
        //        title: 'Transaction'
        //    },
        //    navbars: [{
        //        position: 'top',
        //        content: ['searchfield']
        //    }, {
        //        position: 'top'
        //    }, {
        //        position: 'bottom',
        //        content: ['<div>Free</div>']
        //    }]
        //});

        $("input[type='submit']").click(iOSLoading);
        $("button[type='submit']").click(iOSLoading);

        $("input[type=button]").each(validateForm);

        //$('.dropdown-submenu > a').submenupicker();
        $('[data-submenu]').submenupicker();
    });

    function iOSLoading() {
        var $button = $(this),
        $form = $button.closest('form'),
        loadMsg = $button.attr('data-loadMessage');

        //validate form before ajax submit
        var formValid = $form.validate().form();
        if (!formValid) return false;

        $button.focus(); //focus to button
        $button.blur(); //remove focus to button to prevent double enter

        if (loadMsg === undefined)
            loadMsg = 'Loading';

        $.iOSLoadingScreen(loadMsg);
    }

    function validateForm() {
        // set button
        var $btn = $(this),
        $form = $btn.closest('form');

        var clickhandler = this.onclick;
        this.onclick = null;


        // before click handler hide labelify
        //btn.click(get from attribute)
        $btn.click(function () {
            //validate form before ajax submit
            var formValid = $form.validate().form();
            if (!formValid) { return false; }
            else { $btn.click(clickhandler); } //handler set by user 
        });        
    }

    return app;
});