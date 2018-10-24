using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface ITrainingDataLogger
    {
        void InsertValuesIntoTrainingData(string request, string response);
    }
}
