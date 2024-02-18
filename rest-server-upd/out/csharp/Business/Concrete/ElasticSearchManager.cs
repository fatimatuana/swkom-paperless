using Business.Abstract;
using Entities.Concrete;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ElasticSearchManager : IElasticSearchService
    {
        private readonly ElasticClient _elasticClient;

        public ElasticSearchManager() //ElasticClient elasticClient
        {

            ConnectionSettings connSettings =
                new ConnectionSettings(new Uri("http://localhost:9200/"))
                      .DefaultIndex("esearchitems")
                      .DefaultMappingFor<ElasticDocument>(m => m
                      .PropertyName(p => p.Key, "key")

          );

            _elasticClient = new ElasticClient(connSettings);
            if (!_elasticClient.Indices.Exists("esearchitems").Exists)
            {
                _elasticClient.Indices.Create("esearchitems",
                     index => index.Map<ElasticDocument>(
                          x => x
                         .AutoMap()
                  ));
            }
        }

        public void PutDocument(ElasticDocument elasticDocument)
        {
            _elasticClient.Index(elasticDocument, s => s.Index("esearchitems"));
        }

        public ElasticDocument SearchDocument(string key)
        {
            var response = _elasticClient.Search<ElasticDocument>(i => i
  .Query(q => q.Bool(b => b
        .Should(
            s => s.Match(m => m.Query(key).Field(f => f.Content).Fuzziness(Fuzziness.EditDistance(1)))
        ))));

            if(response.Documents.Count() > 0)
            {
                return response.Documents.First();
            }

            throw new Exception("Doc not found");
        }
    }
}
