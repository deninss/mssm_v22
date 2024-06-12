using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using mssm.Models.TeamModel;
using mssm.Models.ProjectModel;
using System.Numerics;
using mssm.Models;

namespace mssm.Context
{
    public class ApplicationDbContext : IdentityDbContext<UserContext>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Team> Team { get; set; }
        public DbSet<ProjectModel> Project { get; set; }
        public DbSet<TaskModel> Task { get; set; } 
        public DbSet<SubtasksModel> Subtasks { get; set; } 
        public DbSet<DepartmentModel> Department { get; set; } 
        public DbSet<Role> Role { get; set; } 
        public DbSet<TeamMembers> TeamMembers { get; set; } 
        public DbSet<DepartmentMembers> DepartmentMembers { get; set; }
        public DbSet<ExecutorTask> ExecutorTask { get; set; }
    }
}
