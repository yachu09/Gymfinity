using AngularApp5.Server.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace AngularApp5.Server.Helpers
{
    public class UserContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Bobski",
                        Street = "Bobstreet",
                        City = "Bobznań",
                        State = "Wielkopolskie",
                        Zipcode = "69-888"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
