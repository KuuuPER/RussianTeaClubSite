using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RussianTeaClub.Domain.Entities
{
    public class Tag
    {
        public Tag()
        {
            this.Articles = new HashSet<Article>();
        }

        [Key]
        public Guid TagId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}