using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFileOperation
    {
        Task<string> UploadFile(IFormFile file);
        Task<Stream> GetFile(string key);
    }

}
