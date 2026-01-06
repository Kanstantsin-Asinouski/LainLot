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

export const getRecordFields = async (currentTable, token) => {
  var response = null;

  switch (currentTable) {
    case 'About':
      response = await AboutService.GetAboutFields(token);
      break;
    case 'AccessLevels':
      response = await AccessLevelsService.GetAccessLevelsFields(token);
      break;
    case 'BaseBelts':
      response = await BaseBeltsService.GetBaseBeltsFields(token);
      break;
    case 'BaseNecklines':
      response = await BaseNecklinesService.GetBaseNecklinesFields(token);
      break;
    case 'BasePantsCuffs':
      response = await BasePantsCuffsService.GetBasePantsCuffsFields(token);
      break;
    case 'BasePants':
      response = await BasePantsService.GetBasePantsFields(token);
      break;
    case 'BaseSleeveCuffs':
      response = await BaseSleeveCuffsService.GetBaseSleeveCuffsFields(token);
      break;
    case 'BaseSleeves':
      response = await BaseSleevesService.GetBaseSleevesFields(token);
      break;
    case 'BaseSportSuits':
      response = await BaseSportSuitsService.GetBaseSportSuitsFields(token);
      break;
    case 'BaseSweaters':
      response = await BaseSweatersService.GetBaseSweatersFields(token);
      break;
    case 'Cart':
      response = await CartService.GetCartFields(token);
      break;
    case 'Categories':
      response = await CategoriesService.GetCategoriesFields(token);
      break;
    case 'CategoryHierarchy':
      response =
        await CategoryHierarchyService.GetCategoryHierarchyFields(token);
      break;
    case 'Colors':
      response = await ColorsService.GetColorsFields(token);
      break;
    case 'Contacts':
      response = await ContactsService.GetContactsFields(token);
      break;
    case 'Countries':
      response = await CountriesService.GetCountriesFields(token);
      break;
    case 'Currencies':
      response = await CurrenciesService.GetCurrenciesFields(token);
      break;
    case 'CustomBelts':
      response = await CustomBeltsService.GetCustomBeltsFields(token);
      break;
    case 'CustomizableProducts':
      response =
        await CustomizableProductsService.GetCustomizableProductsFields(token);
      break;
    case 'CustomNecklines':
      response = await CustomNecklinesService.GetCustomNecklinesFields(token);
      break;
    case 'CustomPantsCuffs':
      response = await CustomPantsCuffsService.GetCustomPantsCuffsFields(token);
      break;
    case 'CustomPants':
      response = await CustomPantsService.GetCustomPantsFields(token);
      break;
    case 'CustomSleeveCuffs':
      response =
        await CustomSleeveCuffsService.GetCustomSleeveCuffsFields(token);
      break;
    case 'CustomSleeves':
      response = await CustomSleevesService.GetCustomSleevesFields(token);
      break;
    case 'CustomSportSuits':
      response = await CustomSportSuitsService.GetCustomSportSuitsFields(token);
      break;
    case 'CustomSweaters':
      response = await CustomSweatersService.GetCustomSweatersFields(token);
      break;
    case 'FabricTypes':
      response = await FabricTypesService.GetFabricTypesFields(token);
      break;
    case 'Languages':
      response = await LanguagesService.GetLanguagesFields(token);
      break;
    case 'Orders':
      response = await OrdersService.GetOrdersFields(token);
      break;
    case 'OrderHistory':
      response = await OrderHistoryService.GetOrderHistoryFields(token);
      break;
    case 'OrderStatuses':
      response = await OrderStatusesService.GetOrderStatusesFields(token);
      break;
    case 'PaymentMethods':
      response = await PaymentMethodsService.GetPaymentMethodsFields(token);
      break;
    case 'Payments':
      response = await PaymentsService.GetPaymentsFields(token);
      break;
    case 'PaymentStatuses':
      response = await PaymentStatusesService.GetPaymentStatusesFields(token);
      break;
    case 'ProductImages':
      response = await ProductImagesService.GetProductImagesFields(token);
      break;
    case 'ProductOrders':
      response = await ProductOrdersService.GetProductOrdersFields(token);
      break;
    case 'Products':
      response = await ProductsService.GetProductsFields(token);
      break;
    case 'ProductTranslations':
      response =
        await ProductTranslationsService.GetProductTranslationsFields(token);
      break;
    case 'Reviews':
      response = await ReviewsService.GetReviewsFields(token);
      break;
    case 'ShippingAddresses':
      response =
        await ShippingAddressesService.GetShippingAddressesFields(token);
      break;
    case 'SizeOptions':
      response = await SizeOptionsService.GetSizeOptionsFields(token);
      break;
    case 'UserOrderHistory':
      response = await UserOrderHistoryService.GetUserOrderHistoryFields(token);
      break;
    case 'UserProfiles':
      response = await UserProfilesService.GetUserProfilesFields(token);
      break;
    case 'UserRoles':
      response = await UserRolesService.GetUserRolesFields(token);
      break;
    case 'Users':
      response = await UsersService.GetUsersFields(token);
      break;
    default:
      break;
  }

  return response;
};
