using System;
using System.Collections.Generic;
using System.Linq;
using RussianTeaClub.Domain.Abstract;
using RussianTeaClub.Domain.Entities;

namespace RussianTeaClub.Domain.Concrete
{
    public class EfArticleRepository : IArticleRepository
    {
        EfDbContext context = new EfDbContext();

        public IEnumerable<Article> Articles
        {
            get { return context.Articles.Include("Tags"); }
        }

        public void SaveArticle(Article article)
        {
            var dbEntry = context.Articles.Find(article.ArticleId);
            if (dbEntry != null)
            {
                dbEntry.Name = article.Name;
                dbEntry.Description = article.Description;
                dbEntry.Content = article.Content;

                dbEntry.Tags.ToList().ForEach(t => dbEntry.Tags.Remove(t));

                dbEntry.Tags = article.Tags;
                dbEntry.ImagesData = article.ImagesData;
            }
            else
            {
                context.Articles.Add(article);
            }

            context.SaveChanges();
        }

        public Article DeleteArticle(Guid articleId)
        {
            var article = context.Articles.Find(articleId);

            if (article != null)
            {
                context.Articles.Remove(article);
                context.SaveChanges();
            }

            return article;
        }
    }
}