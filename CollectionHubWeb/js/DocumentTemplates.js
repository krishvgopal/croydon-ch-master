//jQuery.fn.visible = function () { return this.css('visibility', 'visible'); }; jQuery.fn.invisible = function () { return this.css('visibility', 'hidden'); }; jQuery.fn.visibilityToggle = function () { return this.css('visibility', function (i, visibility) { return (visibility == 'visible') ? 'hidden' : 'visible'; }); };
$("#documentDetails").hide();

refreshBatchProcessJobs();

function getUserId() {
    if ($("#selectAll").is(':checked')) { return 0; }
    else { return $("#UserSessionToken").val(); }
}

function refreshBatchProcessJobs() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetDocumentTemplates",
        data: "{'userId':'" + getUserId() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#dataTableDocumentTemplates").dataTable({
                "destroy": true,
                "scrollCollapse": true,
                "paging": false,
                "bFilter": false,
                "aaData": result,
                "aoColumns": [
                    { mData: 'CHT_Name' },
                    { mData: 'CHT_Notes' },
                    { mData: 'CHT_ID' }
                ],
                "aoColumnDefs": [
                {
                    "sTitle": "Document Note",
                    "aTargets": ["CHT_Notes"]
                },
                {
                    "sTitle": "Document Name",
                    "aTargets": ["CHT_Name"],
                    "mRender": function (value, type, full) {
                        return '<a href="#" onClick="selectTemplate(' + full.CHT_ID + ');">' + value + '</a>';
                    }
                },{
                    "aTargets": ["CHT_ID"],
                    "bVisible": false
                },{ "width": "40%", "targets": 0 }
                 ,{ "width": "60%", "targets": 1 }
                ]
            });
        }
    });
};

function refreshTemplateDocument(templateId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetDocumentTemplate",
        data: "{'templateId':'" + templateId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }

            console.log(result.CHT_Name + ' # ' + result.CHT_Notes);

            $("#templateName").append('<h3>' + result.CHT_Name + '</h3>');
            $("#templateDescription").append('<h2>' + result.CHT_Notes + '</h2>');

        }
    });
}

function selectTemplate (templateId) {
    $("#documentList").hide();
    $("#documentDetails").show();
    refreshTemplateDocument(templateId);
}

function createNewTemplate() {
    //console.log("{'userId':'" + $("#UserSessionToken").val() + "','documentName':'" + $("#newDocumentTemplateName").val() + "'}");
    $.ajax({
        type: "POST",
        url: "DataService.aspx/CreateNewDocumentTemplate",
        data: "{'userId':'" + $("#UserSessionToken").val() + "','documentName':'" + $("#newDocumentTemplateName").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }

            if (result = true) {
                $('#createTemplate').modal('hide');
                refreshBatchProcessJobs();
                console.log('createNewTemplate_2');
                $("#documentDetails").hide();
                $("#documentList").show();
            } 
            //else {
            //    $('#createTemplate').modal('hide');
            //    refreshBatchProcessJobs();
            //    console.log('createNewTemplate_3');
            //    $("#documentDetails").hide();
            //    $("#documentList").show();
            //}
        }
    });
}

//function refreshBatchRunHistory() {
//    $.ajax({
//        type: "POST",
//        url: "DataService.aspx/GetBatchRunHistory",
//        data: "{}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            if (result.hasOwnProperty("d")) { result = result.d; }
//            $("#dataTableBatchRunHistory").dataTable({
//                "destroy": true,
//                "aaData": result,
//                "aoColumns": [
//                    { mData: 'B_ID' },
//                    { mData: 'UserName' },
//                    { mData: 'BatchName' },
//                    { mData: 'DateCreated' },
//                    { mData: 'Records' },
//                    { mData: 'BatchStatus' },
//                    { mData: 'DebtAtStart' },
//                    { mData: 'DebtOSNow' }
//                ],
//                "aoColumnDefs": [
//                {
//                    "sTitle": "",
//                    "aTargets": ["B_ID"],
//                    "mRender": function (value, type, full) {
//                        return '<a target="_blank" href="ProcessView.aspx?p=' + full.B_ID + ' ">View</a>';
//                    }
//                },
//                {
//                    "sTitle": "Date Created"
//                        , "aTargets": ["Date_Created"]
//                        , "mRender": function (value, type, full) {
//                            if (value != null) {
//                                var dtStart = new Date(parseInt(value.substr(6)));
//                                var dtStartWrapper = moment(dtStart);
//                                return dtStartWrapper.format('DD/MM/YYYY');
//                            } else { return ''; }
//                        }
//                }

//                ]

//            });
//        }
//    });
//};


// refreshBatchProcessHistory();

//function refreshBatchProcessHistory() {
//    $.ajax({
//        type: "POST",
//        url: "DataService.aspx/GetBatchProcessHistory",
//        data: "{}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            if (result.hasOwnProperty("d")) { result = result.d; }
//            $("#dataTableBatchJobs").dataTable({
//                "destroy": true,
//                "scrollCollapse": true,
//                "paging": false,
//                "bFilter": false,
//                "aaData": result,
//                "aoColumns": [
//                    { mData: 'bph_process_date' },
//                    { mData: 'bp_debt_source' },
//                    { mData: 'bp_batch_name' },
//                    { mData: 'pm_name' },
//                    { mData: 'bph_records_affected' },
//                    { mData: 'UserName' }
//                ],
//            });
//        }
//    });
//};
