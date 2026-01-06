import ForeignKeysService from 'api/ForeignKeysService.js';

export const getForeignKeyById = async (
  foreignFieldKey,
  id,

  token
) => {
  var response = null;

  switch (foreignFieldKey) {
    case 'fkAccessLevels':
      response = await ForeignKeysService.GetFkAccessLevelsData(
        id,

        token
      );
      break;
    case 'fkLanguages':
      response = await ForeignKeysService.GetFkLanguagesData(
        id,

        token
      );
      break;
    case 'fkCategories':
      response = await ForeignKeysService.GetFkCategoriesData(
        id,

        token
      );
      break;
    case 'fkFabricTypes':
      response = await ForeignKeysService.GetFkFabricTypesData(
        id,

        token
      );
      break;
    case 'fkProducts':
      response = await ForeignKeysService.GetFkProductsData(
        id,

        token
      );
      break;
    case 'fkProductImages':
      response = await ForeignKeysService.GetFkProductImagesData(
        id,

        token
      );
      break;
    case 'fkProductTranslations':
      response = await ForeignKeysService.GetFkProductTranslationsData(
        id,

        token
      );
      break;
    case 'fkReviews':
      response = await ForeignKeysService.GetFkReviewsData(id, token);
      break;
    case 'fkOrders':
      response = await ForeignKeysService.GetFkOrdersData(id, token);
      break;
    case 'fkOrderHistory':
      response = await ForeignKeysService.GetFkOrderHistoryData(
        id,

        token
      );
      break;
    case 'fkPayments':
      response = await ForeignKeysService.GetFkPaymentsData(
        id,

        token
      );
      break;
    case 'fkUsers':
      response = await ForeignKeysService.GetFkUsersData(id, token);
      break;
    case 'fkUserRoles':
      response = await ForeignKeysService.GetFkUserRolesData(
        id,

        token
      );
      break;
    case 'fkOrderStatus':
      response = await ForeignKeysService.GetFkOrderStatusData(
        id,

        token
      );
      break;
    case 'fkColors':
      response = await ForeignKeysService.GetFkColorsData(id, token);
      break;
    case 'fkCurrencies':
      response = await ForeignKeysService.GetFkCurrenciesData(
        id,

        token
      );
      break;
    case 'fkSizeOptions':
      response = await ForeignKeysService.GetFkSizeOptionsData(
        id,

        token
      );
      break;
    case 'fkBaseNecklines':
      response = await ForeignKeysService.GetFkBaseNecklinesData(
        id,

        token
      );
      break;
    case 'fkBaseSweaters':
      response = await ForeignKeysService.GetFkBaseSweatersData(
        id,

        token
      );
      break;
    case 'fkBaseSleeves':
      response = await ForeignKeysService.GetFkBaseSleevesData(
        id,

        token
      );
      break;
    case 'fkBaseSleeveCuffsLeft':
      response = await ForeignKeysService.GetFkBaseSleeveCuffsLeftData(
        id,

        token
      );
      break;
    case 'fkBaseSleeveCuffsRight':
      response = await ForeignKeysService.GetFkBaseSleeveCuffsRightData(
        id,

        token
      );
      break;
    case 'fkBaseBelts':
      response = await ForeignKeysService.GetFkBaseBeltsData(
        id,

        token
      );
      break;
    case 'fkBasePants':
      response = await ForeignKeysService.GetFkBasePantsData(
        id,

        token
      );
      break;
    case 'fkBasePantsCuffs':
      response = await ForeignKeysService.GetFkBasePantsCuffsData(
        id,

        token
      );
      break;
    case 'fkBasePantsCuffsLeft':
      response = await ForeignKeysService.GetFkBasePantsCuffsLeftData(
        id,

        token
      );
      break;
    case 'fkBasePantsCuffsRight':
      response = await ForeignKeysService.GetFkBasePantsCuffsRightData(
        id,

        token
      );
      break;
    case 'fkCustomNecklines':
      response = await ForeignKeysService.GetFkCustomNecklinesData(
        id,

        token
      );
      break;
    case 'fkCustomSweaters':
      response = await ForeignKeysService.GetFkCustomSweatersData(
        id,

        token
      );
      break;
    case 'fkCustomSleeves':
      response = await ForeignKeysService.GetFkCustomSleevesData(
        id,

        token
      );
      break;
    case 'fkCustomSleeveCuffsLeft':
      response = await ForeignKeysService.GetFkCustomSleeveCuffsLeftData(
        id,

        token
      );
      break;
    case 'fkCustomSleeveCuffsRight':
      response = await ForeignKeysService.GetFkCustomSleeveCuffsRightData(
        id,

        token
      );
      break;
    case 'fkCustomBelts':
      response = await ForeignKeysService.GetFkCustomBeltsData(
        id,

        token
      );
      break;
    case 'fkCustomPants':
      response = await ForeignKeysService.GetFkCustomPantsData(
        id,

        token
      );
      break;
    case 'fkCustomPantsCuffsLeft':
      response = await ForeignKeysService.GetFkCustomPantsCuffsLeftData(
        id,

        token
      );
      break;
    case 'fkCustomPantsCuffsRight':
      response = await ForeignKeysService.GetFkCustomPantsCuffsRightData(
        id,

        token
      );
      break;
    case 'fkCustomSportSuits':
      response = await ForeignKeysService.GetFkCustomSportSuitsData(
        id,

        token
      );
      break;
    case 'fkCustomizableProducts':
      response = await ForeignKeysService.GetFkCustomizableProductsData(
        id,

        token
      );
      break;
    case 'fkProductOrders':
      response = await ForeignKeysService.GetFkProductOrdersData(
        id,

        token
      );
      break;
    case 'fkCountries':
      response = await ForeignKeysService.GetFkCountriesData(
        id,

        token
      );
      break;
    case 'fkPaymentMethods':
      response = await ForeignKeysService.GetFkPaymentMethodsData(
        id,

        token
      );
      break;
    case 'fkPaymentStatuses':
      response = await ForeignKeysService.GetFkPaymentStatusesData(
        id,

        token
      );
      break;
    case 'fkShippingAddresses':
      response = await ForeignKeysService.GetFkShippingAddressesData(
        id,

        token
      );
      break;
    default:
      break;
  }

  return response;
};
