using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BistroDriveWebApp.Models
{
    public class ProfileSettingsViewModel
    {
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        
        public string Avatar_Url { get; set; }

        [Required]
        [StringLength(120, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(120, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [StringLength(24, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Телефон")]
        public string Telphone { get; set; }

        [Required]
        [Display(Name = "Город")]
        public int City { get; set; }

        [StringLength(64, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Выезжаю на дом")]
        public string WithTravel { get; set; }

        [StringLength(16384, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Обо мне")]
        public string Description { get; set; }
        public IEnumerable<city> City_List { set; get; }
    }
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [System.Web.Mvc.Remote("IsUserNameAvailable","Account",ErrorMessage ="Данное имя пользователя уже используеться")]
        [RegularExpression(@"^([A-Za-z0-9_]+)$", ErrorMessage = "Имя пользователя может содержать только символы, цифры и знаки подчёркивания.")]
        [StringLength(120, ErrorMessage = "{0} должно иметь не менее {2} символов", MinimumLength = 4)]
        [Display(Name = "Имя пользователя:")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [System.Web.Mvc.Remote("IsCorrectEmail", "Account", ErrorMessage = "Неизвестный Email Сервер")]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль:")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(120, ErrorMessage = "{0} должно содержать не менее {2} символов", MinimumLength = 4)]
        [Display(Name = "Имя:")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(120, ErrorMessage = "{0} должна быть {2} символов", MinimumLength = 4)]
        [Display(Name = "Фамилия:")]
        public string Surname { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} должен содержать не менее {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }
    }
}
