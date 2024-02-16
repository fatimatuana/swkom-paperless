using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class DocumentManager : IDocumentService
    {

        IDocumentDal _documentDal;

        public DocumentManager(IDocumentDal documentDal)
        {
            _documentDal = documentDal;
        }

        [ValidationAspect(typeof(DocumentValidator))]
        public void Add(Document entity)
        {
            _documentDal.Add(entity);
        }

        public void Delete(Document document)
        {
            _documentDal.Delete(document);
        }

        public List<Document> Getall()
        {
            return _documentDal.GetAll();
        }

        public Document GetById(int id)
        {
            return _documentDal.Get(i => i.Id == id);            
        }

        public Document GetByTitle(string title)
        {
            return _documentDal.Get(t => t.Title == title);
        }

        public void Update(Document document)
        {
            _documentDal.Update(document);
        }


        public void PostFileAsync(IFormFile fileData)
        {
            try
            {
                var document = new Document()
                {
                    //Id = 10,
                    Title = fileData.FileName,
                    //Documentfile = fileData.ContentDisposition,
                    DocumentType = 1,
                    Content = fileData.ContentDisposition
                };

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);

                    document.Documentfile = stream.ToArray();
                }

                _documentDal.Add(document);
                //var result = dbContextClass.FileDetails.Add(fileDetails);
                //await dbContextClass.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
