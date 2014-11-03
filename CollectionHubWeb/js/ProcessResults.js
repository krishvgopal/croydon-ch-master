
var batchRunId = QueryString()["p"];
$("#ThisId").val( batchRunId );

refreshBatchRunHistory(batchRunId);
loadBatchName(batchRunId);
refreshBatchProcessParentHeader(batchRunId);

function refreshBatchRunHistory(batchRunId)
{
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetBatchProcessResults",
        data: "{'batchProcessId':'" + batchRunId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }

            $("#resultsPane").css('visibility', 'visible');;

            $("#dataTableBatchProcessResults").dataTable({
                "destroy": true,
                "iDisplayLength": 25,
                "aaData": result,
                aoColumns:
                    [
                        { mData: 'RowIdentifier' },
                        { mData: 'RecordID' },
                        { mData: 'PIN' },
                        { mData: 'UPRN' },
                        { mData: 'Source' },
                        { mData: 'DebtAccount' },
                        { mData: 'FullName' },
                        { mData: 'FullAddress' },
                        { mData: 'ThisDebt' },
                        { mData: 'ThisDebtOS' },
                        { mData: 'DebtAsAt' },
                        { mData: 'DebtOS' },
                        { mData: 'Reduction' },
                        { mData: 'Included' }
                    ],
                "aoColumnDefs":
                    [
                        {
                            "sTitle": ""
                            , "aTargets": ["RowIdentifier"]
                            , "bVisible": true
                            , "mRender": function (value, type, full)
                                {
                                    if (value != null)
                                    {
                                            if (full.Included)
                                            {
                                                return '<input disabled type="checkbox" checked name="auto_' + value + '" value="' + value + '">';
                                            }
                                            else
                                            {
                                                return '<input disabled type="checkbox" name="auto_' + value + '" value="' + value + '">';
                                            }
                                    }
                                    else
                                    {
                                        return ''
                                    }
                                }
                        }, {
                            "sTitle": ""
                            , "aTargets": ["RecordID"]
                            , "bVisible": false
                        }, {
                            "sTitle": ""
                            , "aTargets": ["Included"]
                            , "bVisible": false
                        }, {
                            "sTitle": ""
                            , "aTargets": ["PIN"]
                            , "bVisible": false
                        }, {
                            "sTitle": ""
                            , "aTargets": ["FullName"]
                            , "mRender": function (value, type, full) {
                                if (value != null) {
                                    return '<a href="DebtView.aspx?cn_pin=' + full.PIN + '&uprn=' + full.UPRN + '" target="_blank">' + value + '</a>';
                                } else { return '' }
                            }
                        }, {
                            "sTitle": ""
                            , "aTargets": ["UPRN"]
                            , "bVisible": false
                        }, {
                            "sTitle": "From Date"
                            , "aTargets": ["FromDate"]
                            , "mRender": function (value, type, full) {
                                if (value != null) {
                                    var dtStart = new Date(parseInt(value.substr(6)));
                                    var dtStartWrapper = moment(dtStart);
                                    return dtStartWrapper.format('DD/MM/YYYY');
                                } else { return '' }
                            }
                        }, {
                            "sTitle": "Until Date"
                            , "aTargets": ["UntilDate"]
                            , "mRender": function (value, type, full) {
                                if (value != null) {
                                    var dtStart = new Date(parseInt(value.substr(6)));
                                    var dtStartWrapper = moment(dtStart);
                                    return dtStartWrapper.format('DD/MM/YYYY');
                                } else { return '' }
                            }
                        }
                    ]
            });
        }
    });
}
function loadBatchName(batchProcessRunId)
{
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetBatchName",
        data: "{'batchId':'" + batchProcessRunId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#processTitle").append('<h3>' + result.d + '</h3>');
        }
    });
}
function openSingleView(uprn, pin) {
    alert( uprn + ' ' + pin );
}
function amendProcess() {
    window.location.href = "ProcessView.aspx?p=" + $("#ParentId").val() + "&b=" + $("#ThisId").val();
};
function refreshBatchProcessParentHeader(batchRunId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetBatchProcessParentHeader",
        data: "{'batchRunId':'" + batchRunId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#ParentId").val(result.BatchProcessId);
        }
    });
}
