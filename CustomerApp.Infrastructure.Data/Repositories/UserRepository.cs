using System.Linq;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Infrastructure.Data.Repositories
{
    public class UserRepository: IUserRepository
    {
        readonly CustomerAppContext _ctx;

        public UserRepository(CustomerAppContext ctx)
        {
            _ctx = ctx;
        }
        
        public FilteredList<IUser> ReadAll(Filter filter)
        {
            //Create a Filtered List
            var filteredList = new FilteredList<IUser>();
            
            //If there is a Filter then filter the list and set Count
            if (filter != null && filter.ItemsPrPage > 0 && filter.CurrentPage > 0)
            {
                filteredList.List = _ctx.Users
                    .Select(u => new AuthUser { Id = u.Id, Email = u.Email})
                    .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                    .Take(filter.ItemsPrPage);
                filteredList.Count = _ctx.Users.Count();
                return filteredList;
            }
            
            //Else just return the full list and get the count from the list (to save a SQL call)
            filteredList.List = _ctx.Users
                .Select(u => new AuthUser { Id = u.Id, Email = u.Email});
            filteredList.Count = filteredList.List.Count();
            return filteredList;
        }
    }
}