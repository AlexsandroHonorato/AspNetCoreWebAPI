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
        private readonly IRepository _repository;
        public AlunoController( IRepository repository){ 
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get(){

            var alunos = _repository.GetAllAlunos(true);

            return Ok(alunos);
        }

        //Exemplo de chamada passandro o Id por meio da rota 
        //api/aluno/byid/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id){

            var aluno = _repository.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            
            return Ok(aluno);
        }       

        [HttpPost]
        public IActionResult Post(Aluno aluno ){
            
            _repository.Add(aluno);
            if(_repository.SaveChanges())
            return Ok(aluno);

            return BadRequest("Aluno não Cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno){          
            var alunoConsulta = _repository.GetAlunoById(id, false);
            if(alunoConsulta == null) return BadRequest("Aluno não encontrado");

            _repository.Update(aluno);
            if(_repository.SaveChanges())
            return Ok(aluno);

            return BadRequest("Aluno não Atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno){            
            var alunoConsulta = _repository.GetAlunoById(id, false);
            if(alunoConsulta == null) return BadRequest("Aluno não encontrado");

            _repository.Update(aluno);
            if(_repository.SaveChanges())
            
            return Ok(aluno);

            return BadRequest("Aluno não Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){

            var aluno = _repository.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("Aluno não encontrado");
           
            _repository.Delete(aluno);
            if(_repository.SaveChanges())

            return Ok("Aluno Deletado");

            return BadRequest("Aluno não Deletado");
        }
            
    }
}