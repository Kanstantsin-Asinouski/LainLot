import ApiService from '../ApiService.js';

export default class OrdersService {
  static async GetOrdersCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrdersCount', token);
  }

  static async GetOrdersFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrdersFields', token);
  }

  static async GetOrders(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrders', token, null, { limit, page });
  }

  static async GetOrdersById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrdersById', token, null, { id });
  }

  static async CreateOrders(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateOrders', token, newRecord);
  }

  static async UpdateOrders(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateOrders', token, oldRecord);
  }

  static async DeleteOrders(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteOrders', token, null, { id });
  }
}
