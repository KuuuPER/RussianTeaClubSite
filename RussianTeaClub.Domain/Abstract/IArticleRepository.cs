using System;
using System.Collections.Generic;
using RussianTeaClub.Domain.Entities;

namespace RussianTeaClub.Domain.Abstract
{
    /// <summary> Репозиторий для статей </summary>
    public interface IArticleRepository
    {
        /// <summary> Статьи </summary>
        IEnumerable<Article> Articles { get; }

        void SaveArticle(Article article);
        Article DeleteArticle(Guid guid);
    }
}