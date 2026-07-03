using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace azure_webapp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;
        this._configuration = configuration;
    }

    [BindProperty]
    public ContactForm Contact { get; set; } = new();

    public bool SubmittedSuccessfully { get; set; }

    public void OnGet()
    {
        ViewData["Greetings"] = _configuration["Greetings"];
    }

    public IActionResult OnPost()
    {
        ViewData["Greetings"] = _configuration["Greetings"];

        if (!ModelState.IsValid)
        {
            return Page();
        }

        _logger.LogInformation("Contact form submitted by {Name} ({Email})", Contact.Name, Contact.Email);
        SubmittedSuccessfully = true;
        Contact = new ContactForm();

        return Page();
    }

    public class ContactForm
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; } = string.Empty;
    }
}
