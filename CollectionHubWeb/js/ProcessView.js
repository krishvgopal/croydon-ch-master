
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
function QueryString() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
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
        selectItems += '<option value="' + pair[1] + '">' + pair[0] + '</option>';
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

function postValues()
{
    // GET ALL THE FIELDS STARTING WITH AUTO_

    $("input[id^='auto_']").each(function (index) {
        console.log(index + ": " + $(this).attr('id') );
        //console.log(index + ": " + $(this).text());
    });

    $("select[id^='auto_']").each(function (index) {
        //console.log(index);
        console.log(index + ": " + $(this).val());
    });

    // TURN THESE INTO A POST TO THE SERVER

    // EXPECT ID RETURN

    // SHOW RESULTS IN TABLE
}