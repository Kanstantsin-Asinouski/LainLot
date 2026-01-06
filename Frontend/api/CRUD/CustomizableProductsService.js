import ApiService from '../ApiService.js';

export default class CustomizableProductsService {
  static async GetCustomizableProductsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomizableProductsCount', token);
  }

  static async GetCustomizableProductsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomizableProductsFields', token);
  }

  static async GetCustomizableProducts(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomizableProducts', token, null, { limit, page });
  }

  static async GetCustomizableProductsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCustomizableProductsById', token, null, { id });
  }

  static async CreateCustomizableProducts(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateCustomizableProducts', token, newRecord);
  }

  static async UpdateCustomizableProducts(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateCustomizableProducts', token, oldRecord);
  }

  static async DeleteCustomizableProducts(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteCustomizableProducts', token, null, { id });
  }
}
