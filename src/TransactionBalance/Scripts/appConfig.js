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

        // route for the promotion page
        .when('/account/register', {
            templateUrl: 'account/register',
            controller: 'promotionCtrler'
        })
            
        .otherwise({ 
            redirectTo: '/' 
        });
    }]);

    var indexController = function ($scope, $rootScope, $http, $location) {
             
        $scope.$on('$routeChangeStart', function (scope, next, current) {
             
            if ($rootScope.IsloggedIn==true)
            {               
                //$scope.authenicateUser($location.path(),$scope.authenicateUserComplete, $scope.authenicateUserError);
            }         
        });

        $scope.$on('$routeChangeSuccess', function (scope, next, current) {
         
            setTimeout(function () {
                //if ($scope.isCollapsed == true) {                   
                //    set95PercentWidth();
                //}              
            }, 1000);
         

        });

        $scope.initializeController = function () {
            $rootScope.displayContent = false;
            if ($location.path() != "") {
                //$scope.initializeApplication($scope.initializeApplicationComplete, $scope.initializeApplicationError);
            }

            console.log('init');
        }

        $scope.initializeApplication = function (successFunction, errorFunction) {
            //blockUI.start();
            //$scope.AjaxGet("/api/main/InitializeApplication", successFunction, errorFunction);
            //blockUI.stop();
            console.log('initApp')            
        };
    };

    indexController.$inject = ['$scope', '$rootScope', '$http', '$location'];
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

        $('.dropdown-submenu > a').submenupicker();
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