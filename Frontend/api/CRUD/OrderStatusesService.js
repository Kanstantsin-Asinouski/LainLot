import ApiService from '../ApiService.js';

export default class OrderStatusesService {
  static async GetOrderStatusesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrderStatusesCount', token);
  }

  static async GetOrderStatusesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrderStatusesFields', token);
  }

  static async GetOrderStatuses(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrderStatuses', token, null, { limit, page });
  }

  static async GetOrderStatusesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrderStatusesById', token, null, { id });
  }

  static async CreateOrderStatuses(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateOrderStatuses', token, newRecord);
  }

  static async UpdateOrderStatuses(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateOrderStatuses', token, oldRecord);
  }

  static async DeleteOrderStatuses(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteOrderStatuses', token, null, { id });
  }
}
