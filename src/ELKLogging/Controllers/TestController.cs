using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ELKLogging.Controllers
{
    public class Request
    {
        public String Path { get; set; }
        [Object(Ignore = true)]
        public HttpContext IgnoreMe { get; set; }        
    }

    public class TestController : Controller
    {
        // GET: /<controller>/        
        public IActionResult Add()
        {
            var settings = new ConnectionSettings(new Uri("http://192.168.99.100:9200/"))
                .DefaultIndex("default")
                .MapDefaultTypeIndices(m => m
                    .Add(typeof(Request), "requests-")
                );
                //.MapPropertiesFor<Microsoft.AspNetCore.Http.HttpRequest>(m => m
                //    .Ignore(r => r.HttpContext)
                //);            

            ElasticClient client = new ElasticClient(settings);


            client.Map<Request>(m => m
        .Index("requests-")
        .AutoMap()        
        .Properties(p => p            
            .Text(s => s.Name(r => r.Path))

            //.Object<HttpContext>(s => s
            //    .Name(n => n.IgnoreMe)
            //    //.Index(false)
            //    .Enabled(false)
            //    .IncludeInAll(false)
            //    .Store(false)
            //)
        )
    );            


            client.Index(new Request { Path = this.Request.Path, IgnoreMe = this.Request.HttpContext });
            return Ok();
        }
    }
}
