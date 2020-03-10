using blog.inputs;
using blog.Models.Advertisement;
using blog.Models.Article;
using blog.Models.Classification;
using blog.Models.Comment;
using blog.Models.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace blog.Services
{
    public interface IAboutMeService
    {
        string AddAdvertisement(Advertisement advertisement);
        string DeleteAdvertisement(int advertisementId);
        string GetAllAdvertisements(); 
        string GetConsumer();
    }
}
