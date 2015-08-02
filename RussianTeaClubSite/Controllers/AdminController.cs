using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using RussianTeaClub.Domain.Abstract;
using RussianTeaClub.Domain.Entities;
using RussianTeaClubSite.ViewModels;

namespace RussianTeaClubSite.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IArticleRepository _repository;

        public AdminController(IArticleRepository repository)
        {
            _repository = repository;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(_repository.Articles);
        }

        public ViewResult Create()
        {
            return View("Edit", new ArticleViewModel() { ArticleId = Guid.NewGuid() });
        }
        
        public ViewResult Edit(Guid articleId)
        {
            var article = _repository.Articles
                .FirstOrDefault(a => a.ArticleId == articleId);

            var tags = string.Empty;
            if (article.Tags != null)
            {
                tags = string.Join(", ", article.Tags.Select(t => t.Name));
            }
            

            return View(new ArticleViewModel
            {
                ArticleId = article.ArticleId,
                Content = article.Content,
                Description = article.Description,
                Name = article.Name,
                Tags = tags,
                ContentImages = article.ImagesData.ToList()
            });
        }

        [HttpPost]
        public ActionResult Edit(ArticleViewModel articleViewModel, HttpPostedFileBase[] images)
        {
            if (ModelState.IsValid)
            {
                List<Tag> tags = null;

                if (!articleViewModel.Tags.IsEmpty() || articleViewModel.Tags != null)
                {
                    tags = articleViewModel.Tags.Replace(" ", string.Empty).Split(',').Select(t => new Tag
                    {
                        TagId = Guid.NewGuid(),
                        Name = t
                    }).ToList();
                }

                var article = new Article
                {
                    ArticleId = articleViewModel.ArticleId,
                    Name = articleViewModel.Name,
                    Content = articleViewModel.Content,
                    Description = articleViewModel.Description,
                    Tags = tags,
                };

                var imageList = new List<ContentImage>(images.Length);

                if (images != null && images.Length > 0)
                {
                    foreach (var image in images)
                    {
                        var contentImage = new ContentImage
                        {
                            ArticleId = article.ArticleId,
                            ImageMimeType = image.ContentType,
                            ImageData = new byte[image.ContentLength]
                        };

                        image.InputStream.Read(contentImage.ImageData, 0, image.ContentLength);

                        imageList.Add(contentImage);
                    }
                }

                _repository.SaveArticle(article, imageList);
                
                TempData["message"] = $"Изменения в статье {article.Name} применены";

                return RedirectToAction("Index");
            }
            
            return View(articleViewModel);
        }

        public ActionResult Delete(Guid articleId)
        {
            var deletedArticle = _repository.DeleteArticle(articleId);

            if (deletedArticle != null)
            {
                TempData["message"] = $"Статья {deletedArticle.Name} была удалена";
            }

            return RedirectToAction("Index");
        }
    }
}