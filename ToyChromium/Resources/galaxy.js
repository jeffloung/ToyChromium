/*
* 说明：
* */
var isDebug = true;

$(function () {
    if (isDebug) {
        console.log('脚本运行开始');
    }

    removeItem();
    addInfo();

    if (isDebug) {
        console.log('脚本运行结束');
    }
});

function removeItem() {
    $('span').css('pointer-events', 'none');

    setTimeout(function () {
        $('#icon-nav').remove();
        $('#about').remove();
    }, 2000);
    setTimeout(function () {
        if ($('#icon-nav').length > 0) {
            $('#icon-nav').remove();

        }
        if ($('#about').length > 0) {
            $('#about').remove();
        }
    }, 20000);
}

function addInfo() {
    let style = "z-index:99999;position:absolute;top:100px;left:0;height:64px;width:200px;color:#fff;background-color:rgba(238,238,238,.14);padding:20px;";
    $("#layout").append('<div style="' + style + '"><h2>触摸银河系</h2><br><span>拖动画面，探索银河系</span></div>');
}