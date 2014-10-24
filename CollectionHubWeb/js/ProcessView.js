
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
                    $("#processFieldTags").append('<span style="border:1px solid #9A9A9A;padding:5px;margin-right:5px;background-color:#F8F8F8">' + this.FieldLabel + '</span>');
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