using Entities.Concrete;
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

        void Add (Document document);

        void Delete (Document document);

        void Update (Document document);
    }
}
