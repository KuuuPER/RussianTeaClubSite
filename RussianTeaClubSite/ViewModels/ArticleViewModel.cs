using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using RussianTeaClub.Domain.Entities;

namespace RussianTeaClubSite.ViewModels
{
    public class ArticleViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid ArticleId { get; set; }

        [Required(ErrorMessage = "Укажите название")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Содержимое")]
        [Required(ErrorMessage = "Введите содержимое статьи")]
        public string Content { get; set; }
        
        [Display(Name = "Содержимое")]
        public List<ContentImage> ContentImages { get; set; }

        [Display(Name = "Тэги. Введите тэги через запятую.")]
        public string Tags { get; set; }
    }
}