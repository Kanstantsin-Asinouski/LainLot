using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatabaseProvider.Models;

public partial class LainLotContext : DbContext
{
    /// <summary>
    /// Scaffold-DbContext "Host=127.0.0.1:5432;Database=LainLot;Username=postgres;Password=123456789" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir Models -Context LainLotContext -Project DatabaseProvider -f
    /// Add-Migration InitialCreate -Project DatabaseProvider
    /// Update-Database -Project DatabaseProvider
    /// </summary>

    private readonly ILogger<LainLotContext> _logger;

    public LainLotContext(DbContextOptions<LainLotContext> options, ILogger<LainLotContext> logger)
        : base(options)
    {
        _logger = logger;
    }

    public virtual DbSet<About> Abouts { get; set; }

    public virtual DbSet<AccessLevel> AccessLevels { get; set; }

    public virtual DbSet<BaseBelt> BaseBelts { get; set; }

    public virtual DbSet<BaseNeckline> BaseNecklines { get; set; }

    public virtual DbSet<BasePant> BasePants { get; set; }

    public virtual DbSet<BasePantsCuff> BasePantsCuffs { get; set; }

    public virtual DbSet<BaseSleeve> BaseSleeves { get; set; }

    public virtual DbSet<BaseSleeveCuff> BaseSleeveCuffs { get; set; }

    public virtual DbSet<BaseSportSuit> BaseSportSuits { get; set; }

