using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersApi.Models;

namespace UsersApi.DataBaseAccess
{
    public class TrainingDataProvider
    {

        public void InsertTrainingData(UserTrainedData userTrainedData)
        {
            string query = "insert into users.trainingdata(logid,request,response) values(?, ?, ?)";
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build() ;
            ISession session = cluster.Connect("users");
            var ps = session.Prepare(query);
            Guid guid = Guid.NewGuid();
            var statement = ps.Bind(guid, userTrainedData.Request, userTrainedData.Response);
            session.Execute(statement);

        }
        public List<UserTrainedData> GetTrainingData()
        {
            string query = "Select * from users.trainingdata";
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("users");
            var result=   session.Execute(query);

            var count = 0;
           List<UserTrainedData> userTrainedData = new List<UserTrainedData> ();

            foreach (var row in result)
            {
                userTrainedData[count].Response = row.GetValue<string>("response");
                userTrainedData[count].Request = row.GetValue<string>("request");
                count++;
            }
            return userTrainedData;

        }
    }
}
