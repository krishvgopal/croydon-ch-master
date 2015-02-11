
loadUsersForAssigning();
doTypeSearch();

function showAutoProcess() {
    if ($("#intrayStatus").val() == "1") {
        $("#autoComplete").css("visibility", "");
    } else {
        $("#autoComplete").css("visibility", "hidden");
    }
    doTypeSearch();  // Load All
    doItemSearch(0); // Load All for All
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
    //console.log("{'userId':'" + $("#userList").val() + "','corresType':'" + typeId + "','processCode':'" + $("#intrayStatus").val() + "'}");
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetAutomaticOutstandingItems",
        data: "{'userId':'" + $("#userList").val() + "','corresType':'" + typeId + "','processCode':'" + $("#intrayStatus").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#dataTableItems").dataTable({
                "destroy": true,
                "paging": false,
                "order": [[3, "desc"]],
                "bFilter": false,
                "aaData": result,
                aoColumns: [
                                { mData: 'DebtId' },
                                { mData: 'DebtId' },
                                { mData: 'DebtReference' },
                                { mData: 'DebtDate' },
                                { mData: 'ItemName' },
                                { mData: 'ItemDescription' },
                                { mData: 'DebtOnDate' },
                                { mData: 'Pin' },
                                { mData: 'Uprn' }
                           ],
                "aoColumnDefs": [
                    {
                        "sTitle": "<input data-toggle=\"tooltip\" data-placement=\"right\" title=\"Select all for processing\" id=\"selectAll\" type=\"checkbox\" class=\"selectAll\" onClick=\"checkAllItems();\">",
                        "aTargets": ["select_id"],
                        "bSortable": false,
                        "bSearchable": false,
                        "mRender": function (value, type, full) { return '<input data-toggle=\"tooltip\" data-placement=\"right\" title=\"Select Row\" id=\"selectItem_' + full.ActionId + '\" type=\"checkbox\" actionId=\"' + full.ActionId + '\" class=\"selectItem\">'; }
                    },{
                        "sTitle": "",
                        "aTargets": ["debtId"],
                        "bSortable": false,
                        "mRender": function (value, type, full) { return '<a href="DebtView.aspx?cn_pin=' + full.Pin + '&uprn=' + full.Uprn + '" target=\"_blank\" ">View Debt Overview</a>'; }
                    },{
                        "sTitle": "Debt Reference",
                        "aTargets": ["debtReference"],
                        "bVisible": true,
                    },{
                        "aTargets": ["debtDate"],
                        "bVisible": true,
                        "mRender": function (value, type, full) {
                            if (value != null) {
                                var dtStart = new Date(parseInt(value.substr(6)));
                                var dtStartWrapper = moment(dtStart);
                                return dtStartWrapper.format('DD/MM/YYYY');
                            } else {
                                return '';
                            }
                        }
                    },{
                        "aTargets": ["documentName"],
                        "bVisible": true,
                    },{
                        "aTargets": ["documentDescription"],
                        "bVisible": true,
                    },{
                        "aTargets": ["debtOnDate"],
                        "bVisible": true,
                    },{
                        "aTargets": ["pin"],
                        "bVisible": false
                    },{
                        "aTargets": ["uprn"],
                        "bVisible": false
                    }
                     , { "width": "5",   "targets": 0 }
                     , { "width": "125", "targets": 1 }
                     , { "width": "100", "targets": 2 }
                     , { "width": "75",  "targets": 3 }
                     , { "width": "100", "targets": 6 }
                ],
            });
        }
    });
}
function doProcess() {
    var processList = [];
    $('input:checkbox.selectItem').each(function () {
        if (this.checked) {
            var attrib = $(this).attr("actionid");
            processList.push(attrib);
        }
    });
    doProcessPost(processList);
}
function doProcessPost(processList) {
    $.ajax({
        type: "POST",
        url: "DocumentService.aspx/ProcessAutomaticItems",
        data: "{'actionItems': " + JSON.stringify(processList) + ",'userId':'1', 'pin':'1', 'uprn':'1', 'debtId':'1'}", 
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#DisplayPDF").html(
                $('<iframe>', {
                    src: "DocumentService.aspx?sessionDocument=" + result.d,
                    width: '0px',
                    height: "0px"
                })
            );
        }
    });
}

function checkAllItems() {
    $('.selectItem').each(function () {
        this.checked = $(".selectAll").is(':checked');
    });
}

function loadUsersForAssigning() {
    $('#userList').val('');
    $('#userList').append($('<option>', { value: 0, text: "Unassigned" }));
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


