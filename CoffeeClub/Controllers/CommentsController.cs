using AutoMapper;
using Contracts;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeClub.Controllers
{
    [Route("api/coffee/{coffeeId}/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CommentsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        //[HttpGet(Name = "GetComments")]
        //public async Task<IActionResult> GetComments()
        //{
        //    var comments = await _repository.Comments.GetAllCommentsAsync(trackChanges: false);

        //    var commentsDto = _mapper.Map<IEnumerable<CommentsDTO>>(comments);

        //    return Ok(commentsDto);
        //}

        [HttpGet("{id}", Name = "CommentById")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _repository.Comments.GetCommentByIdAsync(id, trackChanges: false);

            if (comment == null)
            {
                _logger.LogInfo($"Comment with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var commentDto = _mapper.Map<CommentsDTO>(comment);
                return Ok(commentDto);
            }
        }

        //[HttpPost(Name = "CreateComment")]
        //public async Task<IActionResult> CreateComment([FromBody] CommentsForCreationDTO comment)
        //{
        //    var commentEntity = _mapper.Map<Comments>(comment);

        //    _repository.Comments.CreateComment(commentEntity);
        //    await _repository.SaveAsync();

        //    var commentToReturn = _mapper.Map<CommentsDTO>(commentEntity);

        //    return CreatedAtRoute("CommentById", new { id = commentToReturn.CommentId }, commentToReturn);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateCommentForCoffee(int coffeeId, [FromBody] CommentsForCreationDTO comment)
        {
            var coffee = await _repository.Coffee.GetCoffeeById(coffeeId, trackChanges: false);
            if (coffee == null)
            {
                _logger.LogInfo($"Coffee with id: {coffeeId} doesn't exist in the database.");
                return NotFound();
            }

            var commentEntity = _mapper.Map<Comments>(comment);

            _repository.Comments.CreateComment(coffeeId, commentEntity);
            await _repository.SaveAsync();

            var commentToReturn = _mapper.Map<CommentsDTO>(commentEntity);

            return CreatedAtRoute("CommentById", new { coffeeId, id = commentToReturn.CommentId }, commentToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var comment = await _repository.Comments.GetCommentByIdAsync(id, trackChanges: false);
                if (comment == null)
                {
                    _logger.LogError($"Comment with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Comments.DeleteComment(comment);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteComment action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentsForUpdateDTO comment)
        {
            try
            {
                if (comment == null)
                {
                    _logger.LogError("Comment object sent from client is null.");
                    return BadRequest("Comment object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid comment object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var commentEntity = await _repository.Comments.GetCommentByIdAsync(id, trackChanges: false);
                if (commentEntity == null)
                {
                    _logger.LogError($"Comment with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(comment, commentEntity);

                _repository.Comments.UpdateComment(commentEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateComment action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
