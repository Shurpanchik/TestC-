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
        public virtual DbSet<Forum> Forums { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

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

            //связи Forum
            builder.Entity<Forum>().HasKey(__forum => __forum.Id);
            builder.Entity<Forum>().Property(__forum => __forum.Name).IsRequired();
            builder.Entity<Forum>().HasMany(__forum => __forum.Topics);

            // связи Topic
            builder.Entity<Topic>().HasKey(__topic => __topic.Id);
            builder.Entity<Topic>().Property(__topic => __topic.Name).IsRequired();
            builder.Entity<Topic>().Property(__topic => __topic.Forum).IsRequired();
            builder.Entity<Topic>().HasOne(__topic => __topic.Forum);
            builder.Entity<Topic>().HasMany(__topic => __topic.Posts);

            //связи Post
            builder.Entity<Post>().HasKey(__post => __post.Id);
            builder.Entity<Post>().Property(__post => __post.Text).IsRequired();
            builder.Entity<Post>().Property(__post => __post.Topic).IsRequired();
            builder.Entity<Post>().HasOne(__topic => __topic.Topic);

        }
	}
}