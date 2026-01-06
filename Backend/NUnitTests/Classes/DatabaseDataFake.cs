using DatabaseProvider.Models;
using System.Text;

namespace NUnitTests.Classes
{
    public static class DatabaseDataFake
    {
        public static List<About> GetFakeAboutList()
        {
            return
            [
                new About { Id = 1, FkLanguages = 1, Header = "About Us", Text = "This is about us." },
                new About { Id = 2, FkLanguages = 2, Header = "О нас", Text = "Это о нас." }
            ];
        }

        public static List<AccessLevel> GetFakeAccessLevelList()
        {
            return
            [
                new AccessLevel { Id = 1, Level = 1, Description = "Admin" },
                new AccessLevel { Id = 2, Level = 2, Description = "User" }
            ];
        }

        public static List<BaseBelt> GetFakeBaseBeltList()
        {
            return
            [
                new BaseBelt { Id = 1, Settings = "{ \"material\": \"leather\", \"color\": \"black\" }" },
                new BaseBelt { Id = 2, Settings = "{ \"material\": \"fabric\", \"color\": \"grey\" }" }
            ];
        }

        public static List<BaseNeckline> GetFakeBaseNecklineList()
        {
            return
            [
                new BaseNeckline { Id = 1, Settings = "{ \"type\": \"round\", \"detail\": \"none\" }" },
                new BaseNeckline { Id = 2, Settings = "{ \"type\": \"v-neck\", \"detail\": \"lace\" }" }
            ];
        }

        public static List<BasePant> GetFakeBasePantList()
        {
            return
            [
                new BasePant { Id = 1, Settings = "{ \"style\": \"jeans\", \"fit\": \"slim\" }" },
                new BasePant { Id = 2, Settings = "{ \"style\": \"chinos\", \"fit\": \"regular\" }" }
            ];
        }

        public static List<BasePantsCuff> GetFakeBasePantsCuffList()
        {
            return
            [
                new BasePantsCuff { Id = 1, Settings = "{ \"type\": \"elastic\", \"detail\": \"ribbed\" }" },
                new BasePantsCuff { Id = 2, Settings = "{ \"type\": \"plain\", \"detail\": \"straight cut\" }" }
            ];
        }

        public static List<BaseSleeve> GetFakeBaseSleeveList()
        {
            return
            [
                new BaseSleeve { Id = 1, Settings = "{ \"length\": \"long\", \"pattern\": \"solid\" }" },
                new BaseSleeve { Id = 2, Settings = "{ \"length\": \"short\", \"pattern\": \"striped\" }" }
            ];
        }

        public static List<BaseSleeveCuff> GetFakeBaseSleeveCuffList()
        {
            return
            [
                new BaseSleeveCuff { Id = 1, Settings = "{ \"style\": \"ribbed\", \"feature\": \"elastic\" }" },
                new BaseSleeveCuff { Id = 2, Settings = "{ \"style\": \"buttoned\", \"feature\": \"adjustable\" }" }
            ];
        }

        public static List<BaseSportSuit> GetFakeBaseSportSuitList()
        {
            return
            [
                new BaseSportSuit
                {
                    Id = 1, FkBaseNecklines = 1, FkBaseSweaters = 1, FkBaseSleeves = 1, FkBaseSleeveCuffsLeft = 1, FkBaseSleeveCuffsRight = 1, FkBaseBelts = 1, FkBasePants = 1, FkBasePantsCuffsLeft = 1, FkBasePantsCuffsRight = 1 },
                new BaseSportSuit
                {
                    Id = 2, FkBaseNecklines = 2, FkBaseSweaters = 2, FkBaseSleeves = 2, FkBaseSleeveCuffsLeft = 2, FkBaseSleeveCuffsRight = 2, FkBaseBelts = 2, FkBasePants = 2, FkBasePantsCuffsLeft = 2, FkBasePantsCuffsRight = 2 }
            ];
        }

        public static List<BaseSweater> GetFakeBaseSweaterList()
        {
            return
            [
                new BaseSweater { Id = 1, Settings = "{ \"material\": \"wool\", \"style\": \"pullover\" }" },
                new BaseSweater { Id = 2, Settings = "{ \"material\": \"cotton\", \"style\": \"cardigan\" }" }
            ];
        }

        public static List<Cart> GetFakeCartsList()
        {
            return
            [
                new Cart { Id = 1, FkProductOrders = 1, FkCurrencies = 1, Price = 100, Amount = 10, CreatedAt = DateTime.Now },
                new Cart { Id = 2, FkProductOrders = 1, FkCurrencies = 2, Price = 200, Amount = 20, CreatedAt = DateTime.Now }
            ];
        }

