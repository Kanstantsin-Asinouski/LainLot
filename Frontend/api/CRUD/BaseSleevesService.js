import ApiService from '../ApiService.js';

export default class BaseSleevesService {
  static async GetBaseSleevesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseSleevesCount', token);
  }

  static async GetBaseSleevesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseSleevesFields', token);
  }

  static async GetBaseSleeves(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseSleeves', token, null, { limit, page });
  }

  static async GetBaseSleevesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseSleevesById', token, null, { id });
  }

  static async CreateBaseSleeves(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateBaseSleeves', token, newRecord);
  }

  static async UpdateBaseSleeves(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateBaseSleeves', token, oldRecord);
  }

  static async DeleteBaseSleeves(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteBaseSleeves', token, null, { id });
  }
}
