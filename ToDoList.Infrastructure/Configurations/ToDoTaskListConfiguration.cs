using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Configurations
{
    public class ToDoTaskListConfiguration : IEntityTypeConfiguration<ToDoTaskList>
    {
        public void Configure(EntityTypeBuilder<ToDoTaskList> builder)
        {
            builder.HasKey(tl => tl.Id);
            builder.Property(tl => tl.Name);
            // Tasks is not added here as it is a nvaigation property and is defined below
            
            // One-to-Many between ToDoTaskList and ToDoTask
            builder.HasMany(tl => tl.Tasks)
                .WithOne() //Only ToDoTaskList know about ToDoTask
                .HasForeignKey("ListId") //Name of FK in EF core ToDoTask table
                .IsRequired() // Make this relationship required to make sure deletion works
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}