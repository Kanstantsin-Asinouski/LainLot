import ApiService from '../ApiService.js';

export default class PaymentMethodsService {
  static async GetPaymentMethodsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPaymentMethodsCount', token);
  }

  static async GetPaymentMethodsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPaymentMethodsFields', token);
  }

  static async GetPaymentMethods(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPaymentMethods', token, null, { limit, page });
  }

  static async GetPaymentMethodsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPaymentMethodsById', token, null, { id });
  }

  static async CreatePaymentMethods(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreatePaymentMethods', token, newRecord);
  }

  static async UpdatePaymentMethods(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdatePaymentMethods', token, oldRecord);
  }

  static async DeletePaymentMethods(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeletePaymentMethods', token, null, { id });
  }
}
