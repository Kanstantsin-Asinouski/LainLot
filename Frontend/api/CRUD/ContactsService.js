import ApiService from '../ApiService.js';

export default class ContactsService {
  static async GetContactsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetContactsCount', token);
  }

  static async GetContactsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetContactsFields', token);
  }

  static async GetContacts(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetContacts', token, null, { limit, page });
  }

  static async GetContactsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetContactsById', token, null, { id });
  }

  static async CreateContacts(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateContacts', token, newRecord);
  }

  static async UpdateContacts(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateContacts', token, oldRecord);
  }

  static async DeleteContacts(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteContacts', token, null, { id });
  }
}
