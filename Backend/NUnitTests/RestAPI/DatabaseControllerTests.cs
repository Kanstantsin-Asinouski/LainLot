using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnitTests.Classes;
using Moq;
using RestAPI.Controllers;
using DatabaseRepository.Classes;
using DatabaseRepository.Interfaces;
using DB = DatabaseProvider.Models;
using RestAPI.Models;

namespace NUnitTests.RestAPI
{
    public class DatabaseControllerTests
    {
        private Mock<ILogger<DatabaseController>> _logger;
        private Mock<ILogger<DB.LainLotContext>> _contextLogger;
        private DB.LainLotContext? _context;

        #region Mock loggers
        private Mock<ILogger<Repository<DB.About>>> _aboutLogger;
        private Mock<ILogger<Repository<DB.AccessLevel>>> _accessLevelLogger;
        private Mock<ILogger<Repository<DB.BaseBelt>>> _baseBeltLogger;
        private Mock<ILogger<Repository<DB.BaseNeckline>>> _baseNecklineLogger;
        private Mock<ILogger<Repository<DB.BasePant>>> _basePantLogger;
        private Mock<ILogger<Repository<DB.BasePantsCuff>>> _basePantsCuffLogger;
        private Mock<ILogger<Repository<DB.BaseSleeve>>> _baseSleeveLogger;
        private Mock<ILogger<Repository<DB.BaseSleeveCuff>>> _baseSleeveCuffLogger;
        private Mock<ILogger<Repository<DB.BaseSportSuit>>> _baseSportSuitLogger;
        private Mock<ILogger<Repository<DB.BaseSweater>>> _baseSweaterLogger;
        private Mock<ILogger<Repository<DB.Cart>>> _cartLogger;
        private Mock<ILogger<Repository<DB.Category>>> _categoryLogger;
        private Mock<ILogger<Repository<DB.CategoryHierarchy>>> _categoryHierarchyLogger;
        private Mock<ILogger<Repository<DB.Color>>> _colorLogger;
        private Mock<ILogger<Repository<DB.Contact>>> _contactLogger;
        private Mock<ILogger<Repository<DB.Country>>> _countryLogger;
        private Mock<ILogger<Repository<DB.Currency>>> _currencyLogger;
        private Mock<ILogger<Repository<DB.CustomBelt>>> _customBeltLogger;
        private Mock<ILogger<Repository<DB.CustomNeckline>>> _customNecklineLogger;
        private Mock<ILogger<Repository<DB.CustomPant>>> _customPantLogger;
        private Mock<ILogger<Repository<DB.CustomPantsCuff>>> _customPantsCuffLogger;
        private Mock<ILogger<Repository<DB.CustomSleeve>>> _customSleeveLogger;
        private Mock<ILogger<Repository<DB.CustomSleeveCuff>>> _customSleeveCuffLogger;
        private Mock<ILogger<Repository<DB.CustomSportSuit>>> _customSportSuitLogger;
        private Mock<ILogger<Repository<DB.CustomSweater>>> _customSweaterLogger;
        private Mock<ILogger<Repository<DB.CustomizableProduct>>> _customizableProductLogger;
        private Mock<ILogger<Repository<DB.FabricType>>> _fabricTypeLogger;
        private Mock<ILogger<Repository<DB.Language>>> _languageLogger;
        private Mock<ILogger<Repository<DB.Order>>> _orderLogger;
        private Mock<ILogger<Repository<DB.OrderHistory>>> _orderHistoryLogger;
        private Mock<ILogger<Repository<DB.OrderStatus>>> _orderStatusLogger;
        private Mock<ILogger<Repository<DB.Payment>>> _paymentLogger;
        private Mock<ILogger<Repository<DB.PaymentMethod>>> _paymentMethodLogger;
        private Mock<ILogger<Repository<DB.PaymentStatus>>> _paymentStatusLogger;
        private Mock<ILogger<Repository<DB.Product>>> _productLogger;
        private Mock<ILogger<Repository<DB.ProductImage>>> _productImageLogger;
        private Mock<ILogger<Repository<DB.ProductOrder>>> _productOrderLogger;
        private Mock<ILogger<Repository<DB.ProductTranslation>>> _productTranslationLogger;
        private Mock<ILogger<Repository<DB.Review>>> _reviewLogger;
        private Mock<ILogger<Repository<DB.ShippingAddress>>> _shippingAddressLogger;
        private Mock<ILogger<Repository<DB.SizeOption>>> _sizeOptionLogger;
        private Mock<ILogger<Repository<DB.User>>> _userLogger;
        private Mock<ILogger<Repository<DB.UserOrderHistory>>> _userOrderHistoryLogger;
        private Mock<ILogger<Repository<DB.UserProfile>>> _userProfileLogger;
        private Mock<ILogger<Repository<DB.UserRole>>> _userRoleLogger;
        #endregion

        #region Repositories
        private IRepository<DB.About>? _aboutRepository;
        private IRepository<DB.AccessLevel>? _accessLevelRepository;
        private IRepository<DB.BaseBelt>? _baseBeltRepository;
        private IRepository<DB.BaseNeckline>? _baseNecklineRepository;
        private IRepository<DB.BasePant>? _basePantRepository;
        private IRepository<DB.BasePantsCuff>? _basePantsCuffRepository;
        private IRepository<DB.BaseSleeve>? _baseSleefeRepository;
        private IRepository<DB.BaseSleeveCuff>? _baseSleeveCuffRepository;
        private IRepository<DB.BaseSportSuit>? _baseSportSuitRepository;
        private IRepository<DB.BaseSweater>? _baseSweaterRepository;
        private IRepository<DB.Cart>? _cartRepository;
        private IRepository<DB.Category>? _categoryRepository;
        private IRepository<DB.CategoryHierarchy>? _categoryHierarchyRepository;
        private IRepository<DB.Color>? _colorRepository;
        private IRepository<DB.Contact>? _contactRepository;
        private IRepository<DB.Country>? _countryRepository;
        private IRepository<DB.Currency>? _currencyRepository;
        private IRepository<DB.CustomBelt>? _customBeltRepository;
        private IRepository<DB.CustomNeckline>? _customNecklineRepository;
        private IRepository<DB.CustomPant>? _customPantRepository;
        private IRepository<DB.CustomPantsCuff>? _customPantsCuffRepository;
        private IRepository<DB.CustomSleeve>? _customSleefeRepository;
        private IRepository<DB.CustomSleeveCuff>? _customSleeveCuffRepository;
        private IRepository<DB.CustomSportSuit>? _customSportSuitRepository;
        private IRepository<DB.CustomSweater>? _customSweaterRepository;
        private IRepository<DB.CustomizableProduct>? _customizableProductRepository;
        private IRepository<DB.FabricType>? _fabricTypeRepository;
        private IRepository<DB.Language>? _languageRepository;
        private IRepository<DB.Order>? _orderRepository;
        private IRepository<DB.OrderHistory>? _orderHistoryRepository;
        private IRepository<DB.OrderStatus>? _orderStatusRepository;
        private IRepository<DB.Payment>? _paymentRepository;
        private IRepository<DB.PaymentMethod>? _paymentMethodRepository;
        private IRepository<DB.PaymentStatus>? _paymentStatusRepository;
        private IRepository<DB.Product>? _productRepository;
        private IRepository<DB.ProductImage>? _productImageRepository;
        private IRepository<DB.ProductOrder>? _productOrderRepository;
        private IRepository<DB.ProductTranslation>? _productTranslationRepository;
        private IRepository<DB.Review>? _reviewRepository;
        private IRepository<DB.ShippingAddress>? _shippingAddressRepository;
        private IRepository<DB.SizeOption>? _sizeOptionRepository;
        private IRepository<DB.User>? _userRepository;
        private IRepository<DB.UserOrderHistory>? _userOrderHistoryRepository;
        private IRepository<DB.UserProfile>? _userProfileRepository;
        private IRepository<DB.UserRole>? _userRoleRepository;
        #endregion

        private DatabaseController _restApiController;

        private int _limit;
        private int _page;

