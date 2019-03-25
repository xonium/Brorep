using Brorep.Domain;
using System;
using System.Linq.Expressions;

namespace Brorep.Application.Users.Queries.GetUserDetail
{
    public class UserDetailModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public static Expression<Func<User, UserDetailModel>> Projection
        {
            get
            {
                return user => new UserDetailModel
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email
                };
            }
        }

        public static UserDetailModel Create(User user)
        {
            return Projection.Compile().Invoke(user);
        }
    }
}
