import ApiService from './ApiService.js';

export default class CheckCredentialsService {
  static async Login(email, password) {
    return await ApiService.sendRequest('auth', 'post', 'auth', 'login', null, { email, password }, null);
  }
}
