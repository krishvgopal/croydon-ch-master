
loadUsersForAssigning();
doTypeSearch();

function showAutoProcess() {
    if ($("#intrayStatus").val() == "1" && $("#actionStatus").val() == "1" ) {
        $("#autoComplete").css("visibility", "");
    } else {
        $("#autoComplete").css("visibility", "hidden");
    }
    doTypeSearch();
}

function doTypeSearch() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetAutomaticOutstandingGroups",
        data: "{'userId':'" + $("#userList").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#dataTableTypes").dataTable({
                "destroy": true,
                "paging": false,
                "bFilter": false,
                "aaData": result,
                "aoColumns": [ { mData: 'Key' },{ mData: 'Value' } ],
                "aoColumnDefs": [ { "sTitle": "Correspondence Type", "aTargets": ["group_name"], "mRender": function (value, type, full) { return '<a href="#" onClick="doItemSearch('+ full.Key +');">' + full.Value + '</a>'; } }, { "aTargets": ["group_id"], "bVisible": false } ],
            });
        }
    });
}

function doItemSearch(typeId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetAutomaticOutstandingGroups",
        data: "{'userId':'" + $("#userList").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#dataTableTypes").dataTable({
                "destroy": true,
                "paging": false,
                "bFilter": false,
                "aaData": result,
                "aoColumns": [{ mData: 'Key' }, { mData: 'Value' }],
                "aoColumnDefs": [{ "sTitle": "Correspondence Type", "aTargets": ["group_name"], "mRender": function (value, type, full) { return '<a href="#" onClick="doItemSearch(' + full.Key + ');">' + full.Value + '</a>'; } }, { "aTargets": ["group_id"], "bVisible": false }],
            });
        }
    });
}

function loadUsersForAssigning() {
    $('#userList').val('');
    $('#userList').append($('<option>', {
        value: 0,
        text: "Unassigned"
    }));
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetSystemUsers",
        data: "{'showInvalid':'false'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                $('#userList').append($('<option>', {
                    value: item.UserId,
                    text: item.UserName
                }));
            });
        }
    });
}