

/*******************************TAB切换***********************************/

function tabSlider(obj,objs,objsn){
	
	//$(obj + ':first').addClass('cata_til_onck');
	//$(objsn + ':first').css('display','block');
	//autoroll();
	hookThumb();
	
	var i=-1;//第i+1个tab开始
		
	function autoroll(){
		nca = $(obj).length-1;
		i++;
		if(i > nca){
		i = 0;
		}
		slide(i);
	}
	
	function slide(i){
		$(obj).eq(i).addClass(objs).siblings().removeClass(objs);
		$(objsn).eq(i).css('display','block').siblings(objsn).css('display','none');
	}
	
	function hookThumb(){    
		$(obj).hover(
		function(){
				i = $(this).prevAll().length;
				slide(i); 
		},function(){
			$(obj).eq(i).removeClass(objs);
		}); 
	}
}


function tabSliderclock(obj,objs,objsn){
	$(function(){
		$(obj + ':first').addClass('cata_til_onck');
		$(objsn + ':first').css('display','block');
		autoroll();
		hookThumb();
	})
	 var i=-1;
		
	function autoroll(){
		nca = $(obj).length-1;
		i++;
		if(i > nca){
		i = 0;
		}
		slide(i);
	}
	
	function slide(i){
		$(obj).eq(i).addClass(objs).siblings().removeClass(objs);
		$(objsn).eq(i).css('display','block').siblings(objsn).css('display','none');
	}
	
	function hookThumb(){    
		$(obj).click(
		function(){
				i = $(this).prevAll().length;
				slide(i); 
		}); 
	}
}
//tab开始
$(document).ready(function(){
	tabSliderclock(".news_tab a","tabacur",".news_tabcon");
	tabSlider("","","");
	tabSliderclock(".app-list-ul li","app-tab-active",".app-list-content");
})

