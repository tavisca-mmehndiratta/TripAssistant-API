using System;
using System.Collections.Generic;
using System.Linq;
using Core.Contracts;
namespace TripAssistantSearchEngineApi
{
    public class ContextCheckerService : IContextCheckerService
    {
        private readonly IContextGenerator _contextGenerator;
        string context;
        public ContextCheckerService(IContextGenerator contextGenerator)
        {
            _contextGenerator = contextGenerator;
        }
        public string GetFilteredQueryResponse(string contextInput)
        {
            context = _contextGenerator.GetContextResponse(contextInput);
            string result = "";
            string[] response = context.Split(" ");
            if (response[0].Equals("yes") || response[0].Equals("no"))
            {
                result += response[0];
                if (response[0].Equals("yes"))
                {
                    if (response[response.Length - 1].Equals("complete"))
                    {
                        result += " ok city " + response[1] + " duration " + response[2];
                    }
                    else if (response[response.Length - 1].Equals("only"))
                    {
                        result += " ok city " + response[1] + " duration 1";
                    }
                    else if(response[response.Length - 1].Equals("current"))
                    {
                        result += " ok duration 2 current";
                    }
                    else if(response[response.Length - 1].Equals("las"))
                    {
                        for(int i = 1; i < response.Length; i++)
                        {
                            if (response[i].Equals("avail"))
                            {
                                break;
                            }
                            result += " " + response[i];
                        }
                    }
                    else if(response[response.Length - 1].Equals("donea"))
                    {
                        int i = 1;
                        result += " activity";
                        while (response[i] != "ignore")
                        {
                            result += " " + response[i];
                            i++;
                        }
                        i = Array.IndexOf(response, "continue");
                        i++;
                        result += " city";
                        while(response[i] != "donea")
                        {
                            result += " " + response[i];
                            i++;
                        }
                    }
                    else if(response[response.Length - 1].Equals("attraction"))
                    {
                        result += " attraction ";
                        int i = 1;
                        while(i<response.Length-1)
                        {
                            result += " " + response[i];
                            i++;
                        }
                    }
                    else if (response[response.Length - 1].Equals("remove"))
                    {
                        if (response[1].Equals("not"))
                        {
                            string remove = response[1] + " " + response[2].ToLower();
                            context = contextInput.Replace(remove, "");
                            context = GetFilteredQueryResponse(context);
                            result = "";
                            result += context;
                        }
                        else
                        {
                            result = "no I cannot process this request!";
                        }
                    }
                    else if (response[response.Length - 1].Equals("duration"))
                    {
                        result += " ok duration " + response[1] + " current";
                    }
                }
                else
                {
                    result += PerformOperationForIncorrectContext(response);
                }
            }
            else
            {
                result = "Other";
            }

            return result;
        }
        public string PerformOperationForIncorrectContext(string[] response)
        {
            string result = "";
            if (response[response.Length - 1].Equals("send"))
            {
                for (int index = 2; index <= 7; index++)
                {
                    result += " " + response[index];
                }
            }
            else if (response[response.Length - 1].Equals("trip"))
            {
                int index = Array.IndexOf(response, "activity");
                for (int iterator = index + 1; iterator < response.Length - 1; iterator++)
                {
                    result += " " + response[iterator];
                }
            }
            else
            {
                result += " I can not process this request!!";
            }
            return result;
        }
    }    
}
