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

                    console.log( 'MAN ' + this.IsMandatory);

                    if (this.DataType == 'string') {
                        $("#processFieldTags").append(getTextBox(this.bf_id, this.FieldLabel, this.DefaultValue, this.HelpText));
                    }
                    if (this.DataType == 'currency') {
                        $("#processFieldTags").append(getTextBox(this.bf_id, this.FieldLabel, this.DefaultValue, this.HelpText));
                    }
                    if (this.DataType == 'textarea') {
                        $("#processFieldTags").append(getTextArea(this.bf_id, this.FieldLabel, this.DefaultValue, this.HelpText));
                    }
                    if (this.DataType == 'datetime') {
                        $("#processFieldTags").append(getDateTime(this.bf_id, this.FieldLabel, this.DefaultValue, this.HelpText));
                        applyDatePickers();
                    }
                    if (this.DataType == 'boolean') {
                        $("#processFieldTags").append(getCheckBox(this.bf_id, this.FieldLabel, this.DefaultValue, this.HelpText));
                    }
                    if (this.DataType == 'multiselect') {
                        $("#processFieldTags").append(getMultiSelect(this.bf_id, this.FieldLabel, this.DefaultValue, this.HelpText, this.FieldData, true));
                    }
                    if (this.DataType == 'select') {
                        $("#processFieldTags").append(getMultiSelect(this.bf_id, this.FieldLabel, this.DefaultValue, this.HelpText, this.FieldData, false));
                    }
                });
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

function getTextBox(id, label, defaultValue, helpText) {
    return '<div class="form-group"><label>' + label + '</label><input id="auto_' + id + '" class="form-control" value="' + defaultValue + '"><p class="help-block">' + helpText + '</p></div>';
}
function getTextArea(id, label, defaultValue, helpText) {
    return '<div class="form-group"><label>' + label + '</label><textarea id="auto_' + id + '" class="form-control" >' + defaultValue + '</textarea><p class="help-block">' + helpText + '</p></div>';
}
function getDateTime(id, label, defaultValue, helpText) {
    return '<div class="form-group"><label>' + label + '</label><input name="datepickerEnabled" id="auto_datepicker_' + id + '" class="form-control" value="' + defaultValue + '"><p class="help-block">' + helpText + '</p></div>';
}
function getCheckBox(id, label, defaultValue, helpText) {
    return '<div class="form-group"><label>' + label + '</label><div class="checkbox"><label><input id="auto_' + id + '" type="checkbox" value="' + defaultValue + '">' + helpText + '</label></div></div>'
}
function getMultiSelect(id, label, defaultValue, helpText, fieldData, isMultiSelect) {
    var returnValue = '';
    var selectItems = '';
    var tokenPairs  = fieldData.split(";");
    for (i = 0; i < tokenPairs.length; i++) {
        var pair = tokenPairs[i].split('|');

        if (pair[1] == defaultValue) {
            selectItems += '<option value="' + pair[1] + '" selected="selected">' + pair[0] + '</option>';
        }
        else {
            selectItems += '<option value="' + pair[1] + '">' + pair[0] + '</option>';
        }
    }
    if (isMultiSelect) {
       returnValue = '<div class="form-group"><label>' + label + '</label><select id="auto_' + id + '" multiple class="form-control">' + selectItems + '</select></div>';
    } else {
       returnValue = '<div class="form-group"><label>' + label + '</label><select id="auto_' + id + '" class="form-control">' + selectItems + '</select></div>';
    }
    return returnValue;
}

function applyDatePickers()
{
    $("input[name='datepickerEnabled']").datepicker();
}
function postValues() {
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
        },
        error: function (result) {
            alert('Could not update that record.');
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