using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface ITrainingDataLogger
    {
        void InsertValuesIntoTrainingData(string request, string response);
    }
}
