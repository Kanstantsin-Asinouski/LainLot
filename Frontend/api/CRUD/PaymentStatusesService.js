import ApiService from '../ApiService.js';

export default class PaymentStatusesService {
  static async GetPaymentStatusesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPaymentStatusesCount', token);
  }

  static async GetPaymentStatusesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPaymentStatusesFields', token);
  }

  static async GetPaymentStatuses(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPaymentStatuses', token, null, { limit, page });
  }

  static async GetPaymentStatusesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetPaymentStatusesById', token, null, { id });
  }

  static async CreatePaymentStatuses(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreatePaymentStatuses', token, newRecord);
  }

  static async UpdatePaymentStatuses(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdatePaymentStatuses', token, oldRecord);
  }

  static async DeletePaymentStatuses(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeletePaymentStatuses', token, null, { id });
  }
}
