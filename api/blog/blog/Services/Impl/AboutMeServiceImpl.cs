using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using blog.inputs;
using blog.Models;
using blog.Models.Advertisement;
using blog.Models.Article;
using blog.Models.Classification;
using blog.Models.Comment;
using blog.Models.Consumer;
using log4net;
using Newtonsoft.Json;

namespace blog.Services.Impl
{
    public class AboutMeServiceImpl : IAboutMeService
    {
        private readonly UserContext _consumerContext;

        private readonly AdvertisementContext _advertisementContext;

        private const string SHEMA = "bank_cookie";

        private readonly ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(AboutMeServiceImpl));

        public AboutMeServiceImpl(UserContext consumerContext, AdvertisementContext advertisementContext)
        {
            _consumerContext = consumerContext;
            _advertisementContext = advertisementContext;
        }

        public string AddAdvertisement(Advertisement advertisement)
        {
            _advertisementContext.Advertisement.Add(advertisement);
            if (_advertisementContext.SaveChanges() == 1)
            {
                return "广告添加成功";
            }
            return "广告添加失败";
        }

        public string DeleteAdvertisement(int advertisementId)
        {
            Advertisement advertisement =  _advertisementContext.Advertisement.Find(advertisementId);
            if (advertisement != null)
            {
                _advertisementContext.Advertisement.Remove(advertisement);
                if (_advertisementContext.SaveChanges() == 1)
                {
                    return "广告删除成功";
                }
                return "广告删除失败";
            }
            return "广告ID不存在";
        }

        public string GetAllAdvertisements()
        {
            List<Advertisement> advertisements = _advertisementContext.Advertisement.ToList();
            string json;
            if (advertisements != null)
            {
                json = JsonConvert.SerializeObject(advertisements);
                return json;
            }
            return "不存在任何广告";

        }

        public string  GetConsumer()
        {
            User consumer =  _consumerContext.User.ToList().FirstOrDefault();
            string json;
            if(consumer != null)
            {
                json = JsonConvert.SerializeObject(consumer);
                return json;
            }
            return "不存在用户数据";
        }
    }
}
