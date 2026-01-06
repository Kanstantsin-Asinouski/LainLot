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

export const getTableTotalCount = async (currentTable, token) => {
  var response = null;

  switch (currentTable) {
    case 'About':
      response = await AboutService.GetAboutCount(token);
      break;
    case 'AccessLevels':
      response = await AccessLevelsService.GetAccessLevelsCount(token);
      break;
    case 'BaseBelts':
      response = await BaseBeltsService.GetBaseBeltsCount(token);
      break;
    case 'BaseNecklines':
      response = await BaseNecklinesService.GetBaseNecklinesCount(token);
      break;
    case 'BasePantsCuffs':
      response = await BasePantsCuffsService.GetBasePantsCuffsCount(token);
      break;
    case 'BasePants':
      response = await BasePantsService.GetBasePantsCount(token);
      break;
    case 'BaseSleeveCuffs':
      response = await BaseSleeveCuffsService.GetBaseSleeveCuffsCount(token);
      break;
    case 'BaseSleeves':
      response = await BaseSleevesService.GetBaseSleevesCount(token);
      break;
    case 'BaseSportSuits':
      response = await BaseSportSuitsService.GetBaseSportSuitsCount(token);
      break;
    case 'BaseSweaters':
      response = await BaseSweatersService.GetBaseSweatersCount(token);
      break;
    case 'Cart':
      response = await CartService.GetCartCount(token);
      break;
    case 'Categories':
      response = await CategoriesService.GetCategoriesCount(token);
      break;
    case 'CategoryHierarchy':
      response =
        await CategoryHierarchyService.GetCategoryHierarchyCount(token);
      break;
    case 'Colors':
      response = await ColorsService.GetColorsCount(token);
      break;
    case 'Contacts':
      response = await ContactsService.GetContactsCount(token);
      break;
    case 'Countries':
      response = await CountriesService.GetCountriesCount(token);
      break;
    case 'Currencies':
      response = await CurrenciesService.GetCurrenciesCount(token);
      break;
    case 'CustomBelts':
      response = await CustomBeltsService.GetCustomBeltsCount(token);
      break;
    case 'CustomizableProducts':
      response =
        await CustomizableProductsService.GetCustomizableProductsCount(token);
      break;
    case 'CustomNecklines':
      response = await CustomNecklinesService.GetCustomNecklinesCount(token);
      break;
    case 'CustomPantsCuffs':
      response = await CustomPantsCuffsService.GetCustomPantsCuffsCount(token);
      break;
    case 'CustomPants':
      response = await CustomPantsService.GetCustomPantsCount(token);
      break;
    case 'CustomSleeveCuffs':
      response =
        await CustomSleeveCuffsService.GetCustomSleeveCuffsCount(token);
      break;
    case 'CustomSleeves':
      response = await CustomSleevesService.GetCustomSleevesCount(token);
      break;
    case 'CustomSportSuits':
      response = await CustomSportSuitsService.GetCustomSportSuitsCount(token);
      break;
    case 'CustomSweaters':
      response = await CustomSweatersService.GetCustomSweatersCount(token);
      break;
    case 'FabricTypes':
      response = await FabricTypesService.GetFabricTypesCount(token);
      break;
    case 'Languages':
      response = await LanguagesService.GetLanguagesCount(token);
      break;
    case 'Orders':
      response = await OrdersService.GetOrdersCount(token);
      break;
    case 'OrderHistory':
      response = await OrderHistoryService.GetOrderHistoryCount(token);
      break;
    case 'OrderStatuses':
      response = await OrderStatusesService.GetOrderStatusesCount(token);
      break;
    case 'PaymentMethods':
      response = await PaymentMethodsService.GetPaymentMethodsCount(token);
      break;
    case 'Payments':
      response = await PaymentsService.GetPaymentsCount(token);
      break;
    case 'PaymentStatuses':
      response = await PaymentStatusesService.GetPaymentStatusesCount(token);
      break;
    case 'ProductImages':
      response = await ProductImagesService.GetProductImagesCount(token);
      break;
    case 'ProductOrders':
      response = await ProductOrdersService.GetProductOrdersCount(token);
      break;
    case 'Products':
      response = await ProductsService.GetProductsCount(token);
      break;
    case 'ProductTranslations':
      response =
        await ProductTranslationsService.GetProductTranslationsCount(token);
      break;
    case 'Reviews':
      response = await ReviewsService.GetReviewsCount(token);
      break;
    case 'ShippingAddresses':
      response =
        await ShippingAddressesService.GetShippingAddressesCount(token);
      break;
    case 'SizeOptions':
      response = await SizeOptionsService.GetSizeOptionsCount(token);
      break;
    case 'UserOrderHistory':
      response = await UserOrderHistoryService.GetUserOrderHistoryCount(token);
      break;
    case 'UserProfiles':
      response = await UserProfilesService.GetUserProfilesCount(token);
      break;
    case 'UserRoles':
      response = await UserRolesService.GetUserRolesCount(token);
      break;
    case 'Users':
      response = await UsersService.GetUsersCount(token);
      break;
    default:
      break;
  }

  return response;
};
