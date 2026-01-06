import ApiService from '../ApiService.js';

export default class BaseNecklinesService {
  static async GetBaseNecklinesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseNecklinesCount', token);
  }

  static async GetBaseNecklinesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseNecklinesFields', token);
  }

  static async GetBaseNecklines(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseNecklines', token, null, { limit, page });
  }

  static async GetBaseNecklinesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetBaseNecklinesById', token, null, { id });
  }

  static async CreateBaseNecklines(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateBaseNecklines', token, newRecord);
  }

  static async UpdateBaseNecklines(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateBaseNecklines', token, oldRecord);
  }

  static async DeleteBaseNecklines(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteBaseNecklines', token, null, { id });
  }
}
