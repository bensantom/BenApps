using Microsoft.AspNetCore.Mvc;
using Reddit.Domain.Models;
using Reddit.Domain.Services.Abstractions;
using static Reddit.Domain.Util.Constant;
using System.Text.Json;

namespace Reddit.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        public HomeController(IServiceManager serviceManager, IConfiguration config, ILogger<HomeController> logger)
        {
            _serviceManager = serviceManager;
            _config = config;
            _logger = logger;
        }
        public ActionResult Index()
        {
            try
            {
                var clientId = _config.GetValue<string>("AppIdentitySettings:ClientId");
                var redirectUrl = _config.GetValue<string>("AppIdentitySettings:RedirectUrl");
                var authUrl = _config.GetValue<string>("AppIdentitySettings:AuthUrl");

                return Redirect($"{authUrl}?client_id={clientId}&response_type=code&state=reddit-app&redirect_uri={redirectUrl}&duration=temporary&scope=read");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to sign in");
                return View("Error", ex.Message);
            }
        }
        public ActionResult SignIn()
        {
            try
            {
                var clientId = _config.GetValue<string>("AppIdentitySettings:ClientId");
                var redirectUrl = _config.GetValue<string>("AppIdentitySettings:RedirectUrl");
                var authUrl = _config.GetValue<string>("AppIdentitySettings:AuthUrl");

                return Redirect($"{authUrl}?client_id={clientId}&response_type=code&state=reddit-app&redirect_uri={redirectUrl}&duration=temporary&scope=read");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to sign in");
                return View("Error", ex.Message);
            }
        }
        public async Task<ActionResult> Portal(string code, string state)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString(AuthKey)))
                {
                    var clientId = _config.GetValue<string>("AppIdentitySettings:ClientId");
                    var redirectUrl = _config.GetValue<string>("AppIdentitySettings:RedirectUrl");
                    var secretKey = _config.GetValue<string>("AppIdentitySettings:SecretKey");
                    var accessTokenUrl = _config.GetValue<string>("AppIdentitySettings:AccessTokenUrl");

                    var result = await _serviceManager.RedditService.RetrieveToken(code, redirectUrl, clientId, secretKey, accessTokenUrl);
                    var token = JsonSerializer.Deserialize<AuthToken>(result);

                    if (token == null || string.IsNullOrEmpty(token.Token))
                        throw new UnauthorizedAccessException("Authentication failed");

                    HttpContext.Session.SetString(AuthKey, token.Token);
                }

                return View();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "failed to get token");
                return View("Error", ex.Message);
            }
        }
        public async Task<IActionResult> Posts(string subReddits, int limit)
        {
            try
            {
                RedditPostModel? result = null;
                var token = HttpContext.Session.GetString("AuthToken");
                if (!string.IsNullOrEmpty(token))
                    result = await _serviceManager.RedditService.GetNewPosts(token, subReddits, limit);
                else
                    return Unauthorized();
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "failed to get token");
                return BadRequest(); 
            }
        }
        public ActionResult Error(string? message)
        {
            return View("Error", message);
        }
    }
}
