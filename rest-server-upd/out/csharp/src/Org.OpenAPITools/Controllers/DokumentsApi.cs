using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.OpenAPITools.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace Org.OpenAPITools.Controllers
{
    [Route("/api/testget")]
    [ApiController]
    public class DokumentsApi : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllDocumentsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Token 3916a4ba37fca60eb43af5db9f960389600d8da0");
                HttpResponseMessage response = await client.GetAsync("http://localhost:8080/api/documents/?query=deneme");

                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                {
                    Console.Write("Failure");
                }

                return (IActionResult)response;

            }

            ///// <summary>
            ///// 
            ///// </summary>
            ///// <param name="id"></param>
            ///// <param name="original"></param>
            ///// <response code="200">Success</response>
            //[HttpGet]
            //[Route("/api/documents/{id}/download")]
            //[ValidateModelState]
            //[SwaggerOperation("DownloadDocument")]
            //public virtual IActionResult DownloadDocument([FromRoute(Name = "id")][Required] int id, [FromQuery(Name = "original")] bool? original)
            //{

            //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            //    // return StatusCode(200);

            //    return StatusCode(500, "Internal server error, - dokumentApi in controllers");

            //    throw new NotImplementedException();
            //}
        }
    }
}
