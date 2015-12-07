using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class OrderDitailViewModel
    {
        public order Order { set; get; }
        public IEnumerable<orderproduct> ProductsList { set; get; }
        public aspnetuser Customer { set; get; }
    }

    public class OrderListViewModel
    {
        public string UserName { set; get; }
        public string Firstname { set; get; }
        public string Surname { set; get; }
        public IEnumerable<order> IncommingOrderList { set; get; }
        public IEnumerable<order> OutcommingOrderList { set; get; }
    }
    public class DishListViewModel
    {
        public string UserName { set; get; }
        public string Firstname { set; get; }
        public string Surname { set; get; }
        public IEnumerable<dish> DishList { set; get; }
        public bool WithTravel { set; get; }
    }
    public class ProfileViewModel
    {
        public string Id { set; get; }
        public string Avatar_Url { set; get; }
        public string UserName { set; get; }
        public string Firstname { set; get; }
        public string Surname { set; get; }
        public string Description { set; get; }
        public int  Rating { set; get; }
        public IEnumerable<dish> DishList { set; get;  }
    }

    public class DishViewModel
    {
        public int Id_Dish { set; get; }

        [Required]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Имя")]
        public string Name { set; get; }

        [Required]
        [StringLength(1024, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Описание")]
        public string Description { set; get; }

        [Required]
        [StringLength(1024, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength =  3)]
        [Display(Name = "Ингридиенты")]
        public string Ingridients { set; get; }

        [Required]
        [Display(Name = "Цена")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Введите правильную цену")]
        public decimal Price { set; get; }

        [Display(Name = "Цена с ингридиентами")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Введите правильную цену")]
        public decimal PriceWithIngridients { set; get; }

        [Display(Name = "Фото")]
        public string Image_Url { set; get; }

        [Required]
        [Display(Name = "Тип")]
        public int Id_Type { set; get; }
        
        [Display(Name = "Я могу обучить")]
        public string CanTeach { set; get; }
    }
}