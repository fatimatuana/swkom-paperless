using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string Upload(IFormFile file)
        {
            var env = "C:/Users/fatim/Desktop/test-/rest-server-upd/out/csharp/src/Org.OpenAPITools/wwwroot/";
            var uploadDirecotroy = "uploads/";
            var uploadPath = Path.Combine(env, uploadDirecotroy);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var strem = File.Create(filePath))
            {
                file.CopyTo(strem);
            }
            return fileName;
        }
    }
}
