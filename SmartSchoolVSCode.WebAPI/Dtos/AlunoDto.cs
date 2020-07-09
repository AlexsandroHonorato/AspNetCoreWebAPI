using System;

namespace SmartSchoolVSCode.WebAPI.Dtos
{
    public class AlunoDto
    {
        public int Id { get; set; }   
        public int Matricula { get; set; }     
        public string Nome { get; set; }
        public int Idade { get; set; }  
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }
        public DateTime DataIni { get; set; }
        public bool Ativo { get; set; }
     
    }
}