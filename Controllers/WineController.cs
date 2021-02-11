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
    [Route("api")]
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
        [Route("wines")]
        public ActionResult<List<Wine>> GetWines()
        {
            try
            {
                return new OkObjectResult(_wines);
            }
            catch (System.Exception ex)
            {
                logger.error(ex.Message);
                return new BadrequestResult();
            }
            
        }

        [HttpGet]
        [Route("wines/{wineId}")]
        public ActionResult<Wine> GetWineById(int wineId)
        {
            Wine _wine = _wines.Find(w => w.WineId == wineId);
            if (_wine != null)
            {
                return new OkObjectResult(_wine);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        [Route("wines")]
        public ActionResult<Wine> AddWine(Wine wine)
        {
            if (wine == null)
            {
                return new BadRequestResult();
            }
            else
            {
                wine.WineId = _wines.Count + 1;
                _wines.Add(wine);
                return new OkObjectResult(wine);
            }

        }

        // !Delete op request body
        [HttpDelete]
        [Route("wines")]
        public ActionResult<Wine> RemoveWine(Wine wine)
        {
            // We doen dit via een anonieme functie(delegate). Dit is een functie zonder naam die we lokaal
            // definiëren en meegeven aan de Find. Deze functie zal het gevonden Wine object terugkeren
            // zodat we dit via Remove kunnen verwijderen.
            Wine _wine = _wines.Find(delegate (Wine w)
            {
                return w.WineId == wine.WineId;
            });

            if (_wine != null)
            {
                _wines.Remove(_wine);
                return new OkObjectResult(wine);
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }


        // !Delete op ID
        [HttpDelete]
        [Route("wines/{wineId}")]
        public ActionResult<Wine> RemoveWineById(int wineId)
        {
            Wine _wine = _wines.Find(delegate (Wine w)
            {
                return w.WineId == wineId;
            });

            if (_wine != null)
            {
                _wines.Remove(_wine);
                return new OkObjectResult(wineId);
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }

        [HttpPut]
        [Route("wines")]
        public ActionResult<Wine> UpdateWine(Wine wine)
        {
            Wine _wine = _wines.Find(delegate (Wine w)
            {
                return w.WineId == wine.WineId;
            });

            if (_wine != null)
            {
                _wine.Name = wine.Name;
                return new OkObjectResult(wine);
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }
    }
}
