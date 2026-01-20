using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SampleCkWebApp.Application.PaymentMethods.Interfaces.Application;
using SampleCkWebApp.WebApi.Controllers;
using Contracts.DTOs.PaymentMethod;

namespace SampleCkWebApp.WebApi.Controllers.PaymentMethods;

/// <summary>
/// Controller for managing payment methods in the expense tracker system
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PaymentMethodsController : ApiControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService;
    
    public PaymentMethodsController(IPaymentMethodService paymentMethodService)
    {
        _paymentMethodService = paymentMethodService;
    }
    
    /// <summary>
    /// Retrieves all payment methods from the system
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all payment methods</returns>
    /// <response code="200">Successfully retrieved payment methods</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PaymentMethodDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaymentMethods(CancellationToken cancellationToken)
    {
        var result = await _paymentMethodService.GetAllPaymentMethodsAsync(cancellationToken);
        
        return result.Match(
            paymentMethods => Ok(paymentMethods.ToList()),  //  Convert to response
            Problem);  
    }
    
    /// <summary>
    /// Retrieves a specific payment method by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the payment method</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The payment method information</returns>
    /// <response code="200">Payment method found and returned</response>
    /// <response code="404">Payment method not found</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PaymentMethodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaymentMethodById(
        [FromRoute, Required] int id, 
        CancellationToken cancellationToken)
    {
        var result = await _paymentMethodService.GetPaymentMethodByIdAsync(id, cancellationToken);
        
        return result.Match(
            paymentMethod => Ok(paymentMethod),
            Problem);
    }
    
    /// <summary>
    /// Creates a new payment method in the system
    /// </summary>
    /// <param name="request">Payment method creation request containing name and other details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The newly created payment method</returns>
    /// <response code="201">Payment method successfully created</response>
    /// <response code="400">Validation error (invalid name or other fields)</response>
    /// <response code="409">Payment method with this name already exists</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [ProducesResponseType(typeof(PaymentMethodDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePaymentMethod(
        [FromBody, Required] CreatePaymentMethodDto request, 
        CancellationToken cancellationToken)
    {
        var result = await _paymentMethodService.CreatePaymentMethodAsync(request, cancellationToken);
        
        return result.Match(
            paymentMethod => CreatedAtAction(nameof(GetPaymentMethodById), new { id = paymentMethod.Id }, paymentMethod),  //  Return created payment method
            Problem);
    }

    /// <summary>
    /// Updates an existing payment method
    /// </summary>
    /// <param name="id">The unique identifier of the payment method</param>
    /// <param name="request">Payment method update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated payment method</returns>
    /// <response code="200">Payment method successfully updated</response>
    /// <response code="400">Validation error</response>
    /// <response code="404">Payment method not found</response>
    /// <response code="409">Payment method name already exists</response>
    /// <response code="500">Internal server error</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PaymentMethodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdatePaymentMethod(
        [FromRoute, Required] int id,
        [FromBody, Required] UpdatePaymentMethodDto request,
        CancellationToken cancellationToken)
    {
        var result = await _paymentMethodService.UpdatePaymentMethodAsync(id, request, cancellationToken);
        
        return result.Match(
            paymentMethod => Ok(paymentMethod),
            Problem);
    }

    /// <summary>
    /// Deletes a payment method from the system
    /// </summary>
    /// <param name="id">The unique identifier of the payment method</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>No content on success</returns>
    /// <response code="204">Payment method successfully deleted</response>
    /// <response code="404">Payment method not found</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletePaymentMethod(
        [FromRoute, Required] int id,
        CancellationToken cancellationToken)
    {
        var result = await _paymentMethodService.DeletePaymentMethodAsync(id, cancellationToken);
        
        return result.Match(
            _ => NoContent(),  //  Returns 204 No Content on success
            Problem);
    }
}