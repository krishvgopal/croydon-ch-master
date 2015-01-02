



function createNewNote(pin, uprn) {



}
function loadDebtsList(noteId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetNoteDebts",
        data: "{'noteId':'" + noteId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                console.log(item.ArrangementName);
                $('#assignTo').append($('<option>', {
                    value: item.DebtId,
                    text: item.DebtSource + ', [' + item.DebtAccRef + '], £' + item.DebtOutstanding
                }));
            });
        },
        failure: function (error) {
            // TODO: HANDLE ERROR
            console.log(error);
        }
    });
}
function loadCategories() {

    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetDebtNoteCategories",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                console.log(item.ArrangementName);
                $('#category').append($('<option>', {
                    value: item.Code,
                    text: item.Value
                }));
            });
        },
        failure: function (error) {
            // TODO: HANDLE ERROR
            console.log(error);
        }
    });

}
function loadDebtorNote(noteId) {

    loadCategories();
    loadDebtsList(noteId);

    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetDebtorNote",
        data: "{'noteId':'" + noteId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#ourRef").text(result.d.OurRef);
            $("#theirRef").val(result.d.TheirRef);
            $("#reason").val(result.d.NoteReason);
            $("#noteText").val(result.d.NoteContent);
            $("#oldMobile").text(result.d.MobilePhone);
            $("#oldPhone").text(result.d.FixedPhone);
            $("#oldEmail").text(result.d.Email);
            $("#subject").val(result.d.Subject);
            $("#subjectAddress").val(result.d.SubjectAddress);
        },
        failure: function (error) {
            // TODO: HANDLE ERROR
            console.log(error);
        }
    });
}

