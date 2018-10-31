using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IUserDetailsProvider
    {
        Task<List<Activities>> GetUsersPreferences(string url);
        Task<List<Activities>> GetUsersPastExperience(string url);
    }
}
