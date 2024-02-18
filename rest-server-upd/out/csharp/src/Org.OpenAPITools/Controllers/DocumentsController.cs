﻿using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations; // for [Required]..

//bizimkii 
namespace Org.OpenAPITools.Controllers
{
    [Route("/api/v2/documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        IDocumentService _documentService; 
        IElasticSearchService _elasticSearchService;

        public DocumentsController(IDocumentService documentService, IElasticSearchService elasticSearchService)
        {
            _documentService = documentService;
            _elasticSearchService = elasticSearchService;
        }

        //[HttpPost]
        //public IActionResult Add(Document document)
        //{
        //    _documentService.Add(document);
        //        return Ok();
        //}

        //[HttpGet]
        //public IActionResult Getall()
        //{
        //    var result = _documentService.Getall();
        //    return Ok(result);
        //}

        [HttpGet]
        public IActionResult Get(string title)
        {
            var result = _documentService.GetByTitle(title);
            //var result = _documentService.GetByTitle("if21b140_github_repo.txt");
            return Ok(result.Content);
        }

        [HttpGet("search")]
        public IActionResult Search(string text)
        {
            var result = _elasticSearchService.SearchDocument(text);
            //var result = _documentService.GetByTitle("if21b140_github_repo.txt");
            return Ok(result.Content);
        }

        [HttpPost]
        public ActionResult UploadImage(IFormFile file)
        {
            
            //Document document = new Document();
            //document.Title = file.FileName;
            _documentService.PostFileAsync(file);
   
            if(file == null) { throw new DocumentCtr_NullReferenceException(); }
            return Ok(file.FileName);
        }

        [HttpDelete]
        [Route("/api/v2/documents/{id}")]
        public IActionResult Delete([FromRoute][Required] int id)
        {
            var document = _documentService.GetById(id);
            _documentService.Delete(document);
            return Ok();
        }

    }
}

public class DocumentCtr_NullReferenceException : NullReferenceException{ };