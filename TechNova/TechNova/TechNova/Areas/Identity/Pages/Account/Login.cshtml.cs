// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace TechNova.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }
            
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            Console.WriteLine($"[Login] OnGetAsync invoked. ErrorMessage present: {!string.IsNullOrEmpty(ErrorMessage)}");

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            Console.WriteLine($"[Login] External login schemes count: {ExternalLogins?.Count ?? 0}");

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            Console.WriteLine($"[Login] OnPostAsync invoked.");
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Console.WriteLine($"[Login] External login schemes count: {ExternalLogins?.Count ?? 0}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("[Login] ModelState is NOT valid.");
                foreach (var kv in ModelState)
                {
                    if (kv.Value.Errors.Count > 0)
                    {
                        Console.WriteLine($"[Login] ModelState error for '{kv.Key}': {string.Join(" | ", kv.Value.Errors.Select(e => e.ErrorMessage))}");
                    }
                }
                return Page();
            }

            Console.WriteLine($"[Login] Attempting sign-in for Email: '{Input?.Email}' RememberMe: {Input?.RememberMe}");
            try
            {
                // Antes de intentar elsignin, compruebo si el usuario existe
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    Console.WriteLine("[Login] No user found with that email.");
                }
                else
                {
                    var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                    Console.WriteLine($"[Login] User found. Id: {user.Id}, EmailConfirmed: {emailConfirmed}, LockoutEnabled: {user.LockoutEnabled}, AccessFailedCount: {user.AccessFailedCount}");
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                Console.WriteLine($"[Login] SignIn result - Succeeded: {result.Succeeded}, RequiresTwoFactor: {result.RequiresTwoFactor}, IsLockedOut: {result.IsLockedOut}, IsNotAllowed: {result.IsNotAllowed}");

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    Console.WriteLine("[Login] Result.Succeeded => redirecting.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    Console.WriteLine("[Login] Result.RequiresTwoFactor => redirect to 2fa page.");
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    Console.WriteLine("[Login] Result.IsLockedOut => redirect to Lockout page.");
                    return RedirectToPage("./Lockout");
                }
                if (result.IsNotAllowed)
                {
                    // IsNotAllowed suele indicar que no está permitido iniciar sesión (p. e
                    // g. email no confirmado)
                    Console.WriteLine("[Login] Result.IsNotAllowed => user not allowed to sign in (e.g. email not confirmed).");
                }
                else
                {
                    Console.WriteLine("[Login] Invalid login attempt (wrong password or user); adding ModelState error.");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Login] Exception during sign-in attempt: {ex}");
                throw;
            }
        }
    }
}
