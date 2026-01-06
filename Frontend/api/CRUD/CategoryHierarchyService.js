import ApiService from '../ApiService.js';

export default class CategoryHierarchyService {
  static async GetCategoryHierarchyCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCategoryHierarchyCount', token);
  }

  static async GetCategoryHierarchyFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCategoryHierarchyFields', token);
  }

  static async GetCategoryHierarchy(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCategoryHierarchy', token, null, { limit, page });
  }

  static async GetCategoryHierarchyById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCategoryHierarchyById', token, null, { id });
  }

  static async CreateCategoryHierarchy(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateCategoryHierarchy', token, newRecord);
  }

  static async UpdateCategoryHierarchy(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateCategoryHierarchy', token, oldRecord);
  }

  static async DeleteCategoryHierarchy(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteCategoryHierarchy', token, null, { id });
  }
}
