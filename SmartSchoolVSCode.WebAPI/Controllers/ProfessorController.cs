using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchoolVSCode.WebAPI.Models;

namespace SmartSchoolVSCode.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public ProfessorController(){
            
        }

        [HttpGet]
        public IActionResult Get(){

            return Ok("Professores: Alex, Gi, Jey");
        }     
    }
}