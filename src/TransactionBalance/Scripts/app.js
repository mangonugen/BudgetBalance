//asp.net mvc amd javascript
//http://volaresystems.com/blog/post/2014/05/27/Adding-RequireJS-to-an-ASPNET-MVC-project
//http://www.codeproject.com/Tips/860859/ASP-NET-MVC-RequireJS-Module-Optimization
//http://therealmofcode.com/posts/2014/03/aspnet-mvc-bundling-minification-requirejs.html
"use strict";

require(["jquery", "jquery.bootstrap", "jquery.mmenu", "modernizr"], function ($) {
    $(function () {
        //$('#menu').mmenu({
        //    classes: "mm-light",
        //    //zposition : "front",
        //    offCanvas: {
        //        //position  : "right",
        //        //zposition: "front",
        //        pageNodetype: "section"
        //    },
        //    navbar: {
        //        title: "Transaction"
        //    },
        //    navbars: [{
        //        content: ["searchfield"]
        //    }, {
        //        content: ["prev", "title"]
        //    }],
        //    extensions: ["pageshadow", "effect-menu-slide"]
        //    //searchfield: true
        //});

        //	The menu
        $('#menu').mmenu({
            extensions: ['theme-light', 'effect-menu-slide', 'pageshadow'],
            counters: true,
            dividers: {
                fixed: true
            },
            navbar: {
                title: 'Transaction'
            },
            navbars: [{
                position: 'top',
                content: ['searchfield']
            }, {
                position: 'top'
            }, {
                position: 'bottom',
                content: ['<div>Free</div>']
            }]
        });
    });
});