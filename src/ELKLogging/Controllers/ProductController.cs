using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ELKLogging.Models;
using ELKLogging.ElasticSearch;

namespace ELKLogging.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private ESClientProvider _esClientProvider;
        public ProductController(ESClientProvider esClientProvider)
        {
            _esClientProvider = esClientProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Product product)
        {
            product.Id = Guid.NewGuid();

            var res = await _esClientProvider.Client.IndexAsync(product);
            if (!res.IsValid)
            {
                throw new InvalidOperationException(res.DebugInformation);
            }

            return Ok();
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find(string term)
        {
            var res = await _esClientProvider.Client.SearchAsync<Product>(x => x
                .Query( q => q.
                    SimpleQueryString(qs => qs.Query(term))));
            if (!res.IsValid)
            {
                throw new InvalidOperationException(res.DebugInformation);
            }

            return Json(res.Documents);
        }
    }
}
