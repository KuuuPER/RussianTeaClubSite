using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RussianTeaClub.Domain.Entities
{
    public class Article
    {
        public Article()
        {
            ArticleId = Guid.NewGuid();
        }

        public Article(string guid)
        {
            ArticleId = Guid.Parse(guid);
        }

        [HiddenInput(DisplayValue = false)]
        public Guid ArticleId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Содержимое")]
        public string Content { get; set; }

        [Display(Name = "Тэги. Введите тэги через запятую.")]
        public virtual ICollection<Tag> Tags { get; set; }

        [Display(Name = "Картинащка... Добавьте картинощку...")]
        public virtual ICollection<ContentImage> ImagesData { get; set; }
    }
}
