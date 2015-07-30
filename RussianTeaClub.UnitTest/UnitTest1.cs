using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RussianTeaClub.Domain.Abstract;
using RussianTeaClub.Domain.Concrete;
using RussianTeaClub.Domain.Entities;
using RussianTeaClubSite.Controllers;
using RussianTeaClubSite.HtmlHelpers;
using RussianTeaClubSite.Infrastructure.Abstract;
using RussianTeaClubSite.ViewModels;

namespace RussianTeaClub.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanPaginate()
        {
            // Организация (arrange)
            var mock = new Mock<IArticleRepository>();
            mock.Setup(m => m.Articles).Returns(new List<Article>
            {
                new Article("375B7984-4C3F-446C-A8A3-E86077AADD3B") { Name = "Хуйня1", Description = "Статья про хуй", Content = "Жил был хуй", Tags = new List<Tag>(2)
                {
                    new Tag{ TagId = Guid.NewGuid(), Name = "хуй" },
                    new Tag{ TagId = Guid.NewGuid(), Name = "член" },
                    new Tag{ TagId = Guid.NewGuid(), Name = "писька" },
                    new Tag{ TagId = Guid.NewGuid(), Name = "пеннис" }
                }},
                new Article("375B7984-4C3F-446C-A8A3-E86077AADD3B") { Name = "Хуйня2", Description = "Статья про хуй", Content = "Жил был хуй", Tags = new List<Tag>(2)
                {
                    new Tag{ TagId = Guid.NewGuid(), Name = "хуй" },
                }},
                new Article("375B7984-4C3F-446C-A8A3-E86077AADD3B") { Name = "Хуйня3", Description = "Статья про хуй", Content = "Жил был хуй", Tags = new List<Tag>(2)
                {
                    new Tag{ TagId = Guid.NewGuid(), Name = "член" },
                }},
                new Article("375B7984-4C3F-446C-A8A3-E86077AADD3B") { Name = "Хуйня4", Description = "Статья про хуй", Content = "Жил был хуй", Tags = new List<Tag>(2)
                {
                    new Tag{ TagId = Guid.NewGuid(), Name = "писька" },
                }},
                new Article("375B7984-4C3F-446C-A8A3-E86077AADD3B") { Name = "Хуйня5", Description = "Статья про хуй", Content = "Жил был хуй", Tags = new List<Tag>(2)
                {
                    new Tag{ TagId = Guid.NewGuid(), Name = "пеннис" },
                }},
            });
            var controller = new ArticleController(mock.Object) {_pageSize = 3};

            // Действие (act)
            var result = (ArticleListViewModel)controller.List(2).Model;

            // Утверждение (assert)
            var articles = result.Articles.ToList();
            Assert.IsTrue(articles.Count == 2);
            Assert.AreEqual(articles[0].Name, "Хуйня4");
            Assert.AreEqual(articles[1].Name, "Хуйня5");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            var mock = new Mock<IArticleRepository>();
            mock.Setup(m => m.Articles).Returns(new List<Article>
            {
                new Article("375B7984-4C3F-446C-A8A3-E86077AADD3B") { Name = "Хуйня1", Description = "Статья про хуй", Content = "Жил был хуй", Tags = new List<Tag>(2)
                {
                    new Tag{ TagId = Guid.NewGuid(), Name = "хуй" },
                    new Tag{ TagId = Guid.NewGuid(), Name = "член" },
                    new Tag{ TagId = Guid.NewGuid(), Name = "писька" },
                    new Tag{ TagId = Guid.NewGuid(), Name = "пеннис" }
                }},
                new Article("375B7984-4C3F-446C-A8A3-E86077AADD3B") { Name = "Хуйня2", Description = "Статья про хуй", Content = "Жил был хуй", Tags = new List<Tag>(2)
                {
                    new Tag{ TagId = Guid.NewGuid(), Name = "хуй" },
                }},
                new Article("375B7984-4C3F-446C-A8A3-E86077AADD3B") { Name = "Хуйня3", Description = "Статья про хуй", Content = "Жил был хуй", Tags = new List<Tag>(2)
                {
                    new Tag{ TagId = Guid.NewGuid(), Name = "член" },
                }},
                new Article("375B7984-4C3F-446C-A8A3-E86077AADD3B") { Name = "Хуйня4", Description = "Статья про хуй", Content = "Жил был хуй", Tags = new List<Tag>(2)
                {
                    new Tag{ TagId = Guid.NewGuid(), Name = "писька" },
                }},
                new Article("375B7984-4C3F-446C-A8A3-E86077AADD3B") { Name = "Хуйня5", Description = "Статья про хуй", Content = "Жил был хуй", Tags = new List<Tag>(2)
                {
                    new Tag{ TagId = Guid.NewGuid(), Name = "пеннис" },
                }},
            });
            var controller = new ArticleController(mock.Object) { _pageSize = 3 };

            // Действие (act)
            var result = (ArticleListViewModel)controller.List(2).Model;

            // Утверждение (assert)
            var pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            var pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            var result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void TestArticleRepository()
        {
            var articleRepository = new EfArticleRepository();

            var article = new Article("4525D21E-460C-44C9-AA6D-E791261EC843");
            article.Name = "Какое-то имя";
            article.Content = "Какое-то содержимое";
            article.Description = "Какое-то описание";
            article.Tags = new List<Tag>(3)
            {
                new Tag{Name = "Тэг1"},
                new Tag{Name = "Тэг2"},
                new Tag{Name = "Тэг3"},
            };

            articleRepository.SaveArticle(article);
        }

        [TestMethod]
        public void TestMenuItems()
        {
            var menuItems = new[] {"Домой", "Вакансии"};

            var target = new NavController();

            var result = ((IEnumerable<MenuViewModel>) target.Menu().Model).ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].ItemName, menuItems[0]);
            Assert.AreEqual(result[1].ItemName, menuItems[1]);
        }

        [TestMethod]
        public void CanSelectMenu()
        {
            var target = new NavController();

            var itemToSelect = "Вакансии";

            var result = ((IEnumerable<MenuViewModel>)target.Menu(itemToSelect).Model).Single(i => i.IsSelected);

            Assert.AreEqual(result.ItemName, itemToSelect);
        }

        [TestMethod]
        public void CanEditArticle()
        {
            var articleIds = GetArticleIds();

            var mock = new Mock<IArticleRepository>();
            mock.Setup(m => m.Articles).Returns(GetArticles(articleIds));

            var controller = new AdminController(mock.Object);

            var article1 = controller.Edit(articleIds[0]).ViewData.Model as Article;
            var article2 = controller.Edit(articleIds[1]).ViewData.Model as Article;
            var article3 = controller.Edit(articleIds[2]).ViewData.Model as Article;

            Assert.AreEqual(articleIds[0], article1.ArticleId);
            Assert.AreEqual(articleIds[1], article2.ArticleId);
            Assert.AreEqual(articleIds[2], article3.ArticleId);
        }

        [TestMethod]
        public void CannotEditNonexistenceArticle()
        {
            var articleIds = GetArticleIds();

            var mock = new Mock<IArticleRepository>();
            mock.Setup(m => m.Articles).Returns(GetArticles(articleIds));

            var controller = new AdminController(mock.Object);

            var result = controller.Edit(Guid.Parse("E3D32116-62DF-4830-8005-CAEEB6D34892")).ViewData.Model as Article;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CanDeleteArticle()
        {
            var articleIds = GetArticleIds();

            var mock = new Mock<IArticleRepository>();
            mock.Setup(m => m.Articles).Returns(GetArticles(articleIds));

            var controller = new AdminController(mock.Object);

            var deletedArticleId = articleIds[3];

            controller.Delete(deletedArticleId);

            mock.Verify(m => m.DeleteArticle(deletedArticleId));
        }

        [TestMethod]
        public void CanLoginWithValidCredentials()
        {
            var mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "12345"))
                .Returns(true);

            var model = new LoginViewModel()
            {
                UserName = "admin",
                Password = "12345"
            };

            var target = new AccountController(mock.Object);

            const string returnUrl = "/MyUrl";
            var result = target.Login(model, returnUrl);

            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual(returnUrl, ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void CannotLoginWithInvalidCredentials()
        {
            var mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPass"))
                .Returns(false);

            var model = new LoginViewModel()
            {
                UserName = "badUser",
                Password = "badPass"
            };

            var target = new AccountController(mock.Object);

            const string returnUrl = "/MyUrl";
            var result = target.Login(model, returnUrl);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }

        private static Guid[] GetArticleIds()
        {
            return new[]
            {
                new Guid("EA727FD2-0F24-4155-8A4B-9AC5B8EF1103"),
                new Guid("FF4577A3-0219-458F-8421-2258EC73E9D4"),
                new Guid("68FDA7CA-7494-49F8-8F62-D088415A7A07"),
                new Guid("225D735B-1C63-49D5-8755-A7C008479DEB"), 
                new Guid("EF3A7D9B-4752-48B7-A96E-7D909A0C6262")
            };
        }

        private static List<Article> GetArticles(Guid[] articleIds)
        {
            return new List<Article>
            {
                new Article
                {
                    ArticleId = articleIds[0],
                    Name = "статья1"
                },
                new Article
                {
                    ArticleId = articleIds[1],
                    Name = "статья2"
                },
                new Article
                {
                    ArticleId = articleIds[2],
                    Name = "статья3"
                },
                new Article
                {
                    ArticleId = articleIds[3],
                    Name = "статья4"
                },
                new Article
                {
                    ArticleId = articleIds[4],
                    Name = "статья5"
                }
            };
        }
    }
}
