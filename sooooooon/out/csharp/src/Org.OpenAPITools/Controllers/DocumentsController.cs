using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Org.OpenAPITools.Controllers
{
    [Route("/api/documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        public IActionResult Add(Document document)
        {
            _documentService.Add(document);
                return Ok();
        }

        [HttpGet]
        public IActionResult Getall()
        {
            var result = _documentService.Getall();
            return Ok(result);
        }

    }
}
