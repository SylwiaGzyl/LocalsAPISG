using LocalsAPISG.Entites;
using LocalsAPISG.Entities;
using System.Collections.Generic;
using System.Linq;

namespace LocalsAPISG
{
    public class LocalsSeed
    {

        private readonly LocalsDbContext _dbContext;

        public LocalsSeed(LocalsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {

                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Locals.Any())
                {
                    var locals = GetLocals();
                    _dbContext.Locals.AddRange(locals);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };
            return roles;
        }
        private IEnumerable<Locals> GetLocals()
        {
            var locals = new List<Locals>()
            {
                new Locals()
                {
                    Name = "Dong-A",
                    Category = "Vietnamese Food",
                    Description = "Vietnamese kitchen with climatic interior",
                    ContactEmail = "contact@adong.com",
                    ContactNumber = "34825879",
                    Dishes = new List<Menu>()
                    {
                        new Menu
                        {
                            Name = "Crispy Chicken",
                            Price = 24,
                        }
                    },

                    Address = new Address
                    {
                        City = "Kraków",
                        Street = "Miodowa 7",
                        PostalCode = "30055"

                    },

                },
                new Locals()
                {
                    Name = "Cybermachina",
                    Category = "Pub",
                    Description = "Place with board and console games",
                    ContactEmail = "contact@cyber.com",
                    ContactNumber = "123565465",
                    Dishes = new List<Menu>()
                    {
                        new Menu
                        {
                            Name = "Bulbasauuur",
                            Price = 22,
                        }
                    },
                    Address = new Address
                    {
                        City = "Kraków",
                        Street = "Rynek 123",
                        PostalCode = "30054"

                    },

                }
        };
                return locals;
            }
        }
    }

