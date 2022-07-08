using System;
using System.Collections.Generic;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;

namespace Api.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentDataService  _commentDataService;

        public CommentController(ILogger<CommentController> logger, ICommentDataService commentDataService)
        {
            _logger = logger;
            _commentDataService = commentDataService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Comment>> GetAll()
        {
            try
            {
                var comment = _commentDataService.GetAll();
                return Ok(comment);
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
        public ActionResult<Comment> Get([FromRoute] Guid id)
        {
            try
            { 
                var comment = _commentDataService.Get(id);
                return Ok(comment);
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
        public ActionResult<Comment> Post([FromBody] Comment comment)
        {
            try { 
                var p = _commentDataService.Create(comment);
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
        public IActionResult Put([FromRoute] Guid id, [FromBody] Comment comment)
        {
            try { 

                var p = new Comment
                {
                    Id = id,
                    Content = comment?.Content,
                    Author = comment?.Author,
                    PostId = comment.PostId,
                    CreationDate = comment.CreationDate

                };
                var res = _commentDataService.Update(p);

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
            try
            { 
                var res = _commentDataService.Delete(id);
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
    }
}