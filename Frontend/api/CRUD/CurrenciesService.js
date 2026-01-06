import ApiService from '../ApiService.js';

export default class CurrenciesService {
  static async GetCurrenciesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCurrenciesCount', token);
  }

  static async GetCurrenciesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCurrenciesFields', token);
  }

  static async GetCurrencies(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCurrencies', token, null, { limit, page });
  }

  static async GetCurrenciesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCurrenciesById', token, null, { id });
  }

  static async CreateCurrencies(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateCurrencies', token, newRecord);
  }

  static async UpdateCurrencies(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateCurrencies', token, oldRecord);
  }

  static async DeleteCurrencies(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteCurrencies', token, null, { id });
  }
}
