using System.Threading.Tasks;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        // Validate the user data
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Create the user
        var user = new User();
        user.Username = userDto.Username;
        user.Email = userDto.Email;
        user.Password = userDto.Password;

        // Save the user
        await _userManager.CreateAsync(user);

        // Return the success message
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        // Validate the login data
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Get the user
        var user = await _userManager.FindByUsernameAsync(loginDto.Username);

        // Check if the password is correct
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return Unauthorized();
        }

        // Generate a JWT token
        var token = await _tokenProvider.GenerateTokenAsync(user);

        // Return the JWT token
        return Ok(new { token });
    }
}