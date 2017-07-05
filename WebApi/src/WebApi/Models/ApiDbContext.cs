using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace WebApi.Models
{
	public class ApiDbContext : DbContext
	{
		public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
		{
		}

        // конструктор без параметров для тестов
        public ApiDbContext() 
        {
        }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			var factory = new LoggerFactory();
			factory.AddProvider(new NLogLoggerProvider());
			optionsBuilder.UseLoggerFactory(factory);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

            // описываем связи таблицы Message
			builder.Entity<Message>().HasKey(__message => __message.Id);
			builder.Entity<Message>().Property(__message => __message.Text).IsRequired();
			builder.Entity<Message>().Property(__message => __message.CreateDate).IsRequired();
			builder.Entity<Message>().HasOne(__message => __message.Question);

            // описываем связи таблицы Comment
            builder.Entity<Comment>().HasKey(__comment => __comment.Id);
            builder.Entity<Comment>().Property(__comment => __comment.Text).IsRequired();
            builder.Entity<Comment>().Property(__comment => __comment.CreateDate).IsRequired();
            builder.Entity<Comment>().Property(__comment => __comment.ChangeDate).IsRequired();
            builder.Entity<Comment>().Property(__comment => __comment.AuthorName).IsRequired();
            builder.Entity<Comment>().HasOne(__comment => __comment.Message);

        }
	}
}