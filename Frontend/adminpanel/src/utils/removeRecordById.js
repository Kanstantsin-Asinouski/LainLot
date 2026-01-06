import AboutService from 'api/CRUD/AboutService.js';
import AccessLevelsService from 'api/CRUD/AccessLevelsService.js';
import BaseBeltsService from 'api/CRUD/BaseBeltsService.js';
import BaseNecklinesService from 'api/CRUD/BaseNecklinesService.js';
import BasePantsCuffsService from 'api/CRUD/BasePantsCuffsService.js';
import BasePantsService from 'api/CRUD/BasePantsService.js';
import BaseSleeveCuffsService from 'api/CRUD/BaseSleeveCuffsService.js';
import BaseSleevesService from 'api/CRUD/BaseSleevesService.js';
import BaseSportSuitsService from 'api/CRUD/BaseSportSuitsService.js';
import BaseSweatersService from 'api/CRUD/BaseSweatersService.js';
import CartService from 'api/CRUD/CartService.js';
import CategoriesService from 'api/CRUD/CategoriesService.js';
import CategoryHierarchyService from 'api/CRUD/CategoryHierarchyService.js';
import ColorsService from 'api/CRUD/ColorsService.js';
import ContactsService from 'api/CRUD/ContactsService.js';
import CountriesService from 'api/CRUD/CountriesService.js';
import CurrenciesService from 'api/CRUD/CurrenciesService.js';
import CustomBeltsService from 'api/CRUD/CustomBeltsService.js';
import CustomizableProductsService from 'api/CRUD/CustomizableProductsService.js';
import CustomNecklinesService from 'api/CRUD/CustomNecklinesService.js';
import CustomPantsCuffsService from 'api/CRUD/CustomPantsCuffsService.js';
import CustomPantsService from 'api/CRUD/CustomPantsService.js';
import CustomSleeveCuffsService from 'api/CRUD/CustomSleeveCuffsService.js';
import CustomSleevesService from 'api/CRUD/CustomSleevesService.js';
import CustomSportSuitsService from 'api/CRUD/CustomSportSuitsService.js';
import CustomSweatersService from 'api/CRUD/CustomSweatersService.js';
import FabricTypesService from 'api/CRUD/FabricTypesService.js';
import LanguagesService from 'api/CRUD/LanguagesService.js';
import OrdersService from 'api/CRUD/OrdersService.js';
import OrderHistoryService from 'api/CRUD/OrderHistoryService.js';
import OrderStatusesService from 'api/CRUD/OrderStatusesService.js';
import PaymentMethodsService from 'api/CRUD/PaymentMethodsService.js';
import PaymentsService from 'api/CRUD/PaymentsService.js';
import PaymentStatusesService from 'api/CRUD/PaymentStatusesService.js';
import ProductImagesService from 'api/CRUD/ProductImagesService.js';
import ProductOrdersService from 'api/CRUD/ProductOrdersService.js';
import ProductsService from 'api/CRUD/ProductsService.js';
import ProductTranslationsService from 'api/CRUD/ProductTranslationsService.js';
import ReviewsService from 'api/CRUD/ReviewsService.js';
import ShippingAddressesService from 'api/CRUD/ShippingAddressesService.js';
import SizeOptionsService from 'api/CRUD/SizeOptionsService.js';
import UserOrderHistoryService from 'api/CRUD/UserOrderHistoryService.js';
import UserProfilesService from 'api/CRUD/UserProfilesService.js';
import UserRolesService from 'api/CRUD/UserRolesService.js';
import UsersService from 'api/CRUD/UsersService.js';

