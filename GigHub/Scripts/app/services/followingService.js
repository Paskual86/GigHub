var FollowingService = function () {
    var createFollowing = function (followeeId, done, fail) {
        $.post("/api/followings", { FolloweeId: followeeId })
            .done(done)
            .fail(fail);
    }

    var deleteFollowing = function (followeeId, done, fail) {
        $.ajax({
            url: "/api/followings/" + followeeId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail)
    }

    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }
}(); // Esta notacion se llama IEFI, es una notacionn especial en javascript que permite la ejecucion de codigo automaticamente. es como un inicializador.
