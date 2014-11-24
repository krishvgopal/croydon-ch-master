$("#loadingImage").hide();
$("#searchResults").hide();

$(":text").keypress(function (arg) {
    if (arg.which == 13) {
        doSearch();
    }
});

loadStreams();

function doSearch() {
    $("#loadingImage").toggle();
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SearchDebts",
        data: "{'amountFrom':'" + $("#amountFrom").val() + "', 'amountTo':'" + $("#amountTo").val() + "', 'debtStreamCount':'" + $("#debtStreamCount").val() + "', 'includesStreamCode':'" + $("#debtStreamCode").val() + "', 'lastPaymentCode':'" + $("#lastPayment").val() + "', 'debtAgeCode':'" + $("#debtAge").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#searchTable").dataTable({
                "destroy": true,
                "aaData": result.Results,
                aoColumns: [
                    { mData: 'FullName'     },
                    { mData: 'FullAddress'  },
                    { mData: 'DebtStream'   },
                    { mData: 'NumberOfDebts'},
                    { mData: 'TotalAmount'  },
                    { mData: 'LastPaid'     },
                    { mData: 'LatestDebt'   },
                    { mData: 'Source'       },
                    { mData: 'CnPin'        }
                ],
                "aoColumnDefs": [{
                      "aTargets": ["full_name"]
                    , "mRender": function (value, type, full) {
                        return '<a href="DebtView.aspx?cn_pin=' + full.CnPin + '" target=\"_blank\" ">' + full.FullName + '</a>';
                    }
                },{
                    "aTargets": ["source"]
                    , "bVisible": true
                },{
                    "aTargets": ["cnpin"]
                    , "bVisible": false
                },{
                    "sTitle": "Last Paid"
                        , "aTargets": ["last_paid"]
                        , "mRender": function (value, type, full) {
                        if (value != null) {
                            var dtStart = new Date(parseInt(value.substr(6)));
                            var dtStartWrapper = moment(dtStart);
                            return dtStartWrapper.format('DD/MM/YYYY');
                        }else{return ''}
                    }
                },{
                    "sTitle": "Latest Debt"
                        , "aTargets": ["latest_debt"]
                        , "mRender": function (value, type, full) {
                        if (value != null) {
                            var dtStart = new Date(parseInt(value.substr(6)));
                            var dtStartWrapper = moment(dtStart);
                            return dtStartWrapper.format('DD/MM/YYYY');
                        }else{return ''}
                    }
                }
                ,{ "width": "100", "targets": 2 }
                ,{ "width": "100", "targets": 3 }
                //,
                //{
                //    "sTitle": "Latest Debt"
                //        , "aTargets": ["latest_debt"]
                //        , "mRender": function (value, type, full) {
                //            var dtStart = new Date(parseInt(value.substr(6)));
                //            var dtStartWrapper = moment(dtStart);
                //            return dtStartWrapper.format('DD/MM/YYYY');
                //        }
                //}
                ]
            });
            $("#searchResults").show();
            $("#loadingImage").toggle();
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
                    value: item.Id,
                    text: item.StreamName
                }));
            });
        }
    });
}