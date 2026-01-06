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

export const createRecord = async (currentTable, data, token) => {
  var response = null;

  switch (currentTable) {
    case 'About':
      response = await AboutService.CreateAbout(data, token);
      break;
    case 'AccessLevels':
      response = await AccessLevelsService.CreateAccessLevels(
        data,

        token
      );
      break;
    case 'BaseBelts':
      response = await BaseBeltsService.CreateBaseBelts(data, token);
      break;
    case 'BaseNecklines':
      response = await BaseNecklinesService.CreateBaseNecklines(
        data,

        token
      );
      break;
    case 'BasePantsCuffs':
      response = await BasePantsCuffsService.CreateBasePantsCuffs(
        data,

        token
      );
      break;
    case 'BasePants':
      response = await BasePantsService.CreateBasePants(data, token);
      break;
    case 'BaseSleeveCuffs':
      response = await BaseSleeveCuffsService.CreateBaseSleeveCuffs(
        data,

        token
      );
      break;
    case 'BaseSleeves':
      response = await BaseSleevesService.CreateBaseSleeves(
        data,

        token
      );
      break;
    case 'BaseSportSuits':
      response = await BaseSportSuitsService.CreateBaseSportSuits(
        data,

        token
      );
      break;
    case 'BaseSweaters':
      response = await BaseSweatersService.CreateBaseSweaters(
        data,

        token
      );
      break;
    case 'Cart':
      response = await CartService.CreateCart(data, token);
      break;
    case 'Categories':
      response = await CategoriesService.CreateCategories(
        data,

        token
      );
      break;
    case 'CategoryHierarchy':
      response = await CategoryHierarchyService.CreateCategoryHierarchy(
        data,

        token
      );
      break;
    case 'Colors':
      response = await ColorsService.CreateColors(data, token);
      break;
    case 'Contacts':
      response = await ContactsService.CreateContacts(data, token);
      break;
    case 'Countries':
      response = await CountriesService.CreateCountries(data, token);
      break;
    case 'Currencies':
      response = await CurrenciesService.CreateCurrencies(
        data,

        token
      );
      break;
    case 'CustomBelts':
      response = await CustomBeltsService.CreateCustomBelts(
        data,

        token
      );
      break;
    case 'CustomizableProducts':
      response = await CustomizableProductsService.CreateCustomizableProducts(
        data,

        token
      );
      break;
    case 'CustomNecklines':
      response = await CustomNecklinesService.CreateCustomNecklines(
        data,

        token
      );
      break;
    case 'CustomPantsCuffs':
      response = await CustomPantsCuffsService.CreateCustomPantsCuffs(
        data,

        token
      );
      break;
    case 'CustomPants':
      response = await CustomPantsService.CreateCustomPants(
        data,

        token
      );
      break;
    case 'CustomSleeveCuffs':
      response = await CustomSleeveCuffsService.CreateCustomSleeveCuffs(
        data,

        token
      );
      break;
    case 'CustomSleeves':
      response = await CustomSleevesService.CreateCustomSleeves(
        data,

        token
      );
      break;
    case 'CustomSportSuits':
      response = await CustomSportSuitsService.CreateCustomSportSuits(
        data,

        token
      );
      break;
    case 'CustomSweaters':
      response = await CustomSweatersService.CreateCustomSweaters(
        data,

        token
      );
      break;
    case 'FabricTypes':
      response = await FabricTypesService.CreateFabricTypes(
        data,

        token
      );
      break;
    case 'Languages':
      response = await LanguagesService.CreateLanguages(data, token);
      break;
    case 'Orders':
      response = await OrdersService.CreateOrders(data, token);
      break;
    case 'OrderHistory':
      response = await OrderHistoryService.CreateOrderHistory(
        data,

        token
      );
      break;
    case 'OrderStatuses':
      response = await OrderStatusesService.CreateOrderStatuses(
        data,

        token
      );
      break;
    case 'PaymentMethods':
      response = await PaymentMethodsService.CreatePaymentMethods(
        data,

        token
      );
      break;
    case 'Payments':
      response = await PaymentsService.CreatePayments(data, token);
      break;
    case 'PaymentStatuses':
      response = await PaymentStatusesService.CreatePaymentStatuses(
        data,

        token
      );
      break;
    case 'ProductImages':
      response = await ProductImagesService.CreateProductImages(
        data,

        token
      );
      break;
    case 'ProductOrders':
      response = await ProductOrdersService.CreateProductOrders(
        data,

        token
      );
      break;
    case 'Products':
      response = await ProductsService.CreateProducts(data, token);
      break;
    case 'ProductTranslations':
      response = await ProductTranslationsService.CreateProductTranslations(
        data,

        token
      );
      break;
    case 'Reviews':
      response = await ReviewsService.CreateReviews(data, token);
      break;
    case 'ShippingAddresses':
      response = await ShippingAddressesService.CreateShippingAddresses(
        data,

        token
      );
      break;
    case 'SizeOptions':
      response = await SizeOptionsService.CreateSizeOptions(
        data,

        token
      );
      break;
    case 'UserOrderHistory':
      response = await UserOrderHistoryService.CreateUserOrderHistory(
        data,

        token
      );
      break;
    case 'UserProfiles':
      response = await UserProfilesService.CreateUserProfiles(
        data,

        token
      );
      break;
    case 'UserRoles':
      response = await UserRolesService.CreateUserRoles(data, token);
      break;
    case 'Users':
      response = await UsersService.CreateUsers(data, token);
      break;
    default:
      break;
  }

  return response;
};
