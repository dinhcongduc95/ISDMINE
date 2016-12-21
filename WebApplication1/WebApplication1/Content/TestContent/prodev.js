function TrimMark(strText) {

    if (strText != null && strText != '') {
        while (strText.indexOf(',') == 0) {
            strText = strText.substring(1, strText.length);
        }

        while (strText.lastIndexOf(',') == strText.length - 1) {
            strText = strText.slice(0, strText.length - 1);
        }

        while (strText.indexOf(',,') >= 0) {
            strText = strText.replace(',,', ',');
        }

        return strText;
    }
    return '';
}

function Contains(lsItems1, item) {
    for (var i = 0; i < lsItems1.length; i++) {
        var item1 = lsItems1[i];
        if (item1 == item) {
            return true;
        }
    }
    return false;
}

function loadProgessAndProcess(show) {
    if (show) {
        $('#frmProgessBar').css("top", "0");
        $('#frmProgessBar').css("left", "0");
        $('#frmProgessBar').css("right", "0");
        $('#frmProgessBar').css("bottom", "0");
        $('#frmProgessBar').css("height", "100%");
        $('#frmProgessBar').css("width", "100%");
        $('#frmProgessBar').css("position", "fixed");
        $('#frmProgessBar').css("background-color", "#435154");
        $('#frmProgessBar').css("opacity", "0.5");
        $('#frmProgessBar').css("filter", "alpha(opacity=50)");
        $('#frmProgessBar').css("display", "block");
        $('#frmProgessBar').css("z-index", "1000");
    } else {
        $('#frmProgessBar').css("top", "0");
        $('#frmProgessBar').css("left", "0");
        $('#frmProgessBar').css("right", "0");
        $('#frmProgessBar').css("bottom", "0");
        $('#frmProgessBar').css("height", "100%");
        $('#frmProgessBar').css("width", "100%");
        $('#frmProgessBar').css("position", "fixed");
        $('#frmProgessBar').css("background-color", "#435154");
        $('#frmProgessBar').css("opacity", "0.5");
        $('#frmProgessBar').css("filter", "alpha(opacity=50)");
        $('#frmProgessBar').css("display", "none");
        $('#frmProgessBar').css("z-index", "1000");
    }
}

function GetBrowser() {
    var N = navigator.appName, ua = navigator.userAgent, tem;
    var M = ua.match(/(opera|chrome|safari|firefox|msie)\/?\s*(\.?\d+(\.\d+)*)/i);
    if (M && (tem = ua.match(/version\/([\.\d]+)/i)) != null) M[2] = tem[1];
    M = M ? [M[1], M[2]] : [N, navigator.appVersion, '-?'];
    return M;
}

function MenuChangeSize() {
    var menu = $('#accordian');
    if (menu == null) {
        return;
    }
    var bodyHeight = $(document).height();
    var windowsHeight = $(window).height();
    console.log("bodyHeight: " + bodyHeight + ", windowsHeight: +" + windowsHeight);
    if (bodyHeight > windowsHeight ) {
        console.log("has scroll");
        // Có scrol bar
        menu.css("margin-left", "14px");
    }
    if(bodyHeight <= windowsHeight)
    {
        console.log("no scroll");
        // Không có scrol bar
        menu.css("margin-left", "10px");
    }
}

function resizeChange() {
    var idpercentComplete = $('#percentComplete');
    var tbPercentCompleted = $('#tbPercentCompleted');
    if (idpercentComplete != null && tbPercentCompleted != null) {
        var width = idpercentComplete.width();
        var widthChange = width - 60;
        tbPercentCompleted.width(widthChange);
    }

    var browser = readCookie('browser');
    if (browser == null || browser == '' || browser == "undefined") {
        var client = String(GetBrowser());

        if (client != '') {
            browser = client.split(',')[0];
            createCookie('browser', browser, 30);
        }
    }

    var idVideoHeader = $('#idVideoHeader');
    var textIntrol = $('#textIntroduce');
    if (idVideoHeader != null && idVideoHeader.width() > 0) {
        SetSizeVideo(idVideoHeader, browser);
    }

    if (textIntrol != null && textIntrol.width() > 0) {
        SetSizeVideo(textIntrol, browser);
    }
}

