using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SampleCkWebApp.WebApi.Controllers;
using SampleCkWebApp.Application.Transaction.Interfaces.Application;
using Contracts.DTOs.Transaction;
using Microsoft.AspNetCore.Authorization;
using SampleCkWebApp.Contracts.DTOs.Common;

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
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<TransactionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedResponse<TransactionDto>>> GetTransactionsPaginated(
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _transactionService.GetPaginatedTransactionsAsync(page, limit, cancellationToken);
        return result.Match(
            transactions => Ok(transactions),
            errors => Problem(detail: errors.First().Description));
    }

    /// <summary>
    /// Retrieves paginated transactions for a specific user
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="limit">Items per page (default: 10, max: 100)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated list of user transactions</returns>
    /// <response code="200">Successfully retrieved user transactions</response>
    /// <response code="400">Invalid pagination parameters</response>
    /// <response code="404">User not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(PaginatedResponse<TransactionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PaginatedResponse<TransactionDto>>> GetUserTransactionsPaginated(
        [FromRoute, Required] int userId,
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await _transactionService.GetUserTransactionsPaginatedAsync(userId, page, limit, cancellationToken);
        return result.Match(
            transactions => Ok(transactions),
            errors => Problem(detail: errors.First().Description));
    }

    /// <summary>
    /// Retrieves all transactions for a specific user without pagination
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>All user transactions ordered by date</returns>
    /// <response code="200">Successfully retrieved all user transactions</response>
    /// <response code="404">User not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpGet("user/{userId}/all")]
    [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllUserTransactions(
        [FromRoute, Required] int userId,
        CancellationToken cancellationToken = default)
    {
        var result = await _transactionService.GetAllUserTransactionsAsync(userId, cancellationToken);
        
        return result.Match(
            transactions => Ok(transactions),
            errors => Problem(detail: errors.First().Description));
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
    /// Retrieves the total monthly income for a specific user
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="month">The month (1-12)</param>
    /// <param name="year">The year</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The total income for the specified month</returns>
    /// <response code="200">Successfully retrieved monthly income</response>
    /// <response code="400">Invalid month or year</response>
    /// <response code="404">User not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpGet("user/{userId}/income/monthly")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserMonthlyIncome(
        [FromRoute, Required] int userId,
        [FromQuery, Required] int month,
        [FromQuery, Required] int year,
        CancellationToken cancellationToken = default)
    {
        //  Validate month
        if (month < 1 || month > 12)
            return BadRequest(new { error = "Month must be between 1 and 12" });

        //  Validate year
        if (year < 1900 || year > DateTime.UtcNow.Year + 10)
            return BadRequest(new { error = "Year is invalid" });

        var result = await _transactionService.GetUserMonthlyIncomeAsync(userId, month, year, cancellationToken);

        return result.Match(
            income => Ok(new { monthlyIncome = income, month, year }),
            errors => Problem(detail: errors.First().Description));
    }

    /// <summary>
    /// Retrieves the total monthly expenses for a specific user
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="month">The month (1-12)</param>
    /// <param name="year">The year</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The total expenses for the specified month</returns>
    /// <response code="200">Successfully retrieved monthly expenses</response>
    /// <response code="400">Invalid month or year</response>
    /// <response code="404">User not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpGet("user/{userId}/expense/monthly")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserMonthlyExpense(
        [FromRoute, Required] int userId,
        [FromQuery, Required] int month,
        [FromQuery, Required] int year,
        CancellationToken cancellationToken = default)
    {
        //  Validate month
        if (month < 1 || month > 12)
            return BadRequest(new { error = "Month must be between 1 and 12" });

        //  Validate year
        if (year < 1900 || year > DateTime.UtcNow.Year + 10)
            return BadRequest(new { error = "Year is invalid" });

        var result = await _transactionService.GetUserMonthlyExpenseAsync(userId, month, year, cancellationToken);

        return result.Match(
            expense => Ok(new { monthlyExpense = expense, month, year }),
            errors => Problem(detail: errors.First().Description));
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