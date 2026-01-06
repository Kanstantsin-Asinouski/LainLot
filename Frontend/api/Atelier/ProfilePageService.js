import ApiService from '../ApiService.js';

export default class ProfilePageService {
    // AllowAnonymous
    static async GetUserInfo(token) {
        try {
            return await ApiService.sendRequest('auth', 'get', 'auth', 'me', token);
        } catch (error) {
            console.error(`Error fetching Profile page data:`, error);
            return null;
        }
    }
}
