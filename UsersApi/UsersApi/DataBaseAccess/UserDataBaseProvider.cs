using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApi.Models;
using UsersApi.Services;

namespace UsersApi
{
    public class UserDataBaseProvider
    {
        public Dictionary<string, int> GetUser(string username)
        {
            SortedDictionary<string, int> userPreferences = new SortedDictionary<string, int>();
            SortUserPreferences sortUserPreferences = new SortUserPreferences();
            string query = "Select preferences from users.usersdetails where username='" + username+"'";
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("users");
            var result = session.Execute(query);
            foreach (var row in result)
            {
                userPreferences = row.GetValue<SortedDictionary<string, int>>("preferences");
            }
            return sortUserPreferences.UpdateRecord(userPreferences);
        }
        public void InsertUser(Users users)
        {
            string query = "Insert into users.usersdetails(username,email,fullname,password,phoneno,preferences) VALUES(?, ?, ?, ?, ?, ?)";
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("users");
            var ps = session.Prepare(query);
            var statement = ps.Bind(users.UserName, users.Email,users.FullName,users.Password,users.PhoneNo,users.Preferences);
            session.Execute(statement);
        }
    }
}