        [SetUp]
        public void Setup()
        {
            _contextLogger = new Mock<ILogger<DB.LainLotContext>>();

            var options = new DbContextOptionsBuilder<DB.LainLotContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            // Create a fake DbContext
            _context = new DB.LainLotContext(options, _contextLogger.Object);

            #region Create fake loggers
            _aboutLogger = new Mock<ILogger<Repository<DB.About>>>();
            _accessLevelLogger = new Mock<ILogger<Repository<DB.AccessLevel>>>();
            _baseBeltLogger = new Mock<ILogger<Repository<DB.BaseBelt>>>();
            _baseNecklineLogger = new Mock<ILogger<Repository<DB.BaseNeckline>>>();
            _basePantLogger = new Mock<ILogger<Repository<DB.BasePant>>>();
            _basePantsCuffLogger = new Mock<ILogger<Repository<DB.BasePantsCuff>>>();
            _baseSleeveLogger = new Mock<ILogger<Repository<DB.BaseSleeve>>>();
            _baseSleeveCuffLogger = new Mock<ILogger<Repository<DB.BaseSleeveCuff>>>();
            _baseSportSuitLogger = new Mock<ILogger<Repository<DB.BaseSportSuit>>>();
            _baseSweaterLogger = new Mock<ILogger<Repository<DB.BaseSweater>>>();
            _cartLogger = new Mock<ILogger<Repository<DB.Cart>>>();
            _categoryLogger = new Mock<ILogger<Repository<DB.Category>>>();
            _categoryHierarchyLogger = new Mock<ILogger<Repository<DB.CategoryHierarchy>>>();
            _colorLogger = new Mock<ILogger<Repository<DB.Color>>>();
            _contactLogger = new Mock<ILogger<Repository<DB.Contact>>>();
            _countryLogger = new Mock<ILogger<Repository<DB.Country>>>();
            _currencyLogger = new Mock<ILogger<Repository<DB.Currency>>>();
            _customBeltLogger = new Mock<ILogger<Repository<DB.CustomBelt>>>();
            _customNecklineLogger = new Mock<ILogger<Repository<DB.CustomNeckline>>>();
            _customPantLogger = new Mock<ILogger<Repository<DB.CustomPant>>>();
            _customPantsCuffLogger = new Mock<ILogger<Repository<DB.CustomPantsCuff>>>();
            _customSleeveLogger = new Mock<ILogger<Repository<DB.CustomSleeve>>>();
            _customSleeveCuffLogger = new Mock<ILogger<Repository<DB.CustomSleeveCuff>>>();
            _customSportSuitLogger = new Mock<ILogger<Repository<DB.CustomSportSuit>>>();
            _customSweaterLogger = new Mock<ILogger<Repository<DB.CustomSweater>>>();
            _customizableProductLogger = new Mock<ILogger<Repository<DB.CustomizableProduct>>>();
            _fabricTypeLogger = new Mock<ILogger<Repository<DB.FabricType>>>();
            _languageLogger = new Mock<ILogger<Repository<DB.Language>>>();
            _orderLogger = new Mock<ILogger<Repository<DB.Order>>>();
            _orderHistoryLogger = new Mock<ILogger<Repository<DB.OrderHistory>>>();
            _orderStatusLogger = new Mock<ILogger<Repository<DB.OrderStatus>>>();
            _paymentLogger = new Mock<ILogger<Repository<DB.Payment>>>();
            _paymentMethodLogger = new Mock<ILogger<Repository<DB.PaymentMethod>>>();
            _paymentStatusLogger = new Mock<ILogger<Repository<DB.PaymentStatus>>>();
            _productLogger = new Mock<ILogger<Repository<DB.Product>>>();
            _productImageLogger = new Mock<ILogger<Repository<DB.ProductImage>>>();
            _productOrderLogger = new Mock<ILogger<Repository<DB.ProductOrder>>>();
            _productTranslationLogger = new Mock<ILogger<Repository<DB.ProductTranslation>>>();
            _reviewLogger = new Mock<ILogger<Repository<DB.Review>>>();
            _shippingAddressLogger = new Mock<ILogger<Repository<DB.ShippingAddress>>>();
            _sizeOptionLogger = new Mock<ILogger<Repository<DB.SizeOption>>>();
            _userLogger = new Mock<ILogger<Repository<DB.User>>>();
            _userOrderHistoryLogger = new Mock<ILogger<Repository<DB.UserOrderHistory>>>();
            _userProfileLogger = new Mock<ILogger<Repository<DB.UserProfile>>>();
            _userRoleLogger = new Mock<ILogger<Repository<DB.UserRole>>>();
            #endregion

            #region Create fake data
            var abouts = DatabaseDataFake.GetFakeAboutList();
            var accessLevels = DatabaseDataFake.GetFakeAccessLevelList();
            var baseBelts = DatabaseDataFake.GetFakeBaseBeltList();
            var baseNecklines = DatabaseDataFake.GetFakeBaseNecklineList();
            var basePants = DatabaseDataFake.GetFakeBasePantList();
            var basePantsCuffs = DatabaseDataFake.GetFakeBasePantsCuffList();
            var baseSleefes = DatabaseDataFake.GetFakeBaseSleeveList();
            var baseSleeveCuffs = DatabaseDataFake.GetFakeBaseSleeveCuffList();
            var baseSportSuits = DatabaseDataFake.GetFakeBaseSportSuitList();
            var baseSweaters = DatabaseDataFake.GetFakeBaseSweaterList();
            var carts = DatabaseDataFake.GetFakeCartsList();
            var categories = DatabaseDataFake.GetFakeCategoryList();
            var categoryHierarchies = DatabaseDataFake.GetFakeCategoryHierarchyList();
            var colors = DatabaseDataFake.GetFakeColorList();
            var contacts = DatabaseDataFake.GetFakeContactList();
            var countries = DatabaseDataFake.GetFakeCountryList();
            var currencies = DatabaseDataFake.GetFakeCurrencyList();
            var customBelts = DatabaseDataFake.GetFakeCustomBeltList();
            var customNecklines = DatabaseDataFake.GetFakeCustomNecklineList();
            var customPants = DatabaseDataFake.GetFakeCustomPantList();
            var customPantsCuffs = DatabaseDataFake.GetFakeCustomPantsCuffList();
            var customSleefes = DatabaseDataFake.GetFakeCustomSleeveList();
            var customSleeveCuffs = DatabaseDataFake.GetFakeCustomSleeveCuffList();
            var customSportSuits = DatabaseDataFake.GetFakeCustomSportSuitList();
            var customSweaters = DatabaseDataFake.GetFakeCustomSweaterList();
            var customizableProducts = DatabaseDataFake.GetFakeCustomizableProductList();
            var fabricTypes = DatabaseDataFake.GetFakeFabricTypeList();
            var languages = DatabaseDataFake.GetFakeLanguageList();
            var orders = DatabaseDataFake.GetFakeOrderList();
            var orderHistories = DatabaseDataFake.GetFakeOrderHistoryList();
            var orderStatuses = DatabaseDataFake.GetFakeOrderStatusList();
            var payments = DatabaseDataFake.GetFakePaymentList();
            var paymentMethods = DatabaseDataFake.GetFakePaymentMethodList();
            var paymentStatuses = DatabaseDataFake.GetFakePaymentStatusList();
            var products = DatabaseDataFake.GetFakeProductList();
            var productImages = DatabaseDataFake.GetFakeProductImageList();
            var productOrders = DatabaseDataFake.GetFakeProductOrderList();
            var productTranslations = DatabaseDataFake.GetFakeProductTranslationList();
            var reviews = DatabaseDataFake.GetFakeReviewList();
            var shippingAddresses = DatabaseDataFake.GetFakeShippingAddressList();
            var sizeOptions = DatabaseDataFake.GetFakeSizeOptionList();
            var users = DatabaseDataFake.GetFakeUserList();
            var userOrderHistory = DatabaseDataFake.GetFakeUserOrderHistoryList();
            var userProfiles = DatabaseDataFake.GetFakeUserProfileList();
            var userRoles = DatabaseDataFake.GetFakeUserRoleList();
            #endregion

            #region Init base data in fake DbContext
            _context.Abouts.AddRange(abouts);
            _context.AccessLevels.AddRange(accessLevels);
            _context.BaseBelts.AddRange(baseBelts);
            _context.BaseNecklines.AddRange(baseNecklines);
            _context.BasePants.AddRange(basePants);
            _context.BasePantsCuffs.AddRange(basePantsCuffs);
            _context.BaseSleeves.AddRange(baseSleefes);
            _context.BaseSleeveCuffs.AddRange(baseSleeveCuffs);
            _context.BaseSportSuits.AddRange(baseSportSuits);
            _context.BaseSweaters.AddRange(baseSweaters);
            _context.Carts.AddRange(carts);
            _context.Categories.AddRange(categories);
            _context.CategoryHierarchies.AddRange(categoryHierarchies);
            _context.Colors.AddRange(colors);
            _context.Contacts.AddRange(contacts);
            _context.Countries.AddRange(countries);
            _context.Currencies.AddRange(currencies);
            _context.CustomBelts.AddRange(customBelts);
            _context.CustomNecklines.AddRange(customNecklines);
            _context.CustomPants.AddRange(customPants);
            _context.CustomPantsCuffs.AddRange(customPantsCuffs);
            _context.CustomSleeves.AddRange(customSleefes);
            _context.CustomSleeveCuffs.AddRange(customSleeveCuffs);
            _context.CustomSportSuits.AddRange(customSportSuits);
            _context.CustomSweaters.AddRange(customSweaters);
            _context.CustomizableProducts.AddRange(customizableProducts);
            _context.FabricTypes.AddRange(fabricTypes);
            _context.Languages.AddRange(languages);
            _context.Orders.AddRange(orders);
            _context.OrderHistories.AddRange(orderHistories);
            _context.OrderStatuses.AddRange(orderStatuses);
            _context.Payments.AddRange(payments);
            _context.PaymentMethods.AddRange(paymentMethods);
            _context.PaymentStatuses.AddRange(paymentStatuses);
            _context.Products.AddRange(products);
            _context.ProductImages.AddRange(productImages);
            _context.ProductOrders.AddRange(productOrders);
            _context.ProductTranslations.AddRange(productTranslations);
            _context.Reviews.AddRange(reviews);
            _context.ShippingAddresses.AddRange(shippingAddresses);
            _context.SizeOptions.AddRange(sizeOptions);
            _context.Users.AddRange(users);
            _context.UserOrderHistories.AddRange(userOrderHistory);
            _context.UserProfiles.AddRange(userProfiles);
            _context.UserRoles.AddRange(userRoles);
            #endregion

            // Save data in fake DbContext
            _context.SaveChanges();

            _logger = new Mock<ILogger<DatabaseController>>();
            #region Create all instances for repositories
            _aboutRepository = new Repository<DB.About>(_context, _aboutLogger.Object);
            _accessLevelRepository = new Repository<DB.AccessLevel>(_context, _accessLevelLogger.Object);
            _baseBeltRepository = new Repository<DB.BaseBelt>(_context, _baseBeltLogger.Object);
            _baseNecklineRepository = new Repository<DB.BaseNeckline>(_context, _baseNecklineLogger.Object);
            _basePantRepository = new Repository<DB.BasePant>(_context, _basePantLogger.Object);
            _basePantsCuffRepository = new Repository<DB.BasePantsCuff>(_context, _basePantsCuffLogger.Object);
            _baseSleefeRepository = new Repository<DB.BaseSleeve>(_context, _baseSleeveLogger.Object);
            _baseSleeveCuffRepository = new Repository<DB.BaseSleeveCuff>(_context, _baseSleeveCuffLogger.Object);
            _baseSportSuitRepository = new Repository<DB.BaseSportSuit>(_context, _baseSportSuitLogger.Object);
            _baseSweaterRepository = new Repository<DB.BaseSweater>(_context, _baseSweaterLogger.Object);
            _cartRepository = new Repository<DB.Cart>(_context, _cartLogger.Object);
            _categoryRepository = new Repository<DB.Category>(_context, _categoryLogger.Object);
            _categoryHierarchyRepository = new Repository<DB.CategoryHierarchy>(_context, _categoryHierarchyLogger.Object);
            _colorRepository = new Repository<DB.Color>(_context, _colorLogger.Object);
            _contactRepository = new Repository<DB.Contact>(_context, _contactLogger.Object);
            _countryRepository = new Repository<DB.Country>(_context, _countryLogger.Object);
            _currencyRepository = new Repository<DB.Currency>(_context, _currencyLogger.Object);
            _customBeltRepository = new Repository<DB.CustomBelt>(_context, _customBeltLogger.Object);
            _customNecklineRepository = new Repository<DB.CustomNeckline>(_context, _customNecklineLogger.Object);
            _customPantRepository = new Repository<DB.CustomPant>(_context, _customPantLogger.Object);
            _customPantsCuffRepository = new Repository<DB.CustomPantsCuff>(_context, _customPantsCuffLogger.Object);
            _customSleefeRepository = new Repository<DB.CustomSleeve>(_context, _customSleeveLogger.Object);
            _customSleeveCuffRepository = new Repository<DB.CustomSleeveCuff>(_context, _customSleeveCuffLogger.Object);
            _customSportSuitRepository = new Repository<DB.CustomSportSuit>(_context, _customSportSuitLogger.Object);
            _customSweaterRepository = new Repository<DB.CustomSweater>(_context, _customSweaterLogger.Object);
            _customizableProductRepository = new Repository<DB.CustomizableProduct>(_context, _customizableProductLogger.Object);
            _fabricTypeRepository = new Repository<DB.FabricType>(_context, _fabricTypeLogger.Object);
            _languageRepository = new Repository<DB.Language>(_context, _languageLogger.Object);
            _orderRepository = new Repository<DB.Order>(_context, _orderLogger.Object);
            _orderHistoryRepository = new Repository<DB.OrderHistory>(_context, _orderHistoryLogger.Object);
            _orderStatusRepository = new Repository<DB.OrderStatus>(_context, _orderStatusLogger.Object);
            _paymentRepository = new Repository<DB.Payment>(_context, _paymentLogger.Object);
            _paymentMethodRepository = new Repository<DB.PaymentMethod>(_context, _paymentMethodLogger.Object);
            _paymentStatusRepository = new Repository<DB.PaymentStatus>(_context, _paymentStatusLogger.Object);
            _productRepository = new Repository<DB.Product>(_context, _productLogger.Object);
            _productImageRepository = new Repository<DB.ProductImage>(_context, _productImageLogger.Object);
            _productOrderRepository = new Repository<DB.ProductOrder>(_context, _productOrderLogger.Object);
            _productTranslationRepository = new Repository<DB.ProductTranslation>(_context, _productTranslationLogger.Object);
            _reviewRepository = new Repository<DB.Review>(_context, _reviewLogger.Object);
            _shippingAddressRepository = new Repository<DB.ShippingAddress>(_context, _shippingAddressLogger.Object);
            _sizeOptionRepository = new Repository<DB.SizeOption>(_context, _sizeOptionLogger.Object);
            _userRepository = new Repository<DB.User>(_context, _userLogger.Object);
            _userOrderHistoryRepository = new Repository<DB.UserOrderHistory>(_context, _userOrderHistoryLogger.Object);
            _userProfileRepository = new Repository<DB.UserProfile>(_context, _userProfileLogger.Object);
            _userRoleRepository = new Repository<DB.UserRole>(_context, _userRoleLogger.Object);
            #endregion

            _restApiController = new DatabaseController(
                _logger.Object,
                _aboutRepository,
                _accessLevelRepository,
                _baseBeltRepository,
                _baseNecklineRepository,
                _basePantRepository,
                _basePantsCuffRepository,
                _baseSleefeRepository,
                _baseSleeveCuffRepository,
                _baseSportSuitRepository,
                _baseSweaterRepository,
                _cartRepository,
                _categoryRepository,
                _categoryHierarchyRepository,
                _colorRepository,
                _contactRepository,
                _countryRepository,
                _currencyRepository,
                _customBeltRepository,
                _customNecklineRepository,
                _customPantRepository,
                _customPantsCuffRepository,
                _customSleefeRepository,
                _customSleeveCuffRepository,
                _customSportSuitRepository,
                _customSweaterRepository,
                _customizableProductRepository,
                _fabricTypeRepository,
                _languageRepository,
                _orderRepository,
                _orderHistoryRepository,
                _orderStatusRepository,
                _paymentRepository,
                _paymentMethodRepository,
                _paymentStatusRepository,
                _productRepository,
                _productImageRepository,
                _productOrderRepository,
                _productTranslationRepository,
                _reviewRepository,
                _shippingAddressRepository,
                _sizeOptionRepository,
                _userRepository,
                _userOrderHistoryRepository,
                _userProfileRepository,
                _userRoleRepository);

            _limit = 100;
            _page = 1;
        }


