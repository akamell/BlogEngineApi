using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BlogEngineApi.Shared;

namespace BlogEngineApi.User.Infrastructure
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IPostCreateService _postCreateService;
        private readonly IPostGetService _postGetService;
        private readonly IPostUpdateService _postUpdateService;
        private readonly IPostDeleteService _postDeleteService;

        public PostController(
            IConfiguration config,
            IPostCreateService postCreateService,
            IPostGetService postGetService,
            IPostUpdateService postUpdateService,
            IPostDeleteService postDeleteService
        )
        {
            _config = config;
            _postCreateService = postCreateService;
            _postGetService = postGetService;
            _postUpdateService = postUpdateService;
            _postDeleteService = postDeleteService;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            int approvedStatus = 1;
            var result = this._postGetService.GetByStatus(approvedStatus);
            return Ok(result);
        }

        [HttpGet("pending-approve")]
        [AllowAnonymous]
        public IActionResult GetPendingApproval()
        {
            var result = this._postGetService.GetPendingApproval();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await this._postGetService.Get(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = Policies.Writer)]
        public async Task<IActionResult> Post([FromBody] PostRequest postRequest)
        {
            string author = Helper.getUserName(HttpContext);
            try
            {
                var response = await this._postCreateService.Create(postRequest, author);
                if (response <= 0)
                    return BadRequest("Error creating post");

                return StatusCode(201, "success");
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}/approve")]
        [Authorize(Policy = Policies.Editor)]
        public async Task<IActionResult> Approve(int id, ApprovePostRequest approveRequest)
        {
            try
            {
                var response = await this._postUpdateService.ApprovePost(id, approveRequest);
                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Policies.Writer)]
        public async Task<IActionResult> ChangeContent(int id, PostRequest postRequest)
        {
            try
            {
                var response = await this._postUpdateService.ChangeContent(id, postRequest);
                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.Editor)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await this._postDeleteService.Delete(id);
                return StatusCode(200, response);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}