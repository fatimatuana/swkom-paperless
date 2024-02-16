using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDocumentService
    {
        List<Document> Getall();

        Document GetById(int id);

        Document GetByTitle(string title);

        void Add (Document entity);

        void Delete (Document document);

        void Update (Document document);

        void PostFileAsync(IFormFile fileData);
    }
}
