using Microsoft.AspNetCore.Mvc;

namespace WebApp_EnviarCorreos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviarCorreoController(IEmailSender emailSender) : ControllerBase
    {
        private readonly IEmailSender _emailSender = emailSender;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmailDto modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Los campos no son correctos"
                });
            }
            _emailSender.SendEmail(modelo);
            return StatusCode(StatusCodes.Status200OK, new
            {
                message = $"Se ha enviado el correo a {modelo.Para}"
            });
        }
    }
}
