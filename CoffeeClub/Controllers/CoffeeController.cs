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
    /// <summary>
    /// Coffee Controller.
    /// </summary>
    [Route("api/coffee")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {

        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for CoffeeController.
        /// </summary>
        public CoffeeController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all Coffees.
        /// </summary>
        [HttpGet(Name = "GetCoffees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoffees()
        {
            var coffees = await _repository.Coffee.GetAllCoffeesAsync(trackChanges: false);

            var coffeesDto = _mapper.Map<IEnumerable<CoffeeDTO>>(coffees);

            return Ok(coffeesDto);
        }

        /// <summary>
        /// Gets a specific Coffee by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}", Name = "CoffeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCoffee(int id)
        {
            var coffee = await _repository.Coffee.GetCoffeeByIdAsync(id, trackChanges: false);

            if (coffee == null)
            {
                _logger.LogInfo($"Coffee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var coffeeDto = _mapper.Map<CoffeeDTO>(coffee);
                return Ok(coffeeDto);
            }
        }

        /// <summary>
        /// Creates a new Coffee.
        /// </summary>
        [HttpPost(Name = "CreateCoffee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCoffee([FromBody]  CoffeeForCreationDTO coffee)
        {
            var coffeeEntity = _mapper.Map<Coffee>(coffee);

            _repository.Coffee.CreateCoffee(coffeeEntity);
            await _repository.SaveAsync();

            var coffeeToReturn = _mapper.Map<CoffeeForCreationDTO>(coffeeEntity);

            return CreatedAtRoute("CoffeeById", coffeeToReturn);
        }

        /// <summary>
        /// Deletes a specific Coffee.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCoffee(int id)
        {
            try
            {
                var coffee = await _repository.Coffee.GetCoffeeByIdAsync(id, trackChanges: false);
                if (coffee == null)
                {
                    _logger.LogError($"Coffee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (_repository.Comments.GetCommentByIdAsync(id, trackChanges: false).IsCompleted)
                {
                    _logger.LogError($"Cannot delete coffee with id: {id}. It has related comments. Delete those comments first");
                    return BadRequest("Cannot delete coffee. It has related comments. Delete those comments first");
                }

                _repository.Coffee.DeleteCoffee(coffee);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteCoffee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates a specific Coffee.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coffee"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateCoffee(int id, [FromBody] CoffeeForUpdateDTO coffee)
        {
            try
            {
                if (coffee == null)
                {
                    _logger.LogError("Coffee object sent from client is null.");
                    return BadRequest("Coffee object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid coffee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var coffeeEntity = await _repository.Coffee.GetCoffeeByIdAsync(id, trackChanges: false);
                if (coffeeEntity == null)
                {
                    _logger.LogError($"Coffee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(coffee, coffeeEntity);

                _repository.Coffee.UpdateCoffee(coffeeEntity);
               await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateCoffee action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets a specific Coffee with related comments.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCoffeeWithDetails(int id)
        {
            try
            {
                var coffee = await _repository.Coffee.GetCoffeeWithDetailsAsync(id, trackChanges: false);
                if (coffee == null)
                {
                    _logger.LogError($"Coffee with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned coffee with details for id: {id}");

                    return Ok(coffee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetCoffeeWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
