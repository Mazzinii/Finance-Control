using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Person.Data;
using Person.Models.Requests;
using PersonTransation.Models.Entities;
using PersonTransation.Services;

namespace PersonTransation.Controllers
{
    [Route("api/v1/transation")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _service;
        private readonly PersonTransactionContext _context;

        public TransactionController(TransactionService service, PersonTransactionContext context)
        {
            _service = service;
            _context = context;
        }

        [Authorize]
        [HttpPost]
        public async Task<IResult> CreateTransation(TransactionRequest req)
        {
            var transation = new TransactionModel(req.Description, req.Status, req.Value, req.Date, req.PersonId);
            return await _service.Create(transation, _context);
        }

        [Authorize]
        [HttpGet("{personId:guid}/{page:int}/{limit:int}")]
        public async Task<IResult> GetTransation(Guid personId, int page, int limit)
        {
            return await _service.Get(_context, personId, page, limit);
        }

        [HttpGet("{personId:guid}/{descripition}/{value:int}")]
        public async Task<IResult> GetTransationId(string description, string status, int value, DateTime date, Guid personId)
        {

            var transation = new TransactionModel(description, status, value, date, personId);

            return await _service.GetId(transation, _context);
        }

        [Authorize]
        [HttpPatch("{transationId:guid}")]
        public async Task<IResult> PatchTransation(Guid transationId, TransactionRequest oldRequest)
        {
            var patchedTransation = new TransactionModel(oldRequest.Description, oldRequest.Status, oldRequest.Value, oldRequest.Date);


            return await _service.Patch(patchedTransation, _context, transationId);
        }

        [Authorize]
        [HttpDelete("{transationId:guid}")]
        public async Task<IResult> deleteTransation(Guid transationId)
        {
            return await _service.Delete(_context, transationId);
        }
    }
}
