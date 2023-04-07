#nullable enable
using Microsoft.AspNetCore.Mvc;
using PVP_Projektas_API.Interfaces;

namespace PVP_Projektas_API.Controllers;

[Route("[controller]")]
[ApiController]
public class ScannerController : ControllerBase
{
    private readonly IOpenFoodsClient _openFoodsClient;

    public ScannerController(IOpenFoodsClient openFoodsClient)
    {
        _openFoodsClient = openFoodsClient;
    }

    [HttpGet("barcode/{id}")]
    public async Task<ActionResult> GetProductInfo([FromRoute] string id)
    {
        var product = await _openFoodsClient.GetProductInfo(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }
}
