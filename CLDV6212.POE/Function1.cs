using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web.Http;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;

namespace CLDV6212.POE
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "id/{ID}")] HttpRequest req,
            string ID,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            ID = ID ?? data?.ID;

            Dictionary<string, string> details = new Dictionary<string, string>
            {
            { "F12345678", "1st Dose: Phizer\nDate: 04/06/20\nPlace: Table Bay hotel" },
            { "D34278601", "2st Dose: Phizer\nDate: 05/06/20\nPlace: JG meiring High" },
            { "A84127542", "2nd Dose: Phizer\nDate: 04/06/20\nPlace: Table Bay hotel" },
            { "S12758476", "3nd Dose: Phizer\nDate: 04/06/20\nPlace: JG meiring High" },
            { "M87996435", "1st Dose: Phizer\nDate: 05/06/20\nPlace: Varsity Collage" },
            { "5832589643149", "1st Dose: Phizer\nDate: 05/06/20\nPlace: Varsity Collage" },
            { "4673865832457", "2nd Dose: Phizer\nDate: 04/06/20\nPlace: JG meiring High" },
            { "5674837627400", "2nd Dose: Phizer\nDate: 04/06/20\nPlace: Table Bay hotel" },
            { "0440051474747", "1st Dose: Phizer\nDate: 05/06/20\nPlace: JG meiring High" },
            { "4415112365954", "1st Dose: Phizer\nDate: 04/06/20\nPlace: Table Bay hotel" },
            };
           
            if (!Validator.CheckID(ID) && !Validator.CheckPassport(ID))
            {
                return new BadRequestObjectResult("The ID or Passport Number is not a valid number, please try again.");
            }

            if (details.ContainsKey(ID))
            {
                string detailInfo = details[ID];
                return new OkObjectResult($"ID: {ID}\nVaccination Status:\n{detailInfo}");
            }
            else
            {
                return new OkObjectResult($"ID: {ID} Vaccination Status: Not Vaccinated");
            }
        }
    }
}
