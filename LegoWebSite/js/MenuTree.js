﻿startList = function() {

	// code for IE
	if(!document.body.currentStyle) return;
	var subs = document.getElementsByName('submenu');
	for(var i=0; i<subs.length; i++) {
		var li = subs[i].parentNode;
		if(li && li.lastChild.style) {
			li.onmouseover = function() {
				this.lastChild.style.visibility = 'visible';
			}
			li.onmouseout = function() {
				this.lastChild.style.visibility = 'hidden';
			}
		}
	}
}
window.onload=startList;