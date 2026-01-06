import ApiService from '../ApiService.js';

export default class BaseSweatersService {
  static async GetBaseSweatersCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseSweatersCount', token);
  }

  static async GetBaseSweatersFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseSweatersFields', token);
  }

  static async GetBaseSweaters(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseSweaters', token, null, { limit, page });
  }

  static async GetBaseSweatersById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseSweatersById', token, null, { id });
  }

  static async CreateBaseSweaters(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateBaseSweaters', token, newRecord);
  }

  static async UpdateBaseSweaters(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateBaseSweaters', token, oldRecord);
  }

  static async DeleteBaseSweaters(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteBaseSweaters', token, null, { id });
  }
}
