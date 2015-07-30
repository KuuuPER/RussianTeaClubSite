using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using Ninject;
using RussianTeaClub.Domain.Abstract;
using RussianTeaClub.Domain.Concrete;
using RussianTeaClub.Domain.Entities;
using RussianTeaClubSite.Infrastructure.Abstract;
using RussianTeaClubSite.Infrastructure.Concrete;

namespace RussianTeaClubSite.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            var mock = new Mock<IArticleRepository>();

            mock.Setup(m => m.Articles).Returns(new List<Article>
            {
                new Article("C753376E-A34F-4E12-B837-0E94EAB358A9")
                {
                    Name = "Статья1",
                    Description = "Описание статьи1",
                    Content = "Какое-то содержимое статьи1",
                    Tags = new[] {new Tag {Name =  "тэг1"}, new Tag(){Name =  "тэг2"}, }
                },
                new Article("1077A909-0ADB-4863-A96E-975BC9EFF169")
                {
                    Name = "Статья2",
                    Description = "Описание статьи2",
                    Content = "Какое-то содержимое статьи",
                    Tags = new[] {new Tag {Name =  "тэг3"}, }
                },
                new Article("679FDFB2-B0B4-4DE6-827D-0FD227524117")
                {
                    Name = "Статья3",
                    Description = "Описание статьи3",
                    Content = "Какое-то содержимое статьи3",
                    Tags = new [] {new Tag {Name =  "тэг4"}, }
                }
            });

            kernel.Bind<IArticleRepository>().To<EfArticleRepository>();
            kernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}