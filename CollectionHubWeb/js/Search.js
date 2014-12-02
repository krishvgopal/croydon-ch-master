$("#loadingImage").hide();
$("#searchResults").hide();

$(":text").keypress(function (arg) {
    if (arg.which == 13) {
        doSearch();
    }
});
function doSearch() {

    var dobValue = "";
    if ($("#dobMonth").val().length > 0) {
        dobValue = $("#dobYear").val() + "-" + $("#dobMonth").val() + "-" + $("#dobDay").val();
    } else {
        dobValue = $("#dobYear").val();
    }
    
    $("#loadingImage").toggle();
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SearchFullNameFullAddress",
        data: "{'firstName':'" + $("#firstName").val() + "','lastName':'" + $("#lastName").val() + "','nino':'" + $("#nino").val() + "','dob':'" + dobValue + "','address':'" + $("#address").val() + "','street':'" + $("#street").val() + "','postCode':'" + $("#postcode").val() + "','currentAddressOnly':'" + $("#addressCurrent").val() + "','sourceCode':''}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#searchTable").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'FullName' },
                    { mData: 'FullAddress' },
                    { mData: 'FromDate' },
                    { mData: 'UntilDate' },
                    { mData: 'DebtorAge'},
                    { mData: 'TotalDebt' },
                    { mData: 'DebtOutstanding' },
                    { mData: 'Pin' },
                    { mData: 'Source' },
                    { mData: 'CN_Pin' },
                    { mData: 'Urpin' }
                ],
                "aoColumnDefs": [
                    {
                        "sTitle": "From Date",
                        "aTargets": ["fromDate"],
                        "mRender": function(value, type, full) {
                            if (value != null) {
                                var dtStart = new Date(parseInt(value.substr(6)));
                                var dtStartWrapper = moment(dtStart);
                                return dtStartWrapper.format('DD/MM/YYYY');
                            } else {
                                return '';
                            }
                        }
                    }, {
                        "sTitle": "Until Date",
                        "aTargets": ["untilDate"],
                        "mRender": function (value, type, full) {
                            if (value != null) {
                                var dtStart = new Date(parseInt(value.substr(6)));
                                var dtStartWrapper = moment(dtStart);
                                return dtStartWrapper.format('DD/MM/YYYY');
                            } else {
                                return '';
                            }
                        }
                    }, {
                          "sTitle": "O/S Debt"
                        , "aTargets": ["debt_outstanding"]
                        , "mRender": function (value, type, full) {
                            return formatCurrency(value);
                            }
                    }, {
                          "sTitle": "Debt Total"
                          , "aTargets": ["debt_total"]
                          , "mRender": function (value, type, full) {
                            return formatCurrency(value);
                            }
                     }, {
                            "aTargets": ["record_selector"]
                            , "mRender": function (value, type, full) {
                                return '<a href="DebtView.aspx?cn_pin=' + full.CN_Pin + '&uprn=' + full.Urpin + '" target=\"_blank\" ">' + value + '</a>';
                            }
                     }, {
                    "aTargets": ["source"]
                    , "bVisible": false
                },{
                    "aTargets": ["pin_id"]
                    , "bVisible": false
                }, {
                    "aTargets": ["cn_pin"]
                    , "bVisible": false
                }, {
                    "aTargets": ["uprn"]
                    , "bVisible": false
                },  { "width": "95px", "targets": 2 },
                    { "width": "95px", "targets": 3 },
                    { "width": "120px", "targets": 4 },
                    { "width": "125px", "targets": 5 },
                    { "width": "125px", "targets": 6 }
                ]
            });
            $("#searchResults").show();
            $("#loadingImage").toggle();
        }
    });
}
//function loadStreams() {
//    $.ajax({
//        type: "POST",
//        url: "DataService.aspx/GetDebtStreams",
//        data: "{}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            $.each(result.d, function (i, item) {

//                $('#debtStreamCode').append($('<option>', {
//                    value: item.Code,
//                    text: item.StreamName
//                }));
//            });
//        }
//    });
//}