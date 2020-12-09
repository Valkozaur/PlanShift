namespace PlanShift.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using PlanShift.Data.Models;
    using PlanShift.Services.Data.EmployeeGroupServices;
    using PlanShift.Services.Data.InvitationVerificationServices;
    using PlanShift.Web.ViewModels.InviteEmployeeValidation;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<PlanShiftUser> signInManager;
        private readonly UserManager<PlanShiftUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IInviteEmployeeVerificationsService inviteEmployeeVerificationsService;
        private readonly IEmployeeGroupService employeeGroupService;

        public RegisterModel(
            UserManager<PlanShiftUser> userManager,
            SignInManager<PlanShiftUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IInviteEmployeeVerificationsService inviteEmployeeVerificationsService,
            IEmployeeGroupService employeeGroupService)
        {
            this.Input = new InputModel();

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.inviteEmployeeVerificationsService = inviteEmployeeVerificationsService;
            this.employeeGroupService = employeeGroupService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [RegularExpression("^[A-Z]{1}[a-z,.'-]+$")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [RegularExpression("^[A-Z]{1}[a-z,.'-]+$")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string ValidationId { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null, string validationId = null)
        {
            if (validationId != null)
            {
                var validationInfo = await this.inviteEmployeeVerificationsService.GetVerificationAsync<InviteEmployeeVerificationEmailViewModel>(validationId);

                this.Input.ValidationId = validationId;
                this.Input.Email = validationInfo.Email;
            }

            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var userFullName = string.Join(' ', this.Input.FirstName, this.Input.LastName);

                var user = new PlanShiftUser { UserName = this.Input.Email, Email = this.Input.Email, FirstName = this.Input.FirstName, LastName = this.Input.LastName};
                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User created a new account with password.");

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(this.Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (!string.IsNullOrWhiteSpace(this.Input.ValidationId))
                    {
                        var validationDetails = await this.inviteEmployeeVerificationsService.GetVerificationAsync<InviteEmployeeVerificationInfoViewModel>(this.Input.ValidationId);

                        this.logger.LogInformation($"User {user.Email} added to group with groupId = {validationDetails.GroupId}");
                        await this.employeeGroupService.AddEmployeeToGroupAsync(user.Id, validationDetails.GroupId, validationDetails.Salary, validationDetails.Position);
                    }

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