        [TearDown]
        public void FinishTest()
        {
            _context?.Database.EnsureDeleted();
            _context?.ChangeTracker.Clear();
            _context?.Dispose();
        }

        #region About table

        [Test]
        public void GetAbouts_Return_2_Items()
        {
            var result = _restApiController.GetAbout(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<About>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetAboutById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetAboutById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_About_Entity(int id)
        {
            var result = await _restApiController.DeleteAbout(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_About_Entity()
        {
            var entity = new About()
            {
                Id = 3,
                FkLanguages = 1,
                Header = "Header 3",
                Text = "Text 3"
            };

            var result = await _restApiController.CreateAbout(entity);

            var list = _restApiController.GetAbout(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetAboutById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<About>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.FkLanguages, Is.EqualTo(entity.FkLanguages));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_About_Entity(int id)
        {
            var entity = await _restApiController.GetAboutById(id);

            entity.Value.Text = "Text 3";

            await _restApiController.UpdateAbout(entity.Value);

            var updateEntity = await _restApiController.GetAboutById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Text, Is.EqualTo(entity.Value.Text));
                Assert.That(entity?.Value?.Text, Is.EqualTo(updateEntity?.Value?.Text));
            });
        }

        #endregion

        #region AccessLevels table

        [Test]
        public void GetAccessLevels_Return_2_Items()
        {
            var result = _restApiController.GetAccessLevels(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<AccessLevel>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetAccessLevelById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetAccessLevelsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_AccessLevel_Entity(int id)
        {
            var result = await _restApiController.DeleteAccessLevels(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_AccessLevel_Entity()
        {
            var entity = new AccessLevel()
            {
                Id = 3,
                Description = "Description 3",
                Level = 3
            };

            var result = await _restApiController.CreateAccessLevels(entity);

            var list = _restApiController.GetAccessLevels(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetAccessLevelsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<AccessLevel>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Level, Is.EqualTo(entity.Level));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_AccessLevel_Entity(int id)
        {
            var entity = await _restApiController.GetAccessLevelsById(id);

            entity.Value.Description = "Text 3";

            await _restApiController.UpdateAccessLevels(entity.Value);

            var updateEntity = await _restApiController.GetAccessLevelsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Description, Is.EqualTo(entity.Value.Description));
                Assert.That(entity?.Value?.Description, Is.EqualTo(updateEntity?.Value?.Description));
            });
        }

        #endregion

        #region Basebelts table

        [Test]
        public void GetBaseBelts_Return_2_Items()
        {
            var result = _restApiController.GetBaseBelts(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<BaseBelt>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetBaseBeltsById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetBaseBeltsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_BaseBelts_Entity(int id)
        {
            var result = await _restApiController.DeleteBaseBelts(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_BaseBelts_Entity()
        {
            var entity = new BaseBelt()
            {
                Id = 3,
                Settings = "Settings 3"
            };

            var result = await _restApiController.CreateBaseBelts(entity);

            var list = _restApiController.GetBaseBelts(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetBaseBeltsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<BaseBelt>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Settings, Is.EqualTo(entity.Settings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_BaseBelts_Entity(int id)
        {
            var entity = await _restApiController.GetBaseBeltsById(id);

            entity.Value.Settings = "Updated Settings";

            await _restApiController.UpdateBaseBelts(entity.Value);

            var updatedEntity = await _restApiController.GetBaseBeltsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Settings, Is.EqualTo(entity.Value.Settings));
                Assert.That(entity?.Value?.Settings, Is.EqualTo(updatedEntity?.Value?.Settings));
            });
        }

        #endregion

        #region BaseNecklines table

        [Test]
        public void GetBaseNecklines_Return_2_Items()
        {
            var result = _restApiController.GetBaseNecklines(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<BaseNeckline>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetBaseNecklinesById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetBaseNecklinesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_BaseNecklines_Entity(int id)
        {
            var result = await _restApiController.DeleteBaseNecklines(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_BaseNecklines_Entity()
        {
            var entity = new BaseNeckline()
            {
                Id = 3,
                Settings = "Settings for 3"
            };

            var result = await _restApiController.CreateBaseNecklines(entity);

            var list = _restApiController.GetBaseNecklines(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetBaseNecklinesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<BaseNeckline>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Settings, Is.EqualTo(entity.Settings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_BaseNecklines_Entity(int id)
        {
            var entity = await _restApiController.GetBaseNecklinesById(id);

            entity.Value.Settings = "Updated Settings";

            await _restApiController.UpdateBaseNecklines(entity.Value);

            var updatedEntity = await _restApiController.GetBaseNecklinesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Settings, Is.EqualTo(entity.Value.Settings));
                Assert.That(entity?.Value?.Settings, Is.EqualTo(updatedEntity?.Value?.Settings));
            });
        }

        #endregion

        #region BasePants table

        [Test]
        public void GetBasePants_Return_2_Items()
        {
            var result = _restApiController.GetBasePants(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<BasePant>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetBasePantsById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetBasePantsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_BasePants_Entity(int id)
        {
            var result = await _restApiController.DeleteBasePants(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_BasePants_Entity()
        {
            var entity = new BasePant()
            {
                Id = 3,
                Settings = "Settings 3"
            };

            var result = await _restApiController.CreateBasePants(entity);

            var list = _restApiController.GetBasePants(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetBasePantsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<BasePant>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Settings, Is.EqualTo(entity.Settings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_BasePants_Entity(int id)
        {
            var entity = await _restApiController.GetBasePantsById(id);

            entity.Value.Settings = "Updated Settings";

            await _restApiController.UpdateBasePants(entity.Value);

            var updatedEntity = await _restApiController.GetBasePantsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Settings, Is.EqualTo(entity.Value.Settings));
                Assert.That(entity?.Value?.Settings, Is.EqualTo(updatedEntity?.Value?.Settings));
            });
        }

        #endregion

        #region BasePantsCuffs table

        [Test]
        public void GetBasePantsCuffs_Return_2_Items()
        {
            var result = _restApiController.GetBasePantsCuffs(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<BasePantsCuff>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetBasePantsCuffsById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetBasePantsCuffsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_BasePantsCuffs_Entity(int id)
        {
            var result = await _restApiController.DeleteBasePantsCuffs(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_BasePantsCuffs_Entity()
        {
            var entity = new BasePantsCuff()
            {
                Id = 3,
                Settings = "Settings 3"
            };

            var result = await _restApiController.CreateBasePantsCuffs(entity);

            var list = _restApiController.GetBasePantsCuffs(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetBasePantsCuffsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<BasePantsCuff>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Settings, Is.EqualTo(entity.Settings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_BasePantsCuffs_Entity(int id)
        {
            var entity = await _restApiController.GetBasePantsCuffsById(id);

            entity.Value.Settings = "Updated Settings";

            await _restApiController.UpdateBasePantsCuffs(entity.Value);

            var updatedEntity = await _restApiController.GetBasePantsCuffsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Settings, Is.EqualTo("Updated Settings"));
                Assert.That(entity?.Value?.Settings, Is.EqualTo("Updated Settings"));
            });
        }

        #endregion

        #region BaseSleeves table

        [Test]
        public void GetBaseSleeves_Return_2_Items()
        {
            var result = _restApiController.GetBaseSleeves(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<BaseSleeve>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetBaseSleevesById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetBaseSleevesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_BaseSleeves_Entity(int id)
        {
            var result = await _restApiController.DeleteBaseSleeves(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_BaseSleeves_Entity()
        {
            var entity = new BaseSleeve()
            {
                Id = 3,
                Settings = "Settings 3"
            };

            var result = await _restApiController.CreateBaseSleeves(entity);

            var list = _restApiController.GetBaseSleeves(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetBaseSleevesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<BaseSleeve>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Settings, Is.EqualTo(entity.Settings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_BaseSleeves_Entity(int id)
        {
            var entity = await _restApiController.GetBaseSleevesById(id);

            entity.Value.Settings = "Updated Settings";

            await _restApiController.UpdateBaseSleeves(entity.Value);

            var updatedEntity = await _restApiController.GetBaseSleevesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Settings, Is.EqualTo("Updated Settings"));
                Assert.That(entity?.Value?.Settings, Is.EqualTo("Updated Settings"));
            });
        }

        #endregion

        #region BaseSleeveCuffs table

        [Test]
        public void GetBaseSleeveCuffs_Return_2_Items()
        {
            var result = _restApiController.GetBaseSleeveCuffs(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<BaseSleeveCuff>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetBaseSleeveCuffsById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetBaseSleeveCuffsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_BaseSleeveCuffs_Entity(int id)
        {
            var result = await _restApiController.DeleteBaseSleeveCuffs(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_BaseSleeveCuffs_Entity()
        {
            var entity = new BaseSleeveCuff()
            {
                Id = 3,
                Settings = "Settings 3"
            };

            var result = await _restApiController.CreateBaseSleeveCuffs(entity);

            var list = _restApiController.GetBaseSleeveCuffs(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetBaseSleeveCuffsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<BaseSleeveCuff>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Settings, Is.EqualTo(entity.Settings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_BaseSleeveCuffs_Entity(int id)
        {
            var entity = await _restApiController.GetBaseSleeveCuffsById(id);

            entity.Value.Settings = "Updated Settings";

            await _restApiController.UpdateBaseSleeveCuffs(entity.Value);

            var updatedEntity = await _restApiController.GetBaseSleeveCuffsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Settings, Is.EqualTo("Updated Settings"));
                Assert.That(entity?.Value?.Settings, Is.EqualTo("Updated Settings"));
            });
        }

        #endregion

        #region BaseSportSuits table

        [Test]
        public void GetBaseSportSuits_Return_2_Items()
        {
            var result = _restApiController.GetBaseSportSuits(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<BaseSportSuit>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetBaseSportSuitById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetBaseSportSuitsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_BaseSportSuit_Entity(int id)
        {
            var result = await _restApiController.DeleteBaseSportSuits(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_BaseSportSuit_Entity()
        {
            var entity = new BaseSportSuit()
            {
                Id = 3,
                FkBaseNecklines = 1,
                FkBaseSweaters = 1,
                FkBaseSleeves = 1,
                FkBaseSleeveCuffsLeft = 1,
                FkBaseSleeveCuffsRight = 2,
                FkBaseBelts = 1,
                FkBasePants = 1,
                FkBasePantsCuffsLeft = 1,
                FkBasePantsCuffsRight = 2
            };

            var result = await _restApiController.CreateBaseSportSuits(entity);

            var list = _restApiController.GetBaseSportSuits(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetBaseSportSuitsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<BaseSportSuit>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Id, Is.EqualTo(entity.Id));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_BaseSportSuit_Entity(int id)
        {
            var entity = await _restApiController.GetBaseSportSuitsById(id);

            entity.Value.FkBaseSweaters = 2;

            await _restApiController.UpdateBaseSportSuits(entity.Value);

            var updatedEntity = await _restApiController.GetBaseSportSuitsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity, Is.Not.Null);
                Assert.That(updatedEntity.Value.FkBaseSweaters, Is.EqualTo(entity.Value.FkBaseSweaters));
            });
        }

        #endregion

        #region BaseSweaters table

        [Test]
        public void GetBaseSweaters_Return_2_Items()
        {
            var result = _restApiController.GetBaseSweaters(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<BaseSweater>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetBaseSweatersById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetBaseSweatersById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_BaseSweaters_Entity(int id)
        {
            var result = await _restApiController.DeleteBaseSweaters(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_BaseSweaters_Entity()
        {
            var entity = new BaseSweater()
            {
                Id = 3,
                Settings = "Settings 3"
            };

            var result = await _restApiController.CreateBaseSweaters(entity);

            var list = _restApiController.GetBaseSweaters(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetBaseSweatersById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<BaseSweater>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Settings, Is.EqualTo(entity.Settings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_BaseSweaters_Entity(int id)
        {
            var entity = await _restApiController.GetBaseSweatersById(id);

            entity.Value.Settings = "Updated Settings";

            await _restApiController.UpdateBaseSweaters(entity.Value);

            var updatedEntity = await _restApiController.GetBaseSweatersById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Settings, Is.EqualTo("Updated Settings"));
                Assert.That(entity?.Value?.Settings, Is.EqualTo("Updated Settings"));
            });
        }

        #endregion

        #region Cart table

        [Test]
        public void GetCarts_Return_2_Items()
        {
            var result = _restApiController.GetCart(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<Cart>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCartById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCartById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_Cart_Entity(int id)
        {
            var result = await _restApiController.DeleteCart(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_Cart_Entity()
        {
            var entity = new Cart()
            {
                Id = 3,
                FkProductOrders = 1,
                FkCurrencies = 1,
                Price = 100,
                Amount = 10,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _restApiController.CreateCart(entity);

            var list = _restApiController.GetCart(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCartById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<Cart>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Amount, Is.EqualTo(entity.Amount));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_Cart_Entity(int id)
        {
            var entity = await _restApiController.GetCartById(id);

            entity.Value.Amount = 5;

            await _restApiController.UpdateCart(entity.Value);

            var updateEntity = await _restApiController.GetCartById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Amount, Is.EqualTo(entity.Value.Amount));
                Assert.That(entity?.Value?.Amount, Is.EqualTo(updateEntity?.Value?.Amount));
            });
        }

        #endregion

        #region Categories table

        [Test]
        public void GetCategories_Return_2_Items()
        {
            var result = _restApiController.GetCategories(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<Category>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCategoryById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCategoriesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_Category_Entity(int id)
        {
            var result = await _restApiController.DeleteCategories(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_Category_Entity()
        {
            var entity = new Category()
            {
                Id = 3,
                FkLanguages = 1,
                Name = "New Category",
                Description = "Category Description"
            };

            var result = await _restApiController.CreateCategories(entity);

            var list = _restApiController.GetCategories(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCategoriesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<Category>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Name, Is.EqualTo(entity.Name));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_Category_Entity(int id)
        {
            var entity = await _restApiController.GetCategoriesById(id);

            entity.Value.Name = "Updated Name";

            await _restApiController.UpdateCategories(entity.Value);

            var updateEntity = await _restApiController.GetCategoriesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Name, Is.EqualTo(entity.Value.Name));
                Assert.That(entity?.Value?.Name, Is.EqualTo(updateEntity?.Value?.Name));
            });
        }

        #endregion

        #region CategoryHierarchy table

        [Test]
        public void GetCategoryHierarchies_Return_2_Items()
        {
            var result = _restApiController.GetCategoryHierarchy(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<CategoryHierarchy>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCategoryHierarchyById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCategoryHierarchyById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_CategoryHierarchy_Entity(int id)
        {
            var result = await _restApiController.DeleteCategoryHierarchy(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_CategoryHierarchy_Entity()
        {
            var entity = new CategoryHierarchy()
            {
                Id = 3,
                ParentId = 1,
                FkCategories = 2
            };

            var result = await _restApiController.CreateCategoryHierarchy(entity);

            var list = _restApiController.GetCategoryHierarchy(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCategoryHierarchyById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<CategoryHierarchy>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.FkCategories, Is.EqualTo(entity.FkCategories));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_CategoryHierarchy_Entity(int id)
        {
            var entity = await _restApiController.GetCategoryHierarchyById(id);

            entity.Value.ParentId = 2;

            await _restApiController.UpdateCategoryHierarchy(entity.Value);

            var updateEntity = await _restApiController.GetCategoryHierarchyById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.ParentId, Is.EqualTo(entity.Value.ParentId));
                Assert.That(entity?.Value?.ParentId, Is.EqualTo(updateEntity?.Value?.ParentId));
            });
        }

        #endregion

        #region Colors table

        [Test]
        public void GetColors_Return_2_Items()
        {
            var result = _restApiController.GetColors(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<Color>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetColorById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetColorsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_Color_Entity(int id)
        {
            var result = await _restApiController.DeleteColors(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_Color_Entity()
        {
            var entity = new Color()
            {
                Id = 3,
                Name = "Purple",
                ImageData = Encoding.ASCII.GetBytes("https://example.com/image1.jpg")
            };

            var result = await _restApiController.CreateColors(entity);

            var list = _restApiController.GetColors(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetColorsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<Color>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Name, Is.EqualTo(entity.Name));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_Color_Entity(int id)
        {
            var entity = await _restApiController.GetColorsById(id);

            entity.Value.Name = "Updated Name";

            await _restApiController.UpdateColors(entity.Value);

            var updateEntity = await _restApiController.GetColorsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Name, Is.EqualTo(entity.Value.Name));
                Assert.That(entity?.Value?.Name, Is.EqualTo(updateEntity?.Value?.Name));
            });
        }

        #endregion

        #region Contacts table

        [Test]
        public void GetContacts_Return_2_Items()
        {
            var result = _restApiController.GetContacts(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<Contact>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetContactById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetContactsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_Contact_Entity(int id)
        {
            var result = await _restApiController.DeleteContacts(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_Contact_Entity()
        {
            var entity = new Contact()
            {
                Id = 3,
                Address = "Address 3",
                Email = "Email 3",
                FkLanguages = 1,
                Phone = "000-000-000"
            };

            var result = await _restApiController.CreateContacts(entity);

            var list = _restApiController.GetContacts(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetContactsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<Contact>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.FkLanguages, Is.EqualTo(entity.FkLanguages));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_Contact_Entity(int id)
        {
            var entity = await _restApiController.GetContactsById(id);

            entity.Value.Email = "Text 3";

            await _restApiController.UpdateContacts(entity.Value);

            var updateEntity = await _restApiController.GetContactsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Email, Is.EqualTo(entity.Value.Email));
                Assert.That(entity?.Value?.Email, Is.EqualTo(updateEntity?.Value?.Email));
            });
        }

        #endregion

        #region Countries tables

        [Test]
        public void GetCountries_Return_2_Items()
        {
            var result = _restApiController.GetCountries(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<Country>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCountryById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCountriesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_Country_Entity(int id)
        {
            var result = await _restApiController.DeleteCountries(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_Country_Entity()
        {
            var entity = new Country
            {
                Id = 3,
                Name = "Canada"
            };

            var result = await _restApiController.CreateCountries(entity);

            var list = _restApiController.GetCountries(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCountriesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<Country>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Name, Is.EqualTo(entity.Name));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_Country_Entity(int id)
        {
            var entity = await _restApiController.GetCountriesById(id);

            entity.Value.Name = "Updated Name";

            await _restApiController.UpdateCountries(entity.Value);

            var updateEntity = await _restApiController.GetCountriesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Name, Is.EqualTo("Updated Name"));
                Assert.That(entity?.Value?.Name, Is.EqualTo(updateEntity?.Value?.Name));
            });
        }

        #endregion

        #region Currencies tables

        [Test]
        public void GetCurrencies_Returns_2_Items()
        {
            var result = _restApiController.GetCurrencies(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<Currency>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCurrenciesById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCurrenciesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_Currencies_Entity(int id)
        {
            var result = await _restApiController.DeleteCurrencies(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_Currencies_Entity()
        {
            var entity = new Currency()
            {
                Id = 3,
                Name = "Euro"
            };

            var result = await _restApiController.CreateCurrencies(entity);

            var list = _restApiController.GetCurrencies(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCurrenciesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<Currency>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Name, Is.EqualTo(entity.Name));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_Currencies_Entity(int id)
        {
            var entity = await _restApiController.GetCurrenciesById(id);

            entity.Value.Name = "Updated Currency Name";

            await _restApiController.UpdateCurrencies(entity.Value);

            var updatedEntity = await _restApiController.GetCurrenciesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Name, Is.EqualTo("Updated Currency Name"));
                Assert.That(entity?.Value?.Name, Is.EqualTo(updatedEntity?.Value?.Name));
            });
        }

        #endregion

        #region CustomBelts table

        [Test]
        public void GetCustomBelts_Returns_2_Items()
        {
            var result = _restApiController.GetCustomBelts(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<CustomBelt>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCustomBeltsById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCustomBeltsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.FkBaseBelts, Is.GreaterThanOrEqualTo(1));  // Ensure FK is correctly set
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_CustomBelts_Entity(int id)
        {
            var result = await _restApiController.DeleteCustomBelts(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_CustomBelts_Entity()
        {
            var entity = new CustomBelt()
            {
                Id = 3,
                FkBaseBelts = 1,
                CustomSettings = "Length: 100cm; Color: Black;"
            };

            var result = await _restApiController.CreateCustomBelts(entity);

            var list = _restApiController.GetCustomBelts(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCustomBeltsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<CustomBelt>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
                Assert.That(entityThatWasAdded.Value.FkBaseBelts, Is.EqualTo(entity.FkBaseBelts));
                Assert.That(entityThatWasAdded.Value.CustomSettings, Is.EqualTo(entity.CustomSettings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_CustomBelts_Entity(int id)
        {
            var entity = await _restApiController.GetCustomBeltsById(id);

            entity.Value.CustomSettings = "Updated: Length 110cm; Color: Blue;";

            await _restApiController.UpdateCustomBelts(entity.Value);

            var updatedEntity = await _restApiController.GetCustomBeltsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.FkBaseBelts, Is.EqualTo(entity.Value.FkBaseBelts));
                Assert.That(updatedEntity?.Value?.CustomSettings, Is.EqualTo(entity.Value.CustomSettings));
            });
        }

        #endregion

        #region CustomNecklines table

        [Test]
        public void GetCustomNecklines_Returns_2_Items()
        {
            var result = _restApiController.GetCustomNecklines(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<CustomNeckline>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCustomNecklinesById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCustomNecklinesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.FkBaseNecklines, Is.GreaterThanOrEqualTo(1));  // Ensure FK is correctly set
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_CustomNecklines_Entity(int id)
        {
            var result = await _restApiController.DeleteCustomNecklines(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_CustomNecklines_Entity()
        {
            var entity = new CustomNeckline()
            {
                Id = 3,
                FkBaseNecklines = 1,
                CustomSettings = "Design: V-neck; Color: Navy Blue;"
            };

            var result = await _restApiController.CreateCustomNecklines(entity);

            var list = _restApiController.GetCustomNecklines(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCustomNecklinesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<CustomNeckline>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.FkBaseNecklines, Is.EqualTo(entity.FkBaseNecklines));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_CustomNecklines_Entity(int id)
        {
            var entity = await _restApiController.GetCustomNecklinesById(id);

            entity.Value.CustomSettings = "Updated: Design Round-neck; Color: Red;";

            await _restApiController.UpdateCustomNecklines(entity.Value);

            var updatedEntity = await _restApiController.GetCustomNecklinesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.FkBaseNecklines, Is.EqualTo(entity.Value.FkBaseNecklines));
                Assert.That(updatedEntity?.Value?.CustomSettings, Is.EqualTo(entity.Value.CustomSettings));
            });
        }

        #endregion

        #region CustomPants table

        [Test]
        public void GetCustomPants_Returns_2_Items()
        {
            var result = _restApiController.GetCustomPants(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<CustomPant>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCustomPantsById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCustomPantsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.FkBasePants, Is.GreaterThanOrEqualTo(1));  // Ensure FK is correctly set
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_CustomPants_Entity(int id)
        {
            var result = await _restApiController.DeleteCustomPants(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_CustomPants_Entity()
        {
            var entity = new CustomPant()
            {
                Id = 3,
                FkBasePants = 1,
                CustomSettings = "Size: 32; Color: Black; Fit: Slim;"
            };

            var result = await _restApiController.CreateCustomPants(entity);

            var list = _restApiController.GetCustomPants(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCustomPantsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<CustomPant>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.FkBasePants, Is.EqualTo(entity.FkBasePants));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_CustomPants_Entity(int id)
        {
            var entity = await _restApiController.GetCustomPantsById(id);

            entity.Value.CustomSettings = "Updated: Size 34; Color: Grey; Fit: Relaxed;";

            await _restApiController.UpdateCustomPants(entity.Value);

            var updatedEntity = await _restApiController.GetCustomPantsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.FkBasePants, Is.EqualTo(entity.Value.FkBasePants));
                Assert.That(updatedEntity?.Value?.CustomSettings, Is.EqualTo(entity.Value.CustomSettings));
            });
        }

        #endregion

        #region CustomPantsCuffs table

        [Test]
        public void GetCustomPantsCuffs_Returns_2_Items()
        {
            var result = _restApiController.GetCustomPantsCuffs(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<CustomPantsCuff>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCustomPantsCuffsById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCustomPantsCuffsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.FkBasePantCuffs, Is.GreaterThanOrEqualTo(1));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_CustomPantsCuffs_Entity(int id)
        {
            var result = await _restApiController.DeleteCustomPantsCuffs(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_CustomPantsCuffs_Entity()
        {
            var entity = new CustomPantsCuff()
            {
                Id = 3,
                FkBasePantCuffs = 1,
                CustomSettings = "Material: Wool; Style: Turned-Up;"
            };

            var result = await _restApiController.CreateCustomPantsCuffs(entity);

            var list = _restApiController.GetCustomPantsCuffs(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCustomPantsCuffsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<CustomPantsCuff>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
                Assert.That(entityThatWasAdded.Value.FkBasePantCuffs, Is.EqualTo(entity.FkBasePantCuffs));
                Assert.That(entityThatWasAdded.Value.CustomSettings, Is.EqualTo(entity.CustomSettings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_CustomPantsCuffs_Entity(int id)
        {
            var entity = await _restApiController.GetCustomPantsCuffsById(id);

            entity.Value.CustomSettings = "Updated: Material: Cotton; Style: Ribbed Cuffs;";

            await _restApiController.UpdateCustomPantsCuffs(entity.Value);

            var updatedEntity = await _restApiController.GetCustomPantsCuffsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.FkBasePantCuffs, Is.EqualTo(entity.Value.FkBasePantCuffs));
                Assert.That(updatedEntity?.Value?.CustomSettings, Is.EqualTo(entity.Value.CustomSettings));
            });
        }

        #endregion

        #region CustomSleeves table

        [Test]
        public void GetCustomSleeves_Returns_2_Items()
        {
            var result = _restApiController.GetCustomSleeves(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<CustomSleeve>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCustomSleevesById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCustomSleevesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.FkBaseSleeves, Is.GreaterThanOrEqualTo(1));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_CustomSleeves_Entity(int id)
        {
            var result = await _restApiController.DeleteCustomSleeves(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_CustomSleeves_Entity()
        {
            var entity = new CustomSleeve()
            {
                Id = 3,
                FkBaseSleeves = 1,
                CustomSettings = "Length: Long; Material: Silk; Style: Bell;"
            };

            var result = await _restApiController.CreateCustomSleeves(entity);

            var list = _restApiController.GetCustomSleeves(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCustomSleevesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<CustomSleeve>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
                Assert.That(entityThatWasAdded.Value.FkBaseSleeves, Is.EqualTo(entity.FkBaseSleeves));
                Assert.That(entityThatWasAdded.Value.CustomSettings, Is.EqualTo(entity.CustomSettings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_CustomSleeves_Entity(int id)
        {
            var entity = await _restApiController.GetCustomSleevesById(id);

            entity.Value.CustomSettings = "Updated: Length: Short; Material: Cotton; Style: Puffed;";

            await _restApiController.UpdateCustomSleeves(entity.Value);

            var updatedEntity = await _restApiController.GetCustomSleevesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.FkBaseSleeves, Is.EqualTo(entity.Value.FkBaseSleeves));
                Assert.That(updatedEntity?.Value?.CustomSettings, Is.EqualTo(entity.Value.CustomSettings));
            });
        }

        #endregion

        #region CustomSleeveCuffs table

        [Test]
        public void GetCustomSleeveCuffs_Returns_2_Items()
        {
            var result = _restApiController.GetCustomSleeveCuffs(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<CustomSleeveCuff>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCustomSleeveCuffsById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCustomSleeveCuffsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.FkBaseSleeveCuffs, Is.GreaterThanOrEqualTo(1));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_CustomSleeveCuffs_Entity(int id)
        {
            var result = await _restApiController.DeleteCustomSleeveCuffs(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_CustomSleeveCuffs_Entity()
        {
            var entity = new CustomSleeveCuff()
            {
                Id = 3,
                FkBaseSleeveCuffs = 1,
                CustomSettings = "Material: Velvet; Button: 4 gold buttons;"
            };

            var result = await _restApiController.CreateCustomSleeveCuffs(entity);

            var list = _restApiController.GetCustomSleeveCuffs(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCustomSleeveCuffsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<CustomSleeveCuff>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
                Assert.That(entityThatWasAdded.Value.FkBaseSleeveCuffs, Is.EqualTo(entity.FkBaseSleeveCuffs));
                Assert.That(entityThatWasAdded.Value.CustomSettings, Is.EqualTo(entity.CustomSettings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_CustomSleeveCuffs_Entity(int id)
        {
            var entity = await _restApiController.GetCustomSleeveCuffsById(id);
            entity.Value.CustomSettings = "Updated: Material: Leather; Button: 3 silver buttons;";

            await _restApiController.UpdateCustomSleeveCuffs(entity.Value);

            var updatedEntity = await _restApiController.GetCustomSleeveCuffsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.FkBaseSleeveCuffs, Is.EqualTo(entity.Value.FkBaseSleeveCuffs));
                Assert.That(updatedEntity?.Value?.CustomSettings, Is.EqualTo(entity.Value.CustomSettings));
            });
        }

        #endregion

        #region CustomSportSuits table

        [Test]
        public void GetCustomSportSuits_Returns_2_Items()
        {
            var result = _restApiController.GetCustomSportSuits(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<CustomSportSuit>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCustomSportSuitsById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCustomSportSuitsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.FkCustomNecklines, Is.Not.Null);
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_CustomSportSuits_Entity(int id)
        {
            var result = await _restApiController.DeleteCustomSportSuits(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_CustomSportSuits_Entity()
        {
            var entity = new CustomSportSuit()
            {
                Id = 3,
                FkCustomNecklines = 1,
                FkCustomSweaters = 1,
                FkCustomSleeves = 1,
                FkCustomSleeveCuffsLeft = 1,
                FkCustomSleeveCuffsRight = 1,
                FkCustomBelts = 1,
                FkCustomPants = 1,
                FkCustomPantsCuffsLeft = 1,
                FkCustomPantsCuffsRight = 1
            };

            var result = await _restApiController.CreateCustomSportSuits(entity);

            var list = _restApiController.GetCustomSportSuits(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCustomSportSuitsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
                Assert.That(list.Value as List<CustomSportSuit>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_CustomSportSuits_Entity(int id)
        {
            var entity = await _restApiController.GetCustomSportSuitsById(id);

            entity.Value.FkCustomNecklines = 2;

            await _restApiController.UpdateCustomSportSuits(entity.Value);

            var updatedEntity = await _restApiController.GetCustomSportSuitsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.FkCustomNecklines, Is.EqualTo(entity.Value.FkCustomNecklines));
            });
        }

        #endregion

        #region CustomSweaters table

        [Test]
        public void GetCustomSweaters_Returns_2_Items()
        {
            var result = _restApiController.GetCustomSweaters(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<CustomSweater>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCustomSweatersById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCustomSweatersById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.FkBaseSweaters, Is.Not.Null);
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_CustomSweaters_Entity(int id)
        {
            var result = await _restApiController.DeleteCustomSweaters(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_CustomSweaters_Entity()
        {
            var entity = new CustomSweater()
            {
                Id = 3,
                FkBaseSweaters = 1,
                CustomSettings = "Material: Wool; Color: Navy Blue; Size: M"
            };

            var result = await _restApiController.CreateCustomSweaters(entity);

            var list = _restApiController.GetCustomSweaters(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCustomSweatersById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<CustomSweater>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
                Assert.That(entityThatWasAdded.Value.FkBaseSweaters, Is.EqualTo(entity.FkBaseSweaters));
                Assert.That(entityThatWasAdded.Value.CustomSettings, Is.EqualTo(entity.CustomSettings));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_CustomSweaters_Entity(int id)
        {
            var originalEntity = await _restApiController.GetCustomSweatersById(id);

            var updatedCustomSettings = "Updated: Material: Cashmere; Color: Black; Size: L";
            originalEntity.Value.CustomSettings = updatedCustomSettings;

            await _restApiController.UpdateCustomSweaters(originalEntity.Value);

            var updatedEntity = await _restApiController.GetCustomSweatersById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.FkBaseSweaters, Is.EqualTo(originalEntity.Value.FkBaseSweaters));
                Assert.That(updatedEntity?.Value?.CustomSettings, Is.EqualTo(updatedCustomSettings));
            });
        }

        #endregion

        #region CustomizableProducts table

        [Test]
        public void GetCustomizableProducts_Return_2_Items()
        {
            var result = _restApiController.GetCustomizableProducts(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<CustomizableProduct>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetCustomizableProductById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetCustomizableProductsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_CustomizableProduct_Entity(int id)
        {
            var result = await _restApiController.DeleteCustomizableProducts(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_CustomizableProduct_Entity()
        {
            var entity = new CustomizableProduct()
            {
                Id = 3,
                FkCustomSportSuits = 1,
                FkFabricTypes = 1,
                FkSizeOptions = 1,
                Price = 100,
                CustomizationDetails = "",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var result = await _restApiController.CreateCustomizableProducts(entity);

            var list = _restApiController.GetCustomizableProducts(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetCustomizableProductsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<CustomizableProduct>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.CustomizationDetails, Is.EqualTo(entity.CustomizationDetails));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_CustomizableProduct_Entity(int id)
        {
            var entity = await _restApiController.GetCustomizableProductsById(id);

            entity.Value.CustomizationDetails = "Updated Detail";

            await _restApiController.UpdateCustomizableProducts(entity.Value);

            var updateEntity = await _restApiController.GetCustomizableProductsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.CustomizationDetails, Is.EqualTo(entity.Value.CustomizationDetails));
                Assert.That(entity?.Value?.CustomizationDetails, Is.EqualTo(updateEntity?.Value?.CustomizationDetails));
            });
        }

        #endregion

        #region FabricTypes table

        [Test]
        public void GetFabricTypes_Return_2_Items()
        {
            var result = _restApiController.GetFabricTypes(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<FabricType>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetFabricTypeById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetFabricTypesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_FabricType_Entity(int id)
        {
            var result = await _restApiController.DeleteFabricTypes(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_FabricType_Entity()
        {
            var entity = new FabricType()
            {
                Id = 3,
                Name = "Silk"
            };

            var result = await _restApiController.CreateFabricTypes(entity);

            var list = _restApiController.GetFabricTypes(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetFabricTypesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<FabricType>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Name, Is.EqualTo(entity.Name));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_FabricType_Entity(int id)
        {
            var entity = await _restApiController.GetFabricTypesById(id);

            entity.Value.Name = "Updated Name";

            await _restApiController.UpdateFabricTypes(entity.Value);

            var updateEntity = await _restApiController.GetFabricTypesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Name, Is.EqualTo(entity.Value.Name));
                Assert.That(entity?.Value?.Name, Is.EqualTo(updateEntity?.Value?.Name));
            });
        }

        #endregion

        #region Languages table

        [Test]
        public void GetLanguages_Return_2_Items()
        {
            var result = _restApiController.GetLanguages(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<Language>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetLanguageById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetLanguagesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_Language_Entity(int id)
        {
            var result = await _restApiController.DeleteLanguages(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_Language_Entity()
        {
            var entity = new Language()
            {
                Id = 3,
                Abbreviation = "PL-PL",
                DateFormat = "DDMMYYYY",
                Description = "Language 3",
                FullName = "Language 3",
                TimeFormat = "HH:MM:SS"
            };

            var result = await _restApiController.CreateLanguages(entity);

            var list = _restApiController.GetLanguages(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetLanguagesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<Language>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Abbreviation, Is.EqualTo(entity.Abbreviation));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_Language_Entity(int id)
        {
            var entity = await _restApiController.GetLanguagesById(id);

            entity.Value.FullName = "Text 3";

            await _restApiController.UpdateLanguages(entity.Value);

            var updateEntity = await _restApiController.GetLanguagesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.FullName, Is.EqualTo(entity.Value.FullName));
                Assert.That(entity?.Value?.FullName, Is.EqualTo(updateEntity?.Value?.FullName));
            });
        }

        #endregion

        #region Orders

        [Test]
        public void GetOrders_Return_2_Items()
        {
            var result = _restApiController.GetOrders(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<Order>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetOrderById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetOrdersById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_Order_Entity(int id)
        {
            var result = await _restApiController.DeleteOrders(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_Order_Entity()
        {
            var entity = new Order()
            {
                Id = 3,
                FkProductOrders = 1,
                FkOrderStatus = 1,
                FkPayments = 1,
                FkShippingAddresses = 1,
                Price = 411,
                Amount = 14,
                OrderDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _restApiController.CreateOrders(entity);

            var list = _restApiController.GetOrders(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetOrdersById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<Order>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_Order_Entity(int id)
        {
            var entity = await _restApiController.GetOrdersById(id);

            entity.Value.Amount = 200;

            await _restApiController.UpdateOrders(entity.Value);

            var updateEntity = await _restApiController.GetOrdersById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Amount, Is.EqualTo(entity.Value.Amount));
                Assert.That(entity?.Value?.Amount, Is.EqualTo(updateEntity?.Value?.Amount));
            });
        }

        #endregion

        #region OrderHistory

        [Test]
        public void GetOrderHistory_Return_2_Items()
        {
            var result = _restApiController.GetOrderHistory(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<OrderHistory>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetOrderHistoryById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetOrderHistoryById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_OrderHistory_Entity(int id)
        {
            var result = await _restApiController.DeleteOrderHistory(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_OrderHistory_Entity()
        {
            var entity = new OrderHistory()
            {
                Id = 3,
                FkOrders = 1,
                FkOrderStatuses = 1,
                ChangedAt = DateTime.Now
            };

            var result = await _restApiController.CreateOrderHistory(entity);

            var list = _restApiController.GetOrderHistory(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetOrderHistoryById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<OrderHistory>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_OrderHistory_Entity(int id)
        {
            var entity = await _restApiController.GetOrderHistoryById(id);

            entity.Value.FkOrderStatuses = 3;

            await _restApiController.UpdateOrderHistory(entity.Value);

            var updateEntity = await _restApiController.GetOrderHistoryById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.FkOrderStatuses, Is.EqualTo(entity.Value.FkOrderStatuses));
                Assert.That(entity?.Value?.FkOrderStatuses, Is.EqualTo(updateEntity?.Value?.FkOrderStatuses));
            });
        }

        #endregion

        #region OrderStatuses table

        [Test]
        public void GetOrderStatuses_Return_2_Items()
        {
            var result = _restApiController.GetOrderStatuses(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<OrderStatus>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetOrderStatusById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetOrderStatusesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_OrderStatus_Entity(int id)
        {
            var result = await _restApiController.DeleteOrderStatuses(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_OrderStatus_Entity()
        {
            var entity = new OrderStatus()
            {
                Id = 3,
                Status = "New Order"
            };

            var result = await _restApiController.CreateOrderStatuses(entity);

            var list = _restApiController.GetOrderStatuses(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetOrderStatusesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<OrderStatus>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Status, Is.EqualTo(entity.Status));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_OrderStatus_Entity(int id)
        {
            var entity = await _restApiController.GetOrderStatusesById(id);

            entity.Value.Status = "Updated Status";

            await _restApiController.UpdateOrderStatuses(entity.Value);

            var updateEntity = await _restApiController.GetOrderStatusesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Status, Is.EqualTo(entity.Value.Status));
                Assert.That(entity?.Value?.Status, Is.EqualTo(updateEntity?.Value?.Status));
            });
        }

        #endregion

        #region Payments table

        [Test]
        public void GetPayments_Return_2_Items()
        {
            var result = _restApiController.GetPayments(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<Payment>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetPaymentById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetPaymentsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_Payment_Entity(int id)
        {
            var result = await _restApiController.DeletePayments(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_Payment_Entity()
        {
            var entity = new Payment()
            {
                Id = 3,
                FkPaymentMethods = 1,
                FkCurrencies = 1,
                FkPaymentStatuses = 1,
                Price = 100,
                PaymentDate = DateTime.Now,
                PaymentNumber = "123"
            };

            var result = await _restApiController.CreatePayments(entity);

            var list = _restApiController.GetPayments(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetPaymentsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<Payment>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.PaymentNumber, Is.EqualTo(entity.PaymentNumber));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_Payment_Entity(int id)
        {
            var entity = await _restApiController.GetPaymentsById(id);

            entity.Value.PaymentNumber = "456";

            await _restApiController.UpdatePayments(entity.Value);

            var updateEntity = await _restApiController.GetPaymentsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.PaymentNumber, Is.EqualTo(entity.Value.PaymentNumber));
                Assert.That(entity?.Value?.PaymentNumber, Is.EqualTo(updateEntity?.Value?.PaymentNumber));
            });
        }

        #endregion

        #region PaymentMethods table

        [Test]
        public void GetPaymentMethods_Returns_2_Items()
        {
            var result = _restApiController.GetPaymentMethods(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<PaymentMethod>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetPaymentMethodById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetPaymentMethodsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.Method, Is.Not.Null.And.Not.Empty);
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_PaymentMethod_Entity(int id)
        {
            var result = await _restApiController.DeletePaymentMethods(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_PaymentMethod_Entity()
        {
            var entity = new PaymentMethod()
            {
                Id = 3,
                Method = "Bitcoin"
            };

            var result = await _restApiController.CreatePaymentMethods(entity);

            var list = _restApiController.GetPaymentMethods(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetPaymentMethodsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<PaymentMethod>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
                Assert.That(entityThatWasAdded.Value.Method, Is.EqualTo(entity.Method));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_PaymentMethod_Entity(int id)
        {
            var entity = await _restApiController.GetPaymentMethodsById(id);
            entity.Value.Method = "Apple Pay";

            await _restApiController.UpdatePaymentMethods(entity.Value);

            var updatedEntity = await _restApiController.GetPaymentMethodsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.Method, Is.EqualTo("Apple Pay"));
            });
        }

        #endregion

        #region PaymentStatuses table

        [Test]
        public void GetPaymentStatuses_Returns_2_Items()
        {
            var result = _restApiController.GetPaymentStatuses(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<PaymentStatus>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetPaymentStatusById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetPaymentStatusesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.Status, Is.Not.Null.And.Not.Empty);
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_PaymentStatus_Entity(int id)
        {
            var result = await _restApiController.DeletePaymentStatuses(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_PaymentStatus_Entity()
        {
            var entity = new PaymentStatus()
            {
                Id = 3,
                Status = "Pending"
            };

            var result = await _restApiController.CreatePaymentStatuses(entity);

            var list = _restApiController.GetPaymentStatuses(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetPaymentStatusesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<PaymentStatus>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
                Assert.That(entityThatWasAdded.Value.Status, Is.EqualTo(entity.Status));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_PaymentStatus_Entity(int id)
        {
            var entity = await _restApiController.GetPaymentStatusesById(id);
            entity.Value.Status = "Completed";

            await _restApiController.UpdatePaymentStatuses(entity.Value);

            var updatedEntity = await _restApiController.GetPaymentStatusesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.Status, Is.EqualTo("Completed"));
            });
        }

        #endregion

        #region Products table

        [Test]
        public void GetProducts_Return_2_Items()
        {
            var result = _restApiController.GetProducts(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<Product>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetProductById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetProductsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_Product_Entity(int id)
        {
            var result = await _restApiController.DeleteProducts(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_Product_Entity()
        {
            var entity = new Product()
            {
                Id = 3,
                Price = 19.99m,
                StockQuantity = 100,
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var result = await _restApiController.CreateProducts(entity);

            var list = _restApiController.GetProducts(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetProductsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<Product>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Price, Is.EqualTo(entity.Price));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_Product_Entity(int id)
        {
            var entity = await _restApiController.GetProductsById(id);

            entity.Value.Price = 29.99m;

            await _restApiController.UpdateProducts(entity.Value);

            var updateEntity = await _restApiController.GetProductsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Price, Is.EqualTo(entity.Value.Price));
                Assert.That(entity?.Value?.Price, Is.EqualTo(updateEntity?.Value?.Price));
            });
        }

        #endregion

        #region ProductImages table

        [Test]
        public void GetProductImages_Return_2_Items()
        {
            var result = _restApiController.GetProductImages(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<ProductImage>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetProductImageById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetProductImagesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_ProductImage_Entity(int id)
        {
            var result = await _restApiController.DeleteProductImages(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_ProductImage_Entity()
        {
            var entity = new ProductImage()
            {
                Id = 3,
                FkProducts = 1,
                ImageData = Encoding.ASCII.GetBytes("https://example.com/image3.jpg")
            };

            var result = await _restApiController.CreateProductImages(entity);

            var list = _restApiController.GetProductImages(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetProductImagesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<ProductImage>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.ImageData, Is.EqualTo(entity.ImageData));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_ProductImage_Entity(int id)
        {
            var entity = await _restApiController.GetProductImagesById(id);

            entity.Value.ImageData = Encoding.ASCII.GetBytes("https://example.com/updated_image.jpg");

            await _restApiController.UpdateProductImages(entity.Value);

            var updateEntity = await _restApiController.GetProductImagesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.ImageData, Is.EqualTo(entity.Value.ImageData));
                Assert.That(entity?.Value?.ImageData, Is.EqualTo(updateEntity?.Value?.ImageData));
            });
        }

        #endregion

        #region ProductOrders table

        [Test]
        public void GetProductOrders_Returns_2_Items()
        {
            var result = _restApiController.GetProductOrders(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<ProductOrder>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetProductOrderById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetProductOrdersById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_ProductOrder_Entity(int id)
        {
            var result = await _restApiController.DeleteProductOrders(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_ProductOrder_Entity()
        {
            var entity = new ProductOrder()
            {
                Id = 3,
                FkProducts = 1,
                FkCustomizableProducts = null
            };

            var result = await _restApiController.CreateProductOrders(entity);

            var list = _restApiController.GetProductOrders(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetProductOrdersById(3);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
                Assert.That(list.Value as List<ProductOrder>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
                Assert.That(entityThatWasAdded.Value.FkProducts, Is.EqualTo(entity.FkProducts));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_ProductOrder_Entity(int id)
        {
            var entity = await _restApiController.GetProductOrdersById(id);
            entity.Value.FkProducts = 2;

            await _restApiController.UpdateProductOrders(entity.Value);

            var updatedEntity = await _restApiController.GetProductOrdersById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.FkProducts, Is.EqualTo(2));
                Assert.That(updatedEntity?.Value?.FkCustomizableProducts, Is.EqualTo(entity.Value.FkCustomizableProducts));
            });
        }

        #endregion

        #region ProductTranslations table

        [Test]
        public void GetProductTranslations_Return_2_Items()
        {
            var result = _restApiController.GetProductTranslations(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<ProductTranslation>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetProductTranslationById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetProductTranslationsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_ProductTranslation_Entity(int id)
        {
            var result = await _restApiController.DeleteProductTranslations(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_ProductTranslation_Entity()
        {
            var entity = new ProductTranslation()
            {
                Id = 3,
                FkLanguages = 1,
                FkProducts = 1,
                Name = "Product 3",
                Description = "Description for Product 3"
            };

            var result = await _restApiController.CreateProductTranslations(entity);

            var list = _restApiController.GetProductTranslations(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetProductTranslationsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<ProductTranslation>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Name, Is.EqualTo(entity.Name));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_ProductTranslation_Entity(int id)
        {
            var entity = await _restApiController.GetProductTranslationsById(id);

            entity.Value.Name = "Updated Product Name";

            await _restApiController.UpdateProductTranslations(entity.Value);

            var updateEntity = await _restApiController.GetProductTranslationsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Name, Is.EqualTo(entity.Value.Name));
                Assert.That(entity?.Value?.Name, Is.EqualTo(updateEntity?.Value?.Name));
            });
        }

        #endregion

        #region Reviews table

        [Test]
        public void GetReviews_Return_2_Items()
        {
            var result = _restApiController.GetReviews(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<Review>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetReviewById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetReviewsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_Review_Entity(int id)
        {
            var result = await _restApiController.DeleteReviews(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_Review_Entity()
        {
            var entity = new Review()
            {
                Id = 3,
                FkProducts = 1,
                FkUsers = 1,
                Rating = 5,
                Comment = "Excellent product!",
                CreatedAt = DateTime.Now
            };

            var result = await _restApiController.CreateReviews(entity);

            var list = _restApiController.GetReviews(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetReviewsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<Review>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.Comment, Is.EqualTo(entity.Comment));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_Review_Entity(int id)
        {
            var entity = await _restApiController.GetReviewsById(id);

            entity.Value.Comment = "Updated comment.";

            await _restApiController.UpdateReviews(entity.Value);

            var updateEntity = await _restApiController.GetReviewsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Comment, Is.EqualTo(entity.Value.Comment));
                Assert.That(entity?.Value?.Comment, Is.EqualTo(updateEntity?.Value?.Comment));
            });
        }

        #endregion

        #region ShippingAddresses table

        [Test]
        public void GetShippingAddresses_Returns_2_Items()
        {
            var result = _restApiController.GetShippingAddresses(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<ShippingAddress>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetShippingAddressById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetShippingAddressesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.FkCountries, Is.Not.Null);
                Assert.That(result?.Value?.Address, Is.Not.Null.And.Not.Empty);
                Assert.That(result?.Value?.City, Is.Not.Null.And.Not.Empty);
                Assert.That(result?.Value?.ZipPostCode, Is.Not.Null.And.Not.Empty);
                Assert.That(result?.Value?.StateProvince, Is.Not.Null.And.Not.Empty);
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_ShippingAddress_Entity(int id)
        {
            var result = await _restApiController.DeleteShippingAddresses(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_ShippingAddress_Entity()
        {
            var entity = new ShippingAddress()
            {
                Id = 3,
                FkCountries = 1,
                Address = "123 New Avenue",
                City = "Metropolis",
                ZipPostCode = "12345",
                StateProvince = "Gotham",
                Email = "batman@gmail.com"
            };

            var result = await _restApiController.CreateShippingAddresses(entity);

            var list = _restApiController.GetShippingAddresses(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetShippingAddressesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<ShippingAddress>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
                Assert.That(entityThatWasAdded.Value.FkCountries, Is.EqualTo(entity.FkCountries));
                Assert.That(entityThatWasAdded.Value.Address, Is.EqualTo(entity.Address));
                Assert.That(entityThatWasAdded.Value.City, Is.EqualTo(entity.City));
                Assert.That(entityThatWasAdded.Value.ZipPostCode, Is.EqualTo(entity.ZipPostCode));
                Assert.That(entityThatWasAdded.Value.StateProvince, Is.EqualTo(entity.StateProvince));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_ShippingAddress_Entity(int id)
        {
            var entity = await _restApiController.GetShippingAddressesById(id);
            entity.Value.Address = "Updated 456 New Avenue";
            entity.Value.City = "Updated Metropolis";
            entity.Value.ZipPostCode = "54321";
            entity.Value.StateProvince = "Updated Gotham";

            await _restApiController.UpdateShippingAddresses(entity.Value);

            var updatedEntity = await _restApiController.GetShippingAddressesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.FkCountries, Is.EqualTo(entity.Value.FkCountries));
                Assert.That(updatedEntity?.Value?.Address, Is.EqualTo("Updated 456 New Avenue"));
                Assert.That(updatedEntity?.Value?.City, Is.EqualTo("Updated Metropolis"));
                Assert.That(updatedEntity?.Value?.ZipPostCode, Is.EqualTo("54321"));
                Assert.That(updatedEntity?.Value?.StateProvince, Is.EqualTo("Updated Gotham"));
            });
        }

        #endregion

        #region SizeOptions table

        [Test]
        public void GetSizeOptions_Returns_2_Items()
        {
            var result = _restApiController.GetSizeOptions(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<SizeOption>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetSizeOptionById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetSizeOptionsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.Size, Is.Not.Null.And.Not.Empty);
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_SizeOption_Entity(int id)
        {
            var result = await _restApiController.DeleteSizeOptions(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_SizeOption_Entity()
        {
            var entity = new SizeOption()
            {
                Id = 3,
                Size = "XL"
            };

            var result = await _restApiController.CreateSizeOptions(entity);

            var list = _restApiController.GetSizeOptions(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetSizeOptionsById(3);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
                Assert.That(list.Value as List<SizeOption>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
                Assert.That(entityThatWasAdded.Value.Size, Is.EqualTo(entity.Size));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_SizeOption_Entity(int id)
        {
            var entity = await _restApiController.GetSizeOptionsById(id);
            entity.Value.Size = "XXL";

            await _restApiController.UpdateSizeOptions(entity.Value);

            var updatedEntity = await _restApiController.GetSizeOptionsById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.Size, Is.EqualTo("XXL"));
            });
        }

        #endregion

        #region Users table

        [Test]
        public void GetUsers_Return_2_Items()
        {
            var result = _restApiController.GetUsers(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<User>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetUserById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetUsersById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_User_Entity(int id)
        {
            var result = await _restApiController.DeleteUsers(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_User_Entity()
        {
            var entity = new User()
            {
                Id = 3,
                FkUserRoles = 1,
                Login = "Login 3",
                Email = "Email 3",
                Password = "Password 3",
                ConfirmEmail = false,
                ConfirmationToken = "ConfirmationToken 3",
                ConfirmationTokenExpires = DateTime.Now
            };

            var result = await _restApiController.CreateUsers(entity);

            var list = _restApiController.GetUsers(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetUsersById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<User>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.FkUserRoles, Is.EqualTo(entity.FkUserRoles));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_User_Entity(int id)
        {
            var entity = await _restApiController.GetUsersById(id);

            entity.Value.Email = "Text 3";

            await _restApiController.UpdateUsers(entity.Value);

            var updateEntity = await _restApiController.GetUsersById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Email, Is.EqualTo(entity.Value.Email));
                Assert.That(entity?.Value?.Email, Is.EqualTo(updateEntity?.Value?.Email));
            });
        }

        #endregion

        #region UserOrderHistory table

        [Test]
        public void GetUserOrderHistory_Returns_2_Items()
        {
            var result = _restApiController.GetUserOrderHistory(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<UserOrderHistory>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetUserOrderHistoryById_Returns_Correct_Entity(int id)
        {
            var result = await _restApiController.GetUserOrderHistoryById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
                Assert.That(result?.Value?.FkOrders, Is.GreaterThan(0));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_UserOrderHistory_Entity(int id)
        {
            var result = await _restApiController.DeleteUserOrderHistory(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_UserOrderHistory_Entity()
        {
            var entity = new UserOrderHistory()
            {
                Id = 3,
                FkOrders = 1
            };

            var result = await _restApiController.CreateUserOrderHistory(entity);

            var list = _restApiController.GetUserOrderHistory(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetUserOrderHistoryById(3);

            Assert.Multiple(() =>
            {
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
                Assert.That(list.Value as List<UserOrderHistory>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value.Id, Is.EqualTo(3));
                Assert.That(entityThatWasAdded.Value.FkOrders, Is.EqualTo(entity.FkOrders));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_UserOrderHistory_Entity(int id)
        {
            var entity = await _restApiController.GetUserOrderHistoryById(id);
            entity.Value.FkOrders = 2;

            await _restApiController.UpdateUserOrderHistory(entity.Value);

            var updatedEntity = await _restApiController.GetUserOrderHistoryById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updatedEntity?.Value?.Id, Is.EqualTo(id));
                Assert.That(updatedEntity?.Value?.FkOrders, Is.EqualTo(2));
            });
        }

        #endregion

        #region UserProfiles table

        [Test]
        public void GetUserProfiles_Return_2_Items()
        {
            var result = _restApiController.GetUserProfiles(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<UserProfile>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetUserProfileById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetUserProfilesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_UserProfile_Entity(int id)
        {
            var result = await _restApiController.DeleteUserProfiles(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_UserProfile_Entity()
        {
            var entity = new UserProfile()
            {
                Id = 3,
                FkUsers = 1,
                FirstName = "FirstName 3",
                LastName = "LastName 3",
                MiddleName = "MiddleName 3",
                Address = "Address 3",
                City = "City 3",
                StateProvince = "StateProvince 3",
                Country = "Country 3",
                Phone = "Phone 3",
                Avatar = "Avatar 3"
            };

            var result = await _restApiController.CreateUserProfiles(entity);

            var list = _restApiController.GetUserProfiles(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetUserProfilesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<UserProfile>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.FkUsers, Is.EqualTo(entity.FkUsers));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_UserProfile_Entity(int id)
        {
            var entity = await _restApiController.GetUserProfilesById(id);

            entity.Value.LastName = "Text 3";

            await _restApiController.UpdateUserProfiles(entity.Value);

            var updateEntity = await _restApiController.GetUserProfilesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.LastName, Is.EqualTo(entity.Value.LastName));
                Assert.That(entity?.Value?.LastName, Is.EqualTo(updateEntity?.Value?.LastName));
            });
        }

        #endregion

        #region UserRoles table

        [Test]
        public void GetUserRoles_Return_2_Items()
        {
            var result = _restApiController.GetUserRoles(_limit, _page);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Value as List<UserRole>, Has.Count.EqualTo(2));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetUserRoleById_Return_Correct_Entity(int id)
        {
            var result = await _restApiController.GetUserRolesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Value?.Id, Is.EqualTo(id));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Delete_UserRole_Entity(int id)
        {
            var result = await _restApiController.DeleteUserRoles(id);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<OkResult>());
            });
        }

        [Test]
        public async Task Add_UserRole_Entity()
        {
            var entity = new UserRole()
            {
                Id = 3,
                FkAccessLevels = 1,
                Name = "Name 3"
            };

            var result = await _restApiController.CreateUserRoles(entity);

            var list = _restApiController.GetUserRoles(_limit, _page);
            var entityThatWasAdded = await _restApiController.GetUserRolesById(3);

            Assert.Multiple(() =>
            {
                Assert.That(list.Value as List<UserRole>, Has.Count.EqualTo(3));
                Assert.That(entityThatWasAdded, Is.Not.Null);
                Assert.That(entityThatWasAdded.Value?.FkAccessLevels, Is.EqualTo(entity.FkAccessLevels));
                Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Update_UserRole_Entity(int id)
        {
            var entity = await _restApiController.GetUserRolesById(id);

            entity.Value.Name = "Text 3";

            await _restApiController.UpdateUserRoles(entity.Value);

            var updateEntity = await _restApiController.GetUserRolesById(id);

            Assert.Multiple(() =>
            {
                Assert.That(updateEntity?.Value?.Name, Is.EqualTo(entity.Value.Name));
                Assert.That(entity?.Value?.Name, Is.EqualTo(updateEntity?.Value?.Name));
            });
        }

        #endregion
    }
}