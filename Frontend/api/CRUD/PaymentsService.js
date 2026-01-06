import ApiService from '../ApiService.js';

export default class PaymentsService {
  static async GetPaymentsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPaymentsCount', token);
  }

  static async GetPaymentsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPaymentsFields', token);
  }

  static async GetPayments(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPayments', token, null, { limit, page });
  }

  static async GetPaymentsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPaymentsById', token, null, { id });
  }

  static async CreatePayments(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreatePayments', token, newRecord);
  }

  static async UpdatePayments(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdatePayments', token, oldRecord);
  }

  static async DeletePayments(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeletePayments', token, null, { id });
  }
}
