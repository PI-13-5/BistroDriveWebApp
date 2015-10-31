using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class OrderViewModel
    {
        [Display(Name = "Имя")]
        public string FirstName { set; get; }

        [Display(Name = "Фамиля")]
        public string Surname { set; get; }

        [Display(Name = "Телефон")]
        public string Phone { set; get; }

        [Display(Name = "Email")]
        public string Email { set; get; }

        [Display(Name = "Адрес")]
        public string Address { set; get; }

        [Display(Name = "На когда")]
        public DateTime DeadLine { set; get; }

        [Display(Name = "Тип оплаты")]
        public int paymentMethod { set; get; }

        [Display(Name = "Предпочитаемый вид связи")]
        public int contactMethod { set; get; }
        
        [Display(Name = "Доставка")]
        public int delivery { set; get; }

        
        public string CookName { set; get; }
        public string CookSurname { set; get; }
        public string CookPhone { set; get; }
        public string CookEmail { set; get; }

        public int idDish { set; get; }

    }
}