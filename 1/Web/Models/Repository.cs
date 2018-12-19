using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public static class Repository
    {
        static List<User> users = new List<User>() {

        new User() {UserId=1,UserName="inoxthaibinh",Password="123456" },
        new User() {UserId=2,UserName="fthuantv9",Password="q1w2e3r4t5" }
    };

        public static User GetUserDetails(User user)
        {
            return users.Where(u => u.UserName.ToLower() == user.UserName.ToLower() &&
            u.Password == user.Password).FirstOrDefault();
        }
    }
}