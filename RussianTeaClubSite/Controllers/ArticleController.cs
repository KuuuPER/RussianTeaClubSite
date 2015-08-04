using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RussianTeaClub.Domain.Abstract;
using RussianTeaClub.Domain.Entities;
using RussianTeaClubSite.ViewModels;

namespace RussianTeaClubSite.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleRepository _repository;
        public int _pageSize = 4;

        public ArticleController(IArticleRepository articleRepository)
        {
            _repository = articleRepository;
        }

        // GET: Articles
        //public ViewResult List(int page = 1)
        //{
        //    var articleViewModel = new ArticleListViewModel
        //    {
        //        Articles = _repository.Articles.OrderBy(a => a.Name).Skip((page - 1)*_pageSize).Take(_pageSize),
        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = page,
        //            ItemsPerPage = _pageSize,
        //            TotalItems = _repository.Articles.Count(),
        //            CurrentTag = string.Empty
        //        },
        //    };

        //    return View(articleViewModel);
        //}

        public ViewResult List(string tag, int page = 1)
        {
            IEnumerable<Article> articles;

            articles = tag != string.Empty ? _repository.Articles.Where(a => a.Tags.Any(t => t.Name == tag)) : _repository.Articles;

            var enumerable = articles as Article[] ?? articles.ToArray();
            var articleViewModel = new ArticleListViewModel
            {
                Articles = enumerable.OrderBy(a => a.Name).Skip((page - 1) * _pageSize).Take(_pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = enumerable.Count(),
                    CurrentTag = tag
                },
            };

            return View(articleViewModel);
        }

        public ViewResult Article(Guid articleId)
        {
            var article = _repository.Articles.SingleOrDefault(a => a.ArticleId == articleId);

            if (article == null)
            {
                throw new NullReferenceException("Статья не найдена!");
            }

            var articleViewModel = new ArticlePageViewModel
            {
                ArticleId = articleId,
                Name = article.Name,
                Content = article.Content,
                Description = article.Description,
                ContentImages = article.ImagesData.ToList(),
                Tags = article.Tags
            };

            return View(articleViewModel);
        }

        public FileContentResult GetImage(Guid articleid, Guid contentImageId)
        {
            var article = _repository.Articles.SingleOrDefault(a => a.ArticleId == articleid);

            if (article != null)
            {
                var image = article.ImagesData.Single(i => i.ContentImageId == contentImageId);

                return File(image.ImageData, image.ImageMimeType);
            }

            return null;
        }
    }
}