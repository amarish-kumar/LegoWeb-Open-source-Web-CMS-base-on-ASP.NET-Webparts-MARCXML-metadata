// -------------------------------------------------------------------
// gAjax RSS Feeds Displayer- By Dynamic Drive, available at: http://www.dynamicdrive.com
// Created: July 17th, 2007 Updated: n/a
// -------------------------------------------------------------------

var gfeedfetcher_loading_image="images/indicator.gif" //Full URL to "loading" image. No need to config after this line!!

google.load("feeds", "1") //Load Google Ajax Feed API (version 1)

function gfeedfetcher(divid, divClass, linktarget){
	this.linktarget=linktarget || "" //link target of RSS entries
	this.feedlabels=[] //array holding lables for each RSS feed
	this.feedurls=[]
	this.feeds=[] //array holding combined RSS feeds' entries from Feed API (result.feed.entries)
	this.feedsfetched=0 //number of feeds fetched
	this.feedlimit=5
	this.showoptions="" //Optional components of RSS entry to show (none by default)
	this.sortstring="date" //sort by "date" by default
	document.write('<div id="'+divid+'" class="'+divClass+'"></div>') //output div to contain RSS entries
	this.feedcontainer=document.getElementById(divid)
	this.itemcontainer="<li>" //default element wrapping around each RSS entry item
}

gfeedfetcher.prototype.addFeed=function(label, url){
	this.feedlabels[this.feedlabels.length]=label
	this.feedurls[this.feedurls.length]=url
}

gfeedfetcher.prototype.filterfeed=function(feedlimit, sortstr){
	this.feedlimit=feedlimit
	if (typeof sortstr!="undefined")
	this.sortstring=sortstr
}

gfeedfetcher.prototype.displayoptions=function(parts){
	this.showoptions=parts //set RSS entry options to show ("date, datetime, time, snippet, label, description")
}

gfeedfetcher.prototype.setentrycontainer=function(containerstr){  //set element that should wrap around each RSS entry item
this.itemcontainer="<"+containerstr.toLowerCase()+">"
}

gfeedfetcher.prototype.init=function(){
	this.feedsfetched=0 //reset number of feeds fetched to 0 (in case init() is called more than once)
	this.feeds=[] //reset feeds[] array to empty (in case init() is called more than once)
	this.feedcontainer.innerHTML='<img src="'+gfeedfetcher_loading_image+'" /> Đợi trong giây lát ...'
	var displayer=this
	for (var i=0; i<this.feedurls.length; i++){ //loop through the specified RSS feeds' URLs
		var feedpointer=new google.feeds.Feed(this.feedurls[i]) //create new instance of Google Ajax Feed API
		var items_to_show=(this.feedlimit<=this.feedurls.length)? 1 : Math.floor(this.feedlimit/this.feedurls.length) //Calculate # of entries to show for each RSS feed
		if (this.feedlimit%this.feedurls.length>0 && this.feedlimit>this.feedurls.length && i==this.feedurls.length-1) //If this is the last RSS feed, and feedlimit/feedurls.length yields a remainder
			items_to_show+=(this.feedlimit%this.feedurls.length) //Add that remainder to the number of entries to show for last RSS feed
		feedpointer.setNumEntries(items_to_show) //set number of items to display
		feedpointer.load(function(r){displayer._fetch_data_as_array(r)}) //call Feed.load() to retrieve and output RSS feed
	}
}


gfeedfetcher._formatdate=function(datestr, showoptions){
	//var itemdate=new Date(datestr)
	//var parseddate=(showoptions.indexOf("datetime")!=-1)? itemdate.toLocaleString() : (showoptions.indexOf("date")!=-1)? itemdate.toLocaleDateString() : (showoptions.indexOf("time")!=-1)? itemdate.toLocaleTimeString() : ""
	
	parseddate =  getCurDate(datestr);
	
	return "<span class='datefield'>"+parseddate+"</span>"
}

