using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolVSCode.WebAPI.Data;
using SmartSchoolVSCode.WebAPI.Models;

namespace SmartSchoolVSCode.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext _context;
        public AlunoController(SmartContext context){
            
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(){
            return Ok(_context.Alunos);
        }

        //Exemplo de chamada passandro o Id por meio da rota 
        //api/aluno/byid/2
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id){

            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            
            return Ok(aluno);
        }
        
        //Exemple de chamada via URl com dois parametros por meio de query string 
        //api/aluno/byname?name=Alex&sobrenome=Honorato
        [HttpGet("byName")]
        public IActionResult GetByName(string name, string sobreNome){
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(name) && a.SobreNome.Contains(sobreNome));
            if(aluno == null) return BadRequest("Aluno não encontrado");
           
            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno ){
            _context.Add(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno){
            _context.Update(aluno);
            _context.SaveChanges();

            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno){
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){

            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            _context.Remove(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }
            
    }
}