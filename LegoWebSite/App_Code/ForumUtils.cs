using System;
using System.Web;
using System.Web.UI;

namespace Forum
{
	public class ForumUtils
	{
		public enum ForumView
		{
			FlatView,
			TreeView,
			TreeViewDynamic
		}

		public static void SetForumView(Page page, ForumView forumView)
		{
			string cookieValue = forumView.ToString();
			HttpCookie httpCookie = new HttpCookie("Forum_View", cookieValue);
			DateTime expires = DateTime.Now;
			expires = expires.AddDays(355);
			httpCookie.Expires = expires;
			page.Response.Cookies.Add(httpCookie);
		}

		public static ForumView GetForumViewFromString(string text)
		{
			ForumView forumView;

			switch (text)
			{
				case "TreeView":
					forumView = ForumView.TreeView;
					break;

				case "TreeViewDynamic":
					forumView = ForumView.TreeViewDynamic;
					break;

				default:
					forumView = ForumView.FlatView;
					break;
			}

			return forumView;
		}

		public static ForumView GetForumView(Page page)
		{
			string cookieValue = null;

			HttpCookie viewCookie = page.Request.Cookies["Forum_View"];
			if (viewCookie != null)
				cookieValue = viewCookie.Value;

			return GetForumViewFromString(cookieValue);
		}

		public static int GetPostsPerPage()
		{
			return 10;
		}
	}
}
