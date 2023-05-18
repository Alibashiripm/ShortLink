using Microsoft.EntityFrameworkCore;

using ShortLink.Domain.Models.Link;
using System.Linq;

namespace ShortLink.Infra.Data.Context
{
    public class ShortLinkContext:DbContext
    {
        public ShortLinkContext(DbContextOptions<ShortLinkContext> options):base(options)
        {

        }



        #region link
        public DbSet<ShortUrl> ShortUrls { get; set; }

        public DbSet<RequestUrl> RequestUrls { get; set; }
        #endregion

        #region on model creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(s=> s.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
