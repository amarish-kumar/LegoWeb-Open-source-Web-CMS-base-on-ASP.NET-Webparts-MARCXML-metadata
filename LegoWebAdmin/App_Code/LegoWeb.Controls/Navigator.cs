//Navigator Class @0-71CCA1A1
//Target Framework version is 2.0
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace LegoWeb.Controls
{
    public enum NavigatorItemType { FirstOn, FirstOff, PrevOn, PrevOff, PageOn, PageOff, NextOn, NextOff, LastOn, LastOff }
    public enum PagerStyle { Centered, Moving }

    public class PagerItem : Control, INamingContainer
    {
        public PagerItem(int page)
        {
            PageNumber = page;
        }

        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null) return (int)ViewState["PageNumber"];
                else return 1;
            }
            set { ViewState["PageNumber"] = value; }
        }

        protected override bool OnBubbleEvent(object source, EventArgs e)
        {
            CommandEventArgs args = new CommandEventArgs("Navigate", PageNumber);
            RaiseBubbleEvent(this, args);
            return true;
        }
    }

    [ParseChildren(true)]
    public class Pager : Control, INamingContainer
    {
        private ITemplate _pageOnTemplate = null;
        private ITemplate _pageOffTemplate = null;
        private PagerStyle _style = PagerStyle.Centered;
        private int _pagerSize = 10;

        public PagerStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        public int PagerSize
        {
            get { return _pagerSize; }
            set { _pagerSize = value; }
        }

        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null) return (int)ViewState["PageNumber"];
                else return 1;
            }
            set { ViewState["PageNumber"] = value; }
        }

        public int MaxPage
        {
            get
            {
                if (ViewState["MaxPage"] != null) return (int)ViewState["MaxPage"];
                else return 1;
            }
            set { ViewState["MaxPage"] = value; }
        }

        [PersistenceMode(PersistenceMode.InnerProperty),
        TemplateContainer(typeof(PagerItem))]
        public ITemplate PageOnTemplate
        {
            get
            {
                return _pageOnTemplate;
            }
            set
            {
                _pageOnTemplate = value;
            }
        }

        public ITemplate PageOffTemplate
        {
            get
            {
                return _pageOffTemplate;
            }
            set
            {
                _pageOffTemplate = value;
            }
        }

        public override void DataBind()
        {
            EnsureChildControls();
            CreateChildControls();
            base.DataBind();
        }

        protected override bool OnBubbleEvent(object source, EventArgs e)
        {
            RaiseBubbleEvent(this, e);
            return true;
        }

        protected override void CreateChildControls()
        {
            if (PageOnTemplate != null && PageOffTemplate != null)
            {
                Controls.Clear();
                int start, end = 0;
                if (Style == PagerStyle.Moving)
                {
                    start = ((PageNumber - 1) / PagerSize) * PagerSize + 1;
                    if (start != 1) { start--; end++; }
                    end += start + PagerSize;
                }
                else
                {
                    start = PageNumber - (PagerSize / 2);
                    if (start + PagerSize > MaxPage) start = MaxPage - PagerSize + 1;
                    if (start < 1) start = 1;
                    end = start + PagerSize - 1;
                }

                for (; start <= end && start <= MaxPage; start++)
                {
                    PagerItem i = new PagerItem(start);
                    if (start == PageNumber)
                        PageOffTemplate.InstantiateIn(i);
                    else
                        PageOnTemplate.InstantiateIn(i);
                    Controls.Add(i);
                }
            }
            else
            {
                this.Controls.Add(new LiteralControl());
            }
        }

    }

    public class NavigatorItem : Control, INamingContainer
    {
        private NavigatorItemType itemType;

        public NavigatorItemType Type
        {
            get { return itemType; }
            set { itemType = value; }
        }

        protected override void AddParsedSubObject(Object obj)
        {
            Controls.Add((Control)obj);
        }

        protected override bool OnBubbleEvent(object source, EventArgs e)
        {
            CommandEventArgs args = new CommandEventArgs("Navigate", Type.ToString());
            RaiseBubbleEvent(this, args);
            return true;
        }
    }

    [ParseChildren(false)]
    public class Navigator : Control, INamingContainer
    {
        private NavigatorItem FirstOn, FirstOff, PrevOn, PrevOff, NextOn, NextOff, LastOn, LastOff;
        private Pager pager;

        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null) return (int)ViewState["PageNumber"];
                else return 1;
            }
            set
            {
                if (value != 0)
                    ViewState["PageNumber"] = value;
                else
                    ViewState["PageNumber"] = 1;
            }
        }

        public int MaxPage
        {
            get
            {
                if (ViewState["MaxPage"] != null) return (int)ViewState["MaxPage"];
                else return 1;
            }
            set
            {
                if (value != 0)
                    ViewState["MaxPage"] = value;
                else
                    ViewState["MaxPage"] = 1;
            }
        }

        protected override bool OnBubbleEvent(object source, EventArgs e)
        {
            if (source is NavigatorItem)
            {
                if (((string)((CommandEventArgs)e).CommandArgument) == "FirstOn") PageNumber = 1;
                if (((string)((CommandEventArgs)e).CommandArgument) == "PrevOn") PageNumber--;
                if (((string)((CommandEventArgs)e).CommandArgument) == "NextOn") PageNumber++;
                if (((string)((CommandEventArgs)e).CommandArgument) == "LastOn") PageNumber = MaxPage;
            }
            else if (source is Pager)
            {
                PageNumber = (int)((CommandEventArgs)e).CommandArgument;
            }
            else PageNumber = 1;
            CommandEventArgs args = new CommandEventArgs("Navigate", PageNumber);
            RaiseBubbleEvent(this, args);
            return true;
        }

        protected override void AddParsedSubObject(Object obj)
        {
            if (obj is NavigatorItem)
            {
                NavigatorItem item = (NavigatorItem)obj;
                if (item.Type == NavigatorItemType.FirstOn) FirstOn = item;
                if (item.Type == NavigatorItemType.FirstOff) FirstOff = item;
                if (item.Type == NavigatorItemType.PrevOn) PrevOn = item;
                if (item.Type == NavigatorItemType.PrevOff) PrevOff = item;
                if (item.Type == NavigatorItemType.NextOn) NextOn = item;
                if (item.Type == NavigatorItemType.NextOff) NextOff = item;
                if (item.Type == NavigatorItemType.LastOn) LastOn = item;
                if (item.Type == NavigatorItemType.LastOff) LastOff = item;
            }
            if (obj is Pager)
                pager = (Pager)obj;
            if (obj is LiteralControl)
                ((LiteralControl)obj).Text = ((LiteralControl)obj).Text.Replace('\n', ' ').Replace('\r', ' ');
            Controls.Add((Control)obj);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (FirstOn != null) FirstOn.Visible = (PageNumber > 1);
            if (FirstOff != null) FirstOff.Visible = (PageNumber == 1);
            if (PrevOn != null) PrevOn.Visible = (PageNumber > 1);
            if (PrevOff != null) PrevOff.Visible = (PageNumber == 1);
            if (NextOn != null) NextOn.Visible = (PageNumber < MaxPage && MaxPage != 1);
            if (NextOff != null) NextOff.Visible = (PageNumber == MaxPage);
            if (LastOn != null) LastOn.Visible = (PageNumber < MaxPage && MaxPage != 1);
            if (LastOff != null) LastOff.Visible = (PageNumber == MaxPage);
            if (pager != null)
            {
                pager.MaxPage = MaxPage;
                pager.PageNumber = PageNumber;
                pager.DataBind();
            }
            base.OnPreRender(e);
        }
    }
}
//End Navigator Class

