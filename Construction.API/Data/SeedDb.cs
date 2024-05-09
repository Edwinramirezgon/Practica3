//this class contains the methods to seed the database whit the defaults values of the entities
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
        //Method for seed the database
        public async Task SeedAsync()
        {

            await _context.Database.EnsureCreatedAsync();

            await CheckProjectConstructionsAsync();

            await CheckDutiesAsync();

            await CheckBudgetsAsync();

            await CheckContructionTeamsAsync();

            await CheckEquipmentsAsync();

            await CheckEquipmentAssignmentsAsync();

            await CheckMaterialsAsync();

            await CheckMaterialsAssignmentsAsync();

            await CheckRoleAsync();

            await CheckUserAsync("1010", "Super", "Admin", "orlapez@gnmail.com", "3015555555", "Cr 25 8965", UserType.Admin);

        }

    //-----------------------------------------Methods indivuals by entities whit default values------------
        private async Task CheckProjectConstructionsAsync()
        {
            if (!_context.ProjectConstructions.Any())
            {
                _context.ProjectConstructions.Add(new ProjectConstruction { Name = "Changualito", Description = "Descripcion", });
                _context.ProjectConstructions.Add(new ProjectConstruction { Name = "Changualo", Description = "Descripcion", });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckDutiesAsync()
        {
            if (!_context.Duties.Any())
            {
                _context.Duties.Add(new Dutie { Name = "Cavar", Description = "Descripcion", ProjectConstructionsId=1});
                _context.Duties.Add(new Dutie { Name = "Perforar", Description = "Descripcion", ProjectConstructionsId=2 });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckBudgetsAsync()
        {
            if (!_context.Budgets.Any())
            {
                _context.Budgets.Add(new Budget { BudgetConstructionTeam = 100000,
                    BudgetDutie = 50000,
                    BudgetEquipment = 80000,
                    BudgetProyectConstruction = 75000,
                    BudgetTotal = ((305000)),
                    ProjectConstructionsId = 1,
                });
                _context.Budgets.Add(new Budget { BudgetConstructionTeam = 60000,
                    BudgetDutie = 90000,
                    BudgetEquipment = 10000,
                    BudgetProyectConstruction = 30000,
                    BudgetTotal = 190000,
                    ProjectConstructionsId = 2,
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckContructionTeamsAsync()
        {
            if (!_context.ConstructionTeams.Any())
            {
                _context.ConstructionTeams.Add(new ConstructionTeam { Name = "Power Rangers",
                    Specialties = "Excavacion, Perforacion"});
                _context.ConstructionTeams.Add(new ConstructionTeam { Name = "Los Cubitos",
                    Specialties = "Transporte, Mezclar cemento"});
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckEquipmentsAsync()
        {
            if (!_context.Equipments.Any())
            {
                _context.Equipments.Add(new Equipment { Name = "Excavadora",
                    Capacity = "3 toneladas cubicas",
                    MaintenanceState = "Proximo a mantenimiento preventivo",
                    Availability = "Disponible",
                    ProjectConstructionsId = 1,
                    DutiesId = 1
                });
                _context.Equipments.Add(new Equipment
                {
                    Name = "Perforadora",
                    Capacity = "5 toneladas cubicas",
                    MaintenanceState = "En mantenimiento",
                    Availability = "No Disponible",
                    ProjectConstructionsId = 2,
                    DutiesId = 2
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckEquipmentAssignmentsAsync()
        {
            if (!_context.EquipmentAssignments.Any())
            {
                _context.EquipmentAssignments.Add(new EquipmentAssignment { ConstructionTeamsId = 1,
                    ProjectConstructionsId = 1
                });
                _context.EquipmentAssignments.Add(new EquipmentAssignment { ConstructionTeamsId = 2,
                    ProjectConstructionsId = 2
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckMaterialsAsync()
        {
            if (!_context.Materials.Any())
            {
                _context.Materials.Add(new Material { Name = "Poleas",
                    Supplier = "Caterpillar", 
                    ProjectConstructionsId = 1 
                });
                _context.Materials.Add(new Material { Name = "Traillas",
                    Supplier = "Kaishan",
                    ProjectConstructionsId = 2
                });
            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckMaterialsAssignmentsAsync()
        {
            if (!_context.MaterialAssignments.Any())
            {
                _context.MaterialAssignments.Add(new MaterialAssignment { MaterialsId = 1,
                    DutiesId = 1 
                });
                _context.MaterialAssignments.Add(new MaterialAssignment { MaterialsId = 2,
                    DutiesId = 2
                });
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