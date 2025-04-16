using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Pages.Account;
//Summary of the login code-behind
public class LoginModel : PageModel
{
    // This class handles the login functionality for the application.
    private readonly SignInManager<IdentityUser> _signInManager;
    // The constructor takes a SignInManager as a parameter to manage user sign-in operations.
    public LoginModel(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }
    // This property binds the input model to the page.
    [BindProperty]
    public InputModel Input { get; set; }
    // This property holds the return URL after a successful login.
    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    // This property holds the return URL after a successful login.
    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        // This method handles the POST request for the login page.
        returnUrl ??= Url.Content("~/Portal");

        if (!ModelState.IsValid)
        {
            return Page();
        }
        // This method checks if the user is already logged in.
        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
        // This method attempts to sign in the user with the provided email and password.
        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl);
        }
        // This method checks if the user is locked out.
        if (result.IsLockedOut)
        {
            return RedirectToPage("/Lockout");
        }
        // This method checks if the user is not found or the password is incorrect.
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }
}
