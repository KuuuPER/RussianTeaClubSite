using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using RussianTeaClub.Domain.Entities;
using RussianTeaClubSite.ViewModels;

namespace RussianTeaClubSite.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
                                              PagingInfo pagingInfo,
                                              Func<int, string> pageUrl)
        {
            var result = new StringBuilder();

            for (var i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString Tags(this HtmlHelper html, ICollection<Tag> tags, Func<string, MvcHtmlString> tagUrl)
        {
            var htmlTags = tags.Select(t => tagUrl(t.Name).ToHtmlString()).ToArray();
            var tagsStr = string.Join(", ", htmlTags);

            return MvcHtmlString.Create(tagsStr);
        }
    }
}