        public static List<Category> GetFakeCategoryList()
        {
            return
            [
                new Category { Id = 1, FkLanguages = 1, Name = "Clothes", Description = "Clothing items." },
                new Category { Id = 2, FkLanguages = 2, Name = "Одежда", Description = "Элементы одежды." }
            ];
        }

        public static List<CategoryHierarchy> GetFakeCategoryHierarchyList()
        {
            return
            [
                new CategoryHierarchy { Id = 1, ParentId = null, FkCategories = 1 },
                new CategoryHierarchy { Id = 2, ParentId = 1, FkCategories = 2 }
            ];
        }

        public static List<Color> GetFakeColorList()
        {
            return
            [
                new Color { Id = 1, Name = "Red", ImageData = Encoding.ASCII.GetBytes("https://example.com/image1.jpg") },
                new Color { Id = 2, Name = "Green", ImageData = Encoding.ASCII.GetBytes("https://example.com/image3.jpg")}
            ];
        }

        public static List<Contact> GetFakeContactList()
        {
            return
            [
                new Contact { Id = 1, FkLanguages = 1, Address = "123 Main St", Phone = "123-456-7890", Email = "contact@example.com" },
                new Contact { Id = 2, FkLanguages = 2, Address = "123 Кол принципал", Phone = "098-765-4321", Email = "contacto@ejemplo.com" }
            ];
        }

        public static List<Country> GetFakeCountryList()
        {
            return
            [
                new Country { Id = 1, Name = "USA" },
                new Country { Id = 2, Name = "Canada" }
            ];
        }

        public static List<Currency> GetFakeCurrencyList()
        {
            return
            [
                new Currency { Id = 1, Name = "USD" },
                new Currency { Id = 2, Name = "EUR" }
            ];
        }

        public static List<CustomBelt> GetFakeCustomBeltList()
        {
            return
            [
                new CustomBelt
                {
                    Id = 1, FkBaseBelts = 1, CustomSettings = "{ \"material\": \"leather\", \"color\": \"navy\", \"buckle\": \"silver\" }" },
                new CustomBelt
                {
                    Id = 2, FkBaseBelts = 2, CustomSettings = "{ \"material\": \"fabric\", \"color\": \"brown\", \"buckle\": \"gold\" }" }
            ];
        }

        public static List<CustomNeckline> GetFakeCustomNecklineList()
        {
            return
            [
                new CustomNeckline
                {
                    Id = 1, FkBaseNecklines = 1, CustomSettings = "{ \"type\": \"round\", \"detail\": \"button-down\", \"decoration\": \"none\" }" },
                new CustomNeckline
                {
                    Id = 2, FkBaseNecklines = 2, CustomSettings = "{ \"type\": \"v-neck\", \"detail\": \"zip-up\", \"decoration\": \"beads\" }" }
            ];
        }

        public static List<CustomPant> GetFakeCustomPantList()
        {
            return
            [
                new CustomPant
                {
                    Id = 1, FkBasePants = 1, CustomSettings = "{ \"style\": \"jeans\", \"fit\": \"skinny\", \"color\": \"dark blue\" }" },
                new CustomPant
                {
                    Id = 2, FkBasePants = 2, CustomSettings = "{ \"style\": \"chinos\", \"fit\": \"relaxed\", \"color\": \"khaki\" }" }
            ];
        }

        public static List<CustomPantsCuff> GetFakeCustomPantsCuffList()
        {
            return
            [
                new CustomPantsCuff { Id = 1, FkBasePantCuffs = 1, CustomSettings = "{ \"type\": \"elastic\", \"detail\": \"ribbed\", \"color\": \"black\" }" },
                new CustomPantsCuff { Id = 2, FkBasePantCuffs = 2, CustomSettings = "{ \"type\": \"plain\", \"detail\": \"straight cut\", \"color\": \"navy\" }" }
            ];
        }

        public static List<CustomSleeve> GetFakeCustomSleeveList()
        {
            return
            [
                new CustomSleeve { Id = 1, FkBaseSleeves = 1, CustomSettings = "{ \"length\": \"long\", \"pattern\": \"solid\", \"color\": \"white\" }" },
                new CustomSleeve { Id = 2, FkBaseSleeves = 2, CustomSettings = "{ \"length\": \"short\", \"pattern\": \"striped\", \"color\": \"blue\" }" }
            ];
        }

        public static List<CustomSleeveCuff> GetFakeCustomSleeveCuffList()
        {
            return
            [
                new CustomSleeveCuff { Id = 1, FkBaseSleeveCuffs = 1, CustomSettings = "{ \"style\": \"ribbed\", \"feature\": \"elastic\", \"color\": \"gray\" }" },
                new CustomSleeveCuff { Id = 2, FkBaseSleeveCuffs = 2, CustomSettings = "{ \"style\": \"buttoned\", \"feature\": \"adjustable\", \"color\": \"black\" }" }
            ];
        }

