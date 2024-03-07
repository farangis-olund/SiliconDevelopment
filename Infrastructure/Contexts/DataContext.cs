using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<UserEntity>(options)
{
	public DbSet<FeatureEntity> Features { get; set; }
	public DbSet<FeatureItemEntity> FeatureItems { get; set; }
    public DbSet<ShowcaseEntity> Showcase { get; set; }
    public DbSet<DownloadAppEntity> DownloadApp { get; set; }
    public DbSet<IntegrationEntity> Integration { get; set; }
    public DbSet<IntegrationToolEntity> IntegrationTools { get; set; }
    public DbSet<SubscriptionEntity> Subscription { get; set; }
    public DbSet<SubscriptionSectionEntity> SubscriptionSection { get; set; }
    public DbSet<SwitchEntity> Switch { get; set; }
    public DbSet<TaskMasterEntity> TaskMaster { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
}


