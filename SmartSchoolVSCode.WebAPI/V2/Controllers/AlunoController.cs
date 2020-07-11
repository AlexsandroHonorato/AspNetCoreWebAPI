using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolVSCode.WebAPI.Data;
using SmartSchoolVSCode.WebAPI.V1.Dtos;
using SmartSchoolVSCode.WebAPI.Models;

namespace SmartSchoolVSCode.WebAPI.V2.Controllers
{
    /// <summary>
    /// Versão 2 do meu controlador de alunos
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {   
        ///
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public AlunoController( IRepository repository, IMapper mapper){ 
            _repository = repository;
            _mapper = mapper;
        }
                
        /// <summary>
        /// Metodo resposável por retonar um único Alunos
        /// Exemplo de chamada passandro o Id por meio da rota 
        /// api/aluno/byid/2
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id){

            var aluno = _repository.GetAlunoById(id, false);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            
            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            return Ok(alunoDto);
        }       

        /// <summary>
        /// Metodo resposável por incluir os alunos
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model){
            
            var aluno = _mapper.Map<Aluno>(model);

            _repository.Add(aluno);
            if(_repository.SaveChanges())
                return Created($"api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não Cadastrado");
        }

        /// <summary>
        /// Esse metodo é responsável por editar os alunos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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