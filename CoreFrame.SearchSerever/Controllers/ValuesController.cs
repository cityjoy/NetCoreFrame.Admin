using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreFrame.SearchSerever.Model;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace CoreFrame.SearchSerever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ElasticClient _client;

        public ValuesController(IEsClientProvider clientProvider)
        {
            _client = clientProvider.GetClient();
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

       

        [HttpPost]
        [Route("value/index")]
        public IIndexResponse Index(Article post)
        {
            if (!_client.IndexExists("default").Exists)
            {
                _client
                .CreateIndex("default", i => i
                .Mappings(m => m
                .Map<Article>(ms => ms.AutoMap())));
            }
            return _client.IndexDocument(post);
        }

        [HttpPost]
        [Route("value/search")]
        public IReadOnlyCollection<Article> Search(string title)
        {
            return _client.Search<Article>(s => s
                .From(0)
                .Size(10)
                .Query(q => q.Match(m => m.Field(f => f.Title).Query(title)))).Documents;
        }
    }
}
