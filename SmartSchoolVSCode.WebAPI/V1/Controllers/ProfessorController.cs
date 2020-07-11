using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchoolVSCode.WebAPI.Data;
using SmartSchoolVSCode.WebAPI.V1.Dtos;
using SmartSchoolVSCode.WebAPI.Models;

namespace SmartSchoolVSCode.WebAPI.V1.Controllers
{
    /// <summary>
    /// Versão 1 do meu controlador de Professores
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {     
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ProfessorController(IRepository repository, IMapper mapper){       
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(){
            var professor = _repository.GetAllProfessores(true);

            return Ok(_mapper.Map<IEnumerable<ProfessorRegisterDto>>(professor));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var professor = _repository.GetProfessorById(id, false);
            if(professor == null) return BadRequest("Professor não encontrado");
            
            var professorDto = _mapper.Map<AlunoDto>(professor);

            return Ok(professorDto);
        }       
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(int id, ProfessorRegisterDto model ){
            var professor = _mapper.Map<Professor>(model);

            _repository.Add(professor);
            if(_repository.SaveChanges())
                 return Created($"api/aluno/{model.Id}", _mapper.Map<ProfessorRegisterDto>(professor));

            return BadRequest("Professor não Cadastrado");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegisterDto model){    
            var professor = _repository.GetAlunoById(id, false);
            if(professor == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, professor);

            _repository.Update(professor);
            if(_repository.SaveChanges())
                return Created($"api/aluno/{model.Id}", _mapper.Map<ProfessorRegisterDto>(professor));

            return BadRequest("Aluno não Atualizado");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegisterDto model){
            var professor = _repository.GetAlunoById(id, false);
            if(professor == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model, professor);

            _repository.Update(professor);
            if(_repository.SaveChanges())
                return Created($"api/aluno/{model.Id}", _mapper.Map<ProfessorRegisterDto>(professor));

            return BadRequest("Aluno não Atualizado");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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