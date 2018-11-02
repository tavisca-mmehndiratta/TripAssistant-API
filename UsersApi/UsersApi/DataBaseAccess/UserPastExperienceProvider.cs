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
                pastExperience["angling"] = row.GetValue<int>("angling");
                pastExperience["bamboo_rafting"] = row.GetValue<int>("bamboo_rafting");
                pastExperience["biking"] = row.GetValue<int>("biking");
                pastExperience["bunjee_jumping"] = row.GetValue<int>("bunjee_jumping");
                pastExperience["cable_car"] = row.GetValue<int>("cable_car");
                pastExperience["camel_safari"] = row.GetValue<int>("camel_safari");
                pastExperience["camping"] = row.GetValue<int>("camping");
                pastExperience["caving"] = row.GetValue<int>("caving");
                pastExperience["kayaking"] = row.GetValue<int>("kayaking");
                pastExperience["cliff_jumping"] = row.GetValue<int>("cliff_jumping");
                pastExperience["climbing"] = row.GetValue<int>("climbing");
                pastExperience["dune_bashing"] = row.GetValue<int>("dune_bashing");
                pastExperience["flying_fox"] = row.GetValue<int>("flying_fox");
                pastExperience["giant_swing"] = row.GetValue<int>("giant_swing");
                pastExperience["heli_skiing"] = row.GetValue<int>("heli_skiing");
                pastExperience["hot_air_balloon"] = row.GetValue<int>("hot_air_balloon");
                pastExperience["microlight_flying"] = row.GetValue<int>("microlight_flying");
                pastExperience["para_sailing"] = row.GetValue<int>("para_sailing");
                pastExperience["paragliding"] = row.GetValue<int>("paragliding");
                pastExperience["rafting"] = row.GetValue<int>("rafting");
                pastExperience["river_rafting"] = row.GetValue<int>("river_rafting");
                pastExperience["safari"] = row.GetValue<int>("safari");
                pastExperience["scuba_diving"] = row.GetValue<int>("scuba_diving");
                pastExperience["sightseeings"] = row.GetValue<int>("sightseeings");
                pastExperience["sky_diving"] = row.GetValue<int>("sky_diving");
                pastExperience["snorkelling"] = row.GetValue<int>("snorkelling");
                pastExperience["surfing"] = row.GetValue<int>("surfing");
                pastExperience["swimming"] = row.GetValue<int>("swimming");
                pastExperience["trekking"] = row.GetValue<int>("trekking");
                pastExperience["underwater_walk"] = row.GetValue<int>("underwater_walk");
                pastExperience["waterfall"] = row.GetValue<int>("waterfall");
                pastExperience["wildlife_safari"] = row.GetValue<int>("wildlife_safari");
                pastExperience["zorbing"] = row.GetValue<int>("zorbing");

            }
            return sortPastExperiences.FilterPastExpereince(pastExperience);
        }
        public void InsertUserPastExperience(UserPastExperiences userPastExperiences)
        {
            bool isAvailable = CheckPresenceOfUser(userPastExperiences.UserName);
            if (isAvailable == false)
            {
                string query = "Insert into users.userspastexperiences(username,activity,adventures,amusement_park,angling,art_gallery,attractions,bamboo_rafting,biking,bunjee_jumping,cable_car,camel_safari,camping,caving,church,cliff_jumping,climbing,dune_bashing,flying_fox,giant_swing,heli_skiing,hindu_temple,hot_air_balloon,kayaking,microlight_flying,mosque,museum,natural_feature,para_sailing,paragliding,park,rafting,river_rafting,safari,scuba_diving,shopping_mall,sightseeings,sky_diving,snorkelling,surfing,swimming,trekking,underwater_walk,waterfall,wildlife_safari,zoo,zorbing) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                ISession session = cluster.Connect("users");
                var ps = session.Prepare(query);
                var statement = ps.Bind(userPastExperiences.UserName,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
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
