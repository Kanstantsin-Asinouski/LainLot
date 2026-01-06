import ApiService from '../ApiService.js';

export default class ProductImagesService {
  static async GetProductImagesCount(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetProductImagesCount', token);
  }

  static async GetProductImagesFields(token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetProductImagesFields', token);
  }

  static async GetProductImages(limit, page, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetProductImages', token, null, { limit, page });
  }

  static async GetProductImagesById(id, token) {
    return ApiService.sendRequest('rest', 'get', 'Database', 'GetProductImagesById', token, null, { id });
  }

  static async CreateProductImages(newRecord, token) {
    return ApiService.sendRequest('rest', 'post', 'Database', 'CreateProductImages', token, newRecord);
  }

  static async UpdateProductImages(oldRecord, token) {
    return ApiService.sendRequest('rest', 'put', 'Database', 'UpdateProductImages', token, oldRecord);
  }

  static async DeleteProductImages(id, token) {
    return ApiService.sendRequest('rest', 'delete', 'Database', 'DeleteProductImages', token, null, { id });
  }
}
