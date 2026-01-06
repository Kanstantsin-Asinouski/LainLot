import ApiService from '../ApiService.js';

export default class CustomSportSuitsService {
  static async GetCustomSportSuitsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSportSuitsCount', token);
  }

  static async GetCustomSportSuitsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSportSuitsFields', token);
  }

  static async GetCustomSportSuits(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSportSuits', token, null, { limit, page });
  }

  static async GetCustomSportSuitsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomSportSuitsById', token, null, { id });
  }

  static async CreateCustomSportSuits(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateCustomSportSuits', token, newRecord);
  }

  static async UpdateCustomSportSuits(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateCustomSportSuits', token, oldRecord);
  }

  static async DeleteCustomSportSuits(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteCustomSportSuits', token, null, { id });
  }
}
