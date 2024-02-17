using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FileOperation : IFileOperation
    {
        private IConfiguration Configuration { get; set; }
        private ILogger<FileOperation> Logger { get; set; }
        private string minioSecretkey => Configuration["MinioAccessInfo:SecretKey"];
        private string minIoPassword => Configuration["MinioAccessInfo:Password"];
        private string minIoEndPoint => Configuration["MinioAccessInfo:EndPoint"];
        private string bucketName => Configuration["MinioAccessInfo:BucketName"];
        private readonly AmazonS3Client _client;

        public FileOperation(IConfiguration Configuration, ILogger<FileOperation> logger)
        {
            this.Configuration = Configuration;
            this.Logger = logger;
            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName("us-east-1"),
                ServiceURL = minIoEndPoint,
                ForcePathStyle = true,
                SignatureVersion = "2"
            };
            _client = new AmazonS3Client(minioSecretkey, minIoPassword, config);
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var key = String.Empty;
            try
            {
                key = Guid.NewGuid().ToString();
                var stream = file.OpenReadStream();
                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    InputStream = stream,
                    AutoCloseStream = true,
                    Key = key,
                    ContentType = file.ContentType
                };
                var encodedFilename = Uri.EscapeDataString(file.FileName);
                request.Metadata.Add("original-filename", encodedFilename);
                request.Headers.ContentDisposition = $"attachment; filename=\"{encodedFilename}\"";
                await _client.PutObjectAsync(request);
            }
            catch (Exception e)
            {
                Logger.LogError("Error ocurred In UploadFileAsync", e);
                throw new Exception("Error ocurred In UploadFileAsync", e);
            }
            return key;
        }

        public string GetPreSignedURL(string key)
        {
            if (string.IsNullOrEmpty(key)) return null;

            return _client.GetPreSignedURL(new GetPreSignedUrlRequest()
            {
                BucketName = bucketName,
                Key = key,
                Expires = DateTime.Now.AddMinutes(30)
            });
        }

        public async Task<GetObjectResponse> GetFile(string key)
        {
            MemoryStream memoryStream = new MemoryStream();
            if (string.IsNullOrEmpty(key)) return null;

            return await _client.GetObjectAsync(bucketName, key);


        }

        string GetContentType(string fileName)
        {
            if (fileName.Contains(".jpg"))
            {
                return "image/jpg";
            }
            else if (fileName.Contains(".jpeg"))
            {
                return "image/jpeg";
            }
            else if (fileName.Contains(".png"))
            {
                return "image/png";
            }
            else if (fileName.Contains(".gif"))
            {
                return "image/gif";
            }
            else if (fileName.Contains(".pdf"))
            {
                return "application/pdf";
            }
            else
            {
                return "application/octet-stream";
            }
        }
    }
}