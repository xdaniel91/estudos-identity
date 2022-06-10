using ApiTreino.Database;
using ApiTreino.Dto;
using ApiTreino.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiTreino.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ISvcPerson svcPerson;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PersonController(ISvcPerson svcPerson, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.svcPerson = svcPerson;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await svcPerson.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await svcPerson.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] PersonDto personDto)
        {
            var result = mapper.Map<PersonDto>(await svcPerson.Add(personDto));
            if (await unitOfWork.SaveChanges())
                return Ok(result);
            return BadRequest();
        }


        public async Task<IActionResult> Update([FromBody] PersonDto personDto)
        {
            svcPerson.Update(personDto);
            if (await unitOfWork.SaveChanges())
                return Ok(personDto);
            return BadRequest();
        }


        [HttpDelete("{id}")]
        public void DeleteById(int id)
        {

        }
    }
}