        public static List<CustomSportSuit> GetFakeCustomSportSuitList()
        {
            return
            [
                new CustomSportSuit { Id = 1, FkCustomNecklines = 1, FkCustomSweaters = 1, FkCustomSleeves = 1, FkCustomSleeveCuffsLeft = 1, FkCustomSleeveCuffsRight = 1, FkCustomBelts = 1, FkCustomPants = 1, FkCustomPantsCuffsLeft = 1, FkCustomPantsCuffsRight = 1 },
                new CustomSportSuit { Id = 2, FkCustomNecklines = 2, FkCustomSweaters = 2, FkCustomSleeves = 2, FkCustomSleeveCuffsLeft = 2, FkCustomSleeveCuffsRight = 2, FkCustomBelts = 2, FkCustomPants = 2, FkCustomPantsCuffsLeft = 2, FkCustomPantsCuffsRight = 2 }
            ];
        }

        public static List<CustomSweater> GetFakeCustomSweaterList()
        {
            return
            [
                new CustomSweater { Id = 1, FkBaseSweaters = 1, CustomSettings = "{ \"material\": \"wool\", \"style\": \"pullover\", \"color\": \"black\" }" },
                new CustomSweater { Id = 2, FkBaseSweaters = 2, CustomSettings = "{ \"material\": \"cotton\", \"style\": \"cardigan\", \"color\": \"white\" }" }
            ];
        }

