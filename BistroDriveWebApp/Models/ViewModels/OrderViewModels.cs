using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class OrderViewModel
    {
        [Required]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Имя")]
        public string FirstName { set; get; }
        
        [Required]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Фамиля")]
        public string Surname { set; get; }
        
        [Required]
        [StringLength(120, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [Display(Name = "Телефон")]
        public string Phone { set; get; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { set; get; }
        
        [Display(Name = "Коментарий к заказу")]
        public string Comment { set; get; }

        [Required]
        [Display(Name = "Город")]
        public int City { set; get; }

        [Required]
        [Display(Name = "Адрес")]
        public string Address { set; get; }

        [Required]
        [Display(Name = "На когда")]
        public DateTime DeadLine { set; get; }

        [Required]
        [Display(Name = "Тип оплаты")]
        public int paymentMethod { set; get; }

        [Required]
        [Display(Name = "Предпочитаемый вид связи")]
        public int contactMethod { set; get; }


        [Required]
        [Display(Name = "Кто закупает ингридиенты")]
        public int ingridientBuyer { set; get; }

        [Required]
        [Display(Name = "Доставка")]
        public int delivery { set; get; }


        public string idCook { set; get; }
        public string CookName { set; get; }
        public string CookSurname { set; get; }
        public string CookPhone { set; get; }
        public string CookEmail { set; get; }

        public int idDish { set; get; }
        public string DishName { set; get; }
        public string DishDescription { set; get; }
        public decimal DishPrice { set; get; }
        public decimal DishPriceWithIngridient { set; get; }
        public string ImageUrl { set; get; }

        public IEnumerable<city> City_List { set; get; }

    }
}