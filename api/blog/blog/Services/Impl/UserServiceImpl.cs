using blog.Models.Consumer;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace blog.Services.Impl
{
    public class UserServiceImpl : IUserService
    {
        private readonly UserContext _userContext;
        private readonly ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(UserServiceImpl));
        public UserServiceImpl(UserContext userContext)
        {
            _userContext = userContext;
        }
        public User CheckLogin(string username, string password)
        {
            string result;

            //加密操作
            using (var md5 = MD5.Create())
            {
                //使用 username 作为加盐对象
                var MD5Value = md5.ComputeHash(Encoding.UTF8.GetBytes(username + password));
                string strResult = BitConverter.ToString(MD5Value);
                result = strResult.Replace("-", "");
            }

            User user = _userContext.User.Where(consumer => consumer.Username == username).FirstOrDefault();
            if (user != null && result.Equals(user.Password))
            {
                log.Info("用户 " + username + " 密码验证正确");
                return user;
            }
            return null;
        }

        public string Register(User user)
        {
            string result;
            string json;
            string username = user.Username;
            string password = user.Password;
            User user1 = _userContext.User.Where(consumer => consumer.Username == username).FirstOrDefault();
            if (user1 == null)
            {
                //加密操作
                using (var md5 = MD5.Create())
                {
                    //使用 username 作为加盐对象
                    var MD5Value = md5.ComputeHash(Encoding.UTF8.GetBytes(username + password));
                    string strResult = BitConverter.ToString(MD5Value);
                    result = strResult.Replace("-", "");
                }
                user.Password = result;
                _userContext.User.Add(user);
                if (_userContext.SaveChanges() == 1)
                {
                    json = JsonConvert.SerializeObject(new { Status = 1 });
                    return json;
                }
            }
            json = JsonConvert.SerializeObject(new { Status = 0 });
            return json;
        }
    }
}
