var AttendanceService = function () {
    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendances", { GigId: gigId })
            .done(done)
            .fail(fail);
    }

    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail)
    }

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}(); // Esta notacion se llama IEFI, es una notacionn especial en javascript que permite la ejecucion de codigo automaticamente. es como un inicializador.
