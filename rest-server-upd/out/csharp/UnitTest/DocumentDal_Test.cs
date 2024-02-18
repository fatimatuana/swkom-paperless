using Amazon.S3.Model;
using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace UnitTest
{
    public class DocumentDal_Test
    {


        [Fact]
        public void Test_Document_GetByTitle()
        {
            // Arrange
            var documentDalMock = new Mock<IDocumentDal>();
            var imapperMock = new Mock<IMapper>();
            var ifileMock = new Mock<IFileOperation>();
            var irqMock = new Mock<IRabbitMQService>();
            documentDalMock.Setup(m => m.Get(x => x.Title ==  "title")).Returns(new Document() { Id = 1 ,Title="title"});

            var documentManager = new DocumentManager(documentDalMock.Object, imapperMock.Object, ifileMock.Object, irqMock.Object);

            // Act
            var result =  documentManager.GetByTitle("title");

            // Assert
            Assert.Equal("title", result.Title);
        }

        [Fact]
        public void Test_Document_GetById()
        {
            // Arrange
            var documentDalMock = new Mock<IDocumentDal>();
            var imapperMock = new Mock<IMapper>(); 
            var ifileMock = new Mock<IFileOperation>();
            var irqMock = new Mock<IRabbitMQService>();

            documentDalMock.Setup(m => m.Get(x=>x.Id==1)).Returns(new Document() {Id=1 });


            var documentManager = new DocumentManager(documentDalMock.Object, imapperMock.Object, ifileMock.Object, irqMock.Object);

            // Act
            var result = documentManager.GetById(1);

            // Assert
            Assert.Equal(1 , result.Id);
        }

    }
}