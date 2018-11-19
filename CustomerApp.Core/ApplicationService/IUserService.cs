using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService
{
    public interface IUserService
    {
        FilteredList<IUser> GetAllUsers(Filter filter);
    }
}