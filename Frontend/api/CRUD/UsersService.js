import ApiService from '../ApiService.js';

export default class UsersService {
  static async GetUsersCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUsersCount', token);
  }

  static async GetUsersFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUsersFields', token);
  }

  static async GetUsers(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUsers', token, null, { limit, page });
  }

  static async GetUsersById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUsersById', token, null, { id });
  }

  static async CreateUsers(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateUsers', token, newRecord);
  }

  static async UpdateUsers(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateUsers', token, oldRecord);
  }

  static async DeleteUsers(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteUsers', token, null, { id });
  }
}
