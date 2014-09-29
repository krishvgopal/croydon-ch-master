$("#loadingImage").hide();
$("#searchResults").hide();

function doSearch() {

    console.log("{'amountFrom':'" + $("#amountFrom").val() + "', 'amountTo':'" + $("#amountTo").val() + "', 'debtStreamCount':'0', 'includesStreamCode':'" + $("#debtStreamCode").val() + "', 'lastPaymentCode':'" + $("#lastPayment").val() + "', 'debtAgeCode':'" + $("#debtAge").val() + "'}");

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
                    
                    { mData: 'FullName' },
                    { mData: 'FullAddress' },
                    { mData: 'DebtStream'},
                    { mData: 'NumberOfDebts' },
                    { mData: 'TotalAmount' },
                    { mData: 'LastPaid' },
                    { mData: 'LatestDebt' },
                    { mData: 'Source' },
                    { mData: 'Pin' },
                    { mData: 'UrPin' }
                ]
                ,
                "aoColumnDefs": [{
                      "aTargets": ["full_name"]
                    , "mRender": function (value, type, full) {
                        return '<a href="DebtView.aspx?cn_pin=' + full.Pin + '" target=\"_blank\" ">' + full.FullName + '</a>';
                    }
                },
                {
                    "aTargets": ["source"]
                    , "bVisible": true
                },{
                    "aTargets": ["pin"]
                    , "bVisible": false
                }
                , {
                    "aTargets": ["urpin"]
                    , "bVisible": false
                },
                {
                    "sTitle": "Last Paid"
                        , "aTargets": ["last_paid"]
                        , "mRender": function (value, type, full) {
                            var dtStart = new Date(parseInt(value.substr(6)));
                            var dtStartWrapper = moment(dtStart);
                            return dtStartWrapper.format('DD/MM/YYYY');
                    }
                }
                ,
                {
                    "sTitle": "Latest Debt"
                        , "aTargets": ["latest_debt"]
                        , "mRender": function (value, type, full) {
                           var dtStart = new Date(parseInt(value.substr(6)));
                           var dtStartWrapper = moment(dtStart);
                           return dtStartWrapper.format('DD/MM/YYYY');
                        }
                }
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