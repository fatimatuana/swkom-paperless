using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Tesseract;

namespace Business.Concrete
{
    public class DocumentManager : IDocumentService
    {
        IMapper _mapper;
        IDocumentDal _documentDal;
        IFileOperation _fileOperation;
        IRabbitMQService _rabbitMQService;

        public DocumentManager(IDocumentDal documentDal, IMapper mapper, IFileOperation fileOperation, IRabbitMQService rabbitMQService)
        {
            _documentDal = documentDal;
            _mapper = mapper;
            _fileOperation = fileOperation;

            _rabbitMQService = rabbitMQService;
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

        public Document GetByKey(string key)
        {
            return _documentDal.Get(i => i.Key == key);
        }


        public Document GetByTitle(string title)
        {
            return _documentDal.Get(t => t.Title == title);
        }

        public void Update(Document document)
        {
            _documentDal.Update(document);
        }

        [ValidationAspect(typeof(DocumentValidator))]
        public async void PostFileAsync(IFormFile fileData)
        {
            try
            {
                var document = new Document()
                {
                    Title = fileData.FileName,
                    DocumentType = 1,
                    Content = fileData.ContentDisposition
                };

                var fileInfo = _mapper.Map<DocumentTypeDto>(document);
                document.DocumentExtension = fileInfo.DocumentExtension;

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);

                    document.Documentfile = stream.ToArray();
                }
                var key = await _fileOperation.UploadFile(fileData);
                document.Key = key;
                _documentDal.Add(document);
           
                _rabbitMQService.SendEvent(key);
            }
            catch (Exception)
            {
                throw new Document_UploadErrorException();
            }
        }
    }
}


public class Document_UploadErrorException : ApplicationException { };
