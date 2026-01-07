using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SampleCkWebApp.WebApi.Controllers;
using SampleCkWebApp.Application.Transaction.Interfaces.Application;
using Contracts.DTOs.Transaction;
using Microsoft.AspNetCore.Authorization;

namespace SampleCkWebApp.WebApi.Controllers.Transactions;

/// <summary>
/// Controller for managing transactions in the expense tracker system
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TransactionsController : ApiControllerBase
{
    private readonly ITransactionService _transactionService;
    
    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    /// <summary>
    /// Retrieves all transactions from the system
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all transactions</returns>
    /// <response code="200">Successfully retrieved transactions</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTransactions(CancellationToken cancellationToken)
    {
        var result = await _transactionService.GetAllTransactionsAsync(cancellationToken);
        
        return result.Match(
            transactions => Ok(transactions.ToList()),  //  Convert to response
            Problem);  
    }
    
    /// <summary>
    /// Retrieves a specific transaction by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the transaction</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The transaction information</returns>
    /// <response code="200">Transaction found and returned</response>
    /// <response code="404">Transaction not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTransactionById(
        [FromRoute, Required] int id, 
        CancellationToken cancellationToken)
    {
        var result = await _transactionService.GetTransactionByIdAsync(id, cancellationToken);
        
        return result.Match(
            transaction => Ok(transaction),
            Problem);
    }
    
    /// <summary>
    /// Creates a new transaction in the system
    /// </summary>
    /// <param name="request">Transaction creation request containing amount, category, payment method, and other details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The newly created transaction</returns>
    /// <response code="201">Transaction successfully created</response>
    /// <response code="400">Validation error (invalid amount, category, payment method, or other fields)</response>
    /// <response code="404">User, category, or payment method not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTransaction(
        [FromBody, Required] CreateTransactionDto request, 
        CancellationToken cancellationToken)
    {
        var result = await _transactionService.CreateTransactionAsync(request, cancellationToken);
        
        return result.Match(
            transaction => CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction),  //  Return created transaction
            Problem);
    }

    /// <summary>
    /// Updates an existing transaction
    /// </summary>
    /// <param name="id">The unique identifier of the transaction</param>
    /// <param name="request">Transaction update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated transaction</returns>
    /// <response code="200">Transaction successfully updated</response>
    /// <response code="400">Validation error</response>
    /// <response code="404">Transaction not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateTransaction(
        [FromRoute, Required] int id,
        [FromBody, Required] UpdateTransactionDto request,
        CancellationToken cancellationToken)
    {
        var result = await _transactionService.UpdateTransactionAsync(id, request, cancellationToken);
        
        return result.Match(
            transaction => Ok(transaction),
            Problem);
    }

    /// <summary>
    /// Deletes a transaction from the system
    /// </summary>
    /// <param name="id">The unique identifier of the transaction</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>No content on success</returns>
    /// <response code="204">Transaction successfully deleted</response>
    /// <response code="404">Transaction not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTransaction(
        [FromRoute, Required] int id,
        CancellationToken cancellationToken)
    {
        var result = await _transactionService.DeleteTransactionAsync(id, cancellationToken);
        
        return result.Match(
            _ => NoContent(),  //  Returns 204 No Content on success
            Problem);
    }
}