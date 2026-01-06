import ApiService from '../ApiService.js';

export default class CustomSleevesService {
  static async GetCustomSleevesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSleevesCount', token);
  }

  static async GetCustomSleevesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSleevesFields', token);
  }

  static async GetCustomSleeves(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSleeves', token, null, { limit, page });
  }

  static async GetCustomSleevesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSleevesById', token, null, { id });
  }

  static async CreateCustomSleeves(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateCustomSleeves', token, newRecord);
  }

  static async UpdateCustomSleeves(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateCustomSleeves', token, oldRecord);
  }

  static async DeleteCustomSleeves(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteCustomSleeves', token, null, { id });
  }
}