function SetSizeVideo(idVideoHeader, browser) {

    var videoIntroduce = $('#videoIntroduce');
    var prodevVideo = $('#prodev-video');
    var playbutton = $('.video-js .vjs-big-play-button');
    var videoYoutube = $('#frmVideo');
    if (videoIntroduce != null && prodevVideo != null && playbutton != null) {
        var widthHeader = idVideoHeader.width();

        switch (browser.toLowerCase()) {
            case "chrome":
                {
                    videoIntroduce.width(widthHeader * 98.796 / 100);
                    videoIntroduce.height(widthHeader * 55.66 / 100);
                    prodevVideo.width(widthHeader * 88.128 / 100 + 5);
                    prodevVideo.height(widthHeader * 49.862 / 100);
                 //   videoYoutube.attr("width", widthHeader * 88.128 / 100 + 5);
                //    videoYoutube.attr("height", widthHeader * 49.862 / 100);

                    var fontSize = widthHeader * 4.8702 / 100 + "px";
                    var wiHePlay = widthHeader * 14.1469 / 100 + "px";
                    playbutton.css("font-size", fontSize);
                    playbutton.css("width", wiHePlay);
                    playbutton.css("height", wiHePlay);
                   
                    break;
                }

            case "firefox":
                {
                    videoIntroduce.width(widthHeader * 99.8397 / 100);
                    videoIntroduce.height(widthHeader * 56.431 / 100);
                    prodevVideo.width(widthHeader * 99.8397 / 100);
                    prodevVideo.height(widthHeader * 56.431 / 100);
                //    videoYoutube.attr("width", widthHeader * 99.8397 / 100);
                //    videoYoutube.attr("height", widthHeader * 56.431 / 100);

                    var firefoxSize = widthHeader * 5.275 / 100 + 1 + "px";
                    var firefoxHePlay = widthHeader * 15.716 / 100 + 1 + "px";
                    playbutton.css("font-size", firefoxSize);
                    playbutton.css("width", firefoxHePlay);
                    playbutton.css("height", firefoxHePlay);
                    break;
                }

            case "msie":
                {
                    videoIntroduce.width(widthHeader * 99 / 100);
                    videoIntroduce.height(widthHeader * 55.66 / 100);
                    prodevVideo.width(widthHeader * 80 / 100);
                    prodevVideo.height(widthHeader * 45 / 100);
                   // videoYoutube.attr("width", widthHeader * 80 / 100);
                  //  videoYoutube.attr("height", widthHeader * 45 / 100);

                    var ieSize = widthHeader * 5.275 / 100 + 1 + "px";
                    var ieHePlay = widthHeader * 15.716 / 100 + 1 + "px";
                    playbutton.css("font-size", ieSize);
                    playbutton.css("width", ieHePlay);
                    playbutton.css("height", ieHePlay);
                    break;
                }
            default:
                {
                    videoIntroduce.width(widthHeader * 99.8397 / 100);
                    videoIntroduce.height(widthHeader * 56.431 / 100);
                    prodevVideo.width(widthHeader * 99.8397 / 100);
                    prodevVideo.height(widthHeader * 56.431 / 100);
                //    videoYoutube.attr("width", widthHeader * 99.8397 / 100);
                 //   videoYoutube.attr("height", widthHeader * 56.431 / 100);

                    var dSize = widthHeader * 4.8 / 100 + 1 + "px";
                    var dHePlay = widthHeader * 14.1 / 100 + 1 + "px";
                    playbutton.css("font-size", dSize);
                    playbutton.css("width", dHePlay);
                    playbutton.css("height", dHePlay);
                    break;
                }
        }
    }
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEq = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEq) == 0) return c.substring(nameEq.length, c.length);
    }
    return null;
}

function GetVideoStreaming(server, port, folder, type, name, streamingType) {

    shaka.polyfill.installAll();

    var video = document.getElementById('prodev-video');
    var player = new shaka.player.Player(video);
    window.player = player;

    player.addEventListener('error', function (event) {
        console.error(event);
    });
    StreamingVideo(server, port, folder, type, name, streamingType);
}

