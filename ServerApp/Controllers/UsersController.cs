using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Data;
using ServerApp.DTO;

namespace ServerApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public ISocialRepository _repository;
        public IMapper _mapper;


        public UsersController(ISocialRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        //api/getusers/
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.GetUsers();
            
            var liste=new List<UserForListDto>();
            
            var result=_mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(result);
        }

        //api/getusers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repository.GetUser(id);
            
            var result=_mapper.Map<UserForDetailsDTO>(user);

            return Ok(result);
        }
    }
}