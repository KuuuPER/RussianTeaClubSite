using System.Collections;
using System.Collections.Generic;
using RussianTeaClub.Domain.Entities;

namespace RussianTeaClubSite.ViewModels
{
    public class ArticleListViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}