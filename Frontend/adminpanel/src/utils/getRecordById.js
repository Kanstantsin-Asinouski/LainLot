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

export const getRecordById = async (currentTable, id, token) => {
  var response = null;

  switch (currentTable) {
    case 'About':
      response = await AboutService.GetAboutById(id, token);
      break;
    case 'AccessLevels':
      response = await AccessLevelsService.GetAccessLevelsById(
        id,

        token
      );
      break;
    case 'BaseBelts':
      response = await BaseBeltsService.GetBaseBeltsById(id, token);
      break;
    case 'BaseNecklines':
      response = await BaseNecklinesService.GetBaseNecklinesById(
        id,

        token
      );
      break;
    case 'BasePantsCuffs':
      response = await BasePantsCuffsService.GetBasePantsCuffsById(
        id,

        token
      );
      break;
    case 'BasePants':
      response = await BasePantsService.GetBasePantsById(id, token);
      break;
    case 'BaseSleeveCuffs':
      response = await BaseSleeveCuffsService.GetBaseSleeveCuffsById(
        id,

        token
      );
      break;
    case 'BaseSleeves':
      response = await BaseSleevesService.GetBaseSleevesById(
        id,

        token
      );
      break;
    case 'BaseSportSuits':
      response = await BaseSportSuitsService.GetBaseSportSuitsById(
        id,

        token
      );
      break;
    case 'BaseSweaters':
      response = await BaseSweatersService.GetBaseSweatersById(
        id,

        token
      );
      break;
    case 'Cart':
      response = await CartService.GetCartById(id, token);
      break;
    case 'Categories':
      response = await CategoriesService.GetCategoriesById(id, token);
      break;
    case 'CategoryHierarchy':
      response = await CategoryHierarchyService.GetCategoryHierarchyById(
        id,

        token
      );
      break;
    case 'Colors':
      response = await ColorsService.GetColorsById(id, token);
      break;
    case 'Contacts':
      response = await ContactsService.GetContactsById(id, token);
      break;
    case 'Countries':
      response = await CountriesService.GetCountriesById(id, token);
      break;
    case 'Currencies':
      response = await CurrenciesService.GetCurrenciesById(id, token);
      break;
    case 'CustomBelts':
      response = await CustomBeltsService.GetCustomBeltsById(
        id,

        token
      );
      break;
    case 'CustomizableProducts':
      response = await CustomizableProductsService.GetCustomizableProductsById(
        id,

        token
      );
      break;
    case 'CustomNecklines':
      response = await CustomNecklinesService.GetCustomNecklinesById(
        id,

        token
      );
      break;
    case 'CustomPantsCuffs':
      response = await CustomPantsCuffsService.GetCustomPantsCuffsById(
        id,

        token
      );
      break;
    case 'CustomPants':
      response = await CustomPantsService.GetCustomPantsById(
        id,

        token
      );
      break;
    case 'CustomSleeveCuffs':
      response = await CustomSleeveCuffsService.GetCustomSleeveCuffsById(
        id,

        token
      );
      break;
    case 'CustomSleeves':
      response = await CustomSleevesService.GetCustomSleevesById(
        id,

        token
      );
      break;
    case 'CustomSportSuits':
      response = await CustomSportSuitsService.GetCustomSportSuitsById(
        id,

        token
      );
      break;
    case 'CustomSweaters':
      response = await CustomSweatersService.GetCustomSweatersById(
        id,

        token
      );
      break;
    case 'FabricTypes':
      response = await FabricTypesService.GetFabricTypesById(
        id,

        token
      );
      break;
    case 'Languages':
      response = await LanguagesService.GetLanguagesById(id, token);
      break;
    case 'Orders':
      response = await OrdersService.GetOrdersById(id, token);
      break;
    case 'OrderHistory':
      response = await OrderHistoryService.GetOrderHistoryById(
        id,

        token
      );
      break;
    case 'OrderStatuses':
      response = await OrderStatusesService.GetOrderStatusesById(
        id,

        token
      );
      break;
    case 'PaymentMethods':
      response = await PaymentMethodsService.GetPaymentMethodsById(
        id,

        token
      );
      break;
    case 'Payments':
      response = await PaymentsService.GetPaymentsById(id, token);
      break;
    case 'PaymentStatuses':
      response = await PaymentStatusesService.GetPaymentStatusesById(
        id,

        token
      );
      break;
    case 'ProductImages':
      response = await ProductImagesService.GetProductImagesById(
        id,

        token
      );
      break;
    case 'ProductOrders':
      response = await ProductOrdersService.GetProductOrdersById(
        id,

        token
      );
      break;
    case 'Products':
      response = await ProductsService.GetProductsById(id, token);
      break;
    case 'ProductTranslations':
      response = await ProductTranslationsService.GetProductTranslationsById(
        id,

        token
      );
      break;
    case 'Reviews':
      response = await ReviewsService.GetReviewsById(id, token);
      break;
    case 'ShippingAddresses':
      response = await ShippingAddressesService.GetShippingAddressesById(
        id,

        token
      );
      break;
    case 'SizeOptions':
      response = await SizeOptionsService.GetSizeOptionsById(
        id,

        token
      );
      break;
    case 'UserOrderHistory':
      response = await UserOrderHistoryService.GetUserOrderHistoryById(
        id,

        token
      );
      break;
    case 'UserProfiles':
      response = await UserProfilesService.GetUserProfilesById(
        id,

        token
      );
      break;
    case 'UserRoles':
      response = await UserRolesService.GetUserRolesById(id, token);
      break;
    case 'Users':
      response = await UsersService.GetUsersById(id, token);
      break;
    default:
      break;
  }

  return response;
};
