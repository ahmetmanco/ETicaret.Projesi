using System.Net;
using _02_Application.Layer.Features.Commands.Product.CreateProduct;
using _02_Application.Layer.Features.Queries.Product.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _05_WebApi.Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
        {
            GetAllProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public Task<IActionResult> Get(int Id)
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response =await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
