using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,UserForUpdateDTO model)
        {
           if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
             return BadRequest("Not Valid Request");
           if(!ModelState.IsValid)
             return BadRequest(ModelState);
            
            var user = await _repository.GetUser(id);

            _mapper.Map(model,user);

            if(await _repository.SaveChanges())
              return Ok();

            throw new System.Exception("güncelleme sırasında hata oluştu");
            
        }
    }
}