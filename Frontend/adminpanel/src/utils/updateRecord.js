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

export const updateRecord = async (currentTable, data, token) => {
  var response = null;

  switch (currentTable) {
    case 'About':
      response = await AboutService.UpdateAbout(data, token);
      break;
    case 'AccessLevels':
      response = await AccessLevelsService.UpdateAccessLevels(
        data,

        token
      );
      break;
    case 'BaseBelts':
      response = await BaseBeltsService.UpdateBaseBelts(data, token);
      break;
    case 'BaseNecklines':
      response = await BaseNecklinesService.UpdateBaseNecklines(
        data,

        token
      );
      break;
    case 'BasePantsCuffs':
      response = await BasePantsCuffsService.UpdateBasePantsCuffs(
        data,

        token
      );
      break;
    case 'BasePants':
      response = await BasePantsService.UpdateBasePants(data, token);
      break;
    case 'BaseSleeveCuffs':
      response = await BaseSleeveCuffsService.UpdateBaseSleeveCuffs(
        data,

        token
      );
      break;
    case 'BaseSleeves':
      response = await BaseSleevesService.UpdateBaseSleeves(
        data,

        token
      );
      break;
    case 'BaseSportSuits':
      response = await BaseSportSuitsService.UpdateBaseSportSuits(
        data,

        token
      );
      break;
    case 'BaseSweaters':
      response = await BaseSweatersService.UpdateBaseSweaters(
        data,

        token
      );
      break;
    case 'Cart':
      response = await CartService.UpdateCart(data, token);
      break;
    case 'Categories':
      response = await CategoriesService.UpdateCategories(
        data,

        token
      );
      break;
    case 'CategoryHierarchy':
      response = await CategoryHierarchyService.UpdateCategoryHierarchy(
        data,

        token
      );
      break;
    case 'Colors':
      response = await ColorsService.UpdateColors(data, token);
      break;
    case 'Contacts':
      response = await ContactsService.UpdateContacts(data, token);
      break;
    case 'Countries':
      response = await CountriesService.UpdateCountries(data, token);
      break;
    case 'Currencies':
      response = await CurrenciesService.UpdateCurrencies(
        data,

        token
      );
      break;
    case 'CustomBelts':
      response = await CustomBeltsService.UpdateCustomBelts(
        data,

        token
      );
      break;
    case 'CustomizableProducts':
      response = await CustomizableProductsService.UpdateCustomizableProducts(
        data,

        token
      );
      break;
    case 'CustomNecklines':
      response = await CustomNecklinesService.UpdateCustomNecklines(
        data,

        token
      );
      break;
    case 'CustomPantsCuffs':
      response = await CustomPantsCuffsService.UpdateCustomPantsCuffs(
        data,

        token
      );
      break;
    case 'CustomPants':
      response = await CustomPantsService.UpdateCustomPants(
        data,

        token
      );
      break;
    case 'CustomSleeveCuffs':
      response = await CustomSleeveCuffsService.UpdateCustomSleeveCuffs(
        data,

        token
      );
      break;
    case 'CustomSleeves':
      response = await CustomSleevesService.UpdateCustomSleeves(
        data,

        token
      );
      break;
    case 'CustomSportSuits':
      response = await CustomSportSuitsService.UpdateCustomSportSuits(
        data,

        token
      );
      break;
    case 'CustomSweaters':
      response = await CustomSweatersService.UpdateCustomSweaters(
        data,

        token
      );
      break;
    case 'FabricTypes':
      response = await FabricTypesService.UpdateFabricTypes(
        data,

        token
      );
      break;
    case 'Languages':
      response = await LanguagesService.UpdateLanguages(data, token);
      break;
    case 'Orders':
      response = await OrdersService.UpdateOrders(data, token);
      break;
    case 'OrderHistory':
      response = await OrderHistoryService.UpdateOrderHistory(
        data,

        token
      );
      break;
    case 'OrderStatuses':
      response = await OrderStatusesService.UpdateOrderStatuses(
        data,

        token
      );
      break;
    case 'PaymentMethods':
      response = await PaymentMethodsService.UpdatePaymentMethods(
        data,

        token
      );
      break;
    case 'Payments':
      response = await PaymentsService.UpdatePayments(data, token);
      break;
    case 'PaymentStatuses':
      response = await PaymentStatusesService.UpdatePaymentStatuses(
        data,

        token
      );
      break;
    case 'ProductImages':
      response = await ProductImagesService.UpdateProductImages(
        data,

        token
      );
      break;
    case 'ProductOrders':
      response = await ProductOrdersService.UpdateProductOrders(
        data,

        token
      );
      break;
    case 'Products':
      response = await ProductsService.UpdateProducts(data, token);
      break;
    case 'ProductTranslations':
      response = await ProductTranslationsService.UpdateProductTranslations(
        data,

        token
      );
      break;
    case 'Reviews':
      response = await ReviewsService.UpdateReviews(data, token);
      break;
    case 'ShippingAddresses':
      response = await ShippingAddressesService.UpdateShippingAddresses(
        data,

        token
      );
      break;
    case 'SizeOptions':
      response = await SizeOptionsService.UpdateSizeOptions(
        data,

        token
      );
      break;
    case 'UserOrderHistory':
      response = await UserOrderHistoryService.UpdateUserOrderHistory(
        data,

        token
      );
      break;
    case 'UserProfiles':
      response = await UserProfilesService.UpdateUserProfiles(
        data,

        token
      );
      break;
    case 'UserRoles':
      response = await UserRolesService.UpdateUserRoles(data, token);
      break;
    case 'Users':
      response = await UsersService.UpdateUsers(data, token);
      break;
    default:
      break;
  }

  return response;
};
