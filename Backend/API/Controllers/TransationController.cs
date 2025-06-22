using Microsoft.AspNetCore.Mvc;
using Person.Data;
using Person.Models.Requests;
using PersonTransation.Models.Entities;
using PersonTransation.Services;

namespace PersonTransation.Controllers
{
    [Route("api/v1/transation")]
    [ApiController]
    public class TransationController : ControllerBase
    {
        private readonly TransationService _service;
        private readonly PersonTransationContext _context = new PersonTransationContext();

        public TransationController(TransationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IResult> CreateTransation(TransationRequest req)
        {
            var transation = new TransationModel(req.Description, req.Status, req.Value, req.Date, req.PersonId);
            return await _service.Create(transation, _context);
        }

        [HttpGet("/{personId:guid}/{page:int}/{limit:int}")]
        public async Task<IResult> GetTransation(Guid personId, int page, int limit)
        {
            return await _service.Get(_context, personId, page, limit);
        }

        [HttpGet("/{personId:guid}/{descripition}/{value:int}")]
        public async Task<IResult> GetTransationId(string description, string status, int value, DateTime date, Guid personId)
        {

            var transation = new TransationModel(description, status, value, date, personId);

            return await _service.GetId(transation, _context);
        }

        [HttpPatch("/{transationId:guid}")]
        public async Task<IResult> PatchTransation(Guid transationId, TransationRequest oldRequest)
        {
            var patchedTransation = new TransationModel(oldRequest.Description, oldRequest.Status, oldRequest.Value, oldRequest.Date);


            return await _service.Patch(patchedTransation, _context, transationId);
        }

        [HttpDelete("/{transationId:guid}")]
        public async Task<IResult> deleteTransation(Guid transationId)
        {
            return await _service.Delete(_context, transationId);
        }
    }
}
