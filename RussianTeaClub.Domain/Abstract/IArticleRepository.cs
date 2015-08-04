using System;
using System.Collections.Generic;
using System.Linq;
using RussianTeaClub.Domain.Entities;

namespace RussianTeaClub.Domain.Abstract
{
    /// <summary> Репозиторий для статей </summary>
    public interface IArticleRepository
    {
        /// <summary> Статьи </summary>
        IEnumerable<Article> Articles { get; }

        void SaveArticle(Article article, List<ContentImage> updatedImages);
        Article DeleteArticle(Guid guid);
    }
}