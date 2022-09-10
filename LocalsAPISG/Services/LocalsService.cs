using AutoMapper;
using LocalsAPISG.Entities;
using LocalsAPISG.Exceptions;
using LocalsAPISG.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace LocalsAPISG.Services
{
    public interface ILocalsService
    {
        LocalsDto GetById(int id);
        IEnumerable<LocalsDto> GetAll();
        int Create(CreateLocalDto dto);
        void Delete(int id);
        void Update(int id, UpdateLocalDto dto);
    }
    public class LocalsService :ILocalsService
    {
        private readonly LocalsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<LocalsService> _logger;
        public LocalsService(LocalsDbContext dbContext, IMapper mapper, ILogger<LocalsService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public void Update(int id, UpdateLocalDto dto)
        {
            var locals = _dbContext
                .Locals
                .FirstOrDefault(c => c.Id == id);

            if (locals == null)
                throw new NotFoundException("Locals not found");

            locals.Name = dto.Name;
            locals.Description = dto.Description;

            _dbContext.SaveChanges();

        }
        public void Delete(int id) 
        {
            _logger.LogError($"Locals with id: {id} DELETE action invoked"); 

            var locals = _dbContext
                .Locals
                .FirstOrDefault(r => r.Id == id);

            if (locals is null) throw new NotFoundException("Local not found");

            _dbContext.Locals.Remove(locals);
            _dbContext.SaveChanges();
            

        }
        public LocalsDto GetById(int id)
        {
            var locals = _dbContext
                .Locals
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (locals is null) throw new NotFoundException("Local not found"); ;
            var result = _mapper.Map<LocalsDto>(locals);
            return result;  
        }

        public IEnumerable<LocalsDto> GetAll()
        {
            var locals = _dbContext
                .Locals
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();
            var localsDtos = _mapper.Map<List<LocalsDto>>(locals);
            return localsDtos;
        }

        public int Create(CreateLocalDto dto)
        {
            var locals = _mapper.Map<Locals>(dto);
            _dbContext.Locals.Add(locals);
            _dbContext.SaveChanges();

            return locals.Id;
        }
    }
}
