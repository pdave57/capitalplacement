using System;
using Microsoft.AspNetCore.Mvc;
using QueryApp2.Models;
using QueryApp2.Services;

namespace QueryApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly IPersonalInfoRepository _personalInfoRepository;
        public PersonalController(IPersonalInfoRepository personalInfoRepository)
        {
            _personalInfoRepository = personalInfoRepository;
        }

        [HttpGet("persons")]
        public async Task<ActionResult<IEnumerable<PersonalInfo>>> Get()
        {
            var personal = await _personalInfoRepository.GetAllPersonalInfoAsync();
            return Ok(personal);
        }

        [HttpGet("persons/{personId}")]
        public async Task<ActionResult> Get(string personId)
        {
            var persons = await _personalInfoRepository.GetPersonalInfoByIdAsync(personId);
            return Ok(persons);
        }

        [HttpPost]
        public async Task<ActionResult<PersonalInfo>> Post(PersonalInfo p)
        {
            p.Id = Guid.NewGuid().ToString();

            var created = await _personalInfoRepository.CreatePersonalInfoAsync(p);
            return CreatedAtAction(nameof(Get), new { personId = created.Id }, created);
        }

        [HttpPut("{personId}")]
        public async Task<ActionResult<PersonalInfo>> Update(string personId, PersonalInfo p)
        {
            var existingperson = await _personalInfoRepository.GetPersonalInfoByIdAsync(personId);

            if (existingperson == null)
            {
                return NotFound();
            }
            p.Id = existingperson.Id;
            var updated = await _personalInfoRepository.UpdatePersonalInfoAsync(p);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existingperson = await _personalInfoRepository.GetPersonalInfoByIdAsync(id);
            if (existingperson == null)
            {
                return NotFound();
            }
            await _personalInfoRepository.DeletePersonalInfoAsync(id);
            return NoContent();
        }

    }
}
