using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.AutoMapper;
using DB = DatabaseProvider.Models;
using DatabaseRepository.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace RestAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/v1/[controller]")]
    public class DatabaseController(
        ILogger<DatabaseController> logger,
        IRepository<DB.About> aboutRepository,
        IRepository<DB.AccessLevel> accessLevelRepository,
        IRepository<DB.BaseBelt> baseBeltRepository,
        IRepository<DB.BaseNeckline> baseNecklineRepository,
        IRepository<DB.BasePant> basePantRepository,
        IRepository<DB.BasePantsCuff> basePantsCuffRepository,
        IRepository<DB.BaseSleeve> baseSleeveRepository,
        IRepository<DB.BaseSleeveCuff> baseSleeveCuffRepository,
        IRepository<DB.BaseSportSuit> baseSportSuitRepository,
        IRepository<DB.BaseSweater> baseSweaterRepository,
        IRepository<DB.Cart> cartRepository,
        IRepository<DB.Category> categoryRepository,
        IRepository<DB.CategoryHierarchy> categoryHierarchyRepository,
        IRepository<DB.Color> colorRepository,
        IRepository<DB.Contact> contactRepository,
        IRepository<DB.Country> countryRepository,
        IRepository<DB.Currency> currencyRepository,
        IRepository<DB.CustomBelt> customBeltRepository,
        IRepository<DB.CustomNeckline> customNecklineRepository,
        IRepository<DB.CustomPant> customPantRepository,
        IRepository<DB.CustomPantsCuff> customPantsCuffRepository,
        IRepository<DB.CustomSleeve> customSleeveRepository,
        IRepository<DB.CustomSleeveCuff> customSleeveCuffRepository,
        IRepository<DB.CustomSportSuit> customSportSuitRepository,
        IRepository<DB.CustomSweater> customSweaterRepository,
        IRepository<DB.CustomizableProduct> customizableProductRepository,
        IRepository<DB.FabricType> fabricTypeRepository,
        IRepository<DB.Language> languageRepository,
        IRepository<DB.Order> orderRepository,
        IRepository<DB.OrderHistory> orderHistoryRepository,
        IRepository<DB.OrderStatus> orderStatusRepository,
        IRepository<DB.Payment> paymentRepository,
        IRepository<DB.PaymentMethod> paymentMethodRepository,
        IRepository<DB.PaymentStatus> paymentStatusRepository,
        IRepository<DB.Product> productRepository,
        IRepository<DB.ProductImage> productImageRepository,
        IRepository<DB.ProductOrder> productOrderRepository,
        IRepository<DB.ProductTranslation> productTranslationRepository,
        IRepository<DB.Review> reviewRepository,
        IRepository<DB.ShippingAddress> shippingAddressRepository,
        IRepository<DB.SizeOption> sizeOptionRepository,
        IRepository<DB.User> userRepository,
        IRepository<DB.UserOrderHistory> userOrderHistoryRepository,
        IRepository<DB.UserProfile> userProfileRepository,
        IRepository<DB.UserRole> userRoleRepository
        ) : ControllerBase
    {
        /// <summary>
        /// CTRL + M + P - expand all
        /// CTRL + M + O - collapse all
        /// </summary>

        private readonly IMapper _mapper = MapperConfig.InitializeAutomapper();
        #region repos init
        private readonly ILogger<DatabaseController> _logger = logger;
        private readonly IRepository<DB.About> _aboutRepository = aboutRepository;
        private readonly IRepository<DB.AccessLevel> _accessLevelRepository = accessLevelRepository;
        private readonly IRepository<DB.BaseBelt> _baseBeltRepository = baseBeltRepository;
        private readonly IRepository<DB.BaseNeckline> _baseNecklineRepository = baseNecklineRepository;
        private readonly IRepository<DB.BasePant> _basePantRepository = basePantRepository;
        private readonly IRepository<DB.BasePantsCuff> _basePantsCuffRepository = basePantsCuffRepository;
        private readonly IRepository<DB.BaseSleeve> _baseSleeveRepository = baseSleeveRepository;
        private readonly IRepository<DB.BaseSleeveCuff> _baseSleeveCuffRepository = baseSleeveCuffRepository;
        private readonly IRepository<DB.BaseSportSuit> _baseSportSuitRepository = baseSportSuitRepository;
        private readonly IRepository<DB.BaseSweater> _baseSweaterRepository = baseSweaterRepository;
        private readonly IRepository<DB.Cart> _cartRepository = cartRepository;
        private readonly IRepository<DB.Category> _categoryRepository = categoryRepository;
        private readonly IRepository<DB.CategoryHierarchy> _categoryHierarchyRepository = categoryHierarchyRepository;
        private readonly IRepository<DB.Color> _colorRepository = colorRepository;
        private readonly IRepository<DB.Contact> _contactRepository = contactRepository;
        private readonly IRepository<DB.Country> _countryRepository = countryRepository;
        private readonly IRepository<DB.Currency> _currencyRepository = currencyRepository;
        private readonly IRepository<DB.CustomBelt> _customBeltRepository = customBeltRepository;
        private readonly IRepository<DB.CustomNeckline> _customNecklineRepository = customNecklineRepository;
        private readonly IRepository<DB.CustomPant> _customPantRepository = customPantRepository;
        private readonly IRepository<DB.CustomPantsCuff> _customPantsCuffRepository = customPantsCuffRepository;
        private readonly IRepository<DB.CustomSleeve> _customSleeveRepository = customSleeveRepository;
        private readonly IRepository<DB.CustomSleeveCuff> _customSleeveCuffRepository = customSleeveCuffRepository;
        private readonly IRepository<DB.CustomSportSuit> _customSportSuitRepository = customSportSuitRepository;
        private readonly IRepository<DB.CustomSweater> _customSweaterRepository = customSweaterRepository;
        private readonly IRepository<DB.CustomizableProduct> _customizableProductRepository = customizableProductRepository;
        private readonly IRepository<DB.FabricType> _fabricTypeRepository = fabricTypeRepository;
        private readonly IRepository<DB.Language> _languageRepository = languageRepository;
        private readonly IRepository<DB.Order> _orderRepository = orderRepository;
        private readonly IRepository<DB.OrderHistory> _orderHistoryRepository = orderHistoryRepository;
        private readonly IRepository<DB.OrderStatus> _orderStatusRepository = orderStatusRepository;
        private readonly IRepository<DB.Payment> _paymentRepository = paymentRepository;
        private readonly IRepository<DB.PaymentMethod> _paymentMethodRepository = paymentMethodRepository;
        private readonly IRepository<DB.PaymentStatus> _paymentStatusRepository = paymentStatusRepository;
        private readonly IRepository<DB.Product> _productRepository = productRepository;
        private readonly IRepository<DB.ProductImage> _productImageRepository = productImageRepository;
        private readonly IRepository<DB.ProductOrder> _productOrderRepository = productOrderRepository;
        private readonly IRepository<DB.ProductTranslation> _productTranslationRepository = productTranslationRepository;
        private readonly IRepository<DB.Review> _reviewRepository = reviewRepository;
        private readonly IRepository<DB.ShippingAddress> _shippingAddressRepository = shippingAddressRepository;
        private readonly IRepository<DB.SizeOption> _sizeOptionRepository = sizeOptionRepository;
        private readonly IRepository<DB.User> _userRepository = userRepository;
        private readonly IRepository<DB.UserOrderHistory> _userOrderHistoryRepository = userOrderHistoryRepository;
        private readonly IRepository<DB.UserProfile> _userProfileRepository = userProfileRepository;
        private readonly IRepository<DB.UserRole> _userRoleRepository = userRoleRepository;
        #endregion

        #region About table

        [HttpGet("GetAboutCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetAboutCount()
        {
            return _aboutRepository.GetAll().Count();
        }

        [HttpGet("GetAboutFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetAboutFields()
        {
            return new About().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetAbout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<About>> GetAbout(int limit, int page)
        {
            var dbList = _aboutRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.About>, List<About>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetAboutById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<About?>> GetAboutById(int id)
        {
            var dbEntity = await _aboutRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.About, About>(dbEntity);
        }

        [HttpPost("CreateAbout")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<About>> CreateAbout(About entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _aboutRepository.Add(_mapper.Map<About, DB.About>(entity));
                return CreatedAtAction(nameof(GetAboutById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateAbout")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<About>> UpdateAbout(About entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _aboutRepository.Update(_mapper.Map<About, DB.About>(entity));
                return CreatedAtAction(nameof(GetAboutById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteAbout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteAbout(int id)
        {
            var entity = await _aboutRepository.GetById(id);

            if (entity != null)
            {
                await _aboutRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region AccessLevels table

        [HttpGet("GetAccessLevelsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetAccessLevelsCount()
        {
            return _accessLevelRepository.GetAll().Count();
        }

        [HttpGet("GetAccessLevelsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetAccessLevelsFields()
        {
            return new AccessLevel().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetAccessLevels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<AccessLevel>> GetAccessLevels(int limit, int page)
        {
            var dbList = _accessLevelRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.AccessLevel>, List<AccessLevel>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetAccessLevelsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccessLevel?>> GetAccessLevelsById(int id)
        {
            var dbEntity = await _accessLevelRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.AccessLevel, AccessLevel>(dbEntity);
        }

        [HttpPost("CreateAccessLevels")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccessLevel>> CreateAccessLevels(AccessLevel entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _accessLevelRepository.Add(_mapper.Map<AccessLevel, DB.AccessLevel>(entity));
                return CreatedAtAction(nameof(GetAccessLevelsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateAccessLevels")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccessLevel>> UpdateAccessLevels(AccessLevel entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _accessLevelRepository.Update(_mapper.Map<AccessLevel, DB.AccessLevel>(entity));
                return CreatedAtAction(nameof(GetAccessLevelsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteAccessLevels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteAccessLevels(int id)
        {
            var entity = await _accessLevelRepository.GetById(id);

            if (entity != null)
            {
                await _accessLevelRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region BaseBelts table

        [HttpGet("GetBaseBeltsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetBaseBeltsCount()
        {
            return _baseBeltRepository.GetAll().Count();
        }

        [HttpGet("GetBaseBeltsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetBaseBeltsFields()
        {
            return new BaseBelt().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetBaseBelts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BaseBelt>> GetBaseBelts(int limit, int page)
        {
            var dbList = _baseBeltRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.BaseBelt>, List<BaseBelt>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetBaseBeltsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseBelt?>> GetBaseBeltsById(int id)
        {
            var dbEntity = await _baseBeltRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.BaseBelt, BaseBelt>(dbEntity);
        }

        [HttpPost("CreateBaseBelts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseBelt>> CreateBaseBelts(BaseBelt entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseBeltRepository.Add(_mapper.Map<BaseBelt, DB.BaseBelt>(entity));
                return CreatedAtAction(nameof(GetBaseBeltsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateBaseBelts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseBelt>> UpdateBaseBelts(BaseBelt entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseBeltRepository.Update(_mapper.Map<BaseBelt, DB.BaseBelt>(entity));
                return CreatedAtAction(nameof(GetBaseBeltsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteBaseBelts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteBaseBelts(int id)
        {
            var entity = await _baseBeltRepository.GetById(id);

            if (entity != null)
            {
                await _baseBeltRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region BaseNecklines table

        [HttpGet("GetBaseNecklinesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetBaseNecklinesCount()
        {
            return _baseNecklineRepository.GetAll().Count();
        }

        [HttpGet("GetBaseNecklinesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetBaseNecklinesFields()
        {
            return new BaseNeckline().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetBaseNecklines")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BaseNeckline>> GetBaseNecklines(int limit, int page)
        {
            var dbList = _baseNecklineRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.BaseNeckline>, List<BaseNeckline>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetBaseNecklinesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseNeckline?>> GetBaseNecklinesById(int id)
        {
            var dbEntity = await _baseNecklineRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.BaseNeckline, BaseNeckline>(dbEntity);
        }

        [HttpPost("CreateBaseNecklines")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseNeckline>> CreateBaseNecklines(BaseNeckline entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseNecklineRepository.Add(_mapper.Map<BaseNeckline, DB.BaseNeckline>(entity));
                return CreatedAtAction(nameof(GetBaseNecklinesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateBaseNecklines")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseNeckline>> UpdateBaseNecklines(BaseNeckline entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseNecklineRepository.Update(_mapper.Map<BaseNeckline, DB.BaseNeckline>(entity));
                return CreatedAtAction(nameof(GetBaseNecklinesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteBaseNecklines")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteBaseNecklines(int id)
        {
            var entity = await _baseNecklineRepository.GetById(id);

            if (entity != null)
            {
                await _baseNecklineRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region BasePants table

        [HttpGet("GetBasePantsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetBasePantsCount()
        {
            return _basePantRepository.GetAll().Count();
        }

        [HttpGet("GetBasePantsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetBasePantsFields()
        {
            return new BasePant().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetBasePants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BasePant>> GetBasePants(int limit, int page)
        {
            var dbList = _basePantRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.BasePant>, List<BasePant>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetBasePantsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BasePant?>> GetBasePantsById(int id)
        {
            var dbEntity = await _basePantRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.BasePant, BasePant>(dbEntity);
        }

        [HttpPost("CreateBasePants")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BasePant>> CreateBasePants(BasePant entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _basePantRepository.Add(_mapper.Map<BasePant, DB.BasePant>(entity));
                return CreatedAtAction(nameof(GetBasePantsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateBasePants")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BasePant>> UpdateBasePants(BasePant entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _basePantRepository.Update(_mapper.Map<BasePant, DB.BasePant>(entity));
                return CreatedAtAction(nameof(GetBasePantsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteBasePants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteBasePants(int id)
        {
            var entity = await _basePantRepository.GetById(id);

            if (entity != null)
            {
                await _basePantRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region BasePantsCuffs table

        [HttpGet("GetBasePantsCuffsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetBasePantsCuffsCount()
        {
            return _basePantsCuffRepository.GetAll().Count();
        }

        [HttpGet("GetBasePantsCuffsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetBasePantsCuffsFields()
        {
            return new BasePantsCuff().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetBasePantsCuffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BasePantsCuff>> GetBasePantsCuffs(int limit, int page)
        {
            var dbList = _basePantsCuffRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.BasePantsCuff>, List<BasePantsCuff>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetBasePantsCuffsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BasePantsCuff?>> GetBasePantsCuffsById(int id)
        {
            var dbEntity = await _basePantsCuffRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.BasePantsCuff, BasePantsCuff>(dbEntity);
        }

        [HttpPost("CreateBasePantsCuffs")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BasePantsCuff>> CreateBasePantsCuffs(BasePantsCuff entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _basePantsCuffRepository.Add(_mapper.Map<BasePantsCuff, DB.BasePantsCuff>(entity));
                return CreatedAtAction(nameof(GetBasePantsCuffsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateBasePantsCuffs")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BasePantsCuff>> UpdateBasePantsCuffs(BasePantsCuff entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _basePantsCuffRepository.Update(_mapper.Map<BasePantsCuff, DB.BasePantsCuff>(entity));
                return CreatedAtAction(nameof(GetBasePantsCuffsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteBasePantsCuffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteBasePantsCuffs(int id)
        {
            var entity = await _basePantsCuffRepository.GetById(id);

            if (entity != null)
            {
                await _basePantsCuffRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region BaseSleeves table

        [HttpGet("GetBaseSleevesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetBaseSleevesCount()
        {
            return _baseSleeveRepository.GetAll().Count();
        }

        [HttpGet("GetBaseSleevesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetBaseSleevesFields()
        {
            return new BaseSleeve().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetBaseSleeves")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BaseSleeve>> GetBaseSleeves(int limit, int page)
        {
            var dbList = _baseSleeveRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.BaseSleeve>, List<BaseSleeve>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetBaseSleevesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseSleeve?>> GetBaseSleevesById(int id)
        {
            var dbEntity = await _baseSleeveRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.BaseSleeve, BaseSleeve>(dbEntity);
        }

        [HttpPost("CreateBaseSleeves")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseSleeve>> CreateBaseSleeves(BaseSleeve entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseSleeveRepository.Add(_mapper.Map<BaseSleeve, DB.BaseSleeve>(entity));
                return CreatedAtAction(nameof(GetBaseSleevesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateBaseSleeves")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseSleeve>> UpdateBaseSleeves(BaseSleeve entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseSleeveRepository.Update(_mapper.Map<BaseSleeve, DB.BaseSleeve>(entity));
                return CreatedAtAction(nameof(GetBaseSleevesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteBaseSleeves")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteBaseSleeves(int id)
        {
            var entity = await _baseSleeveRepository.GetById(id);

            if (entity != null)
            {
                await _baseSleeveRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region BaseSleeveCuffs table

        [HttpGet("GetBaseSleeveCuffsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetBaseSleeveCuffsCount()
        {
            return _baseSleeveCuffRepository.GetAll().Count();
        }

        [HttpGet("GetBaseSleeveCuffsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetBaseSleeveCuffsFields()
        {
            return new BaseSleeveCuff().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetBaseSleeveCuffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BaseSleeveCuff>> GetBaseSleeveCuffs(int limit, int page)
        {
            var dbList = _baseSleeveCuffRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.BaseSleeveCuff>, List<BaseSleeveCuff>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetBaseSleeveCuffsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseSleeveCuff?>> GetBaseSleeveCuffsById(int id)
        {
            var dbEntity = await _baseSleeveCuffRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.BaseSleeveCuff, BaseSleeveCuff>(dbEntity);
        }

        [HttpPost("CreateBaseSleeveCuffs")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseSleeveCuff>> CreateBaseSleeveCuffs(BaseSleeveCuff entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseSleeveCuffRepository.Add(_mapper.Map<BaseSleeveCuff, DB.BaseSleeveCuff>(entity));
                return CreatedAtAction(nameof(GetBaseSleeveCuffsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateBaseSleeveCuffs")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseSleeveCuff>> UpdateBaseSleeveCuffs(BaseSleeveCuff entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseSleeveCuffRepository.Update(_mapper.Map<BaseSleeveCuff, DB.BaseSleeveCuff>(entity));
                return CreatedAtAction(nameof(GetBaseSleeveCuffsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteBaseSleeveCuffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteBaseSleeveCuffs(int id)
        {
            var entity = await _baseSleeveCuffRepository.GetById(id);

            if (entity != null)
            {
                await _baseSleeveCuffRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region BaseSportSuits table

        [HttpGet("GetBaseSportSuitsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetBaseSportSuitsCount()
        {
            return _baseSportSuitRepository.GetAll().Count();
        }

        [HttpGet("GetBaseSportSuitsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetBaseSportSuitsFields()
        {
            return new BaseSportSuit().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetBaseSportSuits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BaseSportSuit>> GetBaseSportSuits(int limit, int page)
        {
            var dbList = _baseSportSuitRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.BaseSportSuit>, List<BaseSportSuit>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetBaseSportSuitsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseSportSuit?>> GetBaseSportSuitsById(int id)
        {
            var dbEntity = await _baseSportSuitRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.BaseSportSuit, BaseSportSuit>(dbEntity);
        }

        [HttpPost("CreateBaseSportSuits")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseSportSuit>> CreateBaseSportSuits(BaseSportSuit entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseSportSuitRepository.Add(_mapper.Map<BaseSportSuit, DB.BaseSportSuit>(entity));
                return CreatedAtAction(nameof(GetBaseSportSuitsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateBaseSportSuits")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseSportSuit>> UpdateBaseSportSuits(BaseSportSuit entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseSportSuitRepository.Update(_mapper.Map<BaseSportSuit, DB.BaseSportSuit>(entity));
                return CreatedAtAction(nameof(GetBaseSportSuitsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteBaseSportSuits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteBaseSportSuits(int id)
        {
            var entity = await _baseSportSuitRepository.GetById(id);

            if (entity != null)
            {
                await _baseSportSuitRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region BaseSweaters table

        [HttpGet("GetBaseSweatersCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetBaseSweatersCount()
        {
            return _baseSweaterRepository.GetAll().Count();
        }

        [HttpGet("GetBaseSweatersFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetBaseSweatersFields()
        {
            return new BaseSweater().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetBaseSweaters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BaseSweater>> GetBaseSweaters(int limit, int page)
        {
            var dbList = _baseSweaterRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.BaseSweater>, List<BaseSweater>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetBaseSweatersById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseSweater?>> GetBaseSweatersById(int id)
        {
            var dbEntity = await _baseSweaterRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.BaseSweater, BaseSweater>(dbEntity);
        }

        [HttpPost("CreateBaseSweaters")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseSweater>> CreateBaseSweaters(BaseSweater entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseSweaterRepository.Add(_mapper.Map<BaseSweater, DB.BaseSweater>(entity));
                return CreatedAtAction(nameof(GetBaseSweatersById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateBaseSweaters")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseSweater>> UpdateBaseSweaters(BaseSweater entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _baseSweaterRepository.Update(_mapper.Map<BaseSweater, DB.BaseSweater>(entity));
                return CreatedAtAction(nameof(GetBaseSweatersById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteBaseSweaters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteBaseSweaters(int id)
        {
            var entity = await _baseSweaterRepository.GetById(id);

            if (entity != null)
            {
                await _baseSweaterRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Cart

        [HttpGet("GetCartCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetCartCount()
        {
            return _cartRepository.GetAll().Count();
        }

        [HttpGet("GetCartFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetCartFields()
        {
            return new Cart().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Cart>> GetCart(int limit, int page)
        {
            var dbList = _cartRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.Cart>, List<Cart>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCartById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cart?>> GetCartById(int id)
        {
            var dbEntity = await _cartRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.Cart, Cart>(dbEntity);
        }

        [HttpPost("CreateCart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Cart>> CreateCart(Cart entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _cartRepository.Add(_mapper.Map<Cart, DB.Cart>(entity));
                return CreatedAtAction(nameof(GetCartById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Cart>> UpdateCart(Cart entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _cartRepository.Update(_mapper.Map<Cart, DB.Cart>(entity));
                return CreatedAtAction(nameof(GetCartById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCart(int id)
        {
            var entity = await _cartRepository.GetById(id);

            if (entity != null)
            {
                await _cartRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Categories

        [HttpGet("GetCategoriesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetCategoriesCount()
        {
            return _categoryRepository.GetAll().Count();
        }

        [HttpGet("GetCategoriesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetCategoriesFields()
        {
            return new Category().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Category>> GetCategories(int limit, int page)
        {
            var dbList = _categoryRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.Category>, List<Category>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCategoriesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category?>> GetCategoriesById(int id)
        {
            var dbEntity = await _categoryRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.Category, Category>(dbEntity);
        }

        [HttpPost("CreateCategories")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category>> CreateCategories(Category entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _categoryRepository.Add(_mapper.Map<Category, DB.Category>(entity));
                return CreatedAtAction(nameof(GetCategoriesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCategories")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Category>> UpdateCategories(Category entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _categoryRepository.Update(_mapper.Map<Category, DB.Category>(entity));
                return CreatedAtAction(nameof(GetCategoriesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCategories(int id)
        {
            var entity = await _categoryRepository.GetById(id);

            if (entity != null)
            {
                await _categoryRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region CategoryHierarchy

        [HttpGet("GetCategoryHierarchyCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetCategoryHierarchyCount()
        {
            return _categoryHierarchyRepository.GetAll().Count();
        }

        [HttpGet("GetCategoryHierarchyFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetCategoryHierarchyFields()
        {
            return new CategoryHierarchy().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetCategoryHierarchy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CategoryHierarchy>> GetCategoryHierarchy(int limit, int page)
        {
            var dbList = _categoryHierarchyRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.CategoryHierarchy>, List<CategoryHierarchy>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCategoryHierarchyById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryHierarchy?>> GetCategoryHierarchyById(int id)
        {
            var dbEntity = await _categoryHierarchyRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.CategoryHierarchy, CategoryHierarchy>(dbEntity);
        }

        [HttpPost("CreateCategoryHierarchy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryHierarchy>> CreateCategoryHierarchy(CategoryHierarchy entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _categoryHierarchyRepository.Add(_mapper.Map<CategoryHierarchy, DB.CategoryHierarchy>(entity));
                return CreatedAtAction(nameof(GetCategoryHierarchyById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCategoryHierarchy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategoryHierarchy>> UpdateCategoryHierarchy(CategoryHierarchy entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _categoryHierarchyRepository.Update(_mapper.Map<CategoryHierarchy, DB.CategoryHierarchy>(entity));
                return CreatedAtAction(nameof(GetCategoryHierarchyById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCategoryHierarchy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCategoryHierarchy(int id)
        {
            var entity = await _categoryHierarchyRepository.GetById(id);

            if (entity != null)
            {
                await _categoryHierarchyRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Colors

        [HttpGet("GetColorsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetColorsCount()
        {
            return _colorRepository.GetAll().Count();
        }

        [HttpGet("GetColorsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetColorsFields()
        {
            return new Color().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetColors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Color>> GetColors(int limit, int page)
        {
            var dbList = _colorRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.Color>, List<Color>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetColorsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Color?>> GetColorsById(int id)
        {
            var dbEntity = await _colorRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.Color, Color>(dbEntity);
        }

        [HttpPost("CreateColors")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Color>> CreateColors(Color entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _colorRepository.Add(_mapper.Map<Color, DB.Color>(entity));
                return CreatedAtAction(nameof(GetColorsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateColors")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Color>> UpdateColors(Color entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _colorRepository.Update(_mapper.Map<Color, DB.Color>(entity));
                return CreatedAtAction(nameof(GetColorsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteColors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteColors(int id)
        {
            var entity = await _colorRepository.GetById(id);

            if (entity != null)
            {
                await _colorRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Contacts

        [HttpGet("GetContactsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetContactsCount()
        {
            return _contactRepository.GetAll().Count();
        }

        [HttpGet("GetContactsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetContactsFields()
        {
            return new Contact().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetContacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Contact>> GetContacts(int limit, int page)
        {
            var dbList = _contactRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.Contact>, List<Contact>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetContactsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contact?>> GetContactsById(int id)
        {
            var dbEntity = await _contactRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.Contact, Contact>(dbEntity);
        }

        [HttpPost("CreateContacts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Contact>> CreateContacts(Contact entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _contactRepository.Add(_mapper.Map<Contact, DB.Contact>(entity));
                return CreatedAtAction(nameof(GetContactsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateContacts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Contact>> UpdateContacts(Contact entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _contactRepository.Update(_mapper.Map<Contact, DB.Contact>(entity));
                return CreatedAtAction(nameof(GetContactsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteContacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteContacts(int id)
        {
            var entity = await _contactRepository.GetById(id);

            if (entity != null)
            {
                await _contactRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Countries table

        [HttpGet("GetCountriesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetCountriesCount()
        {
            return _countryRepository.GetAll().Count();
        }

        [HttpGet("GetCountriesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetCountriesFields()
        {
            return new Country().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetCountries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Country>> GetCountries(int limit, int page)
        {
            var dbList = _countryRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.Country>, List<Country>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCountriesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country?>> GetCountriesById(int id)
        {
            var dbEntity = await _countryRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.Country, Country>(dbEntity);
        }

        [HttpPost("CreateCountries")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Country>> CreateCountries(Country entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _countryRepository.Add(_mapper.Map<Country, DB.Country>(entity));
                return CreatedAtAction(nameof(GetCountriesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCountries")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Country>> UpdateCountries(Country entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _countryRepository.Update(_mapper.Map<Country, DB.Country>(entity));
                return CreatedAtAction(nameof(GetCountriesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCountries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCountries(int id)
        {
            var entity = await _countryRepository.GetById(id);

            if (entity != null)
            {
                await _countryRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Currencies table

        [HttpGet("GetCurrenciesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetCurrenciesCount()
        {
            return _currencyRepository.GetAll().Count();
        }

        [HttpGet("GetCurrenciesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetCurrenciesFields()
        {
            return new Currency().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetCurrencies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Currency>> GetCurrencies(int limit, int page)
        {
            var dbList = _currencyRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.Currency>, List<Currency>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCurrenciesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Currency?>> GetCurrenciesById(int id)
        {
            var dbEntity = await _currencyRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.Currency, Currency>(dbEntity);
        }

        [HttpPost("CreateCurrencies")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Currency>> CreateCurrencies(Currency entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _currencyRepository.Add(_mapper.Map<Currency, DB.Currency>(entity));
                return CreatedAtAction(nameof(GetCurrenciesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCurrencies")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Currency>> UpdateCurrencies(Currency entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _currencyRepository.Update(_mapper.Map<Currency, DB.Currency>(entity));
                return CreatedAtAction(nameof(GetCurrenciesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCurrencies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCurrencies(int id)
        {
            var entity = await _currencyRepository.GetById(id);

            if (entity != null)
            {
                await _currencyRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region CustomBelts table

        [HttpGet("GetCustomBeltsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetCustomBeltsCount()
        {
            return _customBeltRepository.GetAll().Count();
        }

        [HttpGet("GetCustomBeltsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetCustomBeltsFields()
        {
            return new CustomBelt().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetCustomBelts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomBelt>> GetCustomBelts(int limit, int page)
        {
            var dbList = _customBeltRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.CustomBelt>, List<CustomBelt>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCustomBeltsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomBelt?>> GetCustomBeltsById(int id)
        {
            var dbEntity = await _customBeltRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return _mapper.Map<DB.CustomBelt, CustomBelt>(dbEntity);
        }

        [HttpPost("CreateCustomBelts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomBelt>> CreateCustomBelts(CustomBelt customBelt)
        {
            if (customBelt == null)
                return BadRequest();

            try
            {
                await _customBeltRepository.Add(_mapper.Map<CustomBelt, DB.CustomBelt>(customBelt));
                return CreatedAtAction(nameof(GetCustomBeltsById), new { id = customBelt.Id }, customBelt);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCustomBelts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomBelt>> UpdateCustomBelts(CustomBelt customBelt)
        {
            if (customBelt == null)
                return BadRequest();

            try
            {
                await _customBeltRepository.Update(_mapper.Map<CustomBelt, DB.CustomBelt>(customBelt));
                return CreatedAtAction(nameof(GetCustomBeltsById), new { id = customBelt.Id }, customBelt);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCustomBelts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCustomBelts(int id)
        {
            var entity = await _customBeltRepository.GetById(id);

            if (entity != null)
            {
                await _customBeltRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region CustomNecklines table

        [HttpGet("GetCustomNecklinesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetCustomNecklinesCount()
        {
            return _customNecklineRepository.GetAll().Count();
        }

        [HttpGet("GetCustomNecklinesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetCustomNecklinesFields()
        {
            return typeof(CustomNeckline).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetCustomNecklines")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomNeckline>> GetCustomNecklines(int limit, int page)
        {
            var dbList = _customNecklineRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.CustomNeckline>, List<CustomNeckline>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCustomNecklinesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomNeckline?>> GetCustomNecklinesById(int id)
        {
            var dbEntity = await _customNecklineRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.CustomNeckline, CustomNeckline>(dbEntity);
        }

        [HttpPost("CreateCustomNecklines")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomNeckline>> CreateCustomNecklines(CustomNeckline customNeckline)
        {
            if (customNeckline == null)
                return BadRequest("CustomNeckline is null");

            try
            {
                var mappedEntity = _mapper.Map<CustomNeckline, DB.CustomNeckline>(customNeckline);
                await _customNecklineRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetCustomNecklinesById), new { id = mappedEntity.Id }, customNeckline);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCustomNecklines")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomNeckline>> UpdateCustomNecklines(CustomNeckline customNeckline)
        {
            if (customNeckline == null)
                return BadRequest("CustomNeckline is null");

            try
            {
                var mappedEntity = _mapper.Map<CustomNeckline, DB.CustomNeckline>(customNeckline);
                await _customNecklineRepository.Update(mappedEntity);
                return Ok(customNeckline);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCustomNecklines")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCustomNecklines(int id)
        {
            var entity = await _customNecklineRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest("CustomNeckline not found");
            }

            try
            {
                await _customNecklineRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region CustomPants table

        [HttpGet("GetCustomPantsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetCustomPantsCount()
        {
            return _customPantRepository.GetAll().Count();
        }

        [HttpGet("GetCustomPantsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetCustomPantsFields()
        {
            return typeof(CustomPant).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetCustomPants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomPant>> GetCustomPants(int limit, int page)
        {
            var dbList = _customPantRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.CustomPant>, List<CustomPant>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCustomPantsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomPant?>> GetCustomPantsById(int id)
        {
            var dbEntity = await _customPantRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.CustomPant, CustomPant>(dbEntity);
        }

        [HttpPost("CreateCustomPants")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomPant>> CreateCustomPants(CustomPant customPant)
        {
            if (customPant == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomPant, DB.CustomPant>(customPant);
                await _customPantRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetCustomPantsById), new { id = mappedEntity.Id }, customPant);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCustomPants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomPant>> UpdateCustomPants(CustomPant customPant)
        {
            if (customPant == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomPant, DB.CustomPant>(customPant);
                await _customPantRepository.Update(mappedEntity);
                return Ok(customPant);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCustomPants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCustomPants(int id)
        {
            var entity = await _customPantRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _customPantRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region CustomPantsCuffs table

        [HttpGet("GetCustomPantsCuffsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetCustomPantsCuffsCount()
        {
            return _customPantsCuffRepository.GetAll().Count();
        }

        [HttpGet("GetCustomPantsCuffsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetCustomPantsCuffsFields()
        {
            return typeof(CustomPantsCuff).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetCustomPantsCuffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomPantsCuff>> GetCustomPantsCuffs(int limit, int page)
        {
            var dbList = _customPantsCuffRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.CustomPantsCuff>, List<CustomPantsCuff>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCustomPantsCuffsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomPantsCuff?>> GetCustomPantsCuffsById(int id)
        {
            var dbEntity = await _customPantsCuffRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.CustomPantsCuff, CustomPantsCuff>(dbEntity);
        }

        [HttpPost("CreateCustomPantsCuffs")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomPantsCuff>> CreateCustomPantsCuffs(CustomPantsCuff customPantsCuff)
        {
            if (customPantsCuff == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomPantsCuff, DB.CustomPantsCuff>(customPantsCuff);
                await _customPantsCuffRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetCustomPantsCuffsById), new { id = mappedEntity.Id }, customPantsCuff);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCustomPantsCuffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomPantsCuff>> UpdateCustomPantsCuffs(CustomPantsCuff customPantsCuff)
        {
            if (customPantsCuff == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomPantsCuff, DB.CustomPantsCuff>(customPantsCuff);
                await _customPantsCuffRepository.Update(mappedEntity);
                return Ok(customPantsCuff);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCustomPantsCuffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCustomPantsCuffs(int id)
        {
            var entity = await _customPantsCuffRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _customPantsCuffRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region CustomSleeves table

        [HttpGet("GetCustomSleevesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetCustomSleevesCount()
        {
            return _customSleeveRepository.GetAll().Count();
        }

        [HttpGet("GetCustomSleevesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetCustomSleevesFields()
        {
            return typeof(CustomSleeve).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetCustomSleeves")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomSleeve>> GetCustomSleeves(int limit, int page)
        {
            var dbList = _customSleeveRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.CustomSleeve>, List<CustomSleeve>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCustomSleevesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomSleeve?>> GetCustomSleevesById(int id)
        {
            var dbEntity = await _customSleeveRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.CustomSleeve, CustomSleeve>(dbEntity);
        }

        [HttpPost("CreateCustomSleeves")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomSleeve>> CreateCustomSleeves(CustomSleeve customSleeve)
        {
            if (customSleeve == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomSleeve, DB.CustomSleeve>(customSleeve);
                await _customSleeveRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetCustomSleevesById), new { id = mappedEntity.Id }, customSleeve);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCustomSleeves")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomSleeve>> UpdateCustomSleeves(CustomSleeve customSleeve)
        {
            if (customSleeve == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomSleeve, DB.CustomSleeve>(customSleeve);
                await _customSleeveRepository.Update(mappedEntity);
                return Ok(customSleeve);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCustomSleeves")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCustomSleeves(int id)
        {
            var entity = await _customSleeveRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _customSleeveRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region CustomSleeveCuffs table

        [HttpGet("GetCustomSleeveCuffsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetCustomSleeveCuffsCount()
        {
            return _customSleeveCuffRepository.GetAll().Count();
        }

        [HttpGet("GetCustomSleeveCuffsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetCustomSleeveCuffsFields()
        {
            return typeof(CustomSleeveCuff).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetCustomSleeveCuffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomSleeveCuff>> GetCustomSleeveCuffs(int limit, int page)
        {
            var dbList = _customSleeveCuffRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.CustomSleeveCuff>, List<CustomSleeveCuff>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCustomSleeveCuffsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomSleeveCuff?>> GetCustomSleeveCuffsById(int id)
        {
            var dbEntity = await _customSleeveCuffRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.CustomSleeveCuff, CustomSleeveCuff>(dbEntity);
        }

        [HttpPost("CreateCustomSleeveCuffs")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomSleeveCuff>> CreateCustomSleeveCuffs(CustomSleeveCuff customSleeveCuff)
        {
            if (customSleeveCuff == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomSleeveCuff, DB.CustomSleeveCuff>(customSleeveCuff);
                await _customSleeveCuffRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetCustomSleeveCuffsById), new { id = mappedEntity.Id }, customSleeveCuff);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCustomSleeveCuffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomSleeveCuff>> UpdateCustomSleeveCuffs(CustomSleeveCuff customSleeveCuff)
        {
            if (customSleeveCuff == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomSleeveCuff, DB.CustomSleeveCuff>(customSleeveCuff);
                await _customSleeveCuffRepository.Update(mappedEntity);
                return Ok(customSleeveCuff);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCustomSleeveCuffs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCustomSleeveCuffs(int id)
        {
            var entity = await _customSleeveCuffRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _customSleeveCuffRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region CustomSportSuits table

        [HttpGet("GetCustomSportSuitsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetCustomSportSuitsCount()
        {
            return _customSportSuitRepository.GetAll().Count();
        }

        [HttpGet("GetCustomSportSuitsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetCustomSportSuitsFields()
        {
            return typeof(CustomSportSuit).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetCustomSportSuits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomSportSuit>> GetCustomSportSuits(int limit, int page)
        {
            var dbList = _customSportSuitRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.CustomSportSuit>, List<CustomSportSuit>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCustomSportSuitsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomSportSuit?>> GetCustomSportSuitsById(int id)
        {
            var dbEntity = await _customSportSuitRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.CustomSportSuit, CustomSportSuit>(dbEntity);
        }

        [HttpPost("CreateCustomSportSuits")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomSportSuit>> CreateCustomSportSuits(CustomSportSuit customSportSuit)
        {
            if (customSportSuit == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomSportSuit, DB.CustomSportSuit>(customSportSuit);
                await _customSportSuitRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetCustomSportSuitsById), new { id = mappedEntity.Id }, customSportSuit);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCustomSportSuits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomSportSuit>> UpdateCustomSportSuits(CustomSportSuit customSportSuit)
        {
            if (customSportSuit == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomSportSuit, DB.CustomSportSuit>(customSportSuit);
                await _customSportSuitRepository.Update(mappedEntity);
                return Ok(customSportSuit);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCustomSportSuits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCustomSportSuits(int id)
        {
            var entity = await _customSportSuitRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _customSportSuitRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region CustomSweaters table

        [HttpGet("GetCustomSweatersCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetCustomSweatersCount()
        {
            return _customSweaterRepository.GetAll().Count();
        }

        [HttpGet("GetCustomSweatersFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetCustomSweatersFields()
        {
            return typeof(CustomSweater).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetCustomSweaters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomSweater>> GetCustomSweaters(int limit, int page)
        {
            var dbList = _customSweaterRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.CustomSweater>, List<CustomSweater>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCustomSweatersById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomSweater?>> GetCustomSweatersById(int id)
        {
            var dbEntity = await _customSweaterRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.CustomSweater, CustomSweater>(dbEntity);
        }

        [HttpPost("CreateCustomSweaters")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomSweater>> CreateCustomSweaters(CustomSweater customSweater)
        {
            if (customSweater == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomSweater, DB.CustomSweater>(customSweater);
                await _customSweaterRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetCustomSweatersById), new { id = mappedEntity.Id }, customSweater);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCustomSweaters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomSweater>> UpdateCustomSweaters(CustomSweater customSweater)
        {
            if (customSweater == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<CustomSweater, DB.CustomSweater>(customSweater);
                await _customSweaterRepository.Update(mappedEntity);
                return Ok(customSweater);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCustomSweaters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCustomSweaters(int id)
        {
            var entity = await _customSweaterRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _customSweaterRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region CustomizableProducts

        [HttpGet("GetCustomizableProductsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetCustomizableProductsCount()
        {
            return _customizableProductRepository.GetAll().Count();
        }

        [HttpGet("GetCustomizableProductsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetCustomizableProductsFields()
        {
            return new CustomizableProduct().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetCustomizableProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CustomizableProduct>> GetCustomizableProducts(int limit, int page)
        {
            var dbList = _customizableProductRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.CustomizableProduct>, List<CustomizableProduct>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetCustomizableProductsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomizableProduct?>> GetCustomizableProductsById(int id)
        {
            var dbEntity = await _customizableProductRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.CustomizableProduct, CustomizableProduct>(dbEntity);
        }

        [HttpPost("CreateCustomizableProducts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomizableProduct>> CreateCustomizableProducts(CustomizableProduct entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _customizableProductRepository.Add(_mapper.Map<CustomizableProduct, DB.CustomizableProduct>(entity));
                return CreatedAtAction(nameof(GetCustomizableProductsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateCustomizableProducts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomizableProduct>> UpdateCustomizableProducts(CustomizableProduct entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _customizableProductRepository.Update(_mapper.Map<CustomizableProduct, DB.CustomizableProduct>(entity));
                return CreatedAtAction(nameof(GetCustomizableProductsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteCustomizableProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCustomizableProducts(int id)
        {
            var entity = await _customizableProductRepository.GetById(id);

            if (entity != null)
            {
                await _customizableProductRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region FabricTypes

        [HttpGet("GetFabricTypesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetFabricTypesCount()
        {
            return _fabricTypeRepository.GetAll().Count();
        }

        [HttpGet("GetFabricTypesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetFabricTypesFields()
        {
            return new FabricType().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetFabricTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<FabricType>> GetFabricTypes(int limit, int page)
        {
            var dbList = _fabricTypeRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.FabricType>, List<FabricType>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetFabricTypesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FabricType?>> GetFabricTypesById(int id)
        {
            var dbEntity = await _fabricTypeRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.FabricType, FabricType>(dbEntity);
        }

        [HttpPost("CreateFabricTypes")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FabricType>> CreateFabricTypes(FabricType entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _fabricTypeRepository.Add(_mapper.Map<FabricType, DB.FabricType>(entity));
                return CreatedAtAction(nameof(GetFabricTypesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateFabricTypes")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FabricType>> UpdateFabricTypes(FabricType entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _fabricTypeRepository.Update(_mapper.Map<FabricType, DB.FabricType>(entity));
                return CreatedAtAction(nameof(GetFabricTypesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteFabricTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteFabricTypes(int id)
        {
            var entity = await _fabricTypeRepository.GetById(id);

            if (entity != null)
            {
                await _fabricTypeRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Languages

        [HttpGet("GetLanguagesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetLanguagesCount()
        {
            return _languageRepository.GetAll().Count();
        }

        [HttpGet("GetLanguagesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetLanguagesFields()
        {
            return new Language().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetLanguages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Language>> GetLanguages(int limit, int page)
        {
            var dbList = _languageRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.Language>, List<Language>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetLanguagesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Language?>> GetLanguagesById(int id)
        {
            var dbEntity = await _languageRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.Language, Language>(dbEntity);
        }

        [HttpPost("CreateLanguages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Language>> CreateLanguages(Language entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _languageRepository.Add(_mapper.Map<Language, DB.Language>(entity));
                return CreatedAtAction(nameof(GetLanguagesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateLanguages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Language>> UpdateLanguages(Language entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _languageRepository.Update(_mapper.Map<Language, DB.Language>(entity));
                return CreatedAtAction(nameof(GetLanguagesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteLanguages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteLanguages(int id)
        {
            var entity = await _languageRepository.GetById(id);

            if (entity != null)
            {
                await _languageRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Orders

        [HttpGet("GetOrdersCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetOrdersCount()
        {
            return _orderRepository.GetAll().Count();
        }

        [HttpGet("GetOrdersFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetOrdersFields()
        {
            return new Order().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Order>> GetOrders(int limit, int page)
        {
            var dbList = _orderRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.Order>, List<Order>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetOrdersById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order?>> GetOrdersById(int id)
        {
            var dbEntity = await _orderRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.Order, Order>(dbEntity);
        }

        [HttpPost("CreateOrders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Order>> CreateOrders(Order entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _orderRepository.Add(_mapper.Map<Order, DB.Order>(entity));
                return CreatedAtAction(nameof(GetOrdersById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateOrders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Order>> UpdateOrders(Order entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _orderRepository.Update(_mapper.Map<Order, DB.Order>(entity));
                return CreatedAtAction(nameof(GetOrdersById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteOrders(int id)
        {
            var entity = await _orderRepository.GetById(id);

            if (entity != null)
            {
                await _orderRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region OrderHistory

        [HttpGet("GetOrderHistoryCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetOrderHistoryCount()
        {
            return _orderHistoryRepository.GetAll().Count();
        }

        [HttpGet("GetOrderHistoryFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetOrderHistoryFields()
        {
            return new OrderHistory().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetOrderHistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<OrderHistory>> GetOrderHistory(int limit, int page)
        {
            var dbList = _orderHistoryRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.OrderHistory>, List<OrderHistory>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetOrderHistoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderHistory?>> GetOrderHistoryById(int id)
        {
            var dbEntity = await _orderHistoryRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.OrderHistory, OrderHistory>(dbEntity);
        }

        [HttpPost("CreateOrderHistory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderHistory>> CreateOrderHistory(OrderHistory entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _orderHistoryRepository.Add(_mapper.Map<OrderHistory, DB.OrderHistory>(entity));
                return CreatedAtAction(nameof(GetOrderHistoryById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateOrderHistory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderHistory>> UpdateOrderHistory(OrderHistory entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _orderHistoryRepository.Update(_mapper.Map<OrderHistory, DB.OrderHistory>(entity));
                return CreatedAtAction(nameof(GetOrderHistoryById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteOrderHistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteOrderHistory(int id)
        {
            var entity = await _orderHistoryRepository.GetById(id);

            if (entity != null)
            {
                await _orderHistoryRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region OrderStatuses

        [HttpGet("GetOrderStatusesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetOrderStatusesCount()
        {
            return _orderStatusRepository.GetAll().Count();
        }

        [HttpGet("GetOrderStatusesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetOrderStatusesFields()
        {
            return new OrderStatus().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetOrderStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<OrderStatus>> GetOrderStatuses(int limit, int page)
        {
            var dbList = _orderStatusRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.OrderStatus>, List<OrderStatus>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetOrderStatusesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderStatus?>> GetOrderStatusesById(int id)
        {
            var dbEntity = await _orderStatusRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.OrderStatus, OrderStatus>(dbEntity);
        }

        [HttpPost("CreateOrderStatuses")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderStatus>> CreateOrderStatuses(OrderStatus entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _orderStatusRepository.Add(_mapper.Map<OrderStatus, DB.OrderStatus>(entity));
                return CreatedAtAction(nameof(GetOrderStatusesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateOrderStatuses")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderStatus>> UpdateOrderStatuses(OrderStatus entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _orderStatusRepository.Update(_mapper.Map<OrderStatus, DB.OrderStatus>(entity));
                return CreatedAtAction(nameof(GetOrderStatusesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteOrderStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteOrderStatuses(int id)
        {
            var entity = await _orderStatusRepository.GetById(id);

            if (entity != null)
            {
                await _orderStatusRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Payments

        [HttpGet("GetPaymentsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetPaymentsCount()
        {
            return _paymentRepository.GetAll().Count();
        }

        [HttpGet("GetPaymentsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetPaymentsFields()
        {
            return new Payment().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetPayments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Payment>> GetPayments(int limit, int page)
        {
            var dbList = _paymentRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.Payment>, List<Payment>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetPaymentsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Payment?>> GetPaymentsById(int id)
        {
            var dbEntity = await _paymentRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.Payment, Payment>(dbEntity);
        }

        [HttpPost("CreatePayments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Payment>> CreatePayments(Payment entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _paymentRepository.Add(_mapper.Map<Payment, DB.Payment>(entity));
                return CreatedAtAction(nameof(GetPaymentsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdatePayments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Payment>> UpdatePayments(Payment entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _paymentRepository.Update(_mapper.Map<Payment, DB.Payment>(entity));
                return CreatedAtAction(nameof(GetPaymentsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeletePayments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeletePayments(int id)
        {
            var entity = await _paymentRepository.GetById(id);

            if (entity != null)
            {
                await _paymentRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region PaymentMethods table

        [HttpGet("GetPaymentMethodsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetPaymentMethodsCount()
        {
            return _paymentMethodRepository.GetAll().Count();
        }

        [HttpGet("GetPaymentMethodsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetPaymentMethodsFields()
        {
            return typeof(PaymentMethod).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetPaymentMethods")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PaymentMethod>> GetPaymentMethods(int limit, int page)
        {
            var dbList = _paymentMethodRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.PaymentMethod>, List<PaymentMethod>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetPaymentMethodsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentMethod?>> GetPaymentMethodsById(int id)
        {
            var dbEntity = await _paymentMethodRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.PaymentMethod, PaymentMethod>(dbEntity);
        }

        [HttpPost("CreatePaymentMethods")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentMethod>> CreatePaymentMethods(PaymentMethod paymentMethod)
        {
            if (paymentMethod == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<PaymentMethod, DB.PaymentMethod>(paymentMethod);
                await _paymentMethodRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetPaymentMethodsById), new { id = mappedEntity.Id }, paymentMethod);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdatePaymentMethods")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentMethod>> UpdatePaymentMethods(PaymentMethod paymentMethod)
        {
            if (paymentMethod == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<PaymentMethod, DB.PaymentMethod>(paymentMethod);
                await _paymentMethodRepository.Update(mappedEntity);
                return Ok(paymentMethod);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeletePaymentMethods")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeletePaymentMethods(int id)
        {
            var entity = await _paymentMethodRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _paymentMethodRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region PaymentStatuses table

        [HttpGet("GetPaymentStatusesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetPaymentStatusesCount()
        {
            return _paymentStatusRepository.GetAll().Count();
        }

        [HttpGet("GetPaymentStatusesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetPaymentStatusesFields()
        {
            return typeof(PaymentStatus).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetPaymentStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PaymentStatus>> GetPaymentStatuses(int limit, int page)
        {
            var dbList = _paymentStatusRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.PaymentStatus>, List<PaymentStatus>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetPaymentStatusesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentStatus?>> GetPaymentStatusesById(int id)
        {
            var dbEntity = await _paymentStatusRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.PaymentStatus, PaymentStatus>(dbEntity);
        }

        [HttpPost("CreatePaymentStatuses")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentStatus>> CreatePaymentStatuses(PaymentStatus paymentStatus)
        {
            if (paymentStatus == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<PaymentStatus, DB.PaymentStatus>(paymentStatus);
                await _paymentStatusRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetPaymentStatusesById), new { id = mappedEntity.Id }, paymentStatus);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdatePaymentStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentStatus>> UpdatePaymentStatuses(PaymentStatus paymentStatus)
        {
            if (paymentStatus == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<PaymentStatus, DB.PaymentStatus>(paymentStatus);
                await _paymentStatusRepository.Update(mappedEntity);
                return Ok(paymentStatus);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeletePaymentStatuses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeletePaymentStatuses(int id)
        {
            var entity = await _paymentStatusRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _paymentStatusRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region Products

        [HttpGet("GetProductsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetProductsCount()
        {
            return _productRepository.GetAll().Count();
        }

        [HttpGet("GetProductsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetProductsFields()
        {
            return new Product().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Product>> GetProducts(int limit, int page)
        {
            var dbList = _productRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.Product>, List<Product>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetProductsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product?>> GetProductsById(int id)
        {
            var dbEntity = await _productRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.Product, Product>(dbEntity);
        }

        [HttpPost("CreateProducts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> CreateProducts(Product entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _productRepository.Add(_mapper.Map<Product, DB.Product>(entity));
                return CreatedAtAction(nameof(GetProductsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateProducts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> UpdateProducts(Product entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _productRepository.Update(_mapper.Map<Product, DB.Product>(entity));
                return CreatedAtAction(nameof(GetProductsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteProducts(int id)
        {
            var entity = await _productRepository.GetById(id);

            if (entity != null)
            {
                await _productRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region ProductImages

        [HttpGet("GetProductImagesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetProductImagesCount()
        {
            return _productImageRepository.GetAll().Count();
        }

        [HttpGet("GetProductImagesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetProductImagesFields()
        {
            return new ProductImage().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetProductImages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ProductImage>> GetProductImages(int limit, int page)
        {
            var dbList = _productImageRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.ProductImage>, List<ProductImage>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetProductImagesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductImage?>> GetProductImagesById(int id)
        {
            var dbEntity = await _productImageRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.ProductImage, ProductImage>(dbEntity);
        }

        [HttpPost("CreateProductImages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductImage>> CreateProductImages(ProductImage entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _productImageRepository.Add(_mapper.Map<ProductImage, DB.ProductImage>(entity));
                return CreatedAtAction(nameof(GetProductImagesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateProductImages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductImage>> UpdateProductImages(ProductImage entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _productImageRepository.Update(_mapper.Map<ProductImage, DB.ProductImage>(entity));
                return CreatedAtAction(nameof(GetProductImagesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteProductImages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteProductImages(int id)
        {
            var entity = await _productImageRepository.GetById(id);

            if (entity != null)
            {
                await _productImageRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region ProductOrders

        [HttpGet("GetProductOrdersCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetProductOrdersCount()
        {
            return _productOrderRepository.GetAll().Count();
        }

        [HttpGet("GetProductOrdersFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetProductOrdersFields()
        {
            return new ProductOrder().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetProductOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ProductOrder>> GetProductOrders(int limit, int page)
        {
            var dbList = _productOrderRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.ProductOrder>, List<ProductOrder>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetProductOrdersById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductOrder?>> GetProductOrdersById(int id)
        {
            var dbEntity = await _productOrderRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.ProductOrder, ProductOrder>(dbEntity);
        }

        [HttpPost("CreateProductOrders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductOrder>> CreateProductOrders(ProductOrder entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _productOrderRepository.Add(_mapper.Map<ProductOrder, DB.ProductOrder>(entity));
                return CreatedAtAction(nameof(GetProductOrdersById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateProductOrders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductOrder>> UpdateProductOrders(ProductOrder entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _productOrderRepository.Update(_mapper.Map<ProductOrder, DB.ProductOrder>(entity));
                return CreatedAtAction(nameof(GetProductOrdersById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteProductOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteProductOrders(int id)
        {
            var entity = await _productOrderRepository.GetById(id);

            if (entity != null)
            {
                await _productOrderRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region ProductTranslations

        [HttpGet("GetProductTranslationsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetProductTranslationsCount()
        {
            return _productTranslationRepository.GetAll().Count();
        }

        [HttpGet("GetProductTranslationsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetProductTranslationsFields()
        {
            return new ProductTranslation().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetProductTranslations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ProductTranslation>> GetProductTranslations(int limit, int page)
        {
            var dbList = _productTranslationRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.ProductTranslation>, List<ProductTranslation>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetProductTranslationsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductTranslation?>> GetProductTranslationsById(int id)
        {
            var dbEntity = await _productTranslationRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.ProductTranslation, ProductTranslation>(dbEntity);
        }

        [HttpPost("CreateProductTranslations")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductTranslation>> CreateProductTranslations(ProductTranslation entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _productTranslationRepository.Add(_mapper.Map<ProductTranslation, DB.ProductTranslation>(entity));
                return CreatedAtAction(nameof(GetProductTranslationsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateProductTranslations")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductTranslation>> UpdateProductTranslations(ProductTranslation entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _productTranslationRepository.Update(_mapper.Map<ProductTranslation, DB.ProductTranslation>(entity));
                return CreatedAtAction(nameof(GetProductTranslationsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteProductTranslations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteProductTranslations(int id)
        {
            var entity = await _productTranslationRepository.GetById(id);

            if (entity != null)
            {
                await _productTranslationRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Reviews

        [HttpGet("GetReviewsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetReviewsCount()
        {
            return _reviewRepository.GetAll().Count();
        }

        [HttpGet("GetReviewsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetReviewsFields()
        {
            return new Review().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetReviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Review>> GetReviews(int limit, int page)
        {
            var dbList = _reviewRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.Review>, List<Review>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetReviewsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Review?>> GetReviewsById(int id)
        {
            var dbEntity = await _reviewRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.Review, Review>(dbEntity);
        }

        [HttpPost("CreateReviews")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Review>> CreateReviews(Review entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _reviewRepository.Add(_mapper.Map<Review, DB.Review>(entity));
                return CreatedAtAction(nameof(GetReviewsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateReviews")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Review>> UpdateReviews(Review entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _reviewRepository.Update(_mapper.Map<Review, DB.Review>(entity));
                return CreatedAtAction(nameof(GetReviewsById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteReviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteReviews(int id)
        {
            var entity = await _reviewRepository.GetById(id);

            if (entity != null)
            {
                await _reviewRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region ShippingAddresses table

        [HttpGet("GetShippingAddressesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetShippingAddressesCount()
        {
            return _shippingAddressRepository.GetAll().Count();
        }

        [HttpGet("GetShippingAddressesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetShippingAddressFields()
        {
            return typeof(ShippingAddress).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetShippingAddresses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ShippingAddress>> GetShippingAddresses(int limit, int page)
        {
            var dbList = _shippingAddressRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.ShippingAddress>, List<ShippingAddress>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetShippingAddressesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShippingAddress?>> GetShippingAddressesById(int id)
        {
            var dbEntity = await _shippingAddressRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.ShippingAddress, ShippingAddress>(dbEntity);
        }

        [HttpPost("CreateShippingAddresses")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShippingAddress>> CreateShippingAddresses(ShippingAddress shippingAddress)
        {
            if (shippingAddress == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<ShippingAddress, DB.ShippingAddress>(shippingAddress);
                await _shippingAddressRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetShippingAddressesById), new { id = mappedEntity.Id }, shippingAddress);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateShippingAddresses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ShippingAddress>> UpdateShippingAddresses(ShippingAddress shippingAddress)
        {
            if (shippingAddress == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<ShippingAddress, DB.ShippingAddress>(shippingAddress);
                await _shippingAddressRepository.Update(mappedEntity);
                return Ok(shippingAddress);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteShippingAddresses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteShippingAddresses(int id)
        {
            var entity = await _shippingAddressRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _shippingAddressRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region SizeOptions table

        [HttpGet("GetSizeOptionsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetSizeOptionCount()
        {
            return _sizeOptionRepository.GetAll().Count();
        }

        [HttpGet("GetSizeOptionsFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetSizeOptionsFields()
        {
            return typeof(SizeOption).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetSizeOptions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<SizeOption>> GetSizeOptions(int limit, int page)
        {
            var dbList = _sizeOptionRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.SizeOption>, List<SizeOption>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetSizeOptionsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SizeOption?>> GetSizeOptionsById(int id)
        {
            var dbEntity = await _sizeOptionRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.SizeOption, SizeOption>(dbEntity);
        }

        [HttpPost("CreateSizeOptions")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SizeOption>> CreateSizeOptions(SizeOption sizeOption)
        {
            if (sizeOption == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<SizeOption, DB.SizeOption>(sizeOption);
                await _sizeOptionRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetSizeOptionsById), new { id = mappedEntity.Id }, sizeOption);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateSizeOptions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SizeOption>> UpdateSizeOptions(SizeOption sizeOption)
        {
            if (sizeOption == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<SizeOption, DB.SizeOption>(sizeOption);
                await _sizeOptionRepository.Update(mappedEntity);
                return Ok(sizeOption);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteSizeOptions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteSizeOptions(int id)
        {
            var entity = await _sizeOptionRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _sizeOptionRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region Users

        [HttpGet("GetUsersCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetUsersCount()
        {
            return _userRepository.GetAll().Count();
        }

        [HttpGet("GetUsersFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetUsersFields()
        {
            return new User().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<User>> GetUsers(int limit, int page)
        {
            var dbList = _userRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.User>, List<User>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetUsersById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User?>> GetUsersById(int id)
        {
            var dbEntity = await _userRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.User, User>(dbEntity);
        }

        [HttpPost("CreateUsers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> CreateUsers(User entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _userRepository.Add(_mapper.Map<User, DB.User>(entity));
                return CreatedAtAction(nameof(CreateUsers), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateUsers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> UpdateUsers(User entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _userRepository.Update(_mapper.Map<User, DB.User>(entity));
                return CreatedAtAction(nameof(GetUsersById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteUsers(int id)
        {
            var entity = await _userRepository.GetById(id);

            if (entity != null)
            {
                await _userRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region UserOrderHistory table

        [HttpGet("GetUserOrderHistoryCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public int GetUserOrderHistoryCount()
        {
            return _userOrderHistoryRepository.GetAll().Count();
        }

        [HttpGet("GetUserOrderHistoryFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetUserOrderHistoryFields()
        {
            return typeof(UserOrderHistory).GetProperties().Select(prop => prop.Name);
        }

        [HttpGet("GetUserOrderHistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UserOrderHistory>> GetUserOrderHistory(int limit, int page)
        {
            var dbList = _userOrderHistoryRepository.GetAll().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.UserOrderHistory>, List<UserOrderHistory>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetUserOrderHistoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserOrderHistory?>> GetUserOrderHistoryById(int id)
        {
            var dbEntity = await _userOrderHistoryRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.UserOrderHistory, UserOrderHistory>(dbEntity);
        }

        [HttpPost("CreateUserOrderHistory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserOrderHistory>> CreateUserOrderHistory(UserOrderHistory userOrderHistory)
        {
            if (userOrderHistory == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<UserOrderHistory, DB.UserOrderHistory>(userOrderHistory);
                await _userOrderHistoryRepository.Add(mappedEntity);
                return CreatedAtAction(nameof(GetUserOrderHistoryById), new { id = mappedEntity.Id }, userOrderHistory);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateUserOrderHistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserOrderHistory>> UpdateUserOrderHistory(UserOrderHistory userOrderHistory)
        {
            if (userOrderHistory == null)
                return BadRequest();

            try
            {
                var mappedEntity = _mapper.Map<UserOrderHistory, DB.UserOrderHistory>(userOrderHistory);
                await _userOrderHistoryRepository.Update(mappedEntity);
                return Ok(userOrderHistory);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteUserOrderHistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteUserOrderHistory(int id)
        {
            var entity = await _userOrderHistoryRepository.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                await _userOrderHistoryRepository.Delete(entity);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        #endregion

        #region UserProfiles

        [HttpGet("GetUserProfilesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetUserProfilesCount()
        {
            return _userProfileRepository.GetAll().Count();
        }

        [HttpGet("GetUserProfilesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetUserProfilesFields()
        {
            return new UserProfile().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetUserProfiles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UserProfile>> GetUserProfiles(int limit, int page)
        {
            var dbList = _userProfileRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.UserProfile>, List<UserProfile>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetUserProfilesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserProfile?>> GetUserProfilesById(int id)
        {
            var dbEntity = await _userProfileRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.UserProfile, UserProfile>(dbEntity);
        }

        [HttpPost("CreateUserProfiles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserProfile>> CreateUserProfiles(UserProfile entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _userProfileRepository.Add(_mapper.Map<UserProfile, DB.UserProfile>(entity));
                return CreatedAtAction(nameof(GetUserProfilesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateUserProfiles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserProfile>> UpdateUserProfiles(UserProfile entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _userProfileRepository.Update(_mapper.Map<UserProfile, DB.UserProfile>(entity));
                return CreatedAtAction(nameof(GetUserProfilesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteUserProfiles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteUserProfiles(int id)
        {
            var entity = await _userProfileRepository.GetById(id);

            if (entity != null)
            {
                await _userProfileRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region UserRoles

        [HttpGet("GetUserRolesCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public int GetUserRolesCount()
        {
            return _userRoleRepository.GetAll().Count();
        }

        [HttpGet("GetUserRolesFields")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<string> GetUserRolesFields()
        {
            return new UserRole().GetType().GetProperties().Select(x => x.Name);
        }

        [HttpGet("GetUserRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UserRole>> GetUserRoles(int limit, int page)
        {
            var dbList = _userRoleRepository.GetAll().ToList().OrderBy(x => x.Id).Skip((page - 1) * limit).Take(limit).ToList();
            var apiList = _mapper.Map<List<DB.UserRole>, List<UserRole>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        [HttpGet("GetUserRolesById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserRole?>> GetUserRolesById(int id)
        {
            var dbEntity = await _userRoleRepository.GetById(id);
            if (dbEntity == null)
            {
                return NotFound();
            }

            return dbEntity == null ? NotFound() : _mapper.Map<DB.UserRole, UserRole>(dbEntity);
        }

        [HttpPost("CreateUserRoles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserRole>> CreateUserRoles(UserRole entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _userRoleRepository.Add(_mapper.Map<UserRole, DB.UserRole>(entity));
                return CreatedAtAction(nameof(GetUserRolesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpPut("UpdateUserRoles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserRole>> UpdateUserRoles(UserRole entity)
        {
            if (entity == null)
                return BadRequest();

            try
            {
                await _userRoleRepository.Update(_mapper.Map<UserRole, DB.UserRole>(entity));
                return CreatedAtAction(nameof(GetUserRolesById), new { id = entity.Id }, entity);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, exc.InnerException);
            }
        }

        [HttpDelete("DeleteUserRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteUserRoles(int id)
        {
            var entity = await _userRoleRepository.GetById(id);

            if (entity != null)
            {
                await _userRoleRepository.Delete(entity);
                return Ok();
            }

            return BadRequest();
        }

        #endregion

        #region Get Foreign Keys

        [HttpGet("GetFkAccessLevelsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkAccessLevelsData(int id)
        {
            try
            {
                var dbEntity = await _accessLevelRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Level: {dbEntity?.Level} | Description: {dbEntity?.Description}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkLanguagesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkLanguagesData(int id)
        {
            try
            {
                var dbEntity = await _languageRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | FullName: {dbEntity?.FullName} | Abbreviation: {dbEntity?.Abbreviation} | Description: {dbEntity?.Description}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCategoriesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCategoriesData(int id)
        {
            try
            {
                var dbEntity = await _categoryRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Name: {dbEntity?.Name} | ParentID: {dbEntity?.Description}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkFabricTypesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkFabricTypesData(int id)
        {
            try
            {
                var dbEntity = await _fabricTypeRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Name: {dbEntity?.Name}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkProductsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkProductsData(int id)
        {
            try
            {
                var dbEntity = await _productRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Price: {dbEntity?.Price}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkProductImagesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkProductImagesData(int id)
        {
            try
            {
                var dbEntity = await _productImageRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | ImagePath: {dbEntity?.ImageData} | ProductID: {dbEntity?.FkProducts}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkProductTranslationsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkProductTranslationsData(int id)
        {
            try
            {
                var dbEntity = await _productTranslationRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Name: {dbEntity?.Name} | Description: {dbEntity?.Description} | LanguageID: {dbEntity?.FkLanguages}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkReviewsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkReviewsData(int id)
        {
            try
            {
                var dbEntity = await _reviewRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Rating: {dbEntity?.Rating} | Comment: {dbEntity?.Comment} | ProductID: {dbEntity?.FkProducts} | UserID: {dbEntity?.FkUsers}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkOrdersData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkOrdersData(int id)
        {
            try
            {
                var dbEntity = await _orderRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | StatusID: {dbEntity?.FkOrderStatus}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkOrderHistoryData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkOrderHistoryData(int id)
        {
            try
            {
                var dbEntity = await _orderHistoryRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | FkOrderStatuses: {dbEntity?.FkOrderStatuses}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkPaymentsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkPaymentsData(int id)
        {
            try
            {
                var dbEntity = await _paymentRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Price: {dbEntity?.Price} | PaymentMethod: {dbEntity?.PaymentNumber}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkUsersData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkUsersData(int id)
        {
            try
            {
                var dbEntity = await _userRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Login: {dbEntity?.Login}| Email: {dbEntity?.Email}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkUserRolesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkUserRoles(int id)
        {
            try
            {
                var dbEntity = await _userRoleRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Name: {dbEntity?.Name}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkOrderStatusData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkOrderStatusData(int id)
        {
            try
            {
                var dbEntity = await _orderStatusRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Status: {dbEntity?.Status}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkColorsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkColorsData(int id)
        {
            try
            {
                var dbEntity = await _colorRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Name: {dbEntity?.Name} | ImageData: {dbEntity?.ImageData}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCurrenciesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCurrenciesData(int id)
        {
            try
            {
                var dbEntity = await _currencyRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Name: {dbEntity?.Name}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkSizeOptionsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkSizeOptionsData(int id)
        {
            try
            {
                var dbEntity = await _sizeOptionRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Size: {dbEntity?.Size}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkBaseNecklinesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkBaseNecklinesData(int id)
        {
            try
            {
                var dbEntity = await _baseNecklineRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Settings: {dbEntity?.Settings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkBaseSweatersData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkBaseSweatersData(int id)
        {
            try
            {
                var dbEntity = await _baseSweaterRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Settings: {dbEntity?.Settings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkBaseSleevesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkBaseSleevesData(int id)
        {
            try
            {
                var dbEntity = await _baseSleeveRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Settings: {dbEntity?.Settings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkBaseSleeveCuffsLeftData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkBaseSleeveCuffsLeftData(int id)
        {
            try
            {
                var dbEntity = await _baseSleeveCuffRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Settings: {dbEntity?.Settings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkBaseSleeveCuffsRightData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkBaseSleeveCuffsRightData(int id)
        {
            try
            {
                var dbEntity = await _baseSleeveCuffRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Settings: {dbEntity?.Settings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkBaseBeltsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkBaseBeltsData(int id)
        {
            try
            {
                var dbEntity = await _baseBeltRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Settings: {dbEntity?.Settings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkBasePantsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkBasePantsData(int id)
        {
            try
            {
                var dbEntity = await _basePantRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Settings: {dbEntity?.Settings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkBasePantsCuffsLeftData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkBasePantsCuffsLeftData(int id)
        {
            try
            {
                var dbEntity = await _basePantsCuffRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Settings: {dbEntity?.Settings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkBasePantsCuffsRightData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkBasePantsCuffsRightData(int id)
        {
            try
            {
                var dbEntity = await _basePantsCuffRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Settings: {dbEntity?.Settings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCustomNecklinesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCustomNecklinesData(int id)
        {
            try
            {
                var dbEntity = await _customNecklineRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | CustomSettings: {dbEntity?.CustomSettings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkBasePantsCuffsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkBasePantsCuffsData(int id)
        {
            try
            {
                var dbEntity = await _basePantsCuffRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Settings: {dbEntity?.Settings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCustomSweatersData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCustomSweatersData(int id)
        {
            try
            {
                var dbEntity = await _customSweaterRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | CustomSettings: {dbEntity?.CustomSettings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCustomSleevesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCustomSleevesData(int id)
        {
            try
            {
                var dbEntity = await _customSleeveRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | CustomSettings: {dbEntity?.CustomSettings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCustomSleeveCuffsLeftData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCustomSleeveCuffsLeftData(int id)
        {
            try
            {
                var dbEntity = await _customSleeveCuffRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | CustomSettings: {dbEntity?.CustomSettings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCustomSleeveCuffsRightData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCustomSleeveCuffsRightData(int id)
        {
            try
            {
                var dbEntity = await _customSleeveCuffRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | CustomSettings: {dbEntity?.CustomSettings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCustomBeltsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCustomBeltsData(int id)
        {
            try
            {
                var dbEntity = await _customBeltRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | CustomSettings: {dbEntity?.CustomSettings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCustomPantsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCustomPantsData(int id)
        {
            try
            {
                var dbEntity = await _customPantRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | CustomSettings: {dbEntity?.CustomSettings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCustomPantsCuffsLeftData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCustomPantsCuffsLeftData(int id)
        {
            try
            {
                var dbEntity = await _customPantsCuffRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | CustomSettings: {dbEntity?.CustomSettings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCustomPantsCuffsRightData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCustomPantsCuffsRightData(int id)
        {
            try
            {
                var dbEntity = await _customPantsCuffRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | CustomSettings: {dbEntity?.CustomSettings}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCustomSportSuitsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCustomSportSuitsData(int id)
        {
            try
            {
                var dbEntity = await _customSportSuitRepository.GetById(id);

                var customSettingsNecklines = await GetFkCustomNecklinesData(dbEntity.FkCustomNecklines.Value);
                var customSweaters = await GetFkCustomSweatersData(dbEntity.FkCustomSweaters.Value);
                var customSleeves = await GetFkCustomSleevesData(dbEntity.FkCustomSleeves.Value);
                var customSleeveCuffsLeft = await GetFkCustomSleeveCuffsLeftData(dbEntity.FkCustomSleeveCuffsLeft.Value);
                var customSleeveCuffsRight = await GetFkCustomNecklinesData(dbEntity.FkCustomNecklines.Value);
                var customBelts = await GetFkCustomSleeveCuffsRightData(dbEntity.FkCustomSleeveCuffsRight.Value);
                var customPants = await GetFkCustomPantsData(dbEntity.FkCustomPants.Value);
                var customPantsCuffsLeft = await GetFkCustomPantsCuffsLeftData(dbEntity.FkCustomPantsCuffsLeft.Value);
                var customPantsCuffsRight = await GetFkCustomPantsCuffsRightData(dbEntity.FkCustomPantsCuffsRight.Value);

                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | " +
                        $"CustomNecklines: {customSettingsNecklines} | " +
                        $"CustomSweaters: {customSweaters} | " +
                        $"CustomSleeves: {customSleeves} | " +
                        $"CustomSleeveCuffsLeft: {customSleeveCuffsLeft} | " +
                        $"CustomSleeveCuffsRight: {customSleeveCuffsRight} | " +
                        $"CustomBelts: {customBelts} | " +
                        $"CustomPants: {customPants} | " +
                        $"CustomPantsCuffsLeft: {customPantsCuffsLeft} | " +
                        $"CustomPantsCuffsRight: {customPantsCuffsRight} | ";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCustomizableProductsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCustomizableProductsData(int id)
        {
            try
            {
                var dbEntity = await _customizableProductRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Price: {dbEntity?.Price}| CustomizationDetails: {dbEntity?.CustomizationDetails}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkProductOrdersData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkProductOrdersData(int id)
        {
            try
            {
                var dbEntity = await _productOrderRepository.GetById(id);

                string? products = string.Empty;
                string? customizableProducts = string.Empty;

                if (dbEntity != null)
                {
                    if (dbEntity.FkProducts.HasValue)
                    {
                        products = await GetFkProductsData(dbEntity.FkProducts.Value);
                    }

                    if (dbEntity.FkCustomizableProducts.HasValue)
                    {
                        var dbCustomEntity = await _customizableProductRepository.GetById(dbEntity.FkCustomizableProducts.Value);

                        if (dbCustomEntity != null)
                        {
                            customizableProducts = await GetFkCustomizableProductsData(dbCustomEntity.Id);
                        }
                    }
                }

                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | products: {products} | customizableProducts: {customizableProducts}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkCountriesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkCountriesData(int id)
        {
            try
            {
                var dbEntity = await _countryRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Name: {dbEntity?.Name} ";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkPaymentMethodsData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkPaymentMethodsData(int id)
        {
            try
            {
                var dbEntity = await _paymentMethodRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Method: {dbEntity?.Method} ";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkPaymentStatusesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkPaymentStatusesData(int id)
        {
            try
            {
                var dbEntity = await _paymentStatusRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | Status: {dbEntity?.Status} ";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        [HttpGet("GetFkShippingAddressesData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string?> GetFkShippingAddressesData(int id)
        {
            try
            {
                var dbEntity = await _shippingAddressRepository.GetById(id);
                return dbEntity == null
                    ? string.Empty
                    : $"Id: {dbEntity?.Id} | City: {dbEntity?.City} | Address: {dbEntity?.Address}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching data: {ex.Message}");
                throw;
            }
        }

        #endregion
    }
}