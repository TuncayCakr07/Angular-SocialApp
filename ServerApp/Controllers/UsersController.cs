using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServerApp.Data;
using ServerApp.DTO;
using ServerApp.Helpers;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [ServiceFilter(typeof(LastActiveActionFilter))]
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
        public async Task<IActionResult> GetUsers([FromQuery]UserQueryParams userParams)
        {
            await Task.Delay(1000);
           userParams.UserId=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var users = await _repository.GetUsers(userParams);
            
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
        
        [HttpPost("{followerUserId}/follow/{userId}")]
        public async Task<IActionResult>FollowUser(int followerUserId,int userId)
        {
         if(followerUserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
             return Unauthorized();

          if(followerUserId==userId)
          return BadRequest("Kullanıcı Kendi Hesabını Takip Edemez!");

          var IsReadyFollowed=await _repository
          .IsAlreadyFollowed(followerUserId,userId);

          if(IsReadyFollowed)
          return BadRequest("Zaten Kullanıcı Takip Ediliyor");

          if(await _repository.GetUser(userId)==null)
           return NotFound();

           var follow= new UserToUser(){
            UserId=userId,
            FollowerId=followerUserId,
           };
           
           _repository.Add<UserToUser>(follow);

           if(await _repository.SaveChanges())
              return Ok();

           return BadRequest("Hata Oluştu");

        }
    }
}