using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SnakeGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SnakeController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("GET");
            return Ok(Linker.jsonData);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Result direction)
        {
            Console.WriteLine("POST");
            try
            {
                Linker.Snake.Rotate( direction.direction);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
        public class Result
        {
            public string direction { get; set; }
        }
    }
}