using System;
using System.Collections;
using System.Text;
using System.Web.UI;

namespace LegoWebSiteForum.Buslogic
{
	public class ForumPost
	{
		private bool		_notify;
		private DateTime	_postDate;
		private int			_flatSortOrder;
		private int			_parentPostID;
		private int			_postID;
		private int			_postLevel;
		private int			_threadID;
		private int			_treeSortOrder;
		private String		_body;
		private string		_remoteAddr;
		private string		_subject;
		private User		_user;

		public ForumPost()
		{
		}

		public bool Notify
		{
			get
			{
				return _notify;
			}
			set
			{
				_notify = value;
			}
		}

		public DateTime PostDate
		{
			get
			{
				return _postDate;
			}
			set
			{
				_postDate = value;
			}
		}

		public int FlatSortOrder
		{
			get
			{
				return _flatSortOrder;
			}
			set
			{
				_flatSortOrder = value;
			}
		}

		public int ParentPostID
		{
			get
			{
				return _parentPostID;
			}
			set
			{
				_parentPostID = value;
			}
		}

		public int PostID
		{
			get
			{
				return _postID;
			}
			set
			{
				_postID = value;
			}
		}

		public int PostLevel
		{
			get
			{
				return _postLevel;
			}
			set
			{
				_postLevel = value;
			}
		}

		public int ThreadID
		{
			get
			{
				return _threadID;
			}
			set
			{
				_threadID = value;
			}
		}

		public int TreeSortOrder
		{
			get
			{
				return _treeSortOrder;
			}
			set
			{
				_treeSortOrder = value;
			}
		}

		public User User
		{
			get
			{
				return _user;
			}
			set
			{
				_user = value;
			}
		}

		public string Body
		{
			get
			{
				return _body;
			}
			set
			{
				_body = value;
			}
		}

		public string RemoteAddr
		{
			get
			{
				return _remoteAddr;
			}
			set
			{
				_remoteAddr = value;
			}
		}

		public string Subject
		{
			get
			{
				return _subject;
			}
			set
			{
				_subject = value;
			}
		}
	}
}
