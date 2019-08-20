/*
 * 英仙座项目
* 说明：
* 1.此插件需要用到上一级目录中的pem文件，以便生成的插件id相同
* */
var isDebug = true;

/*$(function () {
    if (isDebug) {
        console.log('脚本运行开始');
    }
    alert("load");
    removeItem();
    changeItem();
    changePos();

    $('h2').text($(this).find('option:selected').text());

    addReset();

    if (isDebug) {
        console.log('脚本运行结束');
    }
});

function removeItem() {
    $('.description').remove();
    $('.share-buttons').remove();
    $('#summary').css('width', '420px');
    $('.shower-details-container.iframe-hide-container').children(':eq(1)').remove();
    $('#view-all-summary').remove();
    $('#instructions').remove();
    $('#copyright').remove();
    $('#sky-map').remove();
    setTimeout(function () {
        $('.dg.ac').remove();
        $('#cf_alert_div').remove();
    }, 2000);
    setTimeout(function () {
        if ($('.dg.ac').length > 0) {
            $('.dg.ac').remove();
        }
        if ($('#cf_alert_div').length > 0) {
            $('#cf_alert_div').remove();
        }
    }, 20000)
}

var showerSelect = $('#shower-select');
function changeItem() {
    $('.shower-details-container.iframe-hide-container').children('p').prop('firstChild').nodeValue = '浏览 ';
    $('#current-date').parent().prop('firstChild').nodeValue = '于 ';

    $('#shower-select').children(':eq(0)').text('天琴座流星雨-四月');
    $('#shower-select').children(':eq(1)').text('宝瓶座η流星雨-五月初');
    $('#shower-select').children(':eq(2)').text('宝瓶座δ南流星雨-七月末');
    $('#shower-select').children(':eq(3)').text('摩羯座α流星雨-七月末');
    $('#shower-select').children(':eq(4)').text('英仙座流星雨-八月中旬');
    $('#shower-select').children(':eq(5)').text('金牛座南流星雨-十月');
    $('#shower-select').children(':eq(6)').text('猎户座流星雨-十月末');
    $('#shower-select').children(':eq(7)').text('金牛座北流星雨-十一月');
    $('#shower-select').children(':eq(8)').text('狮子座流星雨-十一月中旬');
    $('#shower-select').children(':eq(9)').text('双子座流星雨-十二月中旬');
    $('#shower-select').children(':eq(10)').text('小熊座流星雨-十二月中旬');
    $('#shower-select').children(':eq(11)').text('象限仪座流星雨-一月初');
    $('#shower-select').children(':eq(12)').text('天鹅座κ流星雨-八月中旬');
    $('#shower-select').children(':eq(13)').text('波江座ω流星雨-十一月中旬');
    $('#shower-select').children(':eq(14)').text('显示全部');
    $('#shower-select').children(':eq(15)').text('选择一个流星雨……');

    $('#shower-select').change(function () {
        var selectText = $(this).find('option:selected').text();
        if (selectText == '显示全部') {
            $('#normal-summary').css('display', 'inline');
        } else if (selectText == '复位') {
            window.location.href = '/';
        }

        console.log(selectText);
        $('h2').text(selectText);
    })
}

var summary = $('#summary');
function changePos() {
    setTimeout(function () {
        let viewHeight = $(window).height();
        summary.css('top', viewHeight - summary.height());
    }, 1000);
}

//增加复位功能
function addReset() {
    let addhtml = '<option value="none">复位</option>';
    $('#shower-select').append(addhtml);
}
*/