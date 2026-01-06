import ApiService from '../ApiService.js';

export default class CustomPantsService {
  static async GetCustomPantsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomPantsCount', token);
  }

  static async GetCustomPantsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomPantsFields', token);
  }

  static async GetCustomPants(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomPants', token, null, { limit, page });
  }

  static async GetCustomPantsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomPantsById', token, null, { id });
  }

  static async CreateCustomPants(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateCustomPants', token, newRecord);
  }

  static async UpdateCustomPants(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateCustomPants', token, oldRecord);
  }

  static async DeleteCustomPants(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteCustomPants', token, null, { id });
  }
}
