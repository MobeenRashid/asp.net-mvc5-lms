$(function () {

  //foundation startup
  $(document).foundation();

  $("#primaryMenuBar").click(function () {
    $(".primary-nav-mobile").toggleClass("translate-out-top", "translate-in-top")
  });

  $(".mobile-nav-cancel").click(function () {
    $(".primary-nav-mobile").toggleClass("translate-out-top", "translate-in-top")
  });

  $(".course-review-container .rating").hover(function () {
    $(".course-review-container .rating i").removeClass("fa-star-o").addClass("fa-star");
  });

  var topBar = $(".dtm-top-bar").height();
  topBar += $(".dtm-top-bar").padding;

  var footer = $(".footer").height();
  footer += $(".footer").padding;
  
  var height = window.screen.availHeight - topBar - footer;

  console.log(height);

  //ApplicationUserController.attachEventsOnDocumentLoad();
  //ApplicationUserController.toggleFollowing();
  //mediaController.btnStartClick();
  //mediaController.btnMuteClicked();
  //mediaController.btnPlayPauseClick();
  //mediaController.btnRewindClick();
  //mediaController.onVideoMetaLoad();
  //mediaController.onVideoPlay();
  //mediaController.onVideoTimeUpdate();
  //mediaController.onVideoProgress();
  //mediaController.onTimeRailClick();
  //mediaController.onVolumeRailClick();
  //mediaController.onVideoExpandClick();

});


