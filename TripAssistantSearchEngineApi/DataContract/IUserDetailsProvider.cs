using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contract
{
    public interface IUserDetailsProvider
    {
        Task<List<Activities>> GetUsersPreferences(string username, string url);
    }
}
