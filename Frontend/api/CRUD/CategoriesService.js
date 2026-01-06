import ApiService from '../ApiService.js';

export default class CategoriesService {
  static async GetCategoriesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCategoriesCount', token);
  }

  static async GetCategoriesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCategoriesFields', token);
  }

  static async GetCategories(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCategories', token, null, { limit, page });
  }

  static async GetCategoriesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCategoriesById', token, null, { id });
  }

  static async CreateCategories(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateCategories', token, newRecord);
  }

  static async UpdateCategories(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateCategories', token, oldRecord);
  }

  static async DeleteCategories(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteCategories', token, null, { id });
  }
}
