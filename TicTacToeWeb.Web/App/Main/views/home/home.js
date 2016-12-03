(function () {
    var controllerId = 'app.views.home';
    angular.module('app').controller(controllerId, [
        '$scope', function ($scope) {
            var vm = this;
            //Home logic...
            vm.button = {};
            vm.buttons = [vm.button];
            vm.click = function (number, state) {
            };
            vm.initialize = function () {
                for (var i = 0; i < 8; i++) {
                    vm.button.number = i;
                    vm.button.state = "";
                    vm.buttons.add(vm.button);
                }
            };
        }
    ]);
})();
//# sourceMappingURL=home.js.map