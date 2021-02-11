using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend_labo01_wijn.Models;

namespace backend_labo01_wijn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WineController : ControllerBase
    {

        private readonly static List<Wine> _wines = new List<Wine>();
        private readonly ILogger<WineController> _logger;

        public WineController(ILogger<WineController> logger)
        {
            //!Aangezien we nog geen databases gezien hebben gaan we een static lijst gebruiken om de data
            //!bij te houden.We gaan deze aanmaken in de constructor van de controller.We maken deze
            //!Aangezien static zodat deze zal blijven bestaan gedurende de tijd onze applicatie actief is.
            _logger = logger;
            if (_wines == null || _wines.Count == 0)
            {
                _wines.Add(new Wine()
                {
                    WineId = 1,
                    Name = "Sangrato Barolo",
                    Country = "ITA",
                    Price = 35,
                    Color = "red",
                    Year = 2005,
                    Grapes = "Nebiollo"
                });
            }
        }

        [HttpGet]
        public List<Wine> GetWines()
        {
            return _wines;
        }
    }
}
