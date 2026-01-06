import ApiService from '../ApiService.js';

export default class BaseBeltsService {
  static async GetBaseBeltsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseBeltsCount', token);
  }

  static async GetBaseBeltsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseBeltsFields', token);
  }

  static async GetBaseBelts(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseBelts', token, null, { limit, page });
  }

  static async GetBaseBeltsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseBeltsById', token, null, { id });
  }

  static async CreateBaseBelts(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateBaseBelts', token, newRecord);
  }

  static async UpdateBaseBelts(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateBaseBelts', token, oldRecord);
  }

  static async DeleteBaseBelts(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteBaseBelts', token, null, { id });
  }
}