export const removeRecordById = async (currentTable, id, token) => {
  var response = null;

  switch (currentTable) {
    case 'About':
      response = await AboutService.DeleteAbout(id, token);
      break;
    case 'AccessLevels':
      response = await AccessLevelsService.DeleteAccessLevels(
        id,

        token
      );
      break;
    case 'BaseBelts':
      response = await BaseBeltsService.DeleteBaseBelts(id, token);
      break;
    case 'BaseNecklines':
      response = await BaseNecklinesService.DeleteBaseNecklines(
        id,

        token
      );
      break;
    case 'BasePantsCuffs':
      response = await BasePantsCuffsService.DeleteBasePantsCuffs(
        id,

        token
      );
      break;
    case 'BasePants':
      response = await BasePantsService.DeleteBasePants(id, token);
      break;
    case 'BaseSleeveCuffs':
      response = await BaseSleeveCuffsService.DeleteBaseSleeveCuffs(
        id,

        token
      );
      break;
    case 'BaseSleeves':
      response = await BaseSleevesService.DeleteBaseSleeves(
        id,

        token
      );
      break;
    case 'BaseSportSuits':
      response = await BaseSportSuitsService.DeleteBaseSportSuits(
        id,

        token
      );
      break;
    case 'BaseSweaters':
      response = await BaseSweatersService.DeleteBaseSweaters(
        id,

        token
      );
      break;
    case 'Cart':
      response = await CartService.DeleteCart(id, token);
      break;
    case 'Categories':
      response = await CategoriesService.DeleteCategories(id, token);
      break;
    case 'CategoryHierarchy':
      response = await CategoryHierarchyService.DeleteCategoryHierarchy(
        id,

        token
      );
      break;
    case 'Colors':
      response = await ColorsService.DeleteColors(id, token);
      break;
    case 'Contacts':
      response = await ContactsService.DeleteContacts(id, token);
      break;
    case 'Countries':
      response = await CountriesService.DeleteCountries(id, token);
      break;
    case 'Currencies':
      response = await CurrenciesService.DeleteCurrencies(id, token);
      break;
    case 'CustomBelts':
      response = await CustomBeltsService.DeleteCustomBelts(
        id,

        token
      );
      break;
    case 'CustomizableProducts':
      response = await CustomizableProductsService.DeleteCustomizableProducts(
        id,

        token
      );
      break;
    case 'CustomNecklines':
      response = await CustomNecklinesService.DeleteCustomNecklines(
        id,

        token
      );
      break;
    case 'CustomPantsCuffs':
      response = await CustomPantsCuffsService.DeleteCustomPantsCuffs(
        id,

        token
      );
      break;
    case 'CustomPants':
      response = await CustomPantsService.DeleteCustomPants(
        id,

        token
      );
      break;
    case 'CustomSleeveCuffs':
      response = await CustomSleeveCuffsService.DeleteCustomSleeveCuffs(
        id,

        token
      );
      break;
    case 'CustomSleeves':
      response = await CustomSleevesService.DeleteCustomSleeves(
        id,

        token
      );
      break;
    case 'CustomSportSuits':
      response = await CustomSportSuitsService.DeleteCustomSportSuits(
        id,

        token
      );
      break;
    case 'CustomSweaters':
      response = await CustomSweatersService.DeleteCustomSweaters(
        id,

        token
      );
      break;
    case 'FabricTypes':
      response = await FabricTypesService.DeleteFabricTypes(
        id,

        token
      );
      break;
    case 'Languages':
      response = await LanguagesService.DeleteLanguages(id, token);
      break;
    case 'Orders':
      response = await OrdersService.DeleteOrders(id, token);
      break;
    case 'OrderHistory':
      response = await OrderHistoryService.DeleteOrderHistory(
        id,

        token
      );
      break;
    case 'OrderStatuses':
      response = await OrderStatusesService.DeleteOrderStatuses(
        id,

        token
      );
      break;
    case 'PaymentMethods':
      response = await PaymentMethodsService.DeletePaymentMethods(
        id,

        token
      );
      break;
    case 'Payments':
      response = await PaymentsService.DeletePayments(id, token);
      break;
    case 'PaymentStatuses':
      response = await PaymentStatusesService.DeletePaymentStatuses(
        id,

        token
      );
      break;
    case 'ProductImages':
      response = await ProductImagesService.DeleteProductImages(
        id,

        token
      );
      break;
    case 'ProductOrders':
      response = await ProductOrdersService.DeleteProductOrders(
        id,

        token
      );
      break;
    case 'Products':
      response = await ProductsService.DeleteProducts(id, token);
      break;
    case 'ProductTranslations':
      response = await ProductTranslationsService.DeleteProductTranslations(
        id,

        token
      );
      break;
    case 'Reviews':
      response = await ReviewsService.DeleteReviews(id, token);
      break;
    case 'ShippingAddresses':
      response = await ShippingAddressesService.DeleteShippingAddresses(
        id,

        token
      );
      break;
    case 'SizeOptions':
      response = await SizeOptionsService.DeleteSizeOptions(
        id,

        token
      );
      break;
    case 'UserOrderHistory':
      response = await UserOrderHistoryService.DeleteUserOrderHistory(
        id,

        token
      );
      break;
    case 'UserProfiles':
      response = await UserProfilesService.DeleteUserProfiles(
        id,

        token
      );
      break;
    case 'UserRoles':
      response = await UserRolesService.DeleteUserRoles(id, token);
      break;
    case 'Users':
      response = await UsersService.DeleteUsers(id, token);
      break;
    default:
      break;
  }

  return response;
};
