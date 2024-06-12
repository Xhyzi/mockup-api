using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using mockup_api.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mockup_api.Controllers
{

    [Route("api/v1")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpPost("auth")]
        public IActionResult GetToken([FromForm] AuthDto authRequest)
        {
            if (authRequest.ClientId == Constants.CLIENT_ID &&  authRequest.ClientSecret == Constants.CLIENT_SECRET)
            {
                var token = GenerateJwtToken();
                return Ok( new { access_token = token }); // Will this be a 204 NoContent, a 201 Created or a 200 Ok???
            }
            else
            {
                return Unauthorized(); // 401
            }
        }

        [Authorize]
        [HttpPut("send")]
        public IActionResult UpdateReadingsData([FromBody] PayloadDto payload)
        {
            if (payload == null)
            {
                return BadRequest(); // 400
            }

            // Prints payload
            Console.WriteLine($"Code: {payload.Code}");
            Console.WriteLine($"Timestamp: {payload.Timestamp}");
            foreach (var data in payload.Data)
            {
                Console.WriteLine($"Name: {data.Name}, Units: {data.Units}, Value: {data.Value}");
            }

            // returns response
            return Ok(new { message = "Telemetry data recieved successfully" });
        }

        private string GenerateJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.JWT_KEY));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "your_issuer",
                Audience = "your_audience",
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = credentials,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "client_id")
                })
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
