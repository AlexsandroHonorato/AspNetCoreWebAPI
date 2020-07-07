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
        private readonly SmartContext _context;
        public ProfessorController(SmartContext context){
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(){

            return Ok(_context.Professores);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id){

            var professor = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(professor == null) return BadRequest("Professor não encontrado");
            
            return Ok(professor);
        }
        
        //Exemple de chamada via URl com dois parametros por meio de query string 
        //api/aluno/byname?name=Alex&sobrenome=Honorato
        [HttpGet("byName")]
        public IActionResult GetByName(string name, string sobreNome){
            var professor = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Nome.Contains(name));
            if(professor == null) return BadRequest("Professor não encontrado");
           
            return Ok(professor);
        }

        [HttpPost]
        public IActionResult Post(Professor professor ){
            _context.Add(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor){
            var professorName = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(professorName == null) return BadRequest("Professor não encontrado");

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor){
             var professorName = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(professorName == null) return BadRequest("Professor não encontrado");

            _context.Update(professor);
            _context.SaveChanges();

            return Ok(professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){

            var professor = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(professor == null) return BadRequest("Professor não encontrado");

            _context.Remove(professor);
            _context.SaveChanges();
            return Ok(professor);
        }     
    }
}