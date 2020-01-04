(function run() {
    //alert('run');
    removeItem();
    other();
})();

function removeItem() {
    document.querySelector('#shared-sidebar').remove()
}

function other() {
    document.querySelector('.main-assistant-detector-container').style.backgroundColor='#cccccc'
}