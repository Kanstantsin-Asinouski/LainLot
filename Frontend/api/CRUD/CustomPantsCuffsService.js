import ApiService from '../ApiService.js';

export default class CustomPantsCuffsService {
  static async GetCustomPantsCuffsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomPantsCuffsCount', token);
  }

  static async GetCustomPantsCuffsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomPantsCuffsFields', token);
  }

  static async GetCustomPantsCuffs(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomPantsCuffs', token, null, { limit, page });
  }

  static async GetCustomPantsCuffsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomPantsCuffsById', token, null, { id });
  }

  static async CreateCustomPantsCuffs(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateCustomPantsCuffs', token, newRecord);
  }

  static async UpdateCustomPantsCuffs(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateCustomPantsCuffs', token, oldRecord);
  }

  static async DeleteCustomPantsCuffs(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteCustomPantsCuffs', token, null, { id });
  }
}
