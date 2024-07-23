using BackEndDevChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BackEndDevChallenge
{
    public class CalculatorContext : DbContext
    {
        public CalculatorContext(DbContextOptions<CalculatorContext> options) : base(options)
        {
        }
        
        public DbSet<MathProblem> MathProblems { get; set; }

        
    }
}
