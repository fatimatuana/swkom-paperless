// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using DataAccess.Concrete;
using Entities.Concrete;

Console.WriteLine("Hello, World!");

DocumentManager docManager = new DocumentManager(new EfDocumentDal());

docManager.Add(new Document { Title = "deneme78ts" });

var results = docManager.Getall();
foreach (var x in results)
{
    Console.WriteLine(x.Title);

}

//var found = docManager.GetByTitle("denemee3");
//found.Title = "denemee4";
//docManager.Update(found);

//results = docManager.Getall();
//foreach (var x in results)
//{
//    Console.WriteLine(x.Title);

//}
