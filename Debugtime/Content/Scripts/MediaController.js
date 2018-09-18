(function () {
    var $video = document.querySelector("#video"),
        $btnStart = $("#btnStart"),
        $playPause = $("#playPause"),
        $btnVolume = $("#volume"),
        $timeNow = $("#currentTime"),
        $totalTime = $("#totalTime"),
        $scrubber = $("#dtmScrubber"),
        $btnExpand = $("#btnExpand"),
        $volumeScrubber = $("#volumeScrubber"),
        $videoDuration = $(".dtm-video-duration");


    //time formate method
    function formatTime(seconds) {
        seconds = Math.round(seconds);
        var minutes = 0;

        if (seconds >= 60) {
            minutes = Math.floor(seconds / 60);
            seconds = seconds - (minutes * 60);
        }

        seconds = seconds + '';
        if (seconds.length === 1 || minutes.length === 1) {
            seconds = '0' + seconds;
            minutes = '0' + minutes;
        }
        return minutes + ':' + seconds;
    };

    $video.addEventListener("loadedmetadata", function () {
        $videoDuration.removeClass("display-non").addClass("display-inlineblock");
        $videoDuration.text(formatTime(video.duration));
    }, true);


    $btnStart.click(function (e) {
        $video.play();

        $playPause.children("i").addClass("fa-pause").removeClass("fa-play");
        $(".dtm-bottom-controls").removeClass("display-non").addClass("display-table");
        $btnStart.children("i").addClass("fa-pause-circle").removeClass("fa-play-circle");
        $btnStart.fadeOut();
    });

   
    $playPause.click(function () {
        if ($(this).children("i").hasClass("fa-repeat"))
        {
            $video.currentTime = 0;
            $video.play();
            $(this).children("i").removeClass("fa-repeat").addClass("fa-pause");
            return;
        }

        if ($video.paused || $video.ended) {
            $video.play();
            $(this).children("i").addClass("fa-pause").removeClass("fa-play");
        }
        else {
            $video.pause();
            $(this).children("i").addClass("fa-play").removeClass("fa-pause");
        }
    });

    $video.addEventListener("play", function () {
        $videoDuration.removeClass("display-inlineblock").addClass("display-non");

        $totalTime.text(formatTime($video.duration));

        $volumeScrubber.height(100 - ($video.volume * 100) + "%");
    }, true);


    $btnVolume.click(function () {
        $video.muted = !video.muted;

        if (video.muted) {
            $(this).children("i").removeClass("fa-volume-up").addClass("fa-volume-off");

        } else {
            $(this).children("i").removeClass("fa-volume-off").addClass("fa-volume-up");
        }

    });

    $video.addEventListener("timeupdate", function () {
        var valueNow = (video.currentTime / video.duration) * 100;

        $timeNow.text(formatTime(video.currentTime));
        $scrubber.css("width", valueNow + "%");
    }, true);

    $video.addEventListener("progress", function () {
        var index = (video.buffered.length - 1);
        var end = video.buffered.end(index);
        var length = (end / video.duration) * 100;
        var prog = document.getElementById("dtmProgress");
        prog.style = "width:" + length + "%;";
    }, true);


    var $timeRail = $("#dtmRail");

    $timeRail.click(function (e) {
        var xCord = e.pageX - $timeRail.offset().left;

        var width = (xCord / this.clientWidth) * 100;

        var time = (width / 100) * video.duration;

        $scrubber.css('width', width + "%");
        $video.currentTime = time;
        if ($video.ended)
            $playPause.click();
    });

    var $volumeRail = $("#volumeRail");

    $volumeRail.click(function (e) {
        var scrubberHeight = e.pageY - $volumeRail.offset().top;
        $volumeScrubber.height(scrubberHeight);

        var totalHeight = 130;

        var volume = 1 - (scrubberHeight / totalHeight);

        $video.volume = volume;

    });

    $video.addEventListener("ended", function () {
        $video.pause();
        $playPause.children("i").removeClass("fa-pause").addClass("fa-repeat");
    }, true);



    $video.addEventListener("error", function (e) {
        //$(".dtm-media-container").addClass("display-non");
        $(".dtm-bottom-controls").addClass("display-non".removeClass("display-inlineblock"));
        alert("An Error Occured");
    }, true);

    function expandToFullscreen(element) {

        $("#player-container").removeClass().addClass("dtm-player-container-fullscreen");
        $("#mediaContainer").removeClass().addClass("dtm-media-container-fullscreen");
        $("#video").removeClass().addClass("dtm-video-element-fullscreen");
        if (element.requestFullscreen) {
            element.requestFullscreen();
        } else if (element.mozRequestFullscreen) {
            element.mozRequestFullscreen();
        } else if (element.webkitRequestFullScreen) {
            element.webkitRequestFullScreen();
        } else if (element.msRequestFullscreen) {
            element.msRequestFullscreen();
        }

        $btnExpand.children("i").removeClass("fa-expand").addClass("fa-compress");
    }

    function exitToFullscreen() {
        $("#player-container").removeClass().addClass("dtm-player-container");
        $("#video").removeClass().addClass("dtm-video-element");
        $("#mediaContainer").removeClass().addClass("dtm-media-container");
        if (document.exitFullscreen) {
            document.exitFullscreen();
        } else if (document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        }

        $btnExpand.children("i").removeClass("fa-compress").addClass("fa-expand");
    }

    $btnExpand.click(function () {

        if ($(this).children("i").hasClass("fa-compress")) {
            exitToFullscreen();
        } else if ($(this).children("i").hasClass("fa-expand")) {
            expandToFullscreen(document.getElementById("player-container"));
        }
    });

}());






