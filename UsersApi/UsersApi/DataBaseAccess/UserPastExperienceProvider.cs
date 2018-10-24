using System;
using System.Collections.Generic;
using System.Linq;
using Cassandra;
using System.Threading.Tasks;
using UsersApi.Models;
using UsersApi.Services;

namespace UsersApi
{
    public class UserPastExperienceProvider
    {
        public Dictionary<string, int> GetUserPastExperience(string username)
        {
            Dictionary<string, int> pastExperience = new Dictionary<string, int>();
            SortPastExperiences sortPastExperiences = new SortPastExperiences();
            string query = "Select * from users.userspastexperiences where username='" + username+"'";
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("users");
            var result = session.Execute(query);
            foreach(var row in result)
            {
                pastExperience["amusement_park"] = row.GetValue<int>("amusement_park");
                pastExperience["art_gallery"] = row.GetValue<int>("art_gallery");
                pastExperience["aquarium"] = row.GetValue<int>("aquarium");
                pastExperience["church"] = row.GetValue<int>("church");
                pastExperience["hindu_temple"] = row.GetValue<int>("hindu_temple");
                pastExperience["mosque"] = row.GetValue<int>("mosque");
                pastExperience["museum"] = row.GetValue<int>("museum");
                pastExperience["park"] = row.GetValue<int>("park");
                pastExperience["shopping_mall"] = row.GetValue<int>("shopping_mall");
                pastExperience["natural_feature"] = row.GetValue<int>("natural_feature");
                pastExperience["attractions"] = row.GetValue<int>("attractions");
                pastExperience["activity"] = row.GetValue<int>("activity");
                pastExperience["adventures"] = row.GetValue<int>("adventures");
                pastExperience["zoo"] = row.GetValue<int>("zoo");
            }
            return sortPastExperiences.FilterPastExpereince(pastExperience);
        }
        public void InsertUserPastExperience(UserPastExperiences userPastExperiences)
        {
            bool isAvailable = CheckPresenceOfUser(userPastExperiences.UserName);
            if (isAvailable == false)
            {
                string query = "Insert into users.userspastexperiences(username,adventures,amusement_park,aquarium,activity,art_gallery,church,hindu_temple,mosque,museum,park,shopping_mall,natural_feature,attractions,zoo) VALUES(?, ?, ?,?,?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                ISession session = cluster.Connect("users");
                var ps = session.Prepare(query);
                var statement = ps.Bind(userPastExperiences.UserName,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
                session.Execute(statement);
            }
            else
            {
                UpdateUserPreferences(userPastExperiences);
            }
        }
        public void UpdateUserPreferences(UserPastExperiences userPastExperiences)
        {
            string query = "Select "+ userPastExperiences.Type+" from users.userspastexperiences where username='"+userPastExperiences.UserName+"'";
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("users");
            var result = session.Execute(query);
            int origVal=0;
            foreach(var row in result)
            {
                origVal += row.GetValue<int>(userPastExperiences.Type);
            }
            int updatevalue = 4 + origVal;
            query = "update users.userspastexperiences SET "+userPastExperiences.Type+"="+updatevalue+" where username='"+userPastExperiences.UserName+"'";
            cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            session = cluster.Connect("users");
            result = session.Execute(query);
        }
        public bool CheckPresenceOfUser(string username)
        {
            string query = "Select * from users.userspastexperiences where username='" + username+"'";
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("users");
            var result = session.Execute(query);
            IEnumerable<Row> rows = result.GetRows();
            int count = rows.Count();
            if (count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
