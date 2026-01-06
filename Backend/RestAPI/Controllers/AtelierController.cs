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
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AtelierController(
        ILogger<AtelierController> logger,
        IRepository<DB.About> aboutRepository,
        IRepository<DB.Contact> contactRepository,
        IRepository<DB.Language> languageRepository)
        : ControllerBase
    {
        /// <summary>
        /// CTRL + M + P - expand all
        /// CTRL + M + O - collapse all
        /// </summary>

        private readonly IMapper _mapper = MapperConfig.InitializeAutomapper();
        #region repos init
        private readonly ILogger<AtelierController> _logger = logger;
        private readonly IRepository<DB.About> _aboutRepository = aboutRepository;
        private readonly IRepository<DB.Contact> _contactRepository = contactRepository;
        private readonly IRepository<DB.Language> _languageRepository = languageRepository;
        #endregion

        #region AboutPage

        [AllowAnonymous]
        [HttpGet("GetAbout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<About>> GetAbout(string lang)
        {
            var langId = GetLanguageIdByAbbreviation(lang);

            var dbList = _aboutRepository
                .GetAll()
                .Where(x => x.FkLanguages == langId)
                .OrderBy(x => x.Id)
                .ToList();

            var apiList = _mapper.Map<List<DB.About>, List<About>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        #endregion

        #region ContactsPage

        [AllowAnonymous]
        [HttpGet("GetContacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Contact>> GetContacts(string lang)
        {
            var langId = GetLanguageIdByAbbreviation(lang);

            var dbList = _contactRepository
                .GetAll()
                .Where(x => x.FkLanguages == langId)
                .OrderBy(x => x.Id)
                .ToList();

            var apiList = _mapper.Map<List<DB.Contact>, List<Contact>>(dbList);

            return apiList == null ? NotFound() : apiList;
        }

        #endregion

        private int GetLanguageIdByAbbreviation(string abbreviation)
        {
            var dbEntity = _languageRepository.GetAll()
                .FirstOrDefault(x => string.Equals(x.Abbreviation.ToLower(), abbreviation.ToLower()));

            return dbEntity == null ? 0 : dbEntity.Id;
        }
    }
}