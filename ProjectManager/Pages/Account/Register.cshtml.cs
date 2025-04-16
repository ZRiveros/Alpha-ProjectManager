using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Pages.Account;
// Summary of the register code-behin
// This class handles the registration functionality for the application.
public class RegisterModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    // The constructor takes a UserManager and SignInManager as parameters to manage user registration and sign-in operations.
    public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    // This property binds the input model to the page.
    [BindProperty]
    public InputModel Input { get; set; }
    // This property holds the return URL after a successful registration.
    public class InputModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
    // This property holds the return URL after a successful registration.
    public async Task<IActionResult> OnPostAsync()
    {
        // This method handles the POST request for the registration page.
        if (!ModelState.IsValid)
        {
            return Page();
        }
        // This method checks if the user is already logged in.
        var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
        var result = await _userManager.CreateAsync(user, Input.Password);
        // This method attempts to create a new user with the provided email and password.
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToPage("/Portal/Index");
        }
        // This method checks if the user is already registered.
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return Page();
    }
}