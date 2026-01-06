import ApiService from '../ApiService.js';

export default class UserProfilesService {
  static async GetUserProfilesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserProfilesCount', token);
  }

  static async GetUserProfilesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserProfilesFields', token);
  }

  static async GetUserProfiles(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserProfiles', token, null, { limit, page });
  }

  static async GetUserProfilesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetUserProfilesById', token, null, { id });
  }

  static async CreateUserProfiles(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateUserProfiles', token, newRecord);
  }

  static async UpdateUserProfiles(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateUserProfiles', token, oldRecord);
  }

  static async DeleteUserProfiles(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteUserProfiles', token, null, { id });
  }
}
