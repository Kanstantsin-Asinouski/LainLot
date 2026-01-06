import ApiService from '../ApiService.js';

export default class ShippingAddressesService {
  static async GetShippingAddressesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetShippingAddressesCount', token);
  }

  static async GetShippingAddressesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetShippingAddressesFields', token);
  }

  static async GetShippingAddresses(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetShippingAddresses', token, null, { limit, page });
  }

  static async GetShippingAddressesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetShippingAddressesById', token, null, { id });
  }

  static async CreateShippingAddresses(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateShippingAddresses', token, newRecord);
  }

  static async UpdateShippingAddresses(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateShippingAddresses', token, oldRecord);
  }

  static async DeleteShippingAddresses(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteShippingAddresses', token, null, { id });
  }
}
