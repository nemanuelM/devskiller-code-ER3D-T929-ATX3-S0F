using System;
using System.Collections.Generic;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;

namespace Api.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostDataService _postDataService;

        public PostController(ILogger<PostController> logger, IPostDataService postDataService)
        {
            _logger = logger;
            _postDataService = postDataService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAll()
        {
            try { 
               var post = _postDataService.GetAll();
                return Ok(post);
            }
            catch (Exception ex)
            {
                var error =
                    $"Error - {System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Message:{ex.Message} - {ex.StackTrace}";
                _logger?.LogError(error);

                return BadRequest();
            }
        }

        [HttpGet("{id:guid}")]
        public ActionResult<Post> Get([FromRoute] Guid id)
        {
            try
            { 
                var post = _postDataService.Get(id);
                return Ok(post);
            }
            catch (Exception ex)
            {
                var error =
                    $"Error - {System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Message:{ex.Message} - {ex.StackTrace}";
                _logger?.LogError(error);

                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<Post> Post([FromBody] Post post)
        {
            try { 
                var p = _postDataService.Create(post);
                return Ok(p);
            }
            catch (Exception ex)
            {
                var error =
                    $"Error - {System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Message:{ex.Message} - {ex.StackTrace}";
                _logger?.LogError(error);

                return BadRequest();
            }
        }

        [HttpPut("{id:guid}")]
        public ActionResult<Post> Put([FromRoute] Guid id, [FromBody] Post post)
        {
            try
            {
                var p = new Post
                {
                    Id = id,
                    Content = post.Content,
                    Title = post.Title,
                    CreationDate = post.CreationDate
                };
                var res = _postDataService.Update(p);

                return Ok(res);            
            }
            catch (Exception ex)
            {
                var error =
                    $"Error - {System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Message:{ex.Message} - {ex.StackTrace}";
                _logger?.LogError(error);

                return BadRequest();
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try { 
                var res = _postDataService.Delete(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var error =
                    $"Error - {System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Message:{ex.Message} - {ex.StackTrace}";
                _logger?.LogError(error);

                return BadRequest();
            }
        }

        [HttpGet("{id:guid}/comments")]
        public ActionResult<IEnumerable<Comment>> GetComments([FromRoute] Guid id)
        {
            try
            { 
                return _postDataService.GetByPostId(id);
            }
            catch (Exception ex)
            {
                var error =
                    $"Error - {System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Message:{ex.Message} - {ex.StackTrace}";
                _logger?.LogError(error);

                return BadRequest();
            }
        }
    }
}