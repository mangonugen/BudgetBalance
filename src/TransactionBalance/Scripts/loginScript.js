var app = angular.module("avzrApp", []);

app.controller("loginCtrl", ['$scope', '$http', function ($scope, $http) {
    console.log('Ctrl')

    $scope.login = function() {
        $http.post($('#formLogin').attr('action'), $scope.serializeObject($('#formLogin')).success(function (response, status, headers, config) {
            $.rmiOSLoadingScreen();
            console.log('success');
        }).error(function (response) {
            $.rmiOSLoadingScreen();
            console.log('error');
        });
    };

    $scope.serializeObject = function ($form) {
        var o = {};
        var a = $form.serializeArray();
        $.each(a, function () {
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
}]);