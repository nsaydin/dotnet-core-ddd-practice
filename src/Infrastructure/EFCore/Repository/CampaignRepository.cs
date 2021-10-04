using Core.Domain.Campaign;

namespace Infrastructure.EFCore.Repository
{
    public class CampaignRepository : Repository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}