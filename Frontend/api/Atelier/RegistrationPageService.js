import ApiService from '../ApiService.js';

export default class RegistrationPageService {
    // AllowAnonymous
    static async GetUserInfo(email, login, password) {
        try {
            return await ApiService.sendRequest('auth', 'get', 'auth', 'me', token);
        } catch (error) {
            console.error(`Error fetching Registration page data:`, error);
            return null;
        }
    }
}
