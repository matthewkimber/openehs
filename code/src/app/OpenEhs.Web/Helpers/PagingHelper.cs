using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OpenEhs.Web.Helpers
{
    /// <summary>
    /// Html Helper extensions contain extensions for paging links
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Get a list of page links
        /// </summary>
        /// <param name="helper">helper that helps you build the links</param>
        /// <param name="currentPage">the current page that you're on</param>
        /// <param name="totalPages">total pages there are</param>
        /// <param name="pageUrl">the page url format</param>
        /// <returns></returns>
        public static IHtmlString PageLinks(this HtmlHelper helper, int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();

            for(int i = 1; i <= totalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                
                if (i == currentPage)
                {
                    tag.AddCssClass("selected");
                }

                result.AppendLine(tag.ToString());
            }

            return new HtmlString(result.ToString());
        }
    }
}