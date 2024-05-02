using Construction.API.Helpers;
using Construction.Shared.Entities;
using Construction.Shared.Enums;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Metrics;
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

            await CheckProjectConstructionsAsync();

            await CheckDutiessAsync();


            await CheckRoleAsync();

            await CheckUserAsync("1010", "Super", "Admin", "orlapez@gnmail.com", "3015555555", "Cr 25 8965", UserType.Admin);


        }
        private async Task CheckDutiessAsync()
        {
            if (!_context.Duties.Any())
            {
                _context.Duties.Add(new Dutie { Name = "Cavar", Description = "Descripcion", ProjectConstructionsId=1});
                _context.Duties.Add(new Dutie { Name = "Perforar", Description = "Descripcion", ProjectConstructionsId=2 });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckProjectConstructionsAsync()
        {
            if (!_context.ProjectConstructions.Any())
            {
                _context.ProjectConstructions.Add(new ProjectConstruction { Name = "Changualito", Description = "Descripcion", });
                _context.ProjectConstructions.Add(new ProjectConstruction { Name = "Changualo", Description = "Descripcion", });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckRoleAsync()
        {

            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());

        }

        private async Task<User> CheckUserAsync(string document, string firstname, string lastname, string email, string phone, string address, UserType userType)
        {


            var user = await _userHelper.GetUserAsync(email);

            if (user == null)
            {



                user = new User
                {

                    Document = document,
                    FirstName = firstname,
                    LastName = lastname,
                    Email = email,
                    PhoneNumber = phone,
                    UserName = email,
                    Address = address,
                    UserType = userType,





                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());







            }


            return user;



        }

     
    }
}