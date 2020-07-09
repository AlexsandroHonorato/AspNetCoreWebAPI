using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolVSCode.WebAPI.Data;
using SmartSchoolVSCode.WebAPI.Dtos;
using SmartSchoolVSCode.WebAPI.Models;

namespace SmartSchoolVSCode.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {   
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public AlunoController( IRepository repository, IMapper mapper){ 
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(){

            var alunos = _repository.GetAllAlunos(true);
        
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
        }

        //Exemplo de chamada passandro o Id por meio da rota 
        //api/aluno/byid/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id){

            var aluno = _repository.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            
            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }       

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model){
            
            var aluno = _mapper.Map<Aluno>(model);

            _repository.Add(aluno);
            if(_repository.SaveChanges())
                return Created($"api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não Cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model){          
            var aluno = _repository.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repository.Update(aluno);
            if(_repository.SaveChanges())
                return Created($"api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não Atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model){            
            var aluno = _repository.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, aluno);

            _repository.Update(aluno);
            if(_repository.SaveChanges())
                return Created($"api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
          

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