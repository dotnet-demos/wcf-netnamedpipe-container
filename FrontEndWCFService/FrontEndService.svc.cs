using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FrontEndWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class FrontEndService : IFrontEndService
    {
        public string GetAreaOfCircle(string value, string delayInSeconds)
        {
            TelemetryClient client = GetTelemetryClient();
            double radius = Convert.ToDouble(value);

            int delay = Convert.ToInt32(delayInSeconds);

            double pi = GetValueOfPi(delay).Result;
            Thread.Sleep(TimeSpan.FromSeconds(delay));

            return string.Format("Area of circle with {0} raidus is {1}", value, pi * radius * radius);
        }

        private static TelemetryClient GetTelemetryClient()
        {
            return new TelemetryClient(TelemetryConfiguration.Active);
        }
        static readonly Random random = new Random();
        private static async Task<double> GetValueOfPi(int delayInSeconds)
        {
            double pi;
            bool shouldGetValueFromServiceConfig = Convert.ToBoolean(ConfigurationManager.AppSettings["ShouldGetPiFromService"]);
            bool shouldGetPiFromService = shouldGetValueFromServiceConfig;// | random.Next() % 2 == 0;
            //shouldGetPiFromService = false; // Hack if the netPipeService is not working.
            if (shouldGetPiFromService)
            {
                InternalServiceReference.InternalServiceClient serviceClient = new InternalServiceReference.InternalServiceClient();
                pi = await serviceClient.GetValueOfPiAsync(delayInSeconds);
                //This public URL will also give the value. https://api.pi.delivery/v1/pi?start=0&numberOfDigits=10
            }
            else
            {
                pi = 3.14;
            }
            GetTelemetryClient().TrackTrace($"{nameof(GetValueOfPi)}Value of pi obtained= {pi}");

            return pi;
        }
    }
}
