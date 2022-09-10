using LocalsAPISG.Entites;
using LocalsAPISG.Entities;
using LocalsAPISG.Exceptions;
using LocalsAPISG.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LocalsAPISG.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly LocalsDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AutheticationSettings _autheticationSettings;
        public AccountService(LocalsDbContext context, IPasswordHasher<User> passwordHasher, AutheticationSettings autheticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _autheticationSettings = autheticationSettings;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password); //hashowanie hasła, tworzenie unikanlnego kodu, zeby nie zapisywać znakow z hasła. kod ten jest potem porównywany z kodem wygenerowanym przy logowaniu
            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if(user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password); //weryfikacja czy uzytkownik do logowania uzył tego samego hasła co przy rejestracji
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");

            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-DD")),
                new Claim("Nationality", user.Nationality)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_autheticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_autheticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_autheticationSettings.JwtIssuer,
                _autheticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
