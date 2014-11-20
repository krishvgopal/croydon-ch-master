$("#loadingImage").hide();
$("#searchResults").hide();
function doSearch() {

    console.log("{'firstName':'" + $("#firstName").val() + "','lastName':'" + $("#lastName").val() + "','nino':'" + $("#nino").val() + "','dob':'" + $("#dob").val() + "','address':'" + $("#address").val() + "','street':'" + $("#street").val() + "','postcode':'" + $("#postcode").val() + "'}");

    $("#loadingImage").toggle();
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SearchFullNameFullAddress",
        data: "{'firstName':'" + $("#firstName").val() + "','lastName':'" + $("#lastName").val() + "','nino':'" + $("#nino").val() + "','dob':'" + $("#dob").val() + "','address':'" + $("#address").val() + "','street':'" + $("#street").val() + "','postCode':'" + $("#postcode").val() + "'}",
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
                    { mData: 'DebtOutstanding' },
                    { mData: 'Pin' },
                    { mData: 'Source' },
                    { mData: 'CN_Pin' },
                    { mData: 'Urpin' }
                ],
                "aoColumnDefs": [{
                      "aTargets": ["record_selector"]
                    , "mRender": function (value, type, full) {
                        return '<a href="DebtView.aspx?cn_pin=' + full.CN_Pin + '&uprn=' + full.Urpin + '" target=\"_blank\" ">' + value + '</a>';
                    }
                },{
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
                }
                ]
            });
            $("#searchResults").show();
            $("#loadingImage").toggle();
        }
    });
}