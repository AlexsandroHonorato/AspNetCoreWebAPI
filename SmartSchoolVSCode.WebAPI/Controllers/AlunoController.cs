using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolVSCode.WebAPI.Models;

namespace SmartSchoolVSCode.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public AlunoController(){
            
        }

           public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno{
                Id = 1,
                Nome = "Alex",
                SobreNome = "Honorato",
                Telefone = "123456789"
            }, new Aluno{
                Id = 2,
                Nome = "Gi",
                SobreNome = "Fernandes",
                Telefone = "98745631"
            }, new Aluno{
                Id = 3,
                Nome = "Jey",
                SobreNome = "Datris",
                Telefone = "88888888"
            }
        };

        [HttpGet]
        public IActionResult Get(){
            return Ok(Alunos);
        }

        //Exemplo de chamada passandro o Id por meio da rota 
        //api/aluno/byid/2
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id){

            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            
            return Ok(aluno);
        }
        
        //Exemple de chamada via URl com dois parametros por meio de query string 
        //api/aluno/byname?name=Alex&sobrenome=Honorato
        [HttpGet("byName")]
        public IActionResult GetByName(string name, string sobreNome){
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(name) && a.SobreNome.Contains(sobreNome));
            if(aluno == null) return BadRequest("Aluno não encontrado");
           
            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno ){
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno){

            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno){
        
        return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, Aluno aluno){

            return Ok(aluno);
        }
    
    }
}