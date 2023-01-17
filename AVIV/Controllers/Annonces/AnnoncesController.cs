using AVIV.API.Api;
using AVIV.Core.Features.Advertisements.Commands.CreateAdvertisement;
using AVIV.Core.Features.Advertisements.Commands.UpdateStatus;
using AVIV.Core.Features.Advertisements.Queries.GetAdvertisementById;
using AVIV.Core.Features.Advertisements.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AVIV.Controllers.Annonces
{
    public class AnnoncesController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<string>> Post(CreateAdvertisementCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdvertisementDto>> Get(string id)
        {
            var item = await Mediator.Send(new GetAdvertisementByIdQuery() { Id = id });
            return Ok(item);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateStatus(string id, UpdateAdvertisementStatusCommand command)
        {
            command.Id = id;

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
