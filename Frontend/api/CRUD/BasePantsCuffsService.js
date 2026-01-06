import ApiService from '../ApiService.js';

export default class BasePantsCuffsService {
  static async GetBasePantsCuffsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBasePantsCuffsCount', token);
  }

  static async GetBasePantsCuffsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBasePantsCuffsFields', token);
  }

  static async GetBasePantsCuffs(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBasePantsCuffs', token, null, { limit, page });
  }

  static async GetBasePantsCuffsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBasePantsCuffsById', token, null, { id });
  }

  static async CreateBasePantsCuffs(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateBasePantsCuffs', token, newRecord);
  }

  static async UpdateBasePantsCuffs(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateBasePantsCuffs', token, oldRecord);
  }

  static async DeleteBasePantsCuffs(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteBasePantsCuffs', token, null, { id });
  }
}
