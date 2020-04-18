var GigDetailsController = function (followingService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-following", toogleFollowing);
    }

    var toogleFollowing = function (e) {
        button = $(e.target);
        var followeeId = button.attr("data-artist-id");

        if (button.hasClass("btn-default"))
            followingService.createFollowing(followeeId, done, fail);
        else
            followingService.deleteFollowing(followeeId, done, fail);
    };

    var fail = function () {
        alert("Something Fail");
    }

    var done = function () {
        var text = (button.text() == "Follow") ? "Following" : "Follow";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    return {
        Init: init
    }

}(FollowingService);