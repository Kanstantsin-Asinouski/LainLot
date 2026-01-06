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

export const getAllRecords = async (
  currentTable,
  limit,
  page,

  token
) => {
  var response = null;

  switch (currentTable) {
    case 'About':
      response = await AboutService.GetAbout(limit, page, token);
      break;
    case 'AccessLevels':
      response = await AccessLevelsService.GetAccessLevels(
        limit,
        page,

        token
      );
      break;
    case 'BaseBelts':
      response = await BaseBeltsService.GetBaseBelts(
        limit,
        page,

        token
      );
      break;
    case 'BaseNecklines':
      response = await BaseNecklinesService.GetBaseNecklines(
        limit,
        page,

        token
      );
      break;
    case 'BasePantsCuffs':
      response = await BasePantsCuffsService.GetBasePantsCuffs(
        limit,
        page,

        token
      );
      break;
    case 'BasePants':
      response = await BasePantsService.GetBasePants(
        limit,
        page,

        token
      );
      break;
    case 'BaseSleeveCuffs':
      response = await BaseSleeveCuffsService.GetBaseSleeveCuffs(
        limit,
        page,

        token
      );
      break;
    case 'BaseSleeves':
      response = await BaseSleevesService.GetBaseSleeves(
        limit,
        page,

        token
      );
      break;
    case 'BaseSportSuits':
      response = await BaseSportSuitsService.GetBaseSportSuits(
        limit,
        page,

        token
      );
      break;
    case 'BaseSweaters':
      response = await BaseSweatersService.GetBaseSweaters(
        limit,
        page,

        token
      );
      break;
    case 'Cart':
      response = await CartService.GetCart(limit, page, token);
      break;
    case 'Categories':
      response = await CategoriesService.GetCategories(
        limit,
        page,

        token
      );
      break;
    case 'CategoryHierarchy':
      response = await CategoryHierarchyService.GetCategoryHierarchy(
        limit,
        page,

        token
      );
      break;
    case 'Colors':
      response = await ColorsService.GetColors(limit, page, token);
      break;
    case 'Contacts':
      response = await ContactsService.GetContacts(
        limit,
        page,

        token
      );
      break;
    case 'Countries':
      response = await CountriesService.GetCountries(
        limit,
        page,

        token
      );
      break;
    case 'Currencies':
      response = await CurrenciesService.GetCurrencies(
        limit,
        page,

        token
      );
      break;
    case 'CustomBelts':
      response = await CustomBeltsService.GetCustomBelts(
        limit,
        page,

        token
      );
      break;
    case 'CustomizableProducts':
      response = await CustomizableProductsService.GetCustomizableProducts(
        limit,
        page,

        token
      );
      break;
    case 'CustomNecklines':
      response = await CustomNecklinesService.GetCustomNecklines(
        limit,
        page,

        token
      );
      break;
    case 'CustomPantsCuffs':
      response = await CustomPantsCuffsService.GetCustomPantsCuffs(
        limit,
        page,

        token
      );
      break;
    case 'CustomPants':
      response = await CustomPantsService.GetCustomPants(
        limit,
        page,

        token
      );
      break;
    case 'CustomSleeveCuffs':
      response = await CustomSleeveCuffsService.GetCustomSleeveCuffs(
        limit,
        page,

        token
      );
      break;
    case 'CustomSleeves':
      response = await CustomSleevesService.GetCustomSleeves(
        limit,
        page,

        token
      );
      break;
    case 'CustomSportSuits':
      response = await CustomSportSuitsService.GetCustomSportSuits(
        limit,
        page,

        token
      );
      break;
    case 'CustomSweaters':
      response = await CustomSweatersService.GetCustomSweaters(
        limit,
        page,

        token
      );
      break;
    case 'FabricTypes':
      response = await FabricTypesService.GetFabricTypes(
        limit,
        page,

        token
      );
      break;
    case 'Languages':
      response = await LanguagesService.GetLanguages(
        limit,
        page,

        token
      );
      break;
    case 'Orders':
      response = await OrdersService.GetOrders(limit, page, token);
      break;
    case 'OrderHistory':
      response = await OrderHistoryService.GetOrderHistory(
        limit,
        page,

        token
      );
      break;
    case 'OrderStatuses':
      response = await OrderStatusesService.GetOrderStatuses(
        limit,
        page,

        token
      );
      break;
    case 'PaymentMethods':
      response = await PaymentMethodsService.GetPaymentMethods(
        limit,
        page,

        token
      );
      break;
    case 'Payments':
      response = await PaymentsService.GetPayments(
        limit,
        page,

        token
      );
      break;
    case 'PaymentStatuses':
      response = await PaymentStatusesService.GetPaymentStatuses(
        limit,
        page,

        token
      );
      break;
    case 'ProductImages':
      response = await ProductImagesService.GetProductImages(
        limit,
        page,

        token
      );
      break;
    case 'ProductOrders':
      response = await ProductOrdersService.GetProductOrders(
        limit,
        page,

        token
      );
      break;
    case 'Products':
      response = await ProductsService.GetProducts(
        limit,
        page,

        token
      );
      break;
    case 'ProductTranslations':
      response = await ProductTranslationsService.GetProductTranslations(
        limit,
        page,

        token
      );
      break;
    case 'Reviews':
      response = await ReviewsService.GetReviews(limit, page, token);
      break;
    case 'ShippingAddresses':
      response = await ShippingAddressesService.GetShippingAddresses(
        limit,
        page,

        token
      );
      break;
    case 'SizeOptions':
      response = await SizeOptionsService.GetSizeOptions(
        limit,
        page,

        token
      );
      break;
    case 'UserOrderHistory':
      response = await UserOrderHistoryService.GetUserOrderHistory(
        limit,
        page,

        token
      );
      break;
    case 'UserProfiles':
      response = await UserProfilesService.GetUserProfiles(
        limit,
        page,

        token
      );
      break;
    case 'UserRoles':
      response = await UserRolesService.GetUserRoles(
        limit,
        page,

        token
      );
      break;
    case 'Users':
      response = await UsersService.GetUsers(limit, page, token);
      break;
    default:
      break;
  }

  return response;
};