function StreamingVideo(server, port, folder, type, name, streamingType) {
    var urlStrm = server + port + folder + type + ":" + name + streamingType;
    var estimator = new shaka.util.EWMABandwidthEstimator();
    var source = new shaka.player.DashVideoSource(urlStrm, null, estimator);
    //console.log("start");
    player.load(source);
    var video = document.getElementById('prodev-video');
    
    if (video != null) {
        var timer = 0;
        video.addEventListener('progress', function (e) {
            console.log("lenth: " + this.buffered.length);
            if (this.buffered.length > 0) {
                $('#prodev-video').attr("poster", "");
                console.log("playing");
                //if (timer != 0)
                //{
                //    clearTimeout(timer);
                //}
                //console.log("duration: " + video.duration);
                //timer = setTimeout(function () {
                //    if (parseInt(video.buffered.end() / video.duration * 100) == 100)
                //    {
                //        // video has loaded.... 
                //        console.log("loaded");
                //    } else {
                //        console.log("is loading...");
                //    }

                //}, 300);
                //console.log(timer);
            } else {
                console.log("loading");
            }
        }, false);
    } else
    {
        console.log("no");
    }
   

     //console.log(percent);
    //if (player.load(source)) {
    //    player.addEventListener('loadeddata', function () {
    //        console.log("Video Duration: " + $('#prodev-video').duration);
    //    }, false);
    //    console.log(player.currentTime);
    //    MediaElement('prodev-video',
    //        {
    //            success: function (me) {
    //                me.play();
    //                me.addEventListener('timeupdate', function () {
    //                    //document.getElementById('time').innerHTML = me.currentTime;
    //                    console.log(me.currentTime);
    //                }, false);

    //                document.getElementById('pp')['onclick'] = function () {
    //                    if (me.paused)
    //                        me.play();
    //                    else
    //                        me.pause();
    //                };
    //            }
    //        });
    //}
}

function videoScreen() {
    var vdo = document.getElementById("prodev-video");
    vdo.requestFullscreen();
}

function vidplay() {
    var video = document.getElementById("prodev-video");
    $('#sliderSpeaker').val(video.volume * 100);
    var playImg = $('#play');
    // var button = document.getElementById("play");
    if (video.paused) {
        video.play();
        playImg.attr("src", "/Content/images/prodev-pause-icon.png");
    } else {
        video.pause();
        playImg.attr("src", "/Content/images/prodev-play-icon.png");
        // button.textContent = ">";
    }
}

function restart() {
    var video = document.getElementById("prodev-video");
    video.currentTime = 0;
}

function skip(value) {
    var video = document.getElementById("prodev-video");
    video.currentTime += value;
}

function onTrackedVideoFrame(secondsCurrentTime, secondsDuration) {
    duration = secondsDuration;
    var percentPlayed = secondsCurrentTime * 100 / secondsDuration - 1.2;
    var percentNotYet = 100 - percentPlayed;
    $('#idPlayed').css("width", percentPlayed + "%");
    $('#idNotPlay').css("width", percentNotYet + "%");

    var finalCurrent = GetFinalTime(secondsCurrentTime);
    var finalDuration = GetFinalTime(secondsDuration);

    $("#current").text(finalCurrent + "/" + finalDuration);
    //$("#duration").text(minutesDuration);
}

function formatSeconds(seconds) {
    var date = new Date(1970, 0, 1);
    date.setSeconds(seconds);
    return date.toTimeString().replace(/.*(\d{2}:\d{2}:\d{2}).*/, "$1");
}

function showTime(finalTime) {

    $('#idTimehv').html(finalTime);
    $('#idTimehv').css("display", "block");
}

function hideTime() {
    $('#idTimehv').css("display", "none");
}

function GetFinalTime(time) {
    var minutes = formatSeconds(time).split(':');
    var finalTime = '';
    if (minutes[0] == "00") {
        finalTime = minutes[1] + ":" + minutes[2];
    }
    else {
        finalTime = minutes[0] + ":" + minutes[1] + ":" + minutes[2];
    }
    return finalTime;
}

function GetYoutube(obj) {
    var link = obj + "?autoplay=1&cc_load_policy=1";
    $('#frmVideo').attr("src", link);
}






