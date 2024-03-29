﻿$("#loadingImage").hide();
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
        data: "{'firstName':'" + $("#firstName").val() + "','lastName':'" + $("#lastName").val() + "','nino':'" + $("#nino").val() + "','dob':'" + dobValue + "','address':'" + $("#address").val() + "','street':'" + $("#street").val() + "','postCode':'" + $("#postcode").val() + "','currentAddressOnly':'" + $("#addressCurrent").val() + "','sourceCode':'', 'organisationName':'" + $("#organisationName").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#searchTable").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'FullName' }       , { mData: 'FullAddress' },
                    { mData: 'FromDate' }       , { mData: 'UntilDate' },
                    { mData: 'TotalDebt' }      , { mData: 'DebtOutstanding' },
                    { mData: 'Pin' }            , { mData: 'Source' },
                    { mData: 'CN_Pin' }         , { mData: 'Urpin' },
                    { mData: 'DebtAddresses' }  , { mData: 'CommAddresses' }
                ],
                "aoColumnDefs": [
                     {
                        "sTitle": "From Date",
                        "aTargets": ["fromDate"],
                        "mRender": function(value, type, full)  { return formatDate(value); }
                     }, {
                        "sTitle": "Until Date",
                        "aTargets": ["untilDate"],
                        "mRender": function (value, type, full) { return formatDate(value); }
                     }, {
                        "sTitle": "O/S Debt",
                        "aTargets": ["debt_outstanding"],
                        "mRender": function (value, type, full) { return formatCurrency(value); }
                     }, {
                        "sTitle": "Debt Total",
                        "aTargets": ["debt_total"],
                        "mRender": function (value, type, full) { return formatCurrency(value); }
                     }, {
                        "aTargets": ["record_selector"],
                        "mRender": function (value, type, full) { return '<a href="DebtView.aspx?cn_pin=' + full.CN_Pin + '&uprn=' + full.Urpin + '" target=\"_blank\" ">' + value + '</a>'; }
                     },
                    { "aTargets": ["source"]        , "bVisible": false },
                    { "aTargets": ["pin_id"]        , "bVisible": false },
                    { "aTargets": ["cn_pin"]        , "bVisible": false },
                    { "aTargets": ["uprn"]          , "bVisible": false },
                    { "aTargets": ["DebtAddresses"] , "bVisible": true },
                    { "aTargets": ["CommAddresses"] , "bVisible": true }
                ]
            });

            // Get the table to refresh to the best column layout
            var table = $('#searchTable').DataTable();
            table.columns.adjust().draw();

            $("#searchResults").show();
            $("#loadingImage").toggle();
        }
    });
}