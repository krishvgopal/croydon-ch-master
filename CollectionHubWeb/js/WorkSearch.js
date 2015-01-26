$("#loadingImage").hide();
$("#searchResults").hide();

$(":text").keypress(function (arg) {
    if (arg.which == 13) {
        doSearch();
    }
});

loadCycles();
loadStreams();
loadUsersForAssigning();

function doSearch() {

    //$("#loadingImage").toggle();
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SearchWorkCases",
        data: "{'sourceList':'" + $("#debtStreamCode").val() + "', 'amountFrom':'" + $("#amountFrom").val() + "', 'amountTo':'" + $("#amountTo").val() + "', 'cycleId':'" + $("#currentTreatmentCycle").val() + "', 'daysSinceIssued':'" + $("#daysOld").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result.d);
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#searchTable").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'DebtId'},
                    { mData: 'DebtSource'},
                    { mData: 'SourceAccountReference' },
                    { mData: 'DebtTotal'},
                    { mData: 'OutstandingBalance' },
                    { mData: 'DebtDate'},
                    { mData: 'Pin' },
                    { mData: 'Uprn' },
                    { mData: 'DebtId' }
                ],
                "aoColumnDefs": [{
                    "sTitle": "",
                    "aTargets": ["debt_id"]
                    , "mRender": function (value, type, full) { return '<a href="DebtView.aspx?cn_pin=' + full.Pin + '&uprn=' + full.Uprn + '" target=\"_blank\" ">View Debt Overview</a><input type="hidden" id="debtId" value="'+ full.DebtId +'">'; }
                }
                , { "aTargets": ["source"], "bVisible": true }
                , { "aTargets": ["cnpin"], "bVisible": false }
                , { "aTargets": ["uprn"], "bVisible": false }
                , {
                      "sTitle": "Debt Date"
                    , "aTargets": ["debt_date"]
                    , "mRender": function (value, type, full) {
                        if (value != null) {
                            var dtStart = new Date(parseInt(value.substr(6)));
                            var dtStartWrapper = moment(dtStart);
                            return dtStartWrapper.format('DD/MM/YYYY');
                        } else {
                            return '';
                        }
                    }
                }
                , {
                      "sTitle": "Assign"
                    , "aTargets": ["assign"]
                    , "mRender": function (value, type, full) { return ''; }
                    , "bVisible": true
                }
                , { "width": "175", "targets": 0 }
                , { "width": "100", "targets": 1 }
                , { "width": "250", "targets": 2 }
                , { "width": "100", "targets": 3 }
                , { "width": "200", "targets": 4 }
                , { "width": "200", "targets": 5 }
                , { "width": "200", "targets": 6 }
                ],
                "initComplete": function(settings, json) {
                    for (var i = 0; i < settings.aoData.length+1; i++) {
                        var newDropdown = $("#assignedUserList").clone();
                        var newId = getUUID();
                        newDropdown.attr("id", newId);
                        newDropdown.attr("rowId", i);
                        newDropdown.css('visibility', '');
                        $("tr:nth-child(" + i + ") td:nth-child(7)").append(newDropdown);
                        $('#' + newId).change(
                            function (event) {
                                assignWork($(this), $(this).val(), $("tr:nth-child(" + $(this).attr('rowId') + ") td:nth-child(1)").find('input').val());
                            }
                        );
                    }
                }
            });
            $("#searchResults").show();
            //$("#loadingImage").toggle();
        }
    });
}

function assignWork(parent, userId, debtId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SetDebtResponsibleUser",
        data: "{'userId':'" + userId + "','debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function(result) {
            if (result.d == true) {
                $(parent).closest("tr").remove();
            }
        }
    });
}

function loadCycles() {
    $('#currentTreatmentCycle').val('');                    //CLEAR CONTENTS
    $('#currentTreatmentCycle').append($('<option>', {      //ADD DEFAULT
        value: 0,
        text: 'None'
    }));
    $.ajax({                                                //LOAD ALL CYCLES FROM DATABASE
        type: "POST",
        url: "DataService.aspx/GetTreatmentCyclesList",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                $('#currentTreatmentCycle').append($('<option>', {
                    value: item.TreatmentCycleId,
                    text: item.TreatmentCycleName
                }));
            });
        }
    });
}
function loadStreams() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetDebtStreams",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                $('#debtStreamCode').append($('<option>', {
                    value: item.Code,
                    text: item.StreamName
                }));
            });
        }
    });
}
function loadUsersForAssigning() {
    $('#assignedUserList').val('');
    $('#assignedUserList').append($('<option>', {
        value: 0,
        text: "No one"
    }));
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetSystemUsers",
        data: "{'showInvalid':'false'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                $('#assignedUserList').append($('<option>', {
                    value: item.UserId,
                    text: item.UserName
                }));
            });
        }
    });
}
