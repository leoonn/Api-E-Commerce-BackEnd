using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Backend.Tools
{
    public class Cookies
    {
        public void CreateCookie(string key, string value, HttpResponse response, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime != 0)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            response.Cookies.Append(key, value, option);
        }

        public string ReadCookie(string key, HttpRequest request)
        {
            return request.Cookies[key];
        }
        public void Remove(string key, HttpResponse response)
        {
            response.Cookies.Delete(key);
        }
    }
}
