import ApiService from '../ApiService.js';

export default class CustomSleeveCuffsService {
  static async GetCustomSleeveCuffsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSleeveCuffsCount', token);
  }

  static async GetCustomSleeveCuffsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSleeveCuffsFields', token);
  }

  static async GetCustomSleeveCuffs(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSleeveCuffs', token, null, { limit, page });
  }

  static async GetCustomSleeveCuffsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSleeveCuffsById', token, null, { id });
  }

  static async CreateCustomSleeveCuffs(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateCustomSleeveCuffs', token, newRecord);
  }

  static async UpdateCustomSleeveCuffs(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateCustomSleeveCuffs', token, oldRecord);
  }

  static async DeleteCustomSleeveCuffs(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteCustomSleeveCuffs', token, null, { id });
  }
}
