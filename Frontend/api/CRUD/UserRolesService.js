import ApiService from '../ApiService.js';

export default class UserRolesService {
  static async GetUserRolesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserRolesCount', token);
  }

  static async GetUserRolesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserRolesFields', token);
  }

  static async GetUserRoles(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserRoles', token, null, { limit, page });
  }

  static async GetUserRolesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserRolesById', token, null, { id });
  }

  static async CreateUserRoles(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateUserRoles', token, newRecord);
  }

  static async UpdateUserRoles(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateUserRoles', token, oldRecord);
  }

  static async DeleteUserRoles(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteUserRoles', token, null, { id });
  }
}
