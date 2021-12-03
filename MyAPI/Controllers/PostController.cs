using AutoMapper;
using AutoMapper.QueryableExtensions;
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

            #region old Code
            //var posts = await _repository.TableNoTracking.Include(p => p.Category).Include(p => p.Author)
            //    .ToListAsync(cancellationToken);

            //var list = posts.Select(p =>
            //{
            //    var dto = Mapper.Map<PostDto>(posts);
            //    return dto;
            //}).ToList();



            //var list = await _repository.TableNoTracking.Select(p => new PostDto
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    Description = p.Description,
            //    Categoryid = p.Categoryid,
            //    AuthorId = p.AuthorId,
            //    AuthorFullName = p.Author.FullName,
            //    CategoryName = p.Category.Name

            //}).ToListAsync(cancellationToken);
            #endregion

            var list = await _repository.TableNoTracking.ProjectTo<PostDto>().ToListAsync(cancellationToken);

            return Ok(list);
        }

        // GET api/<PostController>/5
        [HttpGet("{id:guid}")]
        public async Task<ApiResult<PostDto>> Get(Guid id, CancellationToken cancellationToken)
        {
            //var model = await _repository.GetByIdAsync(cancellationToken, id);
            var dto = await _repository.TableNoTracking.ProjectTo<PostDto>().SingleOrDefaultAsync(p => p.Id == id);
            if (dto == null)
            {
                return NotFound();
            }
            #region old Code
            //var dto = new PostDto
            //{
            //    Id = model.Id,
            //    Title = model.Title,
            //    Description = model.Description,
            //    Categoryid = model.Categoryid,
            //    AuthorId = model.AuthorId,
            //    AuthorFullName = model.Author.FullName,
            //    CategoryName = model.Category.Name
            //};
            #endregion

            return dto;
        }
        [Route("Create")]
        [HttpPost]
        public async Task<ApiResult<PostDto>> Create(PostDto postDto, CancellationToken cancellationToken)
        {

            var model = Mapper.Map<Post>(postDto);

            #region old Code
            //var model = new Post
            //{
            //    Title = postDto.Title,
            //    Description = postDto.Description,
            //    Categoryid = postDto.Categoryid,
            //    AuthorId = postDto.AuthorId
            //};
            #endregion

            await _repository.AddAsync(model, cancellationToken);

            #region old Code
            //Becuse Lazy Lood in EF Core Disable , it does not work

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


            //var ResultDto = await _repository.TableNoTracking.Select(p => new PostDto
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    Description = p.Description,
            //    Categoryid = p.Categoryid,
            //    AuthorId = p.AuthorId,
            //    AuthorFullName = p.Author.FullName,
            //    CategoryName = p.Category.Name

            //}).SingleOrDefaultAsync(p => p.Id == model.Id, cancellationToken);

            #endregion

            var ResultDto = await _repository.TableNoTracking.ProjectTo<PostDto>().SingleOrDefaultAsync(p => p.Id == model.Id, cancellationToken);

            return ResultDto;
        }
        [Route("Update")]
        [HttpPut]
        public async Task<ApiResult<PostDto>> Update(Guid id, PostDto dto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            dto.Id = model.Id;
            Mapper.Map(dto, model);
           
            #region oldCode
            //model.Title = dto.Title;
            //model.Description = dto.Description;
            //model.Categoryid = dto.Categoryid;
            //model.AuthorId = dto.AuthorId;
            #endregion

            await _repository.UpdateAsync(model, cancellationToken);

            #region old code
            //var ResultDto = await _repository.TableNoTracking.Select(p => new PostDto
            //{
            //    Id = p.Id,
            //    Title = p.Title,
            //    Description = p.Description,
            //    Categoryid = p.Categoryid,
            //    AuthorId = p.AuthorId,
            //    AuthorFullName = p.Author.FullName,
            //    CategoryName = p.Category.Name

            //}).SingleOrDefaultAsync(p => p.Id == model.Id, cancellationToken);
            #endregion

            var ResultDto = await _repository.TableNoTracking.ProjectTo<PostDto>().SingleOrDefaultAsync(p => p.Id == model.Id, cancellationToken);

            return ResultDto;
        }

        [HttpDelete("{id:guid}")]
        public async Task<ApiResult> Delete(int id, PostDto postDto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            await _repository.DeleteAsync(model, cancellationToken);

            return Ok();
        }

    }
}
