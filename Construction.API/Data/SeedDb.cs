using Construction.API.Helpers;
using Construction.Shared.Entities;
using Construction.Shared.Enums;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Reflection.Metadata.Ecma335;

namespace Construction.API.Data
{
    public class SeedDb
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;


        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {

            await _context.Database.EnsureCreatedAsync();
            await CheckRoleAsync();

            await CheckUserAsync("1010", "Super", "Admin", "Edwinramirezgon@gmail.com", "3137778067", "carr 42a 78a 17", UserType.Admin);






        }

        private async Task CheckRoleAsync()
        {

            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());

            await _userHelper.CheckRoleAsync(UserType.User.ToString());

        }

        private async Task CheckUserAsync(string document, string firstname, string lastname, string Email, string phone, string address, UserType usertype)
        {


            var user = await _userHelper.GetUserAsync(Email);
            if (user == null)
            {


                /*user = new User
                {
                    Document = document,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = Email,
                    CellPhone = phone,
                    Address = address,
                    UserType=usertype,                };
            };*/

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, usertype.ToString());



            }



        }
    }
}
