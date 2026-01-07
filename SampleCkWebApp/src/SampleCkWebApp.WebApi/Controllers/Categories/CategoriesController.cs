using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SampleCkWebApp.WebApi.Controllers;
using SampleCkWebApp.Application.Category.Interfaces.Application;
using Contracts.DTOs.Category;
using Microsoft.AspNetCore.Authorization;

namespace SampleCkWebApp.WebApi.Controllers.Categories;

/// <summary>
/// Controller for managing categories in the expense tracker system
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriesController : ApiControllerBase
{
    private readonly ICategoryService _categoryService;
    
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    /// <summary>
    /// Retrieves all categories from the system
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all categories</returns>
    /// <response code="200">Successfully retrieved categories</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetAllCategoriesAsync(cancellationToken);
        
        return result.Match(
            categories => Ok(categories.ToList()),  //  Convert to response
            Problem);  
    }
    
    /// <summary>
    /// Retrieves a specific category by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the category</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The category information</returns>
    /// <response code="200">Category found and returned</response>
    /// <response code="404">Category not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategoryById(
        [FromRoute, Required] int id, 
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetCategoryByIdAsync(id, cancellationToken);
        
        return result.Match(
            category => Ok(category),
            Problem);
    }
    
    /// <summary>
    /// Creates a new category in the system
    /// </summary>
    /// <param name="request">Category creation request containing name and description</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The newly created category</returns>
    /// <response code="201">Category successfully created</response>
    /// <response code="400">Validation error (invalid name or description)</response>
    /// <response code="409">Category with this name already exists</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCategory(
        [FromBody, Required] CreateCategoryDto request, 
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.CreateCategoryAsync(request, cancellationToken);
        
        return result.Match(
            category => CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category),  // ✅ Return created category
            Problem);
    }

    /// <summary>
    /// Updates an existing category
    /// </summary>
    /// <param name="id">The unique identifier of the category</param>
    /// <param name="request">Category update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated category</returns>
    /// <response code="200">Category successfully updated</response>
    /// <response code="400">Validation error</response>
    /// <response code="404">Category not found</response>
    /// <response code="409">Category name already exists</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCategory(
        [FromRoute, Required] int id,
        [FromBody, Required] UpdateCategoryDto request,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateCategoryAsync(id, request, cancellationToken);
        
        return result.Match(
            category => Ok(category),
            Problem);
    }

    /// <summary>
    /// Deletes a category from the system
    /// </summary>
    /// <param name="id">The unique identifier of the category</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>No content on success</returns>
    /// <response code="204">Category successfully deleted</response>
    /// <response code="404">Category not found</response>
    /// <response code="500">Internal server error</response>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCategory(
        [FromRoute, Required] int id,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteCategoryAsync(id, cancellationToken);
        
        return result.Match(
            _ => NoContent(),  //  Returns 204 No Content on success
            Problem);
    }
}