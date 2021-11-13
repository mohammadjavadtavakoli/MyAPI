using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.ActionFilter;
using WebFramework.Api;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyAPI.Controllers
{
    [AllowAnonymous]
    [ApiResultFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IRepository<Post> _repository;
        public PostController(IRepository<Post> repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> Get(CancellationToken cancellationToken)
        {
            var list = await _repository.TableNoTracking.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Categoryid = p.Categoryid,
                AuthorId = p.AuthorId,
                AuthorName = p.Author.FullName,
                CategoryName = p.Category.Name

            }).ToListAsync(cancellationToken);

            return Ok(list);
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public async Task<ApiResult<PostDto>> Get(int id, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            if (model == null)
            {
                return NotFound();
            }

            var dto = new PostDto
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Categoryid = model.Categoryid,
                AuthorId = model.AuthorId,
                AuthorName = model.Author.FullName,
                CategoryName = model.Category.Name
            };

            return dto;
        }
        [HttpPost]
        public async Task<ApiResult<PostDto>> Create(PostDto postDto, CancellationToken cancellationToken)
        {

            var model = new Post
            {
                Title = postDto.Title,
                Description = postDto.Description,
                Categoryid = postDto.Categoryid,
                AuthorId = postDto.AuthorId
            };

            await _repository.AddAsync(model, cancellationToken);

            //Becuse Lazy Lood in EF Core Disable
            //model = await _repository.GetByIdAsync(cancellationToken, model.Id);
            //var dto = new PostDto
            //{
            //    Id = model.Id,
            //    Title = model.Title,
            //    Description = model.Description,
            //    Categoryid = model.Categoryid,
            //    AuthorId = model.AuthorId,
            //    AuthorName = model.Author.FullName,
            //    CategoryName = model.Category.Name
            //};

            var ResultDto = await _repository.TableNoTracking.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Categoryid = p.Categoryid,
                AuthorId = p.AuthorId,
                AuthorName = p.Author.FullName,
                CategoryName = p.Category.Name

            }).SingleOrDefaultAsync(p => p.Id == model.Id, cancellationToken);

            return ResultDto;
        }
        [HttpPut]
        public async Task<ApiResult<PostDto>> Update(int id, PostDto dto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            model.Title = dto.Title;
            model.Description = dto.Description;
            model.Categoryid = dto.Categoryid;
            model.AuthorId = dto.AuthorId;

            await _repository.UpdateAsync(model, cancellationToken);

            var ResultDto = await _repository.TableNoTracking.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Categoryid = p.Categoryid,
                AuthorId = p.AuthorId,
                AuthorName = p.Author.FullName,
                CategoryName = p.Category.Name

            }).SingleOrDefaultAsync(p => p.Id == model.Id, cancellationToken);

            return ResultDto;
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(int id , PostDto postDto , CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            await _repository.DeleteAsync(model, cancellationToken);

            return Ok();
        }

        // POST api/<PostController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
