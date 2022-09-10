using AutoMapper;
using LocalsAPISG.Entities;
using LocalsAPISG.Models;
using LocalsAPISG.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocalsAPISG.Controllers
{
    [Route ("api/locals")]
    [ApiController]
    public class LocalsController : ControllerBase
    {
        private readonly ILocalsService _localsService;
        public LocalsController(ILocalsService localsService)
        {
            _localsService = localsService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] UpdateLocalDto dto, [FromRoute] int id)
        {
            
            _localsService.Update(id, dto);
            
            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _localsService.Delete(id);

            
            return NoContent();
        }

        [HttpPost]
        public ActionResult CreateLocal([FromBody]CreateLocalDto dto) //mapowanie encji od klienta a następnie przesłanie jej do naszej bazy danych
        {
            

            var id = _localsService.Create(dto);
            return Created("/api/locals/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<LocalsDto>> GetAll()
        {
            var localsDtos = _localsService.GetAll();
            
            return Ok(localsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<LocalsDto> Get([FromRoute] int id) //umożliwienie znalezienia lokalu po id
        {


            var locals = _localsService.GetById(id);

            return Ok(locals);
        }
    }
}
