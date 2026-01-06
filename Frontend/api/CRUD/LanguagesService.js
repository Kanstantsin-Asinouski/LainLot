import ApiService from '../ApiService.js';

export default class LanguagesService {
  static async GetLanguagesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetLanguagesCount', token);
  }

  static async GetLanguagesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetLanguagesFields', token);
  }

  static async GetLanguages(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetLanguages', token, null, { limit, page });
  }

  static async GetLanguagesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetLanguagesById', token, null, { id });
  }

  static async CreateLanguages(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateLanguages', token, newRecord);
  }

  static async UpdateLanguages(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateLanguages', token, oldRecord);
  }

  static async DeleteLanguages(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteLanguages', token, null, { id });
  }
}
