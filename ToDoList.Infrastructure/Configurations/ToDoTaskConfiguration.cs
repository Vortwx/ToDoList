using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Configurations
{
    public class ToDoTaskConfiguration : IEntityTypeConfiguration<ToDoTask>
    {
        public void Configure(EntityTypeBuilder<ToDoTask> builder)
        {
            // Delegate validation to each Dto
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title);
            builder.Property(t => t.DueDateTime);
            builder.Property(t => t.IsDone);
        }
    }
}