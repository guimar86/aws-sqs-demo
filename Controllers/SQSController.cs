using Microsoft.AspNetCore.Mvc;
using Services;
using Hellang;
using Microsoft.AspNetCore.Http;
using AWS.SQS.Models;

namespace AWS.SQS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SQSController : ControllerBase
    {
        ISQS _service;
        public SQSController(ISQS service)
        {
            _service = service;
        }
        [HttpPost]
        public ActionResult CreateQueue([FromBody] SQSQueueRequest request)
        {
            try
            {
                var queue = _service.CreateSQSQueue(request.Name);
                return Ok(queue);
            }
            catch (System.Exception ex)
            {
                ProblemDetails problem = new ProblemDetails
                {


                    Title = "Error in Creating SQS Queue",
                    Type = ex.GetType().Name,
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest

                };

                return BadRequest(problem);
            }
        }

        [HttpGet]
        public ActionResult ListQueues()
        {
            try
            {

                var retval = _service.ListSQSQueues();
                return Ok(retval);
            }
            catch (System.Exception ex)
            {
                ProblemDetails problem = new ProblemDetails
                {


                    Title = "Error in Creating SQS Queue",
                    Type = ex.GetType().Name,
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest

                };

                return BadRequest(problem);
            }
        }

        [HttpPost("message")]
        public ActionResult SendMessage([FromBody] SQSSendMessageRequest request)
        {

            try
            {
                _service.SendMessage(request.Url, request.Message);
                return Ok();
            }
            catch (System.Exception ex)
            {
                ProblemDetails problem = new ProblemDetails
                {


                    Title = "Error in Creating SQS Queue",
                    Type = ex.GetType().Name,
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest

                };

                return BadRequest(problem);
            }
        }

    }
}