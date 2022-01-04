using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace MyAPI.Controllers.V2
{
    [ApiVersion("2")]
    [AllowAnonymous]
    public class PostController : V1.PostController
    {
        private readonly IRepository<Post> _repository;
        public PostController(IRepository<Post> repository) : base(repository)
        {
            this._repository = repository;
        }

        [NonAction]
        public override Task<ApiResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            return base.Delete(id, cancellationToken);
        }


        [HttpGet("GetAll")]
        public ActionResult<List<Post>> GetAll()
        {
            return _repository.TableNoTracking.ToList();
        }
    }
}
