using System;
using System.Linq;
using System.Web.Mvc;
using RussianTeaClub.Domain.Abstract;
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
        public ViewResult List(int page = 1)
        {
            var articleViewModel = new ArticleListViewModel
            {
                Articles = _repository.Articles.OrderBy(a => a.Name).Skip((page - 1)*_pageSize).Take(_pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _repository.Articles.Count()
                },
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