        public static List<CustomizableProduct> GetFakeCustomizableProductList()
        {
            return
            [
                new CustomizableProduct { Id = 1, FkCustomSportSuits = 1, FkFabricTypes = 1, FkSizeOptions = 1, Price = 100, CustomizationDetails = "{}", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new CustomizableProduct { Id = 2, FkCustomSportSuits = 2, FkFabricTypes = 2, FkSizeOptions = 2, Price = 200, CustomizationDetails = "{}", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            ];
        }

        public static List<FabricType> GetFakeFabricTypeList()
        {
            return
            [
                new FabricType { Id = 1, Name = "Cotton", Description = "Soft and breathable fabric." },
                new FabricType { Id = 2, Name = "Polyester", Description = "Durable and wrinkle-resistant fabric." }
            ];
        }

        public static List<Language> GetFakeLanguageList()
        {
            return
            [
                new Language { Id = 1, FullName = "English", Abbreviation = "EN", Description = "English language.", DateFormat = "MM/dd/yyyy", TimeFormat = "hh:mm tt" },
                new Language { Id = 2, FullName = "Russian", Abbreviation = "RU", Description = "Russian language.", DateFormat = "dd/MM/yyyy", TimeFormat = "HH:mm" }
            ];
        }

        public static List<Order> GetFakeOrderList()
        {
            return
            [
                new Order { Id = 1, FkProductOrders = 1, FkOrderStatus = 1, FkPayments = 1, FkShippingAddresses = 1, Price = 100, Amount = 10, OrderDate = DateTime.Now, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now  },
                new Order { Id = 2, FkProductOrders = 2, FkOrderStatus = 2, FkPayments = 2, FkShippingAddresses = 1, Price = 100, Amount = 10, OrderDate = DateTime.Now, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now  }
            ];
        }

        public static List<OrderHistory> GetFakeOrderHistoryList()
        {
            return
            [
                new OrderHistory { Id = 1, FkOrders = 1, FkOrderStatuses = 1, ChangedAt = DateTime.Now },
                new OrderHistory { Id = 2, FkOrders = 2, FkOrderStatuses = 2, ChangedAt = DateTime.Now.AddDays(-1) }
            ];
        }

        public static List<OrderStatus> GetFakeOrderStatusList()
        {
            return
            [
                new OrderStatus { Id = 1, Status = "Pending" },
                new OrderStatus { Id = 2, Status = "Shipped" }
            ];
        }

        public static List<Payment> GetFakePaymentList()
        {
            return
            [
                new Payment { Id = 1, FkPaymentMethods = 1, FkCurrencies = 1, FkPaymentStatuses = 1, Price = 100, PaymentDate = DateTime.Now, PaymentNumber = "123" },
                new Payment { Id = 2, FkPaymentMethods = 2, FkCurrencies = 2, FkPaymentStatuses = 1, Price = 100, PaymentDate = DateTime.Now, PaymentNumber = "456" }
            ];
        }

        public static List<PaymentMethod> GetFakePaymentMethodList()
        {
            return
            [
                new PaymentMethod { Id = 1, Method = "Credit Card" },
                new PaymentMethod { Id = 2, Method = "PayPal" }
            ];
        }

        public static List<PaymentStatus> GetFakePaymentStatusList()
        {
            return
            [
                new PaymentStatus { Id = 1, Status = "Pending" },
                new PaymentStatus { Id = 2, Status = "Completed" }
            ];
        }

        public static List<Product> GetFakeProductList()
        {
            return
            [
                new Product { Id = 1, Price = 50.00m, StockQuantity = 10, IsActive = true, IsCustomizable = false },
                new Product { Id = 2, Price = 30.00m, StockQuantity = 5, IsActive = true, IsCustomizable = true }
            ];
        }

        public static List<ProductImage> GetFakeProductImageList()
        {
            return
            [
                new ProductImage { Id = 1, FkProducts = 1, ImageData = Encoding.ASCII.GetBytes("https://example.com/image1.jpg") },
                new ProductImage { Id = 2, FkProducts = 2, ImageData = Encoding.ASCII.GetBytes("https://example.com/image3.jpg") }
            ];
        }

        public static List<ProductOrder> GetFakeProductOrderList()
        {
            return
            [
                new ProductOrder { Id = 1, FkProducts = 1, FkCustomizableProducts = null },
                new ProductOrder { Id = 2, FkProducts = null, FkCustomizableProducts = 1 }
            ];
        }

        public static List<ProductTranslation> GetFakeProductTranslationList()
        {
            return
            [
                new ProductTranslation { Id = 1, FkLanguages = 1, FkProducts = 1, Name = "Product 1", Description = "Description of Product 1" },
                new ProductTranslation { Id = 2, FkLanguages = 2, FkProducts = 1, Name = "Продукт 1", Description = "Описание продукта 1" }
            ];
        }

        public static List<Review> GetFakeReviewList()
        {
            return
            [
                new Review { Id = 1, FkProducts = 1, FkUsers = 1, Rating = 5, Comment = "Excellent product!", CreatedAt = DateTime.Now },
                new Review { Id = 2, FkProducts = 2, FkUsers = 2, Rating = 4, Comment = "Very good, but could be better.", CreatedAt = DateTime.Now.AddDays(-2) }
            ];
        }

        public static List<ShippingAddress> GetFakeShippingAddressList()
        {
            return
            [
                new ShippingAddress { Id = 1, FkCountries = 1, Address = "123 Main St", City = "Metropolis", ZipPostCode = "12345", StateProvince = "State1", Email = "test@gmail.com" },
                new ShippingAddress { Id = 2, FkCountries = 2, Address = "456 Elm St", City = "Smallville", ZipPostCode = "67890", StateProvince = "State2", Email = "test@gmail.com" }
            ];
        }

        public static List<SizeOption> GetFakeSizeOptionList()
        {
            return
            [
                new SizeOption { Id = 1, Size = "S" },
                new SizeOption { Id = 2, Size = "M" }
            ];
        }

        public static List<User> GetFakeUserList()
        {
            return
            [
                new User { Id = 1, FkUserRoles = 1, Login = "user1", Email = "user1@example.com", Password = "password1", ConfirmEmail = false, ConfirmationToken = "ConfirmationToken1", ConfirmationTokenExpires = DateTime.Now, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new User { Id = 2, FkUserRoles = 2, Login = "user2", Email = "user2@example.com", Password = "password2", ConfirmEmail = false, ConfirmationToken = "ConfirmationToken2", ConfirmationTokenExpires = DateTime.Now, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            ];
        }

        public static List<UserOrderHistory> GetFakeUserOrderHistoryList()
        {
            return
            [
                new UserOrderHistory { Id = 1, FkOrders = 1, FkUsers = 1 },
                new UserOrderHistory { Id = 2, FkOrders = 2, FkUsers = 2 }
            ];
        }

        public static List<UserProfile> GetFakeUserProfileList()
        {
            return
            [
                new UserProfile { Id = 1, FkUsers = 1, FirstName = "John", LastName = "Doe", MiddleName = "A", Address = "123 Main St", City = "CityA", ZipPostCode = "12345", StateProvince = "StateA", Country = "CountryA", Phone = "123-456-7890", Avatar = "avatar1.png", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new UserProfile { Id = 2, FkUsers = 2, FirstName = "Jane", LastName = "Smith", MiddleName = "B", Address = "456 Elm St", City = "CityB", ZipPostCode = "67890", StateProvince = "StateB", Country = "CountryB", Phone = "098-765-4321", Avatar = "avatar2.png", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
            ];
        }

        public static List<UserRole> GetFakeUserRoleList()
        {
            return
            [
                new UserRole { Id = 1, FkAccessLevels = 1, Name = "Admin" },
                new UserRole { Id = 2, FkAccessLevels = 2, Name = "User" }
            ];
        }
    }
}