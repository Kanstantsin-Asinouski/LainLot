import ApiService from '../ApiService.js';

export default class OrderHistoryService {
  static async GetOrderHistoryCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrderHistoryCount', token);
  }

  static async GetOrderHistoryFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrderHistoryFields', token);
  }

  static async GetOrderHistory(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrderHistory', token, null, { limit, page });
  }

  static async GetOrderHistoryById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetOrderHistoryById', token, null, { id });
  }

  static async CreateOrderHistory(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateOrderHistory', token, newRecord);
  }

  static async UpdateOrderHistory(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateOrderHistory', token, oldRecord);
  }

  static async DeleteOrderHistory(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteOrderHistory', token, null, { id });
  }
}
