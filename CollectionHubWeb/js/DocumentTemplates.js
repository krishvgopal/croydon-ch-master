$("#documentDetails").hide();

refreshBatchProcessJobs();

CKEDITOR.replace('templateDocumentContent',
    {
        height: 550,
        width: 900
    });

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
                        return '<a href="#top" onClick="selectTemplate(' + full.CHT_ID + ');">' + value + '</a>';
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
            
            $("#templateName").attr('templateId', result.CHT_ID);
            $("#templateName").html('<h2>' + result.CHT_Name + '</h2>');
            $("#templateDescription").html('<p><i>' + result.CHT_Notes + '</i></p>');

            CKEDITOR.instances['templateContent'].setData(result.CHT_Content);
        }
    });
}
function selectTemplate (templateId) {
    $("#documentList").hide();
    $("#documentDetails").show();
    refreshTemplateDocument(templateId);
    $("html, body").animate({ scrollTop: 0 }, "slow");
}
function saveTemplate() {

    var htmlValue = CKEDITOR.instances['templateContent'].getData();
    var htmlNotes = $("#templateDescription").text();
    //console.log("{'chtId':'" + $("#templateName").attr('templateId') + "','userId':'" + $("#UserSessionToken").val() + "','content':'" + htmlValue + "','notes':'" + htmlNotes + "'}");
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SaveTemplateContent",
        data: "{'chtId':'" + $("#templateName").attr('templateId') + "','userId':'" + $("#UserSessionToken").val() + "','content':'" + htmlValue + "','notes':'" + htmlNotes + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            if (result = true) {
                refreshBatchProcessJobs();                
                $("#documentDetails").hide();
                $("#documentList").show();
            }
        }
    });
}
function createNewTemplate() {
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
        }
    });
}