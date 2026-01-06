import ApiService from '../ApiService.js';

export default class ReviewsService {
  static async GetReviewsCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetReviewsCount', token);
  }

  static async GetReviewsFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetReviewsFields', token);
  }

  static async GetReviews(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetReviews', token, null, { limit, page });
  }

  static async GetReviewsById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetReviewsById', token, null, { id });
  }

  static async CreateReviews(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateReviews', token, newRecord);
  }

  static async UpdateReviews(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateReviews', token, oldRecord);
  }

  static async DeleteReviews(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteReviews', token, null, { id });
  }
}
