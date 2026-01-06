import ApiService from '../ApiService.js';

export default class UserOrderHistoryService {
  static async GetUserOrderHistoryCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserOrderHistoryCount', token);
  }

  static async GetUserOrderHistoryFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserOrderHistoryFields', token);
  }

  static async GetUserOrderHistory(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserOrderHistory', token, null, { limit, page });
  }

  static async GetUserOrderHistoryById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserOrderHistoryById', token, null, { id });
  }

  static async CreateUserOrderHistory(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateUserOrderHistory', token, newRecord);
  }

  static async UpdateUserOrderHistory(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateUserOrderHistory', token, oldRecord);
  }

  static async DeleteUserOrderHistory(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteUserOrderHistory', token, null, { id });
  }
}
