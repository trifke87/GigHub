var GigDetailsController = function (followingService) {
    var followingButton;
    var init = function () {

    };

    var toggleFollowing = function () {
        followingButton = $(e.target);

        var follweeId = followingButton.attr("data-user-id");

        if (followingButton.hasClass("btn-default"))

    };

    var done = function () {
        var text = (followingButton.text() == "Follow") ? "Following" : "Follow";
        followingButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something failed");
    };

    return {
        init: init
    }

}(FollowingService);


$(".js-toggle-follow").click(function (e) {
    
   
  
    
})