    public virtual DbSet<BaseSweater> BaseSweaters { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryHierarchy> CategoryHierarchies { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<CustomBelt> CustomBelts { get; set; }

    public virtual DbSet<CustomNeckline> CustomNecklines { get; set; }

    public virtual DbSet<CustomPant> CustomPants { get; set; }

    public virtual DbSet<CustomPantsCuff> CustomPantsCuffs { get; set; }

    public virtual DbSet<CustomSleeve> CustomSleeves { get; set; }

    public virtual DbSet<CustomSleeveCuff> CustomSleeveCuffs { get; set; }

    public virtual DbSet<CustomSportSuit> CustomSportSuits { get; set; }

    public virtual DbSet<CustomSweater> CustomSweaters { get; set; }

    public virtual DbSet<CustomizableProduct> CustomizableProducts { get; set; }

    public virtual DbSet<FabricType> FabricTypes { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderHistory> OrderHistories { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    public virtual DbSet<ProductTranslation> ProductTranslations { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }

    public virtual DbSet<SizeOption> SizeOptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserOrderHistory> UserOrderHistories { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_logger != null)
        {
            optionsBuilder.LogTo(message => _logger.LogInformation(message));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<About>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("About_pkey");

            entity.ToTable("About");

            entity.Property(e => e.Header).HasMaxLength(100);

            entity.HasOne(d => d.FkLanguagesNavigation).WithMany(p => p.Abouts)
                .HasForeignKey(d => d.FkLanguages)
                .HasConstraintName("About_FkLanguages_fkey");
        });

        modelBuilder.Entity<AccessLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AccessLevels_pkey");
        });

        modelBuilder.Entity<BaseBelt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BaseBelts_pkey");

            entity.Property(e => e.Settings).HasColumnType("jsonb");
        });

        modelBuilder.Entity<BaseNeckline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BaseNecklines_pkey");

            entity.Property(e => e.Settings).HasColumnType("jsonb");
        });

        modelBuilder.Entity<BasePant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BasePants_pkey");

            entity.Property(e => e.Settings).HasColumnType("jsonb");
        });

        modelBuilder.Entity<BasePantsCuff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BasePantsCuffs_pkey");

            entity.Property(e => e.Settings).HasColumnType("jsonb");
        });

        modelBuilder.Entity<BaseSleeve>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BaseSleeves_pkey");

            entity.Property(e => e.Settings).HasColumnType("jsonb");
        });

        modelBuilder.Entity<BaseSleeveCuff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BaseSleeveCuffs_pkey");

            entity.Property(e => e.Settings).HasColumnType("jsonb");
        });

        modelBuilder.Entity<BaseSportSuit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BaseSportSuits_pkey");

            entity.ToTable("BaseSportSuits");

            entity.HasOne(d => d.FkBaseBeltsNavigation).WithMany(p => p.BaseSportSuits)
                .HasForeignKey(d => d.FkBaseBelts)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BaseSportSuits_FkBaseBelts_fkey");

            entity.HasOne(d => d.FkBaseNecklinesNavigation).WithMany(p => p.BaseSportSuits)
                .HasForeignKey(d => d.FkBaseNecklines)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BaseSportSuits_FkBaseNecklines_fkey");

            entity.HasOne(d => d.FkBasePantsNavigation).WithMany(p => p.BaseSportSuits)
                .HasForeignKey(d => d.FkBasePants)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BaseSportSuits_FkBasePants_fkey");

            entity.HasOne(d => d.FkBasePantsCuffsLeftNavigation).WithMany(p => p.BaseSportSuitFkBasePantsCuffsLeftNavigations)
                .HasForeignKey(d => d.FkBasePantsCuffsLeft)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BaseSportSuits_FkBasePantsCuffsLeft_fkey");

            entity.HasOne(d => d.FkBasePantsCuffsRightNavigation).WithMany(p => p.BaseSportSuitFkBasePantsCuffsRightNavigations)
                .HasForeignKey(d => d.FkBasePantsCuffsRight)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BaseSportSuits_FkBasePantsCuffsRight_fkey");

            entity.HasOne(d => d.FkBaseSleeveCuffsLeftNavigation).WithMany(p => p.BaseSportSuitFkBaseSleeveCuffsLeftNavigations)
                .HasForeignKey(d => d.FkBaseSleeveCuffsLeft)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BaseSportSuits_FkBaseSleeveCuffsLeft_fkey");

            entity.HasOne(d => d.FkBaseSleeveCuffsRightNavigation).WithMany(p => p.BaseSportSuitFkBaseSleeveCuffsRightNavigations)
                .HasForeignKey(d => d.FkBaseSleeveCuffsRight)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BaseSportSuits_FkBaseSleeveCuffsRight_fkey");

            entity.HasOne(d => d.FkBaseSleevesNavigation).WithMany(p => p.BaseSportSuits)
                .HasForeignKey(d => d.FkBaseSleeves)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BaseSportSuits_FkBaseSleeves_fkey");

            entity.HasOne(d => d.FkBaseSweatersNavigation).WithMany(p => p.BaseSportSuits)
                .HasForeignKey(d => d.FkBaseSweaters)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("BaseSportSuits_FkBaseSweaters_fkey");
        });

        modelBuilder.Entity<BaseSweater>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BaseSweaters_pkey");

            entity.Property(e => e.Settings).HasColumnType("jsonb");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cart_pkey");

            entity.ToTable("Cart");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Price).HasPrecision(10, 2);

            entity.HasOne(d => d.FkCurrenciesNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.FkCurrencies)
                .HasConstraintName("Cart_FkCurrencies_fkey");

            entity.HasOne(d => d.FkProductOrdersNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.FkProductOrders)
                .HasConstraintName("Cart_FkProductOrders_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Categories_pkey");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.FkLanguagesNavigation).WithMany(p => p.Categories)
                .HasForeignKey(d => d.FkLanguages)
                .HasConstraintName("Categories_FkLanguages_fkey");
        });

        modelBuilder.Entity<CategoryHierarchy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CategoryHierarchy_pkey");

            entity.ToTable("CategoryHierarchy");

            entity.HasOne(d => d.FkCategoriesNavigation).WithMany(p => p.CategoryHierarchies)
                .HasForeignKey(d => d.FkCategories)
                .HasConstraintName("CategoryHierarchy_FkCategories_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CategoryHierarchy_ParentId_fkey");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Colors_pkey");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Contacts_pkey");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(255);

            entity.HasOne(d => d.FkLanguagesNavigation).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.FkLanguages)
                .HasConstraintName("Contacts_FkLanguages_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Countries_pkey");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Currencies_pkey");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<CustomBelt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomBelts_pkey");

            entity.Property(e => e.CustomSettings).HasColumnType("jsonb");

            entity.HasOne(d => d.FkBaseBeltsNavigation).WithMany(p => p.CustomBelts)
                .HasForeignKey(d => d.FkBaseBelts)
                .HasConstraintName("CustomBelts_FkBaseBelts_fkey");
        });

        modelBuilder.Entity<CustomNeckline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomNecklines_pkey");

            entity.Property(e => e.CustomSettings).HasColumnType("jsonb");

            entity.HasOne(d => d.FkBaseNecklinesNavigation).WithMany(p => p.CustomNecklines)
                .HasForeignKey(d => d.FkBaseNecklines)
                .HasConstraintName("CustomNecklines_FkBaseNecklines_fkey");
        });

        modelBuilder.Entity<CustomPant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomPants_pkey");

            entity.Property(e => e.CustomSettings).HasColumnType("jsonb");

            entity.HasOne(d => d.FkBasePantsNavigation).WithMany(p => p.CustomPants)
                .HasForeignKey(d => d.FkBasePants)
                .HasConstraintName("CustomPants_FkBasePants_fkey");
        });

        modelBuilder.Entity<CustomPantsCuff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomPantsCuffs_pkey");

            entity.Property(e => e.CustomSettings).HasColumnType("jsonb");

            entity.HasOne(d => d.FkBasePantCuffsNavigation).WithMany(p => p.CustomPantsCuffs)
                .HasForeignKey(d => d.FkBasePantCuffs)
                .HasConstraintName("CustomPantsCuffs_FkBasePantCuffs_fkey");
        });

        modelBuilder.Entity<CustomSleeve>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomSleeves_pkey");

            entity.Property(e => e.CustomSettings).HasColumnType("jsonb");

            entity.HasOne(d => d.FkBaseSleevesNavigation).WithMany(p => p.CustomSleeves)
                .HasForeignKey(d => d.FkBaseSleeves)
                .HasConstraintName("CustomSleeves_FkBaseSleeves_fkey");
        });

        modelBuilder.Entity<CustomSleeveCuff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomSleeveCuffs_pkey");

            entity.Property(e => e.CustomSettings).HasColumnType("jsonb");

            entity.HasOne(d => d.FkBaseSleeveCuffsNavigation).WithMany(p => p.CustomSleeveCuffs)
                .HasForeignKey(d => d.FkBaseSleeveCuffs)
                .HasConstraintName("CustomSleeveCuffs_FkBaseSleeveCuffs_fkey");
        });

        modelBuilder.Entity<CustomSportSuit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomSportSuits_pkey");

            entity.HasOne(d => d.FkCustomBeltsNavigation).WithMany(p => p.CustomSportSuits)
                .HasForeignKey(d => d.FkCustomBelts)
                .HasConstraintName("CustomSportSuits_FkCustomBelts_fkey");

            entity.HasOne(d => d.FkCustomNecklinesNavigation).WithMany(p => p.CustomSportSuits)
                .HasForeignKey(d => d.FkCustomNecklines)
                .HasConstraintName("CustomSportSuits_FkCustomNecklines_fkey");

            entity.HasOne(d => d.FkCustomPantsNavigation).WithMany(p => p.CustomSportSuits)
                .HasForeignKey(d => d.FkCustomPants)
                .HasConstraintName("CustomSportSuits_FkCustomPants_fkey");

            entity.HasOne(d => d.FkCustomPantsCuffsLeftNavigation).WithMany(p => p.CustomSportSuitFkCustomPantsCuffsLeftNavigations)
                .HasForeignKey(d => d.FkCustomPantsCuffsLeft)
                .HasConstraintName("CustomSportSuits_FkCustomPantsCuffsLeft_fkey");

            entity.HasOne(d => d.FkCustomPantsCuffsRightNavigation).WithMany(p => p.CustomSportSuitFkCustomPantsCuffsRightNavigations)
                .HasForeignKey(d => d.FkCustomPantsCuffsRight)
                .HasConstraintName("CustomSportSuits_FkCustomPantsCuffsRight_fkey");

            entity.HasOne(d => d.FkCustomSleeveCuffsLeftNavigation).WithMany(p => p.CustomSportSuitFkCustomSleeveCuffsLeftNavigations)
                .HasForeignKey(d => d.FkCustomSleeveCuffsLeft)
                .HasConstraintName("CustomSportSuits_FkCustomSleeveCuffsLeft_fkey");

            entity.HasOne(d => d.FkCustomSleeveCuffsRightNavigation).WithMany(p => p.CustomSportSuitFkCustomSleeveCuffsRightNavigations)
                .HasForeignKey(d => d.FkCustomSleeveCuffsRight)
                .HasConstraintName("CustomSportSuits_FkCustomSleeveCuffsRight_fkey");

            entity.HasOne(d => d.FkCustomSleevesNavigation).WithMany(p => p.CustomSportSuits)
                .HasForeignKey(d => d.FkCustomSleeves)
                .HasConstraintName("CustomSportSuits_FkCustomSleeves_fkey");

            entity.HasOne(d => d.FkCustomSweatersNavigation).WithMany(p => p.CustomSportSuits)
                .HasForeignKey(d => d.FkCustomSweaters)
                .HasConstraintName("CustomSportSuits_FkCustomSweaters_fkey");
        });

        modelBuilder.Entity<CustomSweater>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomSweaters_pkey");

            entity.Property(e => e.CustomSettings).HasColumnType("jsonb");

            entity.HasOne(d => d.FkBaseSweatersNavigation).WithMany(p => p.CustomSweaters)
                .HasForeignKey(d => d.FkBaseSweaters)
                .HasConstraintName("CustomSweaters_FkBaseSweaters_fkey");
        });

        modelBuilder.Entity<CustomizableProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomizableProducts_pkey");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.FkCustomSportSuitsNavigation).WithMany(p => p.CustomizableProducts)
                .HasForeignKey(d => d.FkCustomSportSuits)
                .HasConstraintName("CustomizableProducts_FkCustomSportSuits_fkey");

            entity.HasOne(d => d.FkFabricTypesNavigation).WithMany(p => p.CustomizableProducts)
                .HasForeignKey(d => d.FkFabricTypes)
                .HasConstraintName("CustomizableProducts_FkFabricTypes_fkey");

            entity.HasOne(d => d.FkSizeOptionsNavigation).WithMany(p => p.CustomizableProducts)
                .HasForeignKey(d => d.FkSizeOptions)
                .HasConstraintName("CustomizableProducts_FkSizeOptions_fkey");
        });

        modelBuilder.Entity<FabricType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FabricTypes_pkey");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasPrecision(10, 2);

            entity.HasOne(d => d.FkCurrenciesNavigation).WithMany(p => p.FabricTypes)
                .HasForeignKey(d => d.FkCurrencies)
                .HasConstraintName("FabricTypes_FkCurrencies_fkey");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Languages_pkey");

            entity.Property(e => e.Abbreviation).HasMaxLength(5);
            entity.Property(e => e.DateFormat).HasMaxLength(20);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.TimeFormat).HasMaxLength(20);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Orders_pkey");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.FkOrderStatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkOrderStatus)
                .HasConstraintName("Orders_FkOrderStatus_fkey");

            entity.HasOne(d => d.FkPaymentsNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkPayments)
                .HasConstraintName("Orders_FkPayments_fkey");

            entity.HasOne(d => d.FkProductOrdersNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkProductOrders)
                .HasConstraintName("Orders_FkProductOrders_fkey");

            entity.HasOne(d => d.FkShippingAddressesNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkShippingAddresses)
                .HasConstraintName("Orders_FkShippingAddresses_fkey");
        });

        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderHistory_pkey");

            entity.ToTable("OrderHistory");

            entity.Property(e => e.ChangedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.FkOrderStatusesNavigation).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.FkOrderStatuses)
                .HasConstraintName("OrderHistory_FkOrderStatuses_fkey");

            entity.HasOne(d => d.FkOrdersNavigation).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.FkOrders)
                .HasConstraintName("OrderHistory_FkOrders_fkey");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderStatuses_pkey");

            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Payments_pkey");

            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.PaymentNumber).HasMaxLength(255);
            entity.Property(e => e.Price).HasPrecision(10, 2);

            entity.HasOne(d => d.FkCurrenciesNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.FkCurrencies)
                .HasConstraintName("Payments_FkCurrencies_fkey");

            entity.HasOne(d => d.FkPaymentMethodsNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.FkPaymentMethods)
                .HasConstraintName("Payments_FkPaymentMethods_fkey");

            entity.HasOne(d => d.FkPaymentStatusesNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.FkPaymentStatuses)
                .HasConstraintName("Payments_FkPaymentStatuses_fkey");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PaymentMethods_pkey");

            entity.Property(e => e.Method).HasMaxLength(255);
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PaymentStatuses_pkey");

            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Products_pkey");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsCustomizable).HasDefaultValue(false);
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.FkColorsNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkColors)
                .HasConstraintName("Products_FkColors_fkey");

            entity.HasOne(d => d.FkCurrenciesNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkCurrencies)
                .HasConstraintName("Products_FkCurrencies_fkey");

            entity.HasOne(d => d.FkFabricTypesNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkFabricTypes)
                .HasConstraintName("Products_FkFabricTypes_fkey");

            entity.HasOne(d => d.FkSizeOptionsNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkSizeOptions)
                .HasConstraintName("Products_FkSizeOptions_fkey");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductImages_pkey");

            entity.HasOne(d => d.FkProductsNavigation).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.FkProducts)
                .HasConstraintName("ProductImages_FkProducts_fkey");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductOrders_pkey");

            entity.HasOne(d => d.FkCustomizableProductsNavigation).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.FkCustomizableProducts)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProductOrders_FkCustomizableProducts_fkey");

            entity.HasOne(d => d.FkProductsNavigation).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.FkProducts)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ProductOrders_FkProducts_fkey");
        });

        modelBuilder.Entity<ProductTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductTranslations_pkey");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.FkCategoriesNavigation).WithMany(p => p.ProductTranslations)
                .HasForeignKey(d => d.FkCategories)
                .HasConstraintName("ProductTranslations_FkCategories_fkey");

            entity.HasOne(d => d.FkLanguagesNavigation).WithMany(p => p.ProductTranslations)
                .HasForeignKey(d => d.FkLanguages)
                .HasConstraintName("ProductTranslations_FkLanguages_fkey");

            entity.HasOne(d => d.FkProductsNavigation).WithMany(p => p.ProductTranslations)
                .HasForeignKey(d => d.FkProducts)
                .HasConstraintName("ProductTranslations_FkProducts_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Reviews_pkey");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.FkProductsNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.FkProducts)
                .HasConstraintName("Reviews_FkProducts_fkey");

            entity.HasOne(d => d.FkUsersNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.FkUsers)
                .HasConstraintName("Reviews_FkUsers_fkey");
        });

        modelBuilder.Entity<ShippingAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ShippingAddresses_pkey");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.StateProvince).HasMaxLength(50);
            entity.Property(e => e.ZipPostCode).HasMaxLength(10);

            entity.HasOne(d => d.FkCountriesNavigation).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.FkCountries)
                .HasConstraintName("ShippingAddresses_FkCountries_fkey");
        });

        modelBuilder.Entity<SizeOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SizeOptions_pkey");

            entity.Property(e => e.Size).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.HasIndex(e => e.Email, "Users_Email_key").IsUnique();

            entity.HasIndex(e => e.Login, "Users_Login_key").IsUnique();

            entity.Property(e => e.ConfirmationTokenExpires)
                .HasColumnType("timestamp with time zone");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Email).HasMaxLength(60);
            entity.Property(e => e.Login).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.FkUserRolesNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkUserRoles)
                .HasConstraintName("Users_FkUserRoles_fkey");
        });

        modelBuilder.Entity<UserOrderHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserOrderHistory_pkey");

            entity.ToTable("UserOrderHistory");

            entity.HasOne(d => d.FkOrdersNavigation).WithMany(p => p.UserOrderHistories)
                .HasForeignKey(d => d.FkOrders)
                .HasConstraintName("UserOrderHistory_FkOrders_fkey");

            entity.HasOne(d => d.FkUsersNavigation).WithMany(p => p.UserOrderHistories)
                .HasForeignKey(d => d.FkUsers)
                .HasConstraintName("UserOrderHistory_FkUsers_fkey");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserProfiles_pkey");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.StateProvince).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.ZipPostCode).HasMaxLength(10);

            entity.HasOne(d => d.FkUsersNavigation).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.FkUsers)
                .HasConstraintName("UserProfiles_FkUsers_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserRoles_pkey");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.FkAccessLevelsNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.FkAccessLevels)
                .HasConstraintName("UserRoles_FkAccessLevels_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
