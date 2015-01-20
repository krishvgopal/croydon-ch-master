



function createNewNote() {
    $.ajax({
        type: "POST",
        url: "../DataService.aspx/CreateContactNote",
        data: "{'userId':'" + $("#debtData").attr("userId") + "','pin':'" + $("#debtData").attr("pin") + "','uprn':'" + $("#debtData").attr("uprn") + "','debtId':'" + $("#debtData").attr("debtId") + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#debtData").attr("noteId", result.d);
            loadDebtorNote();
        },
        failure: function (error) {
            // TODO: HANDLE ERROR
            console.log(error);
        }
    });
}
function loadDebtsList(noteId) {
    $.ajax({
        type: "POST",
        url: "../DataService.aspx/GetNoteDebts",
        data: "{'noteId':'" + noteId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
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
        url: "../DataService.aspx/GetDebtNoteCategories",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
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
function loadDebtorNote() {
    loadCategories();
    loadDebtsList($("#debtData").attr("noteId"));
    $.ajax({
        type: "POST",
        url: "../DataService.aspx/GetDebtorNote",
        data: "{'noteId':'" + $("#debtData").attr("noteId") + "'}",
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
function saveNote() {

    //console.log("{'noteId':'" + $("#debtData").attr("noteId") + "','userId':'" + $("#debtData").attr("userId") + "','pin':'" + $("#debtData").attr("pin") + "','debtId':'" + $("#debtData").attr("debtId") + "','categoryId':'" + $("#category").val() + "','theirRef':'" + $("#theirRef").val() + "','reason':'" + $("#reason").val() + "','content':'" + $("#noteText").val() + "','newLandLine':'" + $("#newPhone").val() + "','newMobile':'" + $("#newMobile").val() + "','newEmail':'" + $("#newEmail").val() + "'}");

    $.ajax({
        type: "POST",
        url: "../DataService.aspx/SaveDebtorNote",
        data: "{'noteId':'" + $("#debtData").attr("noteId") + "','userId':'" + $("#debtData").attr("userId") + "','pin':'" + $("#debtData").attr("pin") + "','debtId':'" + $("#debtData").attr("debtId") + "','categoryId':'" + $("#category").val() + "','theirRef':'" + $("#theirRef").val() + "','reason':'" + $("#reason").val() + "','content':'" + $("#noteText").val() + "','newLandLine':'" + $("#newPhone").val() + "','newMobile':'" + $("#newMobile").val() + "','newEmail':'" + $("#newEmail").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {

            // TODO : HANDLE RETURN
        },
        failure: function (error) {
            // TODO: HANDLE ERROR
            console.log(error);
        }
    });
}
function loadForm() {
    $("#debtData").attr("noteId",   QueryString()["noteId"]);
    $("#debtData").attr("debtId",   QueryString()["debtId"]);
    $("#debtData").attr("userId",   QueryString()["userId"]);
    $("#debtData").attr("pin",      QueryString()["pin"]);
    $("#debtData").attr("uprn",     QueryString()["uprn"]);
    if (QueryString()["m"] == "n") { //m = mode, n = new
        createNewNote();
    }
    if (QueryString()["m"] == "r") { //m = mode, r = read
        loadDebtorNote();
    }
}

