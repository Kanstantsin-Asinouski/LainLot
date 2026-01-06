import ApiService from '../ApiService.js';

export default class CartService {
  static async GetCartCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCartCount', token);
  }

  static async GetCartFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCartFields', token);
  }

  static async GetCart(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCart', token, null, { limit, page });
  }

  static async GetCartById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetCartById', token, null, { id });
  }

  static async CreateCart(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateCart', token, newRecord);
  }

  static async UpdateCart(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateCart', token, oldRecord);
  }

  static async DeleteCart(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteCart', token, null, { id });
  }
}
