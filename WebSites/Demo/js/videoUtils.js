var divElement = $(".playOnMouseHover");
var mouseHoverVideos = divElement.find("video");

[].forEach.call(divElement, function (item,index) {
    item.addEventListener('mouseover', hoverVideo.bind(item,index), false);
    item.addEventListener('mouseout', hideVideo.bind(item,index), false);
});

[].forEach.call(divElement, function (item,index) {
    item.addEventListener('mouseover', hoverVideo.bind(item,index), false);
    item.addEventListener('mouseout', hideVideo.bind(item,index), false);
});

function hoverVideo(index, e) {
    mouseHoverVideos[index].play();    
}

function hideVideo(index, e) {
    mouseHoverVideos[index].pause();     
}

var viewEnterVideos = $(".playOnEnterView");

function checkScroll() {
    var fraction = 80;

    for(var i = 0; i < viewEnterVideos.length; i++) {

        var video = viewEnterVideos[i];

        var x = video.offsetLeft, y = video.offsetTop, w = video.offsetWidth, h = video.offsetHeight, r = x + w,
            b = y + h,
            visibleX, visibleY, visible;

            visibleX = Math.max(0, Math.min(w, window.pageXOffset + window.innerWidth - x, r - window.pageXOffset));
            visibleY = Math.max(0, Math.min(h, window.pageYOffset + window.innerHeight - y, b - window.pageYOffset));

            visible = visibleX * visibleY / (w * h);

            if (visible === 1) {
                video.play();
            } else {
                video.pause();
            }

    }

}

window.addEventListener('scroll', checkScroll, false);
window.addEventListener('resize', checkScroll, false);