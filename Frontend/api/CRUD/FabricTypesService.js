import ApiService from '../ApiService.js';

export default class FabricTypesService {
  static async GetFabricTypesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetFabricTypesCount', token);
  }

  static async GetFabricTypesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetFabricTypesFields', token);
  }

  static async GetFabricTypes(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetFabricTypes', token, null, { limit, page });
  }

  static async GetFabricTypesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetFabricTypesById', token, null, { id });
  }

  static async CreateFabricTypes(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateFabricTypes', token, newRecord);
  }

  static async UpdateFabricTypes(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateFabricTypes', token, oldRecord);
  }

  static async DeleteFabricTypes(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteFabricTypes', token, null, { id });
  }
}
