using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolVSCode.WebAPI.Data;
using SmartSchoolVSCode.WebAPI.Models;

namespace SmartSchoolVSCode.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {     
        private readonly IRepository _repository;
        public ProfessorController(IRepository repository){       
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get(){

            return Ok(_repository.GetAllProfessores(true));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){

            var professorConsulta = _repository.GetProfessorById(id, false);
            if(professorConsulta == null) return BadRequest("Professor não encontrado");
            
            return Ok(professorConsulta);
        }       
  
        [HttpPost]
        public IActionResult Post(int id, Professor professor ){

            var professorConsulta = _repository.GetProfessorById(id, false);
            if(professorConsulta == null) return BadRequest("Professor não encontrado");

            _repository.Add(professor);
            if(_repository.SaveChanges())
            return Ok(professor);

            return BadRequest("Professor não Cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor){

            var professorConsulta = _repository.GetProfessorById(id, false);
            if(professorConsulta == null) return BadRequest("Professor não encontrado");

            _repository.Update(professor);
            _repository.SaveChanges();

            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor){

             var professorConsulta = _repository.GetProfessorById(id, false);
            if(professorConsulta == null) return BadRequest("Professor não encontrado");

            _repository.Update(professor);
            _repository.SaveChanges();

            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            
            var professorConsulta = _repository.GetProfessorById(id, false);
            if(professorConsulta == null) return BadRequest("Professor não encontrado");

            _repository.Delete(professorConsulta);
            _repository.SaveChanges();

            return Ok("Professor Deletado");
        }     
    }
}