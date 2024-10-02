using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Data;
using ServerApp.DTO;
using ServerApp.Helpers;
using ServerApp.Models;

namespace ServerApp.Controllers
{
    [ServiceFilter(typeof(LastActiveActionFilter))]
    [Authorize]
    [Route("api/[controller]/{userId}")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        public ISocialRepository _repository;
        public IMapper _mapper;


        public MessagesController(ISocialRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        [HttpPost]
        public async Task<IActionResult>CreateMessage(int userId,MessageForCreateDTO messageForCreateDTO)
        {
           if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
             return Unauthorized();

            messageForCreateDTO.SenderId=userId;
            
            var recipient=await _repository.GetUser(messageForCreateDTO.RecipientId);
            if(recipient==null)
             return BadRequest("Mesaj Göndermek İstediğiniz Kullanıcı Bulunmamaktadır.");
            
           var message=_mapper.Map<Message>(messageForCreateDTO);

           _repository.Add(message);

           if(await _repository.SaveChanges())
           {
             var messageDTO=_mapper.Map<MessageForCreateDTO>(message);
             return Ok(messageDTO);
           }
           
           throw new System.Exception("error");

        }

    }
}