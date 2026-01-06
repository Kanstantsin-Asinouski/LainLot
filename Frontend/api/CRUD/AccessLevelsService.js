import ApiService from '../ApiService.js';

export default class AccessLevelsService {
  static async GetAccessLevelsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetAccessLevelsCount', token);
  }

  static async GetAccessLevelsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetAccessLevelsFields', token);
  }

  static async GetAccessLevels(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetAccessLevels', token, null, { limit, page });
  }

  static async GetAccessLevelsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetAccessLevelsById', token, null, { id });
  }

  static async CreateAccessLevels(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateAccessLevels', token, newRecord);
  }

  static async UpdateAccessLevels(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateAccessLevels', token, oldRecord);
  }

  static async DeleteAccessLevels(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteAccessLevels', token, null, { id });
  }
}
