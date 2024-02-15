using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstract;
using Entities.Concrete;


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
        public void Add(Document document)
        {
            _documentDal.Add(document);
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
    }
}
