using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class DishListViewModel
    {
        public string UserName { set; get; }
        public string Firstname { set; get; }
        public string Surname { set; get; }
        public IEnumerable<dish> DishList { set; get; }
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
        [Display(Name = "Name")]
        public string Name { set; get; }

        [Required]
        [StringLength(1024, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Description")]
        public string Description { set; get; }

        [Required]
        [StringLength(1024, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength =  3)]
        [Display(Name = "Ingridients")]
        public string Ingridients { set; get; }

        [Required]
        [Display(Name = "Price Without Ingridients")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Please enter valid price")]
        public decimal Price { set; get; }

        [Display(Name = "Price With Ingridients")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Please enter valid price")]
        public decimal PriceWithIngridients { set; get; }

        [Display(Name = "Image")]
        public string Image_Url { set; get; }

        [Required]
        [Display(Name = "Type")]
        public int Id_Type { set; get; }
    }
}