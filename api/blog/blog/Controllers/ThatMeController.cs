using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Models.Advertisement;
using blog.Services;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThatMeController : ControllerBase
    {
        private readonly IAboutMeService _aboutMeService;
        private readonly ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(ThatMeController));

        public ThatMeController(IAboutMeService aboutMeService)
        {
            _aboutMeService = aboutMeService;
        }

        [HttpGet("consumer/{userId}")]
        public ActionResult<string> GetConsumer(int userId)
        {
            string result = _aboutMeService.GetConsumer(userId);
            return Ok(result);
        }

        [HttpPost("advertisement")]
        public ActionResult<string> AddAdvertisement(Advertisement advertisement)
        {
            if (advertisement.Advertiser != null && advertisement.Content != null && advertisement.Title != null)
            {
                advertisement.CreateTime = DateTime.Now;
                string result = _aboutMeService.AddAdvertisement(advertisement);
                return Ok(result);
            }
            return NotFound("传参存在空值");
        }

        [HttpGet("advertisements/{userId}")]
        public ActionResult<string> GetAllAdvertisements(int userId)
        {
            string result = _aboutMeService.GetAllAdvertisements(userId);
            return Ok(result);
        }

        [HttpDelete("advertisement/{advertisementId}")]
        public ActionResult<string> DeleteAdvertisement(int advertisementId)
        {
            string result = _aboutMeService.DeleteAdvertisement(advertisementId);
            return Ok(result);
        }
    }
}