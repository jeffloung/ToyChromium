(function run() {
    //alert('run');
    removeItem();
    //other();
})();

function removeItem() {
    var list = document.querySelector('.wrap').childNodes
    document.querySelector('.wrap').innerHTML = list[1].innerHTML
}

function other() {
    document.querySelector('.main-assistant-detector-container').style.backgroundColor='#cccccc'
}