function getCurDate(datestr){
	var the_date = new Date(datestr);
	
	var the_year = the_date.getYear();
	if(the_year < 1900) the_year += 1900; 
	var the_month = the_date.getMonth();
	var the_day = the_date.getDate();
	var the_today = the_date.getDay();
	var the_first = "";
	switch (the_today) {
		case 0:
		the_first = "Chủ nhật";
		break;
	case 1:
		the_first = "Thứ hai";
		break;
	case 2:
		the_first = "Thứ ba"; 
		break;
	case 3:
		the_first = "Thứ tư"; 
		break;
	case 4:
		the_first = "Thứ năm"; 
		break;
	case 5:
		the_first = "Thứ sáu"; 
		
		break;
	case 6:
		the_first = "Thứ bảy"; 
		break;
	}
	var the_seccond = "";
	switch (the_month) {
		case 0:
		the_second = "/1";
		break;
	case 1:
		the_second = "/2";
		break;
	case 2:
		the_second = "/3";
		break;
	case 3:
		the_second = "/4";
		break;
	case 4:
		the_second = "/5";
		break;
	case 5:
		the_second = "/6";
		break;
	case 6:
		the_second = "/7";
		break;
	case 7:
		the_second = "/8";
		break;
	case 8:
		the_second = "/9";
		break;
	case 9:
		the_second = "/10";
		break;
	case 10:
		the_second = "/11";
		break;
	case 11:
		the_second = "/12";
		break;
	}
	return (the_day+""+the_second+"/"+the_year+" "+the_date.toLocaleTimeString());
}

gfeedfetcher._sortarray=function(arr, sortstr){
	var sortstr=(sortstr=="label")? "ddlabel" : sortstr //change "label" string (if entered) to "ddlabel" instead, for internal use
	if (sortstr=="title" || sortstr=="ddlabel"){ //sort array by "title" or "ddlabel" property of RSS feed entries[]
		arr.sort(function(a,b){
		var fielda=a[sortstr].toLowerCase()
		var fieldb=b[sortstr].toLowerCase()
		return (fielda<fieldb)? -1 : (fielda>fieldb)? 1 : 0
		})
	}
	else{ //else, sort by "publishedDate" property (using error handling, as "publishedDate" may not be a valid date str if an error has occured while getting feed
		try{
			arr.sort(function(a,b){return new Date(b.publishedDate)-new Date(a.publishedDate)})
		}
		catch(err){}
	}
}

gfeedfetcher.prototype._fetch_data_as_array=function(result){
	var thisfeed=(!result.error)? result.feed.entries : "" //get all feed entries as a JSON array or "" if failed
	if (thisfeed=="") //if error has occured fetching feed
		alert("Google Feed API Error: "+result.error.message)
	for (var i=0; i<thisfeed.length; i++) //For each entry within feed
		result.feed.entries[i].ddlabel=this.feedlabels[this.feedsfetched] //extend it with a "ddlabel" property
	this.feeds=this.feeds.concat(thisfeed) //add entry to array holding all feed entries
	this._signaldownloadcomplete() //signal the retrieval of this feed as complete (and move on to next one if defined)
}

gfeedfetcher.prototype._signaldownloadcomplete=function(){
	this.feedsfetched+=1
	if (this.feedsfetched==this.feedurls.length) //if all feeds fetched
		this._displayresult(this.feeds) //display results
}


gfeedfetcher.prototype._displayresult=function(feeds){
	var rssoutput=(this.itemcontainer=="<li>")? "<ul>\n" : ""
	gfeedfetcher._sortarray(feeds, this.sortstring)
	for (var i=0; i<feeds.length; i++){
		var itemtitle="<a href=\"" + feeds[i].link + "\" target=\"" + this.linktarget + "\" class=\"titlefield\">" + feeds[i].title + "</a>"
		var itemlabel=/label/i.test(this.showoptions)? '<span class="labelfield">['+this.feeds[i].ddlabel+']</span>' : " "
		var itemdate=gfeedfetcher._formatdate(feeds[i].publishedDate, this.showoptions)
		var itemdescription=/description/i.test(this.showoptions)? "<br />"+feeds[i].content : /snippet/i.test(this.showoptions)? "<br />"+feeds[i].contentSnippet  : ""
		rssoutput+=this.itemcontainer + "<img src=\"images/bullet_0.gif\" />" + "&nbsp;&nbsp;" + itemdate + "<br/>" + itemtitle + " " + itemlabel + " " + "\n" + itemdescription + this.itemcontainer.replace("<", "</") + "\n\n"
	}
	rssoutput+=(this.itemcontainer=="<li>")? "</ul>" : ""
	this.feedcontainer.innerHTML=rssoutput
}
