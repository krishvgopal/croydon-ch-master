$("#resultsPane").hide();

refreshProcessInfo();
refreshProcessHeader();

function refreshProcessInfo() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetBatchProcessFields",
        data: "{'bp_id': " + QueryString()["p"] + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            {
                jQuery.each(result, function () {
                    addFieldToForm(this);
                });
                applyDatePickers();
            }
        }
    });
};
function refreshProcessHeader() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetBatchProcess",
        data: "{'bp_id': " + QueryString()["p"] + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
           {
                jQuery.each(result, function () {
                    $("#processTitle").append('<h3>' + this.BatchName + '</h3>');
                    $("#processDescription").append('<h4>' + this.Description + '</h4>');
                });
           }
        }
    });
}

function addFieldToForm(field) {
    var htmlToAdd = '';
    if (field.DataType == 'string') {
        htmlToAdd = getTextBox(field);}
    if (field.DataType == 'currency') {
        htmlToAdd =  getTextBox(field);}
    if (field.DataType == 'textarea') {
        htmlToAdd = getTextArea(field);}
    if (field.DataType == 'datetime') {
        htmlToAdd =  getDateTime(field);}
    if (field.DataType == 'boolean') {
        htmlToAdd =  getCheckBox(field);}
    if (field.DataType == 'multiselect') {
        htmlToAdd =  getMultiSelect(field);}
    if (field.DataType == 'select') {
        htmlToAdd = getMultiSelect(field);}
    $("#processFieldTags").append( htmlToAdd );
}
function getTextBox(field) {
    return '<div class="form-group"><label>' + field.FieldLabel + '</label><input ' + isRequired(field) + ' id="auto_' + field.bf_id + '" class="form-control" value="' + field.DefaultValue + '"><p class="help-block">' + field.HelpText + '</p></div>';
}
function getTextArea(field) {
    return '<div class="form-group"><label>' + field.FieldLabel + '</label><textarea ' + isRequired(field) + ' id="auto_' + field.bf_id + '" class="form-control" >' + field.DefaultValue + '</textarea><p class="help-block">' + field.HelpText + '</p></div>';
}
function getDateTime(field) {
    return '<div class="form-group"><label>' + field.FieldLabel + '</label><input ' + isRequired(field) + ' name="datepickerEnabled" id="auto_datepicker_' + field.bf_id + '" class="form-control" value="' + field.DefaultValue + '"><p class="help-block">' + field.HelpText + '</p></div>';
}
function getCheckBox(field) {
    return '<div class="form-group"><label>' + field.FieldLabel + '</label><div class="checkbox"><label><input ' + isRequired(field) + ' id="auto_' + field.bf_id + '" type="checkbox" value="' + field.DefaultValue + '">' + field.HelpText + '</label></div></div>'
}
function getMultiSelect(field) {
    var returnValue = '';
    var selectItems = '';
    var t = field.FieldData.split(";");
    
    for (i = 0; i < t.length; i++) {
        var defaultValue = '';
        var p = t[i].split('|');
        if (p[1] == field.DefaultValue) { defaultValue = 'selected="selected"'; }
        selectItems += '<option value="' + p[1] + '" ' + defaultValue + '>' + p[0] + '</option>';
    }

    var multiSelectValue = '';
    if (field.DataType == 'multiselect') { multiSelectValue = 'multiple'; }
    returnValue = '<div class="form-group"><label>' + field.FieldLabel + '</label><select ' + isRequired(field) + ' id="auto_' + field.bf_id + '" ' + multiSelectValue + ' class="form-control">' + selectItems + '</select></div>\n';
   
    return returnValue;
}

function isRequired(field) {
    var required = '';
    if (field.IsMandatory) { required = 'required' }
    return required;
}

function applyDatePickers()
{
    $("input[name='datepickerEnabled']").datepicker();
}

function postValues() {

    if (!$("#mainForm").valid()) { return;} 
    
    var s = '';
    $("input[id^='auto_']").each(function (index) {
        var i = $(this).attr('id');
        var v = $(this).val();
        s += i + '=' + v + '&';
    });
    $("select[id^='auto_']").each(function (index) {
        var i = $(this).attr('id');
        var v = $(this).val();
        s += i + '=' + v + '&';
    });
    s = Base64.encode(s);

    $("#searchPane").hide(100);
    $("#resultsPane").show(100);

    $.ajax({
        type: "POST",
        url: "DataService.aspx/SaveBatchParameters",
        data: "{'batchId':'" + QueryString()['p'] + "','userId':'" + $("#UserSessionToken").val() + "','base64String':'" + s + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#BatchRunId").val( result );
            refreshBatchRun(result);
        }
    });
}
function refreshBatchRun(batchRunId)
{
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetBatchRunRecords",
        data: "{'batchRunId':'" + batchRunId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#dataTableBatchResults").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns:
                    [
                        { mData: 'RowIdentifier' },
                        { mData: 'Source' },
                        { mData: 'DebtAccount' },
                        { mData: 'FullName' },
                        { mData: 'FullAddress' },
                        { mData: 'ThisDebt' },
                        { mData: 'ThisDebtOS' },
                        { mData: 'DebtCount' },
                        { mData: 'AllDebtAmount' },
                        { mData: 'AllDebtOS' },
                        { mData: 'FromDate' },
                        { mData: 'UntilDate' }
                    ],
                "aoColumnDefs":
                    [
                        {
                            "sTitle": ""
                            , "aTargets": ["RowIdentifier"]
                            , "bVisible": true
                            , "mRender": function (value, type, full) {
                                if (value != null) {
                                    if (full.Included) {
                                        return '<input type="checkbox" checked name="auto_' + value + '" value="' + value + '" >';
                                    }
                                    else {
                                        return '<input type="checkbox" name="auto_' + value + '" value="' + value + '">';
                                    }
                                } else { return '' }
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
                    ],
                "initComplete": function (settings, json) {
                    $('input[type="checkbox"]').change(function () {
                        savePreference(this.checked, this.name);
                    });
                }
            });
        }
    });
}

function activateBatch() {
    if ($("#newBatchName").val().length == 0) { return; }
    $.ajax({
        type: "POST",
        url: "DataService.aspx/ActivateBatch",
        data: "{'batchId':'" + $("#BatchRunId").val() + "','batchName':'" + $("#newBatchName").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#searchPane").show(500);
            $("#resultsPane").hide(500);
        }
    });
    $('#acceptApprove').modal('hide');
    window.location.replace("Processes.aspx");
}
function cancelBatch() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/DeactivateBatch",
        data: "{'batchId':'" + $("#BatchRunId").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#searchPane").show(500);
            $("#resultsPane").hide(500);
        }
    });
    $('#cancelApprove').modal('hide');
    window.location.replace("Processes.aspx");
}

function savePreference(checkedState, recordIdentifier) {
    var recordId = recordIdentifier.split('_')[1];
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SaveBatchIncludeStatus",
        data: "{'recordIdentifier':'" + recordId + "','include':'" + checkedState + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
        }
    });
}

function loadBatchName()
{
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetBatchName",
        data: "{'batchId':'" + $("#BatchRunId").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#newBatchName").val(result.d);
        },
        error: function (result) {
            alert('Could not get batch name');
        }
    });
}