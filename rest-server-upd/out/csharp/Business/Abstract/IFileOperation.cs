﻿using Microsoft.AspNetCore.Http;
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
        string GetFile(string key);
    }

}
