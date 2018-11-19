using System;
using System.Collections.Generic;
using System.Linq;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;

namespace CustomerApp.Core.ApplicationService.Services
{
    public class UserService: IUserService
    {
        readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        public FilteredList<IUser> GetAllUsers(Filter filter = null)
        {
            return _userRepo.ReadAll(filter);
        }

    }
}
