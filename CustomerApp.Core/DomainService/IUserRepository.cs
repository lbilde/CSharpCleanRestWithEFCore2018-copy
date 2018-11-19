using CustomerApp.Core.Entity;

namespace CustomerApp.Core.DomainService
{
    public interface IUserRepository
    {
        FilteredList<IUser> ReadAll(Filter filter);
    }
}