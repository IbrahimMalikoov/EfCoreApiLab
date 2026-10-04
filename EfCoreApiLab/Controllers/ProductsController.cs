using AutoMapper;
using EfCoreApiLab.Models;
using EfCoreApiLab.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreApiLab.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductsController(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ReturnResult<IEnumerable<ProductDto>>>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        
        return Ok(new ReturnResult<IEnumerable<ProductDto>> { Result = dtos });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnResult<ProductDto>>> GetById(int id)
    {
        var product = await _repository.GetAsync(id);
        
        if (product == null)
        {
            return NotFound(new ReturnResult<ProductDto> 
            { 
                IsSuccess = false, 
                ErrorMessage = new List<string> { "Product not found" } 
            });
        }

        var dto = _mapper.Map<ProductDto>(product);
        return Ok(new ReturnResult<ProductDto> { Result = dto });
    }

    [HttpPost]
    public async Task<ActionResult<ReturnResult<ProductDto>>> Create(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        product.Category = "General"; 
        
        await _repository.CreateAsync(product);
        
        var createdDto = _mapper.Map<ProductDto>(product);
        return StatusCode(201, new ReturnResult<ProductDto> { Result = createdDto });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReturnResult<ProductDto>>> Update(int id, ProductDto productDto)
    {
        var existingProduct = await _repository.GetAsync(id);
        
        if (existingProduct == null)
        {
            return NotFound(new ReturnResult<ProductDto> 
            { 
                IsSuccess = false, 
                ErrorMessage = new List<string> { "Product not found" } 
            });
        }

        existingProduct.Name = productDto.Name;
        existingProduct.Price = productDto.Price;

        await _repository.UpdateAsync(existingProduct);

        var updatedDto = _mapper.Map<ProductDto>(existingProduct);
        return Ok(new ReturnResult<ProductDto> { Result = updatedDto });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ReturnResult<string>>> Delete(int id)
    {
        var existingProduct = await _repository.GetAsync(id);
        
        if (existingProduct == null)
        {
            return NotFound(new ReturnResult<string> 
            { 
                IsSuccess = false, 
                ErrorMessage = new List<string> { "Product not found" } 
            });
        }

        await _repository.DeleteAsync(id);
        return Ok(new ReturnResult<string> { Result = "Deleted successfully" });
    }
}