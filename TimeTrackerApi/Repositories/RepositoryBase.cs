namespace TimeTrackerApi.Repositories;
public class RepositoryBase
{
    private readonly TimeTrackerContext dbContext;

    public RepositoryBase(TimeTrackerContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}