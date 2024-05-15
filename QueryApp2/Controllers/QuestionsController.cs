using Microsoft.AspNetCore.Mvc;
using QueryApp2.Models;
using QueryApp2.Services;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QueryApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class questionsController : ControllerBase
    {
        private readonly CustomQuestionRepository _questionRepository;
        public questionsController(CustomQuestionRepository questionRepository)
        {
                _questionRepository = questionRepository;
        }
        // GET: api/<questionsController>
        [HttpGet("questions")]
        public async Task<ActionResult<IEnumerable<CustomQuestion>>> GetAllQuestion()
        {
            var questions = await _questionRepository.GetAllCustomQuestionsAsync();
            return Ok(questions);
        }

        // GET api/<questionsController>/5
        [HttpGet("questions/{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var qs = await _questionRepository.GetCustomQuestionByIdAsync(id);
            return Ok(qs);
        }

        // POST api/<questionsController>
        [HttpPost]
        public async Task<ActionResult<CustomQuestion>> Post(CustomQuestion cq)
        {
            cq.Id = Guid.NewGuid().ToString();
            var created = await _questionRepository.CreateQuestionAsync(cq);
            return CreatedAtAction(nameof(Get), new { id = created.Id, created });
        }

        // PUT api/<questionsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult>Update(string id, CustomQuestion cq)
        {
            var existingquestion = await _questionRepository.GetCustomQuestionByIdAsync(id);
            
            if(existingquestion == null)
            {
                return NotFound();
            }
            cq.Id = existingquestion.Id;
            var updated = await _questionRepository.UpdateQuestionAsync(cq); 
            return Ok(updated);

        }

        // DELETE api/<questionsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existingq = await _questionRepository.GetCustomQuestionByIdAsync(id);
            if(existingq == null)
            {
                return NotFound();
            }
            await _questionRepository.DeleteQuestionAsync(id);
            return NoContent();
        }
    }
}
