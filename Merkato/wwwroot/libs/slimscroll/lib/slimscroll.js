/*
 * slimscroll
 * http://github.com/yawetse/slimscroll
 *
 * Copyright (c) 2014 Yaw Joseph Etse. All rights reserved.
 */
'use strict';

var classie = require('classie'),
	extend = require('util-extend'),
	domhelper = require('domhelper');

/**
 * Slimscroll is a small commonjs module with no library dependencies (sans jquery) that transforms any div into a scrollable area with a nice scrollbar
 * @{@link https://github.com/yawetse/slimscroll}
 * @author Yaw Joseph Etse
 * @copyright Copyright (c) 2014 Yaw Joseph Etse. All rights reserved.
 * @license MIT
 * @module slimscroll
 * @requires module:classie
 * @requires module:util-extent
 * @requires module:util
 * @requires module:domhelper
 * @requires module:events
 * @todo need to switch to node events
 */
var slimscroll = function(options,elementsArray){
	/** module default configuration */
	var defaults = {
			idSelector: 'body',
			width : 'auto',// width in pixels of the visible scroll area
			height : '250px',// height in pixels of the visible scroll area
			size : '7px',// width in pixels of the scrollbar and rail
			color: '#000',// scrollbar color, accepts any hex/color value
			position : 'right',// scrollbar position - left/right
			distance : '1px',// distance in pixels between the side edge and the scrollbar
			start : 'top',// default scroll position on load - top / bottom / $('selector')
			opacity : 0.4,// sets scrollbar opacity
			alwaysVisible : false,// enables always-on mode for the scrollbar
			disableFadeOut : false,// check if we should hide the scrollbar when user is hovering over
			railVisible : false,// sets visibility of the rail
			railColor : '#333',// sets rail color
			railOpacity : 0.2,// sets rail opacity
			railDraggable : true,// whether  we should use jQuery UI Draggable to enable bar dragging
			railClass : 'slimScrollRail',// defautlt CSS class of the slimscroll rail
			barClass : 'slimScrollBar',// defautlt CSS class of the slimscroll bar
			wrapperClass : 'slimScrollDiv',// defautlt CSS class of the slimscroll wrapper
			allowPageScroll : false,// check if mousewheel should scroll the window if we reach top/bottom
			wheelStep : 20,// scroll amount applied to each mouse wheel step
			touchScrollStep : 200,// scroll amount applied when user is using gestures
			addedOriginalClass: 'originalScrollableElement',
			borderRadius: '7px',// sets border radius
			railBorderRadius : '7px'// sets border radius of the rail
		},
		o = extend( defaults,options ),
		thisElements = (elementsArray) ? elementsArray : document.querySelectorAll(options.idSelector),
		me,
		rail,
		bar,
		barHeight,
		minBarHeight = 30,
		mousedownPageY,
		mousedownT,
		isDragg,
		currentBar,
		currentTouchDif,
		releaseScroll,
		isOverBar,
		percentScroll,
		queueHide,
		lastScroll,
		isOverPanel;

	/**
	 * creates new slimscrolls
	 */
	this.init = function(){
		// do it for every element that matches selector
		for(var x=0; x<thisElements.length;x++){
			var touchDif,
			barHeight,
			divS = '<div></div>';
			releaseScroll = false;
			// used in event handlers and for better minification
			me = thisElements[x];
			classie.addClass(me,o.addedOriginalClass);

			// ensure we are not binding it again
			if( classie.hasClass(me.parentNode,o.wrapperClass) ){
				// start from last bar position
				var offset = me.scrollTop;
				bar = me.parentNode.querSelector('.' + o.barClass),// find bar and rail,
				rail = me.parentNode.querSelector('.' + o.railClass);

				getBarHeight();

				// check if we should scroll existing instance
				if (typeof options==='object'){
					// Pass height: auto to an existing slimscroll object to force a resize after contents have changed
					if ( 'height' in options && options.height === 'auto' ) {
						me.parentNode.style.height='auto';
						me.style.height='auto';
						var height = me.parentNode.parentNode.scrollHeight;
						me.parent.style.height=height;
						me.style.height=height;
					}

					if ('scrollTo' in options){
						// jump to a static point
						offset = parseInt(o.scrollTo,10);
					}
					else if ('scrollBy' in options){
						// jump by value pixels
						offset += parseInt(o.scrollBy,10);
					}
					else if ('destroy' in options){
						// remove slimscroll elements
						domhelper.removeElement(bar);
						domhelper.removeElement(rail);
						domhelper.unwrapElement(me);
						return;
					}

					// scroll content by the given offset
					// console.log("add scrollContent");
					scrollContent(offset, false, true,me);
				}
				return;
			}

			// optionally set height to the parent's height
			o.height = (options.height === 'auto') ? me.parentNode.offsetHeight : options.height;

			// wrap content
			var wrapper = document.createElement("div");
			classie.addClass(wrapper,o.wrapperClass);
			wrapper.style.position= 'relative';
			wrapper.style.overflow= 'hidden';
			wrapper.style.width= o.width;
			wrapper.style.height= o.height;

			// update style for the div
			me.style.overflow= 'hidden';
			me.style.width= o.width;
			me.style.height= o.height;

			// create scrollbar rail
			rail = document.createElement("div");
			classie.addClass(rail,o.railClass);
			rail.style.width= o.size;
			rail.style.height= '100%';
			rail.style.position= 'absolute';
			rail.style.top= 0;
			rail.style.display= (o.alwaysVisible && o.railVisible) ? 'block' : 'none';
			rail.style['border-radius']= o.railBorderRadius;
			rail.style.background= o.railColor;
			rail.style.opacity= o.railOpacity;
			rail.style.zIndex= 90;

			// create scrollbar
			bar =  document.createElement("div");
			classie.addClass(bar,o.barClass);
			bar.style.background= o.color;
			bar.style.width= o.size;
			bar.style.position= 'absolute';
			bar.style.top= 0;
			bar.style.opacity= o.opacity;
			bar.style.display= o.alwaysVisible ? 'block' : 'none';
			bar.style['border-radius'] = o.borderRadius;
			bar.style.BorderRadius= o.borderRadius;
			bar.style.MozBorderRadius= o.borderRadius;
			bar.style.WebkitBorderRadius= o.borderRadius;
			bar.style.zIndex= 99;

			// set position
			if(o.position === 'right'){
				rail.style.right = o.distance;
				bar.style.right = o.distance;
			}
			else{
				rail.style.left = o.distance;
				bar.style.left = o.distance;
			}

			// wrap it
			domhelper.elementWrap(me,wrapper);

			// append to parent div
			me.parentNode.appendChild(bar);
			me.parentNode.appendChild(rail);

			// set up initial height
			getBarHeight();

			// make it draggable and no longer dependent on the jqueryUI
			bar.addEventListener("mousedown",mousedownEventHandler);
			document.addEventListener("mouseup",mouseupEventHandler);
			bar.addEventListener("selectstart",selectstartEventHandler);
			bar.addEventListener("mouseover",mouseoverEventHandler);
			bar.addEventListener("mouseleave",mouseleaveEventHandler);
			bar.addEventListener('touchstart',scrollContainerTouchStartEventHandler);

			rail.addEventListener("mouseover",railMouseOverEventHandler);
			rail.addEventListener("mouseleave",railMouseLeaveEventHandler);

			me.addEventListener("mouseover",scrollContainerMouseOverEventHandler);
			me.addEventListener("mouseleave",scrollContainerMouseLeaveEventHandler);
			me.addEventListener('DOMMouseScroll', mouseWheelEventHandler, false );
			me.addEventListener('mousewheel', mouseWheelEventHandler, false );
		}

		// check start position
		if (o.start === 'bottom'){
			// scroll content to bottom
			bar.style.top= me.offsetHeight - bar.offsetHeight;
			scrollContent(0, true);
		}
		else if (o.start !== 'top'){
			// assume jQuery selector
			scrollContent( domhelper.getPosition(document.querSelector(o.start).top, null, true));

			// make sure bar stays hidden
			if (!o.alwaysVisible) {
				domhelper.elementHideCss(bar);
			}
		}
		document.addEventListener('touchmove',scrollContainerTouchMoveEventHandler);
	}.bind(this);

	/**
	 * Removes the auto scrolling for touch devices.
	 * @memberOf slimscroll
	 * @private
	 */
	function getBarHeight(){
		if(!bar){
			bar = currentBar;
		}
		// calculate scrollbar height and make sure it is not too small
		barHeight = Math.max((me.offsetHeight / me.scrollHeight) * me.offsetHeight, minBarHeight);
		bar.style.height= barHeight + 'px' ;

		// hide scrollbar if content is not long enough
		var display = (me.offsetHeight === barHeight) ? 'none' : 'block';
		bar.style.display= display;
	}

	/**
	 * Removes the auto scrolling for touch devices.
	 * @function
	 */
	function scrollContent(y, isWheel, isJump,element,bar,isTouch){
		releaseScroll = false;
		var delta = y;
		me = element;
		bar = (bar)? bar : me.parentNode.querySelector('.'+o.barClass);
		var maxTop = me.offsetHeight - bar.offsetHeight;

		if (isWheel){
			// move bar with mouse wheel
			delta = parseInt(bar.style.
				top,10) + y * parseInt(o.wheelStep,10) / 100 * bar.offsetHeight;

			// move bar, make sure it doesn't go out
			delta = Math.min(Math.max(delta, 0), maxTop);

			// if scrolling down, make sure a fractional change to the
			// scroll position isn't rounded away when the scrollbar's CSS is set
			// this flooring of delta would happened automatically when
			// bar.css is set below, but we floor here for clarity
			delta = (y > 0) ? Math.ceil(delta) : Math.floor(delta);

			// scroll the scrollbar
			bar.style.top= delta + 'px';
		}
		else if(isTouch){
			// calculate actual scroll amount
			percentScroll = parseInt(bar.style.top,10) / (me.offsetHeight - bar.offsetHeight);
			delta = percentScroll * (me.scrollHeight - me.offsetHeight);

			// scroll the scrollbar
			bar.style.top= delta + 'px';
		}

		// calculate actual scroll amount
		percentScroll = parseInt(bar.style.top,10) / (me.offsetHeight - bar.offsetHeight);
		delta = percentScroll * (me.scrollHeight - me.offsetHeight);

		if (isJump){
			delta = y;
			var offsetTop = delta / me.scrollHeight * me.offsetHeight;
			offsetTop = Math.min(Math.max(offsetTop, 0), maxTop);
			bar.style.top= offsetTop + 'px';
		}

		// scroll content
		me.scrollTop=delta;

		// console.log("delta",delta,"~~delta",~~delta);
		// fire scrolling event
		// me.dispatchEvent(slimScrollEvent)
		var newevent = document.createEvent("Event");
		newevent.initEvent('slimscrolling',true,true,"blah");
		me.dispatchEvent(newevent, ~~delta);
		// me.trigger('slimscrolling', ~~delta);

		// ensure bar is visible
		showBar();

		// trigger hide when scroll is stopped
		hideBar();
	}
	function showBar(){
		// recalculate bar height
		getBarHeight();
		clearTimeout(queueHide);

		// when bar reached top or bottom
		if (percentScroll === ~~percentScroll){
			//release wheel
			releaseScroll = o.allowPageScroll;

			// publish approporiate event
			if (lastScroll !== percentScroll){
				var msg = (~~percentScroll === 0) ? 'top' : 'bottom';
				var newevent = document.createEvent("Event");
				newevent.initEvent('slimscroll',true,true);
				me.dispatchEvent(newevent, msg);
			}
		}
		else{
			releaseScroll = false;
		}
		lastScroll = percentScroll;

		// show only when required
		if(barHeight >= me.offsetHeight) {
			//allow window scroll
			releaseScroll = true;
			return;
		}
		bar.style.transition="opacity .5s";
		bar.style.opacity=o.opacity;
		if (o.railVisible) {
			rail.style.transform="opacity .5s";
			rail.style.opacity=1;
		}
	}

	function hideBar(){
		// only hide when options allow it
		if (!o.alwaysVisible){
			queueHide = setTimeout(function(){
				if (!(o.disableFadeOut && isOverPanel) && !isOverBar && !isDragg){
					bar.style.transition="opacity 1s";
					bar.style.opacity=0;
					rail.style.transition="opacity 1s";
					rail.style.opacity=0;
				}
			}, 500);
		}
	}

	function mouseWheelEventHandler(e){
		// use mouse wheel only when mouse is over
		if (!isOverPanel) { return; }

		var delta = 0;
		if (e.wheelDelta) {
			delta = -e.wheelDelta/120;
		}
		if (e.detail) {
			delta = e.detail / 3;
		}

		var target = e.target;
		var parentWrapper = domhelper.getParentElement(target,o.wrapperClass);
		if (parentWrapper /* && parentWrapper.isEqualNode(me.parentNode)*/ ){
			// scroll content
			scrollContent(delta, true,null,parentWrapper.querySelector('.'+o.addedOriginalClass));
		}
		else{
			console.log("not the right parent node");
		}

		// stop window scroll
		if (!releaseScroll) {
			e.preventDefault();
		}
	}

	function mousedownEventHandler(e){
		var eTarget = e.target;
		currentBar = eTarget;
		isDragg = true;
		mousedownT = parseInt(eTarget.style.top,10);
		mousedownPageY = e.pageY;
		if(currentBar){
			currentBar.addEventListener("mousemove",mousemoveEventHandler);
		}
		e.preventDefault();
		return false;
	}

	function mousemoveEventHandler(e){
		var currTop = mousedownT + e.pageY - mousedownPageY;
		if(currentBar){
			currentBar.style.top=currTop;
			scrollContent(0, domhelper.getPosition(currentBar).top, false,me,currentBar);// scroll content
		}
	}

	function mouseupEventHandler(e){
		isDragg = false;
		if(currentBar){
			hideBar(currentBar);
			currentBar.removeEventListener('mousemove',mousemoveEventHandler);
		}
	}

	function mouseoverEventHandler(e){ isOverBar = true; }

	function mouseleaveEventHandler(e){ isOverBar = false; }

	function selectstartEventHandler(e){
		// e.stopPropagation();
		// e.preventDefault();
		return false;
	}

	function railMouseOverEventHandler(e){ showBar(); }

	function railMouseLeaveEventHandler(e){ hideBar(); }

	function scrollContainerMouseOverEventHandler(e){
		isOverPanel = true;
		showBar(bar);
		hideBar(bar);
	}

	function scrollContainerMouseLeaveEventHandler(e){
		isOverPanel = true;
		showBar(bar);
		hideBar(bar);
	}

	function scrollContainerTouchStartEventHandler(e){
		// console.log(e.target);
		if (e.touches.length){
			// record where touch started
			currentTouchDif = e.touches[0].pageY;
		}
	}

	function scrollContainerTouchMoveEventHandler(e){
		// prevent scrolling the page if necessary
		if(!releaseScroll){
			e.preventDefault();
		}
		if(e.touches.length){
			// see how far user swiped
			var diff = (currentTouchDif - e.touches[0].pageY) / o.touchScrollStep;
			// scroll content
			scrollContent(diff, true,null,me,currentBar,true);
			currentTouchDif = e.touches[0].pageY;
		}
	}
};

module.exports = slimscroll;

// If there is a window object, that at least has a document property,
// define linotype
if ( typeof window === "object" && typeof window.document === "object" ) {
	window.slimscroll = slimscroll;
}