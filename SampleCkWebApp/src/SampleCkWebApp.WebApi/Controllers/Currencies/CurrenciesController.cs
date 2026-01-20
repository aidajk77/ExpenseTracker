using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SampleCkWebApp.Application.Currencies.Interfaces.Application;
using SampleCkWebApp.WebApi.Controllers;
using Contracts.DTOs.Currency;
using Microsoft.AspNetCore.Authorization;

namespace SampleCkWebApp.WebApi.Controllers.Currencies;

/// <summary>
/// Controller for managing currencies in the expense tracker system
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CurrenciesController : ApiControllerBase
{
    private readonly ICurrencyService _currencyService;
    
    public CurrenciesController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }
    
    /// <summary>
    /// Retrieves all currencies from the system
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all currencies</returns>
    /// <response code="200">Successfully retrieved currencies</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CurrencyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCurrencies(CancellationToken cancellationToken)
    {
        var result = await _currencyService.GetAllCurrenciesAsync(cancellationToken);
        
        return result.Match(
            currencies => Ok(currencies.ToList()),  //  Convert to response
            Problem);  
    }
    
    /// <summary>
    /// Retrieves a specific currency by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the currency</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The currency information</returns>
    /// <response code="200">Currency found and returned</response>
    /// <response code="404">Currency not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CurrencyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCurrencyById(
        [FromRoute, Required] int id, 
        CancellationToken cancellationToken)
    {
        var result = await _currencyService.GetCurrencyByIdAsync(id, cancellationToken);
        
        return result.Match(
            currency => Ok(currency),
            Problem);
    }
    
    /// <summary>
    /// Creates a new currency in the system
    /// </summary>
    /// <param name="request">Currency creation request containing code, name, and symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The newly created currency</returns>
    /// <response code="201">Currency successfully created</response>
    /// <response code="400">Validation error (invalid code, name, or symbol)</response>
    /// <response code="409">Currency with this code already exists</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(CurrencyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCurrency(
        [FromBody, Required] CreateCurrencyDto request, 
        CancellationToken cancellationToken)
    {
        var result = await _currencyService.CreateCurrencyAsync(request, cancellationToken);
        
        return result.Match(
            currency => CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency),  //  Return created currency
            Problem);
    }

    /// <summary>
    /// Updates an existing currency
    /// </summary>
    /// <param name="id">The unique identifier of the currency</param>
    /// <param name="request">Currency update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated currency</returns>
    /// <response code="200">Currency successfully updated</response>
    /// <response code="400">Validation error</response>
    /// <response code="404">Currency not found</response>
    /// <response code="409">Currency code already exists</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CurrencyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCurrency(
        [FromRoute, Required] int id,
        [FromBody, Required] UpdateCurrencyDto request,
        CancellationToken cancellationToken)
    {
        var result = await _currencyService.UpdateCurrencyAsync(id, request, cancellationToken);
        
        return result.Match(
            currency => Ok(currency),
            Problem);
    }

    /// <summary>
    /// Deletes a currency from the system
    /// </summary>
    /// <param name="id">The unique identifier of the currency</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>No content on success</returns>
    /// <response code="204">Currency successfully deleted</response>
    /// <response code="404">Currency not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCurrency(
        [FromRoute, Required] int id,
        CancellationToken cancellationToken)
    {
        var result = await _currencyService.DeleteCurrencyAsync(id, cancellationToken);
        
        return result.Match(
            _ => NoContent(),  //  Returns 204 No Content on success
            Problem);
    }
}