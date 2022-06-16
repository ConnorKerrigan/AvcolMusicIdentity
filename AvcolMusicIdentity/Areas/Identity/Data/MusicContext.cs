using AvcolMusicIdentity.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AvcolMusicIdentity.Models;

namespace AvcolMusicIdentity.Areas.Identity.Data;

public class MusicContext : IdentityDbContext<ACUser>
{
    public MusicContext(DbContextOptions<MusicContext> options)
        : base(options)
    {

    }
    public DbSet<AvcolMusicIdentity.Models.Group> Group { get; set; }

    public DbSet<AvcolMusicIdentity.Models.Lesson> Lesson { get; set; }

    public DbSet<AvcolMusicIdentity.Models.Teacher> Teacher { get; set; }

    public DbSet<AvcolMusicIdentity.Models.Class> Class { get; set; }

    public DbSet<AvcolMusicIdentity.Models.MusicTimetable> MusicTimetable { get; set; }

    public DbSet<AvcolMusicIdentity.Models.Student> Student { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
