using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Tesseract;

namespace Business.Concrete
{
    public class DocumentManager : IDocumentService
    {
        IMapper _mapper;
        IDocumentDal _documentDal;

        public DocumentManager(IDocumentDal documentDal, IMapper mapper)
        {
            _documentDal = documentDal;
            _mapper = mapper;
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

        public const string folderName = "images/";
        public const string trainedDataFolderName = "tessdata";
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

                var fileInfo = _mapper.Map<DocumentTypeDto>(document);
                document.DocumentExtension = fileInfo.DocumentExtension;

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);

                    document.Documentfile = stream.ToArray();
                }

                _documentDal.Add(document);


                string name = fileData.FileName;
                var image = fileData;

                if (image.Length > 0)
                {
                    using (var fileStream = new FileStream(folderName + image.FileName, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
                }

                string tessPath = Path.Combine(trainedDataFolderName, "");
                string result = "";

                using (var engine = new TesseractEngine(tessPath, "DEU", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(folderName + name))
                    {
                       
                        var page = engine.Process(img);
                        result = page.GetText();
                        Console.WriteLine(result);
                    }
                }
                Console.WriteLine(result);

                //var result = dbContextClass.FileDetails.Add(fileDetails);
                //await dbContextClass.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public void PostFileAsync(IFormFile fileData)
        //{
        //    try
        //    {
        //        var document = new Document()
        //        {
        //            Title = fileData.FileName,
        //            DocumentType = 1,
        //            Content = fileData.ContentDisposition
        //        };

        //        // Convert the IFormFile content to a byte array
        //        using (var stream = new MemoryStream())
        //        {
        //            fileData.CopyTo(stream);
        //            document.Documentfile = stream.ToArray();
        //        }

        //        // Perform OCR using Tesseract
        //        string ocrResult = PerformOCR(document.Documentfile);

        //        // Additional processing with OCR result, e.g., save to document or log
        //        document.OcrResult = ocrResult;

        //        _documentDal.Add(document);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        private string PerformOCR(byte[] imageData)
        {
            using (var engine = new TesseractEngine("./tessdata", "deu", EngineMode.Default))
            {
                using (var pix = Pix.LoadFromMemory(imageData))
                {
                    using (var page = engine.Process(pix))
                    {
                        return page.GetText();
                    }
                }
            }
        }
    }
}
