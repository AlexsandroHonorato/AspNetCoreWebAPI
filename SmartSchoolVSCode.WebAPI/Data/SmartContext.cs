using Microsoft.EntityFrameworkCore;
using SmartSchoolVSCode.WebAPI.Models;

namespace SmartSchoolVSCode.WebAPI.Data
{
    public class SmartContext : DbContext
    {
        public SmartContext(DbContextOptions<SmartContext> options) : base(options){
            
        }
        public DbSet<Aluno > Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }  
        public DbSet<AlunoDisciplina> AlunosDiciplinas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder){ //Essse metodo cria a chave no banco Muitos Alunos Para Muitas Disciplina
            builder.Entity<AlunoDisciplina>()
            .HasKey(AD => new{AD.AlunoId, AD.DisciplinaId});
        }
    }
}