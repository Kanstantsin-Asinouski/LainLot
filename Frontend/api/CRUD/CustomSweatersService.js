import ApiService from '../ApiService.js';

export default class CustomSweatersService {
  static async GetCustomSweatersCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSweatersCount', token);
  }

  static async GetCustomSweatersFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSweatersFields', token);
  }

  static async GetCustomSweaters(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSweaters', token, null, { limit, page });
  }

  static async GetCustomSweatersById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSweatersById', token, null, { id });
  }

  static async CreateCustomSweaters(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateCustomSweaters', token, newRecord);
  }

  static async UpdateCustomSweaters(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateCustomSweaters', token, oldRecord);
  }

  static async DeleteCustomSweaters(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteCustomSweaters', token, null, { id });
  }
}
