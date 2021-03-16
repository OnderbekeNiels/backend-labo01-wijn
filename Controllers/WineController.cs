using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend_labo01_wijn.Models;
using backend_labo01_wijn.Repositories;

namespace backend_labo01_wijn.Controllers
{
    [ApiController]
    [Route("api")]
    public class WineController : ControllerBase
    {

        private readonly static List<Wine> _wines = new List<Wine>();
        private readonly ILogger<WineController> _logger;

        private IWineRepository _wineRepository;

        public WineController(ILogger<WineController> logger, IWineRepository wineRepository)
        {
            _wineRepository = wineRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("wines")]
        public async Task<ActionResult<List<Wine>>> GetWines()
        {
            try
            {
                return await _wineRepository.GetWines();
            }
            catch (System.Exception ex)
            {
                return new BadRequestResult();
            }

        }

        [HttpGet]
        [Route("wines/{wineId}")]
        public async Task<ActionResult<Wine>> GetWineById(Guid wineId)
        {
            try
            {
                return await _wineRepository.GetWineById(wineId);
            }
            catch (System.Exception ex)
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        [Route("wines")]
        public async Task<ActionResult<Wine>> AddWine(Wine wine)
        {
            if (wine != null)
            {
                return await _wineRepository.AddWine(wine);
            }
            else
            {
                return new BadRequestResult();
            }

        }


        // !Delete op ID
        [HttpDelete]
        [Route("wines/{wineId}")]
        public async Task<ActionResult<int>> RemoveWineById(Guid wineId)
        {
            if (wineId != null)
            {
                return await _wineRepository.RemoveWineById(wineId);
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }

        [HttpPut]
        [Route("wines")]
        public async Task<ActionResult<Wine>> UpdateWine(Wine wine)
        {
            if (wine != null)
            {

                int action = await _wineRepository.UpdateWine(wine);
                if (action == 1)
                {
                    return wine;
                }
                else
                {
                    return new BadRequestResult();
                }
            }
            else
            {
                return new StatusCodeResult(404);
            }
        }
    }
}
