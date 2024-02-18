using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.AccessControl;
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

        private readonly IMinioClient _minio;

        public FileOperation(IConfiguration Configuration, ILogger<FileOperation> logger)
        {
            this.Configuration = Configuration;
            this.Logger = logger;
            _minio = new MinioClient()
                                   .WithEndpoint(minIoEndPoint)
                                   .WithCredentials(minIoPassword, minioSecretkey)
                                   //.WithSSL()
                                   .Build();

        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var key = String.Empty;
            try
            {
                key = Guid.NewGuid().ToString();
                var stream = file.OpenReadStream();

                var putObjectArgs = new PutObjectArgs()
                   .WithBucket(bucketName).WithStreamData(stream).WithObject(key)
.WithObjectSize(stream.Length);

                await _minio.PutObjectAsync(putObjectArgs);
            }
            catch (Exception e)
            {
                Logger.LogError("Error ocurred In UploadFileAsync", e);
                throw new Exception("Error ocurred In UploadFileAsync", e);
            }
            return key;
        }

        //public string GetPreSignedURL(string key)
        //{
        //    if (string.IsNullOrEmpty(key)) return null;

        //    return _client.GetPreSignedURL(new GetPreSignedUrlRequest()
        //    {
        //        BucketName = bucketName,
        //        Key = key,
        //        Expires = DateTime.Now.AddMinutes(30)
        //    });
        //}

        public async Task<Stream> GetFile(string key)
        {

            var memoryStream = new MemoryStream();
            var getObjectArgs = new GetObjectArgs().WithBucket(bucketName).WithObject(key)
                .WithCallbackStream((stream) => stream.CopyTo(memoryStream));
            await _minio.GetObjectAsync(getObjectArgs);

            // Reset the stream position to the beginning. (This is required for the stream to be read.)
            memoryStream.Position = 0;
            return memoryStream;

        }

    }
}