import ApiService from '../ApiService.js';

export default class AboutService {
  static async GetAboutCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetAboutCount', token);
  }

  static async GetAboutFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetAboutFields', token);
  }

  static async GetAbout(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetAbout', token, null, { limit, page });
  }

  static async GetAboutById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetAboutById', token, null, { id });
  }

  static async CreateAbout(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateAbout', token, newRecord);
  }

  static async UpdateAbout(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateAbout', token, oldRecord);
  }

  static async DeleteAbout(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteAbout', token, null, { id });
  }
}