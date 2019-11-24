"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../environments/environment");
var OrderService = /** @class */ (function () {
    function OrderService(http) {
        this.http = http;
    }
    OrderService.prototype.getOrdersById = function (id) {
        return this.http.get(environment_1.environment.apiUrl + "/api/order/GetOrders" + id);
    };
    return OrderService;
}());
exports.OrderService = OrderService;
//# sourceMappingURL=order.service.js.map