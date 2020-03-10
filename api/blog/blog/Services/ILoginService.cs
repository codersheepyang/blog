using blog.Models.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Services
{
    public interface IUserService
    {
        User CheckLogin(string username, string password);

        string Register(User user);
    }
}
