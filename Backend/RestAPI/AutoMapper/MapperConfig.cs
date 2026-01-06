using AutoMapper;
using DB = DatabaseProvider.Models;
using API = RestAPI.Models;

namespace RestAPI.AutoMapper
{
    public class MapperConfig
    {
        public static IMapper InitializeAutomapper()
        {
            var loggerFactory = new LoggerFactory();
            // Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddGlobalIgnore("Item");

                #region API -> DB

                // Configuring API to Database
                cfg.CreateMap<API.About, DB.About>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkLanguages, opt => opt.MapFrom(s => s.FkLanguages))
                    .ForMember(d => d.Header, opt => opt.MapFrom(s => s.Header))
                    .ForMember(d => d.Text, opt => opt.MapFrom(s => s.Text));

                cfg.CreateMap<API.AccessLevel, DB.AccessLevel>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Level, opt => opt.MapFrom(s => s.Level))
                    .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));

                cfg.CreateMap<API.BaseBelt, DB.BaseBelt>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<API.BaseNeckline, DB.BaseNeckline>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<API.BasePant, DB.BasePant>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<API.BasePantsCuff, DB.BasePantsCuff>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<API.BaseSleeve, DB.BaseSleeve>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<API.BaseSleeveCuff, DB.BaseSleeveCuff>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<API.BaseSportSuit, DB.BaseSportSuit>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseNecklines, opt => opt.MapFrom(s => s.FkBaseNecklines))
                    .ForMember(d => d.FkBaseSweaters, opt => opt.MapFrom(s => s.FkBaseSweaters))
                    .ForMember(d => d.FkBaseSleeves, opt => opt.MapFrom(s => s.FkBaseSleeves))
                    .ForMember(d => d.FkBaseSleeveCuffsLeft, opt => opt.MapFrom(s => s.FkBaseSleeveCuffsLeft))
                    .ForMember(d => d.FkBaseSleeveCuffsRight, opt => opt.MapFrom(s => s.FkBaseSleeveCuffsRight))
                    .ForMember(d => d.FkBaseBelts, opt => opt.MapFrom(s => s.FkBaseBelts))
                    .ForMember(d => d.FkBasePants, opt => opt.MapFrom(s => s.FkBasePants))
                    .ForMember(d => d.FkBasePantsCuffsLeft, opt => opt.MapFrom(s => s.FkBasePantsCuffsLeft))
                    .ForMember(d => d.FkBasePantsCuffsRight, opt => opt.MapFrom(s => s.FkBasePantsCuffsRight));

                cfg.CreateMap<API.BaseSweater, DB.BaseSweater>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<API.Cart, DB.Cart>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkProductOrders, opt => opt.MapFrom(s => s.FkProductOrders))
                    .ForMember(d => d.FkCurrencies, opt => opt.MapFrom(s => s.FkCurrencies))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                    .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt));

                cfg.CreateMap<API.Category, DB.Category>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkLanguages, opt => opt.MapFrom(s => s.FkLanguages))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                    .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));

                cfg.CreateMap<API.CategoryHierarchy, DB.CategoryHierarchy>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.ParentId))
                    .ForMember(d => d.FkCategories, opt => opt.MapFrom(s => s.FkCategories));

                cfg.CreateMap<API.Color, DB.Color>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                    .ForMember(d => d.ImageData, opt => opt.MapFrom(s => s.ImageData));

                cfg.CreateMap<API.Contact, DB.Contact>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkLanguages, opt => opt.MapFrom(s => s.FkLanguages))
                    .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                    .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
                    .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email));

                cfg.CreateMap<API.Country, DB.Country>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

                cfg.CreateMap<API.Currency, DB.Currency>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

                cfg.CreateMap<API.CustomBelt, DB.CustomBelt>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseBelts, opt => opt.MapFrom(s => s.FkBaseBelts))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<API.CustomNeckline, DB.CustomNeckline>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseNecklines, opt => opt.MapFrom(s => s.FkBaseNecklines))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<API.CustomPant, DB.CustomPant>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBasePants, opt => opt.MapFrom(s => s.FkBasePants))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<API.CustomPantsCuff, DB.CustomPantsCuff>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBasePantCuffs, opt => opt.MapFrom(s => s.FkBasePantCuffs))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<API.CustomSleeve, DB.CustomSleeve>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseSleeves, opt => opt.MapFrom(s => s.FkBaseSleeves))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<API.CustomSleeveCuff, DB.CustomSleeveCuff>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseSleeveCuffs, opt => opt.MapFrom(s => s.FkBaseSleeveCuffs))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<API.CustomSportSuit, DB.CustomSportSuit>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkCustomNecklines, opt => opt.MapFrom(s => s.FkCustomNecklines))
                    .ForMember(d => d.FkCustomSweaters, opt => opt.MapFrom(s => s.FkCustomSweaters))
                    .ForMember(d => d.FkCustomSleeves, opt => opt.MapFrom(s => s.FkCustomSleeves))
                    .ForMember(d => d.FkCustomSleeveCuffsLeft, opt => opt.MapFrom(s => s.FkCustomSleeveCuffsLeft))
                    .ForMember(d => d.FkCustomSleeveCuffsRight, opt => opt.MapFrom(s => s.FkCustomSleeveCuffsRight))
                    .ForMember(d => d.FkCustomBelts, opt => opt.MapFrom(s => s.FkCustomBelts))
                    .ForMember(d => d.FkCustomPants, opt => opt.MapFrom(s => s.FkCustomPants))
                    .ForMember(d => d.FkCustomPantsCuffsLeft, opt => opt.MapFrom(s => s.FkCustomPantsCuffsLeft))
                    .ForMember(d => d.FkCustomPantsCuffsRight, opt => opt.MapFrom(s => s.FkCustomPantsCuffsRight));

                cfg.CreateMap<API.CustomSweater, DB.CustomSweater>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseSweaters, opt => opt.MapFrom(s => s.FkBaseSweaters))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<API.CustomizableProduct, DB.CustomizableProduct>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkCustomSportSuits, opt => opt.MapFrom(s => s.FkCustomSportSuits))
                    .ForMember(d => d.FkFabricTypes, opt => opt.MapFrom(s => s.FkFabricTypes))
                    .ForMember(d => d.FkSizeOptions, opt => opt.MapFrom(s => s.FkSizeOptions))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                    .ForMember(d => d.CustomizationDetails, opt => opt.MapFrom(s => s.CustomizationDetails))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                    .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt));

                cfg.CreateMap<API.FabricType, DB.FabricType>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                    .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));

                cfg.CreateMap<API.Language, DB.Language>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName))
                    .ForMember(d => d.Abbreviation, opt => opt.MapFrom(s => s.Abbreviation))
                    .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                    .ForMember(d => d.DateFormat, opt => opt.MapFrom(s => s.DateFormat))
                    .ForMember(d => d.TimeFormat, opt => opt.MapFrom(s => s.TimeFormat));

                cfg.CreateMap<API.Order, DB.Order>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkProductOrders, opt => opt.MapFrom(s => s.FkProductOrders))
                    .ForMember(d => d.FkOrderStatus, opt => opt.MapFrom(s => s.FkOrderStatus))
                    .ForMember(d => d.FkPayments, opt => opt.MapFrom(s => s.FkPayments))
                    .ForMember(d => d.FkShippingAddresses, opt => opt.MapFrom(s => s.FkShippingAddresses))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                    .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
                    .ForMember(d => d.OrderDate, opt => opt.MapFrom(s => s.OrderDate))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                    .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt));

                cfg.CreateMap<API.OrderHistory, DB.OrderHistory>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkOrders, opt => opt.MapFrom(s => s.FkOrders))
                    .ForMember(d => d.FkOrderStatuses, opt => opt.MapFrom(s => s.FkOrderStatuses))
                    .ForMember(d => d.ChangedAt, opt => opt.MapFrom(s => s.ChangedAt));

                cfg.CreateMap<API.OrderStatus, DB.OrderStatus>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status));

                cfg.CreateMap<API.Payment, DB.Payment>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkPaymentMethods, opt => opt.MapFrom(s => s.FkPaymentMethods))
                    .ForMember(d => d.FkCurrencies, opt => opt.MapFrom(s => s.FkCurrencies))
                    .ForMember(d => d.FkPaymentStatuses, opt => opt.MapFrom(s => s.FkPaymentStatuses))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                    .ForMember(d => d.PaymentDate, opt => opt.MapFrom(s => s.PaymentDate))
                    .ForMember(d => d.PaymentNumber, opt => opt.MapFrom(s => s.PaymentNumber));

                cfg.CreateMap<API.PaymentMethod, DB.PaymentMethod>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Method, opt => opt.MapFrom(s => s.Method));

                cfg.CreateMap<API.PaymentStatus, DB.PaymentStatus>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status));

                cfg.CreateMap<API.Product, DB.Product>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                    .ForMember(d => d.StockQuantity, opt => opt.MapFrom(s => s.StockQuantity))
                    .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive))
                    .ForMember(d => d.IsCustomizable, opt => opt.MapFrom(s => s.IsCustomizable))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                    .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt));

                cfg.CreateMap<API.ProductImage, DB.ProductImage>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkProducts, opt => opt.MapFrom(s => s.FkProducts))
                    .ForMember(d => d.ImageData, opt => opt.MapFrom(s => s.ImageData));

                cfg.CreateMap<API.ProductOrder, DB.ProductOrder>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkProducts, opt => opt.MapFrom(s => s.FkProducts))
                    .ForMember(d => d.FkCustomizableProducts, opt => opt.MapFrom(s => s.FkCustomizableProducts));

                cfg.CreateMap<API.ProductTranslation, DB.ProductTranslation>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkLanguages, opt => opt.MapFrom(s => s.FkLanguages))
                    .ForMember(d => d.FkProducts, opt => opt.MapFrom(s => s.FkProducts))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                    .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));

                cfg.CreateMap<API.Review, DB.Review>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkProducts, opt => opt.MapFrom(s => s.FkProducts))
                    .ForMember(d => d.FkUsers, opt => opt.MapFrom(s => s.FkUsers))
                    .ForMember(d => d.Rating, opt => opt.MapFrom(s => s.Rating))
                    .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Comment))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt));

                cfg.CreateMap<API.ShippingAddress, DB.ShippingAddress>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkCountries, opt => opt.MapFrom(s => s.FkCountries))
                    .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                    .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
                    .ForMember(d => d.ZipPostCode, opt => opt.MapFrom(s => s.ZipPostCode))
                    .ForMember(d => d.StateProvince, opt => opt.MapFrom(s => s.StateProvince))
                    .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email));

                cfg.CreateMap<API.SizeOption, DB.SizeOption>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Size, opt => opt.MapFrom(s => s.Size));

                cfg.CreateMap<API.User, DB.User>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkUserRoles, opt => opt.MapFrom(s => s.FkUserRoles))
                    .ForMember(d => d.Login, opt => opt.MapFrom(s => s.Login))
                    .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                    .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
                    .ForMember(d => d.ConfirmEmail, opt => opt.MapFrom(s => s.ConfirmEmail))
                    .ForMember(d => d.ConfirmationToken, opt => opt.MapFrom(s => s.ConfirmationToken))
                    .ForMember(d => d.ConfirmationTokenExpires, opt => opt.MapFrom(s => s.ConfirmationTokenExpires))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                    .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt));

                cfg.CreateMap<API.UserOrderHistory, DB.UserOrderHistory>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkOrders, opt => opt.MapFrom(s => s.FkOrders))
                    .ForMember(d => d.FkUsers, opt => opt.MapFrom(s => s.FkUsers));

                cfg.CreateMap<API.UserProfile, DB.UserProfile>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkUsers, opt => opt.MapFrom(s => s.FkUsers))
                    .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                    .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                    .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.MiddleName))
                    .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                    .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
                    .ForMember(d => d.ZipPostCode, opt => opt.MapFrom(s => s.ZipPostCode))
                    .ForMember(d => d.StateProvince, opt => opt.MapFrom(s => s.StateProvince))
                    .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
                    .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
                    .ForMember(d => d.Avatar, opt => opt.MapFrom(s => s.Avatar))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                    .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt));

                cfg.CreateMap<API.UserRole, DB.UserRole>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkAccessLevels, opt => opt.MapFrom(s => s.FkAccessLevels))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

                #endregion

                #region DB -> API

                cfg.CreateMap<DB.About, API.About>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkLanguages, opt => opt.MapFrom(s => s.FkLanguages))
                    .ForMember(d => d.Header, opt => opt.MapFrom(s => s.Header))
                    .ForMember(d => d.Text, opt => opt.MapFrom(s => s.Text));

                cfg.CreateMap<DB.AccessLevel, API.AccessLevel>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Level, opt => opt.MapFrom(s => s.Level))
                    .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));

                cfg.CreateMap<DB.BaseBelt, API.BaseBelt>()
                   .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                   .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<DB.BaseNeckline, API.BaseNeckline>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<DB.BasePant, API.BasePant>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<DB.BasePantsCuff, API.BasePantsCuff>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<DB.BaseSleeve, API.BaseSleeve>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<DB.BaseSleeveCuff, API.BaseSleeveCuff>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<DB.BaseSportSuit, API.BaseSportSuit>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseNecklines, opt => opt.MapFrom(s => s.FkBaseNecklines))
                    .ForMember(d => d.FkBaseSweaters, opt => opt.MapFrom(s => s.FkBaseSweaters))
                    .ForMember(d => d.FkBaseSleeves, opt => opt.MapFrom(s => s.FkBaseSleeves))
                    .ForMember(d => d.FkBaseSleeveCuffsLeft, opt => opt.MapFrom(s => s.FkBaseSleeveCuffsLeft))
                    .ForMember(d => d.FkBaseSleeveCuffsRight, opt => opt.MapFrom(s => s.FkBaseSleeveCuffsRight))
                    .ForMember(d => d.FkBaseBelts, opt => opt.MapFrom(s => s.FkBaseBelts))
                    .ForMember(d => d.FkBasePants, opt => opt.MapFrom(s => s.FkBasePants))
                    .ForMember(d => d.FkBasePantsCuffsLeft, opt => opt.MapFrom(s => s.FkBasePantsCuffsLeft))
                    .ForMember(d => d.FkBasePantsCuffsRight, opt => opt.MapFrom(s => s.FkBasePantsCuffsRight));

                cfg.CreateMap<DB.BaseSweater, API.BaseSweater>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Settings, opt => opt.MapFrom(s => s.Settings));

                cfg.CreateMap<DB.Cart, API.Cart>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkProductOrders, opt => opt.MapFrom(s => s.FkProductOrders))
                    .ForMember(d => d.FkCurrencies, opt => opt.MapFrom(s => s.FkCurrencies))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                    .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt));

                cfg.CreateMap<DB.Category, API.Category>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkLanguages, opt => opt.MapFrom(s => s.FkLanguages))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                    .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));

                cfg.CreateMap<DB.CategoryHierarchy, API.CategoryHierarchy>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.ParentId))
                    .ForMember(d => d.FkCategories, opt => opt.MapFrom(s => s.FkCategories));

                cfg.CreateMap<DB.Color, API.Color>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                    .ForMember(d => d.ImageData, opt => opt.MapFrom(s => s.ImageData));

                cfg.CreateMap<DB.Contact, API.Contact>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkLanguages, opt => opt.MapFrom(s => s.FkLanguages))
                    .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                    .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
                    .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email));

                cfg.CreateMap<DB.Country, API.Country>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

                cfg.CreateMap<DB.Currency, API.Currency>()
                  .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                  .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

                cfg.CreateMap<DB.CustomBelt, API.CustomBelt>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseBelts, opt => opt.MapFrom(s => s.FkBaseBelts))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<DB.CustomNeckline, API.CustomNeckline>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseNecklines, opt => opt.MapFrom(s => s.FkBaseNecklines))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<DB.CustomPant, API.CustomPant>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBasePants, opt => opt.MapFrom(s => s.FkBasePants))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<DB.CustomPantsCuff, API.CustomPantsCuff>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBasePantCuffs, opt => opt.MapFrom(s => s.FkBasePantCuffs))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<DB.CustomSleeve, API.CustomSleeve>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseSleeves, opt => opt.MapFrom(s => s.FkBaseSleeves))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<DB.CustomSleeveCuff, API.CustomSleeveCuff>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseSleeveCuffs, opt => opt.MapFrom(s => s.FkBaseSleeveCuffs))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<DB.CustomSportSuit, API.CustomSportSuit>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkCustomNecklines, opt => opt.MapFrom(s => s.FkCustomNecklines))
                    .ForMember(d => d.FkCustomSweaters, opt => opt.MapFrom(s => s.FkCustomSweaters))
                    .ForMember(d => d.FkCustomSleeves, opt => opt.MapFrom(s => s.FkCustomSleeves))
                    .ForMember(d => d.FkCustomSleeveCuffsLeft, opt => opt.MapFrom(s => s.FkCustomSleeveCuffsLeft))
                    .ForMember(d => d.FkCustomSleeveCuffsRight, opt => opt.MapFrom(s => s.FkCustomSleeveCuffsRight))
                    .ForMember(d => d.FkCustomBelts, opt => opt.MapFrom(s => s.FkCustomBelts))
                    .ForMember(d => d.FkCustomPants, opt => opt.MapFrom(s => s.FkCustomPants))
                    .ForMember(d => d.FkCustomPantsCuffsLeft, opt => opt.MapFrom(s => s.FkCustomPantsCuffsLeft))
                    .ForMember(d => d.FkCustomPantsCuffsRight, opt => opt.MapFrom(s => s.FkCustomPantsCuffsRight));

                cfg.CreateMap<DB.CustomSweater, API.CustomSweater>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkBaseSweaters, opt => opt.MapFrom(s => s.FkBaseSweaters))
                    .ForMember(d => d.CustomSettings, opt => opt.MapFrom(s => s.CustomSettings));

                cfg.CreateMap<DB.CustomizableProduct, API.CustomizableProduct>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkCustomSportSuits, opt => opt.MapFrom(s => s.FkCustomSportSuits))
                    .ForMember(d => d.FkFabricTypes, opt => opt.MapFrom(s => s.FkFabricTypes))
                    .ForMember(d => d.FkSizeOptions, opt => opt.MapFrom(s => s.FkSizeOptions))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                    .ForMember(d => d.CustomizationDetails, opt => opt.MapFrom(s => s.CustomizationDetails))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                    .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt));

                cfg.CreateMap<DB.FabricType, API.FabricType>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                    .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));

                cfg.CreateMap<DB.Language, API.Language>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.FullName))
                    .ForMember(d => d.Abbreviation, opt => opt.MapFrom(s => s.Abbreviation))
                    .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                    .ForMember(d => d.DateFormat, opt => opt.MapFrom(s => s.DateFormat))
                    .ForMember(d => d.TimeFormat, opt => opt.MapFrom(s => s.TimeFormat));

                cfg.CreateMap<DB.Order, API.Order>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkProductOrders, opt => opt.MapFrom(s => s.FkProductOrders))
                    .ForMember(d => d.FkOrderStatus, opt => opt.MapFrom(s => s.FkOrderStatus))
                    .ForMember(d => d.FkPayments, opt => opt.MapFrom(s => s.FkPayments))
                    .ForMember(d => d.FkShippingAddresses, opt => opt.MapFrom(s => s.FkShippingAddresses))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                    .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
                    .ForMember(d => d.OrderDate, opt => opt.MapFrom(s => s.OrderDate))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                    .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt));

                cfg.CreateMap<DB.OrderHistory, API.OrderHistory>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkOrders, opt => opt.MapFrom(s => s.FkOrders))
                    .ForMember(d => d.FkOrderStatuses, opt => opt.MapFrom(s => s.FkOrderStatuses))
                    .ForMember(d => d.ChangedAt, opt => opt.MapFrom(s => s.ChangedAt));

                cfg.CreateMap<DB.OrderStatus, API.OrderStatus>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status));

                cfg.CreateMap<DB.Payment, API.Payment>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkPaymentMethods, opt => opt.MapFrom(s => s.FkPaymentMethods))
                    .ForMember(d => d.FkCurrencies, opt => opt.MapFrom(s => s.FkCurrencies))
                    .ForMember(d => d.FkPaymentStatuses, opt => opt.MapFrom(s => s.FkPaymentStatuses))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                    .ForMember(d => d.PaymentDate, opt => opt.MapFrom(s => s.PaymentDate))
                    .ForMember(d => d.PaymentNumber, opt => opt.MapFrom(s => s.PaymentNumber));

                cfg.CreateMap<DB.PaymentMethod, API.PaymentMethod>()
                   .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                   .ForMember(d => d.Method, opt => opt.MapFrom(s => s.Method));

                cfg.CreateMap<DB.PaymentStatus, API.PaymentStatus>()
                  .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                  .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status));

                cfg.CreateMap<DB.Product, API.Product>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price))
                    .ForMember(d => d.StockQuantity, opt => opt.MapFrom(s => s.StockQuantity))
                    .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.IsActive))
                    .ForMember(d => d.IsCustomizable, opt => opt.MapFrom(s => s.IsCustomizable))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                    .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt));

                cfg.CreateMap<DB.ProductImage, API.ProductImage>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkProducts, opt => opt.MapFrom(s => s.FkProducts))
                    .ForMember(d => d.ImageData, opt => opt.MapFrom(s => s.ImageData));

                cfg.CreateMap<DB.ProductOrder, API.ProductOrder>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkProducts, opt => opt.MapFrom(s => s.FkProducts))
                    .ForMember(d => d.FkCustomizableProducts, opt => opt.MapFrom(s => s.FkCustomizableProducts));

                cfg.CreateMap<DB.ProductTranslation, API.ProductTranslation>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkLanguages, opt => opt.MapFrom(s => s.FkLanguages))
                    .ForMember(d => d.FkProducts, opt => opt.MapFrom(s => s.FkProducts))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                    .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description));

                cfg.CreateMap<DB.Review, API.Review>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkProducts, opt => opt.MapFrom(s => s.FkProducts))
                    .ForMember(d => d.FkUsers, opt => opt.MapFrom(s => s.FkUsers))
                    .ForMember(d => d.Rating, opt => opt.MapFrom(s => s.Rating))
                    .ForMember(d => d.Comment, opt => opt.MapFrom(s => s.Comment))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt));

                cfg.CreateMap<DB.ShippingAddress, API.ShippingAddress>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkCountries, opt => opt.MapFrom(s => s.FkCountries))
                    .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                    .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
                    .ForMember(d => d.ZipPostCode, opt => opt.MapFrom(s => s.ZipPostCode))
                    .ForMember(d => d.StateProvince, opt => opt.MapFrom(s => s.StateProvince))
                    .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email));

                cfg.CreateMap<DB.SizeOption, API.SizeOption>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.Size, opt => opt.MapFrom(s => s.Size));

                cfg.CreateMap<DB.User, API.User>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkUserRoles, opt => opt.MapFrom(s => s.FkUserRoles))
                    .ForMember(d => d.Login, opt => opt.MapFrom(s => s.Login))
                    .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                    .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
                    .ForMember(d => d.ConfirmEmail, opt => opt.MapFrom(s => s.ConfirmEmail))
                    .ForMember(d => d.ConfirmationToken, opt => opt.MapFrom(s => s.ConfirmationToken))
                    .ForMember(d => d.ConfirmationTokenExpires, opt => opt.MapFrom(s => s.ConfirmationTokenExpires))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                    .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt));

                cfg.CreateMap<DB.UserOrderHistory, API.UserOrderHistory>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkOrders, opt => opt.MapFrom(s => s.FkOrders))
                    .ForMember(d => d.FkUsers, opt => opt.MapFrom(s => s.FkUsers));

                cfg.CreateMap<DB.UserProfile, API.UserProfile>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkUsers, opt => opt.MapFrom(s => s.FkUsers))
                    .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                    .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                    .ForMember(d => d.MiddleName, opt => opt.MapFrom(s => s.MiddleName))
                    .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
                    .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
                    .ForMember(d => d.ZipPostCode, opt => opt.MapFrom(s => s.ZipPostCode))
                    .ForMember(d => d.StateProvince, opt => opt.MapFrom(s => s.StateProvince))
                    .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Country))
                    .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.Phone))
                    .ForMember(d => d.Avatar, opt => opt.MapFrom(s => s.Avatar))
                    .ForMember(d => d.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt))
                    .ForMember(d => d.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt));

                cfg.CreateMap<DB.UserRole, API.UserRole>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                    .ForMember(d => d.FkAccessLevels, opt => opt.MapFrom(s => s.FkAccessLevels))
                    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

                #endregion
            }, loggerFactory);

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}