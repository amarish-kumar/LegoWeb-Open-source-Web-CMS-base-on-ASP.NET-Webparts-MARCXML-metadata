var jsAreaShowTime = 10000;
var JsAreas = new Object();

function GetArea(id)
{
	return JsAreas[id] ? JsAreas[id] : (JsAreas[id] = new JsArea(id));
}

// A single popup window
function JsArea(id)
{
	this.AreaId = id;
}

// Function to return the div Layer
JsArea.prototype.ContentArea= function()
{
	var divArea = document.getElementById(this.AreaId);
	if (divArea != null)
	{
		return divArea;
	}
	return null;
}

var activeAreaId = null;

// Function to show the div Layer
JsArea.prototype.popareaup = function(x, y)
{
if (activeAreaId != null)	
	jsAreaClose(activeAreaId);
	
	var divLayer = this.ContentArea();
	divLayer.style.position = 'absolute';
	divLayer.style.display = 'block';
	divLayer.style.left = x;
	divLayer.style.top = y;
//	divLayer.onmouseover= JsAreaMouseOver;
//	divLayer.onmouseout = jsAreaMouseOut;
	activeAreaId = this.AreaId;
	return false;
}

// Function to hide the div Layer
JsArea.prototype.hide = function()
{
	var divLayer = this.ContentArea();
	if (divLayer != null)
		divLayer.style.display = 'none';
		
	return false;
}

// Function to be called
// by Web forms to show the Popup Window
function PopupArea(areaId)
{
	var area = GetArea(areaId);
	area.popareaup(600,0);
}

// Function to hide the div Layer
function jsAreaClose(areaId)
{   
	GetArea(areaId).hide();
	activeAreaId = divHangTimer = null;	
}

var divHangTimer = null;

// Function to keep the div Layer
// showing for a "period" of time
// after that period, if the mouse
// has been outside the div Layer, 
// it will be hidden automatically
function KeepArea(areaId)
{
	if (areaId == activeAreaId && divHangTimer != null)
	{
		clearTimeout(divHangTimer);
		divHangTimer = null;
	}
}

// Function to release the div Layer
function RelArea(areaId)
{
	if (areaId == activeAreaId && divHangTimer == null)
		divHangTimer = setTimeout('jsAreaClose(\'' + areaId + '\')', jsAreaShowTime);
}

// Function fired when mouse is over the 
// div Layer, used to keep the layer showing
function JsAreaMouseOver(e)
{
	if (!e) 
		var e = window.event;
	var targ = e.target ? e.target : e.srcElement;
	KeepArea(activeAreaId);
}

// Function that fires when mouse is out of
// the scope of the div Layer
function jsAreaMouseOut(e)
{
	if (!e) 
		var e = window.event;
	var targ = e.relatedTarget ? e.relatedTarget : e.toElement;
	var activeAreaView = document.getElementById(activeAreaId);
	if (activeAreaView != null && !jsAreaContains(activeAreaView, targ))
		RelArea(activeAreaId);
}
function jsAreaContains(parent, child)
{
	while(child)
		if (parent == child) return true;
		else 
			child = child.parentNode;
		
		return false;
}