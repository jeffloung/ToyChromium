(function run() {
    //alert('run');
    run_firsh();
})();

function run_firsh() {
    setTimeout(function () {
        var list = document.querySelector('.wrap').childNodes
        document.querySelector('.wrap').innerHTML = list[1].innerHTML
    },500)
	



	
	var m10  = 1000*10;		//间隔5秒刷新一次
	var m30 = 1000*60*30; 	//间隔30分钟刷新一次
	var m60 = 1000*60*60;	//间隔60分钟刷新一次
	setInterval(autoReload, m30); //第二参数是时间,单位是毫秒

	
	
}
function autoReload(){
	console.log((new Date())+',autoReload()');
	window.location.reload();
}
