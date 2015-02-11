
"Use Strict";

$("#showClearedLoadingImage").hide();

var selectedDebtRecord  = null;
var cancelRowSelect     = false;

$('#pageBody_pageIcon').click(function (e) {
    e.preventDefault();
    $("#selectedDebtId").val('');
    refreshSingleDebtView();
    loadDebtActionButtons();
});
$('#pageIcon').click(function (e) {
    e.preventDefault();
    $("#selectedDebtId").val('');
    refreshSingleDebtView();
    loadDebtActionButtons();
});
$(function () {
    loadUsersForAssigning();
    loadRecoveryCycleListQuickSet();
    loadAttributeStatusTypes();
    refreshSingleDebtView();
    loadDebtActionButtons();
});

function selectRow(idValue, source, sourceAccRef) {

    $("#selectedDebtId").val(idValue);

    progressInterval = 100/5;
    progressValue = 0;

    refreshRecoveryCycles(idValue);
    refreshPaymentHistory(idValue, source, sourceAccRef);
    refreshParties(idValue);
    refreshDebtAttributes(idValue);
    refreshArrangements(idValue);

    loadDebtActionButtons();
}
function loadDebtsView(result) {
    if (result.hasOwnProperty("d")) { result = result.d; }
    var vDataMainTable = $("#dataTableMain").dataTable({
        "destroy": true,
        "bSort": false,
        "order": [[2, "desc"]],
        "aaData": result,
        "scrollY": "250",
        "scrollCollapse": true,
        "paging": false,
        "bFilter": false,
        aoColumns: [

            { mData: 'DebtId' },
            { mData: 'DebtSource' },
            { mData: 'DebtAddress'},
            { mData: 'DebtAccRef' },
            { mData: 'DebtReference' },
            { mData: 'DebtTotal' },
            { mData: 'DebtOutstanding' },
            { mData: 'RecoveryCycle' },
            { mData: 'Status' },
            { mData: 'Type' },
            //{ mData: 'Responsible User'},
            { mData: 'GroupOrder' },
            { mData: 'ResponsibleUserId' },
            { mData: 'ResponsibleUserName' }
        ],
        "aoColumnDefs": [
            {
                  "sTitle": "Responsible User"
                , "aTargets": ["respUserName"]
                , "sClass": "right"
                , "bVisible": false
                //, "mRender": function(value, type, full) {
                //    return '<input type="hidden" id="resp_user_debt_id_' + full.DebtId + '" value="' + value + '">';
                //}
            }, {
                "sTitle": "Responsible Id",
                "aTargets": ["respUserId"],
                "sClass": "right",
                "bVisible": true,
                "mRender": function(value, type, full) {
                    return '<input type="hidden" id="resp_user_debt_id_' + full.DebtId + '" value="' + full.DebtId + '" data-userId="' + full.ResponsibleUserId + '">';
                }
            }, {
                "sTitle": "Recovery Cycle",
                "aTargets": ["recoveryCycle"],
                "sClass": "right",
                "bVisible": true,
                "mRender": function (value, type, full) {
                    return '<input type="hidden" id="resp_user_debt_id_' + full.DebtId + '" value="' + full.DebtId + '" data-recoveryCycle="' + full.RecoveryCycle + '">';
                }
            }, { 
                "sTitle": "Initial Debt"
                , "aTargets": ["debt_initial"]
                , "sClass": "right"
                , "mRender": function (value, type, full) {
                    return formatCurrency(value);
                }
            }, {
                    "sTitle": "O/S Debt",
                    "aTargets": ["debt_outstanding"],
                    "sClass": "right",
                    "mRender": function (value, type, full) {
                            return formatCurrency(value);
                        }
            }, {
                    "sTitle": "Debt ID",
                    "aTargets": ["debt_id"],
                    "mRender": function(value, type, full) {
                            return '<a href="#" onclick="selectRow(' + value + ')">' + value + '</a>';
                        }
            }, {
                    "sTitle": "<input data-toggle=\"tooltip\" data-placement=\"right\" title=\"Select record for grouping\" id=\"debtGroupAll\" type=\"checkbox\" class=\"debtGroupAll\">",
                    "bSortable": false,
                    "bSearchable": false,
                    "aTargets": ["select_id"],
                    "mRender": function (value, type, full) {
                    var returnString = '';
                    if (full.GroupDebtId < 0) {
                        returnString = '<input data-toggle="tooltip" data-placement="right" title="Select record for grouping actions" type="checkbox" class="debtGroupItems" debtGroupDebtId="' + value + '" debtRowTotal="' + full.DebtTotal + '">';
                    } else {
                        returnString = '<input data-toggle="tooltip" data-placement="right" title="Select record for grouping actions" type="checkbox" style="visibility:hidden" class="debtGroupItems" debtGroupDebtId="' + value + '" debtRowTotal="' + full.DebtTotal + '">';
                    }
                    return returnString;
                },
            }, {
                    "aTargets": ["group_order"],
                    "bVisible": false,
            }, {
                "width": "10px", "targets": 0
            },
            { "aTargets": ["status"]        , "bVisible": false },
            { "aTargets": ["type"]          , "bVisible": false },
            { "aTargets": ["debtAddress"]   , "bVisible": true }
        ],
        "initComplete": function (settings, json) {

            var source = [];
            var groups = [];

            for (var i = 0; i < settings.aoData.length + 1; i++) {

                var newDropdown         = $("#assignedUserList").clone();
                var newDropdownQuickSet = $("#recoveryCycleQuickSet").clone();
                var newId               = getNewId();
                var newIdQuickSet       = getNewId();

                newDropdown.attr("id", newId);
                newDropdown.attr("rowId", i);
                newDropdown.css('visibility', '');
                newDropdown.css('display', '');

                newDropdownQuickSet.attr("id", newIdQuickSet);
                newDropdownQuickSet.attr("rowId", i);
                newDropdownQuickSet.css('visibility', '');
                newDropdownQuickSet.css('display', '');
                
                $("tr:nth-child(" + i + ") td:nth-child(8)").append(newDropdownQuickSet);
                $("tr:nth-child(" + i + ") td:nth-child(9)").append(newDropdown);
                var thisUser = $("tr:nth-child(" + i + ") td:nth-child(9)").find('input').attr("data-userid");

                $('#' + newIdQuickSet).focus( function (event) { cancelRowSelect = true; });
                $('#' + newId).focus( function (event) { cancelRowSelect = true; });

                //$('#' + newIdQuickSet).val(thisUser);
                $('#' + newIdQuickSet).change(
                    function (event) {
                        cancelRowSelect = true;
                        //assignDebtToUser($(this),
                        //                 $(this).val(),
                        //                 $("tr:nth-child(" + $(this).attr('rowId') + ") td:nth-child(8)").find('input').val()
                        //                );
                    }
                );

                $('#' + newId).val(thisUser);
                $('#' + newId).change(
                    function (event) {
                        cancelRowSelect = true;
                        assignDebtToUser($(this),
                                         $(this).val(),
                                         $("tr:nth-child(" + $(this).attr('rowId') + ") td:nth-child(8)").find('input').val()
                                        );
                    }
                );
            }

            var gId = 9;
            for (var i = 0; i < settings.aoData.length; i++) {
                if (settings.aoData[i].anCells[gId].outerText.length > 0) {
                    source.push(settings.aoData[i].anCells[gId].outerText + '-' + i);
                    groups.push(settings.aoData[i].anCells[gId].outerText);
                }
            }
            for (var j = 0; j < groups.length; j++) {
                var min = 9999;
                var max = 0;
                for (var k = 0; k < source.length; k++) {
                    console.log(source[k].split("-")[0]);
                    if (source[k].split("-")[0] == groups[j]) {
                        if (source[k].split("-")[1] < min) { min = parseInt((source[k].split("-")[1])) + 1; }
                        if (source[k].split("-")[1] > max) { max = parseInt((source[k].split("-")[1])) + 1; }
                    }
                }
                min = min++;
                max = max++;
                $('#dataTableMain tr:nth-child(' + min + ')').addClass('groupTop');
                $('#dataTableMain tr:nth-child(' + max + ')').addClass('groupBottom');
            }

            $("#debtGroupAll").click(function () {
                $(".debtGroupItems").prop('checked', $(this).prop('checked'));
            });
            $('#dataTableMain tbody').on('click', 'tr', function (ee) {

                if (cancelRowSelect) {
                    cancelRowSelect = false;
                    return;
                }

                var selectedRowHtml     = $(ee.currentTarget.cells[0]);
                var selectedRowValue    = selectedRowHtml.find('input:checkbox');

                selectedDebtRecord = ee.currentTarget.cells;

                $('#debtRowTotalValue').val(selectedRowValue.attr('debtRowTotal'));
                if ($('#agmTotalDebtAmount') != 'undefined') {
                    $('#agmTotalDebtAmount').val(selectedRowValue.attr('debtRowTotal'));
                    $('#agmAgreedAmount').val(selectedRowValue.attr('debtRowTotal'));
                }

                selectRow(selectedRowValue.attr('debtGroupDebtId'), ee.currentTarget.cells[1].innerHTML.trim(), ee.currentTarget.cells[2].innerHTML.trim());

                vDataMainTable.$('tr.selected').removeClass('active');

                if ($(this).hasClass('active')) {
                    $(this).removeClass('active');
                }
                else {
                    vDataMainTable.$('tr.active').removeClass('active');
                    $(this).addClass('active');
                }
            });


            //$(".dataTables_scrollBody").css('height', '250px');
        }
    });
}

function assignDebtToUser(parent, userId, debtId) {
    //console.log(  "{'userId':'" + userId + "','debtId':'" + debtId + "'}" );
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SetDebtResponsibleUser",
        data: "{'userId':'" + userId + "','debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            if (result.d == true) {
                // $(parent).closest("tr").remove();
                //var cellName = $(parent).find('option:selected').text();
                //$(parent).closest("tr").find("td:nth-child(7)").html(cellName);
            }
        }
    });
}

function loadAttributesList() {
    $('#debtAttributes').val('');
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetAttributeList",
        data: "{'listDebtAttributes':'true','listPersonAttributes':'true'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                if (item.IsDebtAttribute) {
                    $('#debtAttributes').append($('<option>', {
                        value: item.AttributeId,
                        text: item.AttributeText
                    }));
                }
                if (item.IsPersonAttribute) {
                    $('#personAttributes').append($('<option>', {
                        value: item.AttributeId,
                        text: item.AttributeText
                    }));
                }
            });
        }
    });
}
function loadAttributeStatusTypes() {
    
    $('#attributeCurrentStatuses').val('');
    $('#attributeCurrentStatuses').append($('<option>', {
        value: 0,
        text: "Unknown"
    }));
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetAttributesCurrentStatuses",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                $('#attributeCurrentStatuses').append($('<option>', {
                    value: item.AttributeTypeId,
                    text: item.TypeName
                }));
            });
        }
    });
}
/*
** This method handles loading the users list so we can assign
** the case to a person.
*/
function loadUsersForAssigning() {

    $('#assignedUserList').val('');
    $('#assignedUserList').append($('<option>', {
        value: 0,
        text: "No one"
    }));
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetSystemUsers",
        data: "{'showInvalid':'false'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                $('#assignedUserList').append($('<option>', {
                    value: item.UserId,
                    text: item.UserName
                }));
            });
        }
    });
}
function loadRecoveryCycleListQuickSet() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetTreatmentCyclesList",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                $('#recoveryCycleQuickSet').append($('<option>', {
                    value: item.TreatmentCycleId,
                    text: item.TreatmentCycleName
                }));
            });
        }
    });
}
function loadRecoveryCycleList(debtId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetTreatmentCycles",
        data: "{'debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                $('#recoveryCycles').append($('<option>', {
                    value: item.TreatmentCycleId,
                    text: item.TreatmentCycleName
                }));
            });
        }
    });
}
function loadArrangementFrequenciesList() {
    $('#agmFrequency').val('');
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetFrequencyList",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                console.log(item.ArrangementName);
                $('#agmFrequency').append($('<option>', {
                    value: item.ArrangementCode,
                    text: item.ArrangementName
                }));
            });
        }
    });
}
function loadArrangementPaymentMethodList() {
    $('#agmPaymentMethod').val('');
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetPaymenyMethodList",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                $('#agmPaymentMethod').append($('<option>', {
                    value: item.PaymentMethodCode,
                    text: item.PaymentMethodName
                }));
            });
        }
    });
}
function loadActionStatuses() {
    $('#recoveryActiveStatus').html('');
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetActionStatuses",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                $('#recoveryActiveStatus').append($('<option>', {
                    value: item.ActionStatusId,
                    text: item.ActionStatusText
                }));
            });
        }
    });
}

function updateRecoveryHistory() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetRecoveryCycleHistory",
        data: "{'debtId':'" + $("#selectedDebtId").val() + "','statusId':'" + $("#recoveryActiveStatus").val() + "','nextStep':'" + $("#recoveryNextStep").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefRecovery');
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableRecovery").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'ActionText' },
                    { mData: 'Action' },
                    { mData: 'ActionStatus' },
                    { mData: 'ProcessMethod' },
                    { mData: 'ActionGroup' },
                    { mData: 'Actionable' },
                    { mData: 'ColumnFormat' },
                    { mData: 'Scheduled' },
                    { mData: 'ID' },
                    { mData: 'DebtID' },
                    { mData: 'DebtReference' },
                    { mData: 'ActionStatus' }
                ],
                "aoColumnDefs": [
                    {
                        "aTargets": ["id"],
                        "bVisible": false,
                    }, {
                        "aTargets": ["onClick"],
                        "bVisible": true,
                        "mRender": function (value, type, full) {
                            if (value != null) {
                                return '<a href="#" onclick="processAction(' + full.ID + ',' + full.ActionGroup + ',\'' + full.ActionStatus + '\')">' + value + '</a>';
                            } else { return ''; }
                        }
                    }, {
                        "aTargets": ["debtId"],
                        "bVisible": false,
                    }, {
                        "aTargets": ["actionStatus"],
                        "bVisible": false,
                    }, {
                        "aTargets": ["debtReference"],
                        "bVisible": false,
                    }, {
                        "aTargets": ["actionGroup"],
                        "bVisible": false,
                    }, {
                        "aTargets": ["columnFormat"],
                        "bVisible": false,
                    }, {
                        "sTitle": "Scheduled"
                        , "aTargets": ["scheduled"]
                        , "mRender": function (value, type, full) {
                            if (value != null) {
                                var dtStart = new Date(parseInt(value.substr(6)));
                                var dtStartWrapper = moment(dtStart);
                                return dtStartWrapper.format('DD/MM/YYYY');
                            } else { return ''; }
                        }
                    }
                ],
                "initComplete": function (settings, json) {
                    for (var i = 0; i < settings.aoData.length; i++) {
                        if (settings.aoData[i].anCells[6].outerText.toLowerCase() == "bold") {
                            $("#tableRecovery tr:eq(" + i + ")").css("font-weight", "bold");
                        }
                    }
                }
            });
        }
    });
}

function refreshDebtsList() {
    var showCleared = 'false';
    if ($("#showCleared").val() == 2) { showCleared = 'true'; }
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetDebts",
        data: "{'pin':'" + $("#cnpin").val() + "', 'showCleared':'" + showCleared + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            loadDebtsView(data);
        }
    });
}
function refreshSingleDebtView() {
    var showCleared = 'true';
    if ($("#showCleared").val() == 2) { showCleared = 'true'; }
    $('#selectedDebtId').val('0');
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetDebts",
        data: "{'pin':'" + $("#cnpin").val() + "', 'showCleared':'" + showCleared + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var pin   = $("#cnpin").val();
            var uprn  = $("#uprn").val();
            loadDebtsView(data);
            refreshPersonOverview(pin, uprn);
        }
    });
}
function refreshPersonOverview(sourcePin, uprn) {
    //100% / BY NUMBER OF UNITS
    progressInterval = 100/7;
    progressValue = 0;
    refreshPersonAttributes(sourcePin);
    refreshCurrentAttributes(sourcePin);
    refresMatchList(sourcePin);
    refresMisMisMatchList(sourcePin);
    refreshPersonDetails(sourcePin, uprn);
    refreshPaymentHistoryByPin(sourcePin);
    refreshPartiesByPin(sourcePin);
    refreshArrangementsByPin(sourcePin);
}
function refreshRecoveryCycles(debtId) {
    loadActionStatuses();
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetRecoveryCycleHistory",
        data: "{'debtId':'" + debtId + "','statusId':'0','nextStep':'0'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefRecovery');
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableRecovery").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'ActionText'},
                    { mData: 'Action' },
                    { mData: 'ActionStatus' },
                    { mData: 'ProcessMethod' },
                    { mData: 'ActionGroup' },
                    { mData: 'Actionable' },
                    { mData: 'ColumnFormat' },
                    { mData: 'Scheduled' },
                    { mData: 'ID' },
                    { mData: 'DebtID' },
                    { mData: 'DebtReference' },
                    { mData: 'ActionStatus' }
                ],
                "aoColumnDefs": [
                    {
                        "aTargets": ["id"],
                        "bVisible": false,
                    },{
                        "aTargets": ["onClick"],
                        "bVisible": true,
                        "mRender": function (value, type, full) {
                            if (value != null) {
                                return '<a href="#" onclick="processAction(' + full.ID + ',' + full.ActionGroup + ',\'' + full.ActionStatus + '\')">' + value + '</a>';
                            } else { return ''; }
                        }
                    },{
                        "aTargets": ["debtId"],
                        "bVisible": false,
                    },{
                        "aTargets": ["actionStatus"],
                        "bVisible": false,
                    },{
                        "aTargets": ["debtReference"],
                        "bVisible": false,
                    },{
                        "aTargets": ["actionGroup"],
                        "bVisible": false,
                    },{
                        "aTargets": ["columnFormat"],
                        "bVisible": false,
                    },{
                        "sTitle": "Scheduled"
                        , "aTargets": ["scheduled"]
                        , "mRender": function (value, type, full) {
                            if (value != null) {
                                var dtStart = new Date(parseInt(value.substr(6)));
                                var dtStartWrapper = moment(dtStart);
                                return dtStartWrapper.format('DD/MM/YYYY');
                            } else { return ''; }
                        }
                    }
                ],
                "initComplete": function(settings, json) {
                    for (var i = 0; i < settings.aoData.length; i++) {
                        if (settings.aoData[i].anCells[6].outerText.toLowerCase() == "bold") {
                            $("#tableRecovery tr:eq(" + i + ")").css("font-weight", "bold");
                        }
                    }
                }
            });
        }
    });
}
function refreshPaymentHistoryByPin(pin) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetPaymentsByPin",
        data: "{'pin':'" + pin + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefPayments');
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tablePayments").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'PaymentId' },
                    { mData: 'PaymentSource' },
                    { mData: 'PaymentSourceAccountReference' },
                    { mData: 'PaymentDebtReference' },
                    { mData: 'PaymentAmount' },
                    { mData: 'PaymentDate' }
                ],
                "aoColumnDefs": [
                 {
                     "sTitle": "Amount",
                     "aTargets": ["payment_amount"]
                     , "sClass": "right",
                     "mRender": function (value, type, full) {
                         return formatCurrency(value);
                        }
                 },{
                     "sTitle": "Payment Date",
                     "sClass": "right",
                    "aTargets": ["payment_date"],
                    "mRender": function (value, type, full) {
                        var dtStart = new Date(parseInt(value.substr(6)));
                        var dtStartWrapper = moment(dtStart);
                        return dtStartWrapper.format('DD/MM/YYYY');
                    }
                }]
            });
        }
    });
}
function refreshPaymentHistory(debtId, source, sourceAccRef) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetPaymentsByDebtId",
        data: "{'debtId':'" + debtId + "','source':'" + source + "','sourceAccountReference':'" + sourceAccRef + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefPayments');
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tablePayments").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'PaymentId' },
                    { mData: 'PaymentSource' },
                    { mData: 'PaymentSourceAccountReference' },
                    { mData: 'PaymentDebtReference' },
                    { mData: 'PaymentAmount' },
                    { mData: 'PaymentDate' }
                ],
                "aoColumnDefs": [{
                    "sTitle": "Amount",
                    "aTargets": ["payment_amount"]
                    , "sClass": "right",
                    "mRender": function(value, type, full) {
                        return formatCurrency(value);
                    }
                }, {
                    "sTitle": "Payment Date",
                    "aTargets": ["payment_date"],
                    "mRender": function (value, type, full) {
                        var dtStart = new Date(parseInt(value.substr(6)));
                        var dtStartWrapper = moment(dtStart);
                        return dtStartWrapper.format('DD/MM/YYYY');
                    }
                }]
            });
        }
    });
}
function refreshParties(debtId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetPartiesByDebt",
        data: "{'debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefParties');
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableParties").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'PartyFullName' },
                    { mData: 'PrimaryFlag' }
                ],
            });
        }
    });
}
function refreshPartiesByPin(pin) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetPartiesByPin",
        data: "{'pin':'" + pin + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefParties');
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableParties").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'PartyFullName' },
                    { mData: 'PrimaryFlag' }
                ],
            });
        }
    });
}
function refreshArrangements(debtId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetArrangements",
        data: "{'debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefArrangements');
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableArrangements").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'CreatedDate' },
                    { mData: 'AggreedAmount' },
                    { mData: 'Frequency' },
                    { mData: 'FrequencyDescription' },
                    { mData: 'InstallmentAmount' },
                    { mData: 'NumberOfInstallments' },
                    { mData: 'AgmStatus' }]
            });
        }
    });
}
function refreshArrangementsByPin(pin) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetArrangementsByPin",
        data: "{'pin':'" + pin + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefArrangements');
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableArrangements").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'CreatedDate' },
                    { mData: 'AggreedAmount' },
                    { mData: 'Frequency' },
                    { mData: 'FrequencyDescription' },
                    { mData: 'InstallmentAmount' },
                    { mData: 'NumberOfInstallments' },
                    { mData: 'AgmStatus' }]
            });
        }
    });
}
function refreshDebtAttributes(debtId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetDebtAttribute",
        data: "{'debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefDebtAttributes');
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableDebt").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'AttributeText' },
                    { mData: 'AttributeValue' }],
                "columnDefs": [
                    { "width": "200px", "targets": 0 },
                    { "width": "*%", "targets": 1 }]
            });
        }
    });
}
function refreshPersonAttributes(partyPin) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetPersonAttribute",
        data: "{'sourcePin':'" + partyPin + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {

            doProgress(result.d.length, 'arefPersonAttributes');
            if (result.hasOwnProperty("d")) { result = result.d; }
            
            $("#tablePerson").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'AttributeText' },
                    { mData: 'AttributeValue' },
                    { mData: 'Streams' },
                    { mData: 'ToDate' },
                    { mData: 'FromDate' },
                    { mData: 'StatusText' },
                    { mData: 'PersonAttributeId' }
                ],
                "aoColumnDefs": [
                    {
                        "sTitle": "From Date"
                        , "aTargets": ["from_date"]
                        , "mRender": function (value, type, full) {
                            if (value != null) {
                                var dtStart = new Date(parseInt(value.substr(6)));
                                var dtStartWrapper = moment(dtStart);
                                return dtStartWrapper.format('DD/MM/YYYY');
                            } else { return ''; }
                        }
                    }, {
                        "sTitle": "To Date"
                        , "aTargets": ["to_date"]
                        , "mRender": function (value, type, full) {
                            if (value != null) {
                                var dtStart = new Date(parseInt(value.substr(6)));
                                var dtStartWrapper = moment(dtStart);
                                return dtStartWrapper.format('DD/MM/YYYY');
                            } else { return ''; }
                        }
                    }, {
                      "sTitle": "Current Attribute"
                    , "aTargets": ["set_current"]
                    , "mRender": function (value, type, full) {
                        return '<a href="#" onclick="doSelect($(this),' + full.PersonAttributeId + ')">' + value + '</a>';
                      }
                    },{
                    "aTargets": ["person_attribute_id"]
                    , "bVisible": false
                },
                { "width": "200px", "targets": 0 },
                { "width": "*%", "targets": 1 },
                { "width": "130px", "targets": 2 },
                { "width": "110px", "targets": 3 },
                { "width": "100px", "targets": 4 },
                { "width": "130px", "targets": 5 }
                ]
            });
        }
    });
}
function refreshCurrentAttributes(partyPin) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetCurrentAttribute",
        data: "{'partyPin':'" + partyPin + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefCurrentAttributes');
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableCurrent").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'AttributeText' },
                    { mData: 'AttributeValue' }
                    ],
                "aoColumnDefs": [
                     //{    "sTitle": "Created Date"
                     //   , "aTargets": ["created_date"]
                     //   , "mRender": function (value, type, full) {
                     //       var dtStart = new Date(parseInt(value.substr(6)));
                     //       var dtStartWrapper = moment(dtStart);
                     //       return dtStartWrapper.format('DD/MM/YYYY');
                     //   }
                     //},
                     //{  "sTitle": "Status"
                     //   , "aTargets": ["status"]
                     //   , "mRender": function (value, type, full) {
                     //       if (value == true) {
                     //           return 'Current';
                     //       }
                     //       else {
                     //           return 'Not Current';
                     //       }
                     //   }
                     //},
                    { "width": "200px", "targets": 0},
                    { "width": "*%", "targets": 1 }
                ]
            });
        }
    });
}
function refreshPersonDetails(partyPin, uprn) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetPersonDetails",
        data: "{'pin':'" + partyPin + "','uprn':'" + uprn + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#dataTablePersonDetails").dataTable({
                "destroy": true,
                "aaData": result,
                "scrollCollapse": true,
                "paging": false,
                "bFilter": false,
                "bInfo": false,
                "aoColumns":    [
                                    { mData: 'AccountRef' },
                                    { mData: 'FULLNAME' },
                                    { mData: 'FULLADDRESS' },
                                    { mData: 'NINO' },
                                    { mData: 'DOB' }
                                ],
                "aoColumnDefs": [
                                    {
                                    "sTitle": "To Date",
                                    "aTargets": ["DOB"],
                                    "mRender": function (value, type, full) {
                                        if (value != null) {
                                            var dtStart = new Date(parseInt(value.substr(6)));
                                            var dtStartWrapper = moment(dtStart);
                                            return dtStartWrapper.format('DD/MM/YYYY');
                                        } else {
                                            return '';
                                        }
                                    }
                                    }
                                    , { "width": "175px",   "targets": 0 }
                                    , { "width": "250px",   "targets": 1 }
                                    , { "width": "175px",   "targets": 3 }
                                    , { "width": "80px",    "targets": 4 }
                ]
            });
        }
    });
};
function refresMatchList(partyPin) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetMatchListByPin",
        data: "{'pin':'" + partyPin + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefMatches');
            if (result.hasOwnProperty("d")) { result = result.d; }
            
            $("#matchTable1").dataTable({
                "destroy": true,
                "aaData": result,
                "bSort": false,
                "paging": false,
                "bFilter": false,
                "bInfo": false,
                aoColumns: [
                    { mData: 'InfoString' },
                    { mData: 'SourceName' },
                    { mData: 'SourceAccRef' },
                    { mData: 'FullName' },
                    { mData: 'FullAddress' },
                    { mData: 'NINO' },
                    { mData: 'DOB' },
                    { mData: 'MatchId' }  
                    ],
                "aoColumnDefs": [
                    {
                        "sTitle": "DOB"
                        , "aTargets": ["dob"]
                        , "mRender": function (value, type, full) {
                            if (value != null) {
                                var dtStart = new Date(parseInt(value.substr(6)));
                                var dtStartWrapper = moment(dtStart);
                                return dtStartWrapper.format('DD/MM/YYYY');
                            } else {
                                return '';
                            }
                        }
                    },
                    {
                          "sTitle": "Action"
                        , "aTargets": ["MatchId"]
                        , "mRender": function (value, type, full) {
                                return '<a href="#" onclick="unlinkMatchedResult(' + value + ');">Unlink</a>';
                            }
                    },
                    {
                        "sTitle": ""
                        , "aTargets": ["infoHover"]
                        ,  "mRender": function (value, type, full) {
                                return '<div title="' + full.InfoString + '"><i class="fa fa-info-circle"></i></div>';
                            }
                    }
                     ,  { "width": "10px", "targets": 0 }
                      , { "width": "50px", "targets": 1 }
                      , { "width": "80px", "targets": 2 }
                      , { "width": "250px", "targets": 3 }
                      , { "width": "*%", "targets": 4 }
                      , { "width": "80px", "targets": 5 }
                      , { "width": "80px", "targets": 6 }
                      , { "width": "80px", "targets": 7 }
                ]
            });
        }
    });
}
function refresMisMisMatchList(partyPin) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetMisMatchListByPin",
        data: "{'pin':'" + partyPin + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefMisMatches');
            if (result.hasOwnProperty("d")) { result = result.d; }
            
            $("#matchTable2").dataTable({
                "destroy": true,
                "aaData": result,
                "bSort": false,
                "bFilter" : false,
                "paging": false,
                "bInfo": false,
                aoColumns: [
                    { mData: 'InfoString' },
                    { mData: 'SourceName' },
                    { mData: 'SourceAccRef' },
                    { mData: 'FullName' },
                    { mData: 'FullAddress' },
                    { mData: 'NINO' },
                    { mData: 'DOB' }   ,
                    { mData: 'MatchId' }
                ],
                "aoColumnDefs": [
                    {
                        "sTitle": "Action"
                        , "aTargets": ["MatchId"]
                        , "mRender": function (value, type, full) {
                            return '<a href="#" onclick="linkMatchedResult(' + full.MatchId  + ');">Link</a>';
                        }
                    },
                    {
                        "sTitle": ""
                        , "aTargets": ["infoHover"]
                        , "mRender": function (value, type, full) {
                            return '<div title="' + full.InfoString + '"><i class="fa fa-info-circle"></i></div>';
                        }
                    }
                      , { "width": "10px", "targets": 0 }
                      , { "width": "50px", "targets": 1 }
                      , { "width": "80px", "targets": 2 }
                      , { "width": "250px","targets": 3 }
                      , { "width": "*%",   "targets": 4 }
                      , { "width": "80px", "targets": 5 }
                      , { "width": "80px", "targets": 6 }
                      , { "width": "80px", "targets": 7 }
                    ]
            });
        }
    });
}
function refreshDebtActionTabs(debtId) {

    var activeHtml = 'class="active"';
    var lineItem = '';

    $.ajax({
        type: "POST",
        async: false,
        url: "DataService.aspx/GetTreatmentGroups",
        data: "{'debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(result) {
            
            if (result.hasOwnProperty("d")) { result = result.d; }
           
            $.each(result, function(index, value) {
                lineItem = lineItem + '<li ' + activeHtml  + '> <a href="#debtRecoveryTab_' + value + '" data-toggle="tab">' + value + '</a></li>';
                var isActive = false;
                if (activeHtml.length > 0) { isActive = true; }
                refreshDebtActionItems(isActive, value, debtId);
                activeHtml = '';                
            });
            // ADD UPLOAD TAB
            lineItem = lineItem + '<li> <a href="#debtRecoveryTab_Upload" data-toggle="tab">Upload Document(s)</a></li>';
            $("#debtActionTabMenu").append('<ul class="nav nav-tabs">' + lineItem + '</ul>');
            $("#debtActionTabPanels").append('<div class="tab-pane fade in" id="debtRecoveryTab_Upload"><div style="padding-top:15px"><div class="form-group"><label>Select File</label><input type="file" name="fileToUpload" id="fileToUpload" onchange="fileSelected();"/><div id="progressNumber"></div></div><input type="button" class="btn btn-default" onclick="uploadFile()" value="Upload" /></div></div>');

            // TODO : REMOVE THIS NASTY HACK
            var v = $("#debtActionTabPanels").html();
            $("#debtActionTabPanels").html('<div class="tab-content">' + v + '</div>');
        }
    });
}
function refreshDebtActionItems(isActive, groupName, debtId) {
    var lineItem = '';
    var active = '';
    $.ajax({
        type: "POST",
        async: false,
        url: "DataService.aspx/GetTreatmentsForGroup",
        data: "{'actionType':'" + groupName + "','debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $.each(result, function (index, value) {
                lineItem = lineItem + '<p><a href="#" onclick="createAdHocDocument('+ value.Id +');">' + value.Name + '</a></p>';
            });
            if (isActive) { active = 'active'; } else { active = ''; }
            $("#debtActionTabPanels").append('<div class="tab-pane fade in ' + active + '" id="debtRecoveryTab_' + groupName + '"><div style="padding-top:15px">' + lineItem + '</div></div>');
        }
    });
}

function linkMatchedResult(matchId) {
    var sourcePin = $("#cnpin").val();
    $.ajax({
        type: "POST",
        url: "DataService.aspx/CreateMatch",
        data: "{'matchId':'" + matchId + "','pin':'" + sourcePin + "','userId':'" + $('#UserSessionToken').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            refresMatchList(sourcePin);
            refresMisMisMatchList(sourcePin)
        },
        failure: function (error) {
            alert(error.message);
        }
    });
}
function unlinkMatchedResult(matchId)
{
    var sourcePin = $("#cnpin").val();
    $.ajax({
        type: "POST",
        url: "DataService.aspx/RemoveMatch",
        data: "{'matchId':'" + matchId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            refresMatchList(sourcePin);
        },
        failure: function (error) {
            alert(error.message);
        }
    });
}

function createDebtAttribute() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/CreateDebtAttribute",
        data: "{'debtId':'" + $("#selectedDebtId").val() + "','userId':'" + $('#UserSessionToken').val() + "','attributeId':'" + $('#debtAttributes').val() + "','isCurrent':'true','attributeValue':'" + $('#debtAttributesValue').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != true) {
                alert('error: CreateDebtAttribute returned FALSE');
            } else {
                $("#debtAttributesValue").val("");
                refreshDebtAttributes($("#selectedDebtId").val());
            }
            $('#debtAttributeModal').modal('hide');
        },
        failure: function (error) {
            alert(error);
            $('#debtAttributeModal').modal('hide');
        }
    });
}
function createPersonAttribute() {
    console.log("{'sourceRef':'" + $("#cnpin").val() + "','userId':'" + $('#UserSessionToken').val() + "','attributeId':'" + $('#personAttributes').val() + "','isCurrent':'true','attributeValue':'" + $('#personAttributesValue').val() + "'}");
    $.ajax({
        type: "POST",
        url: "DataService.aspx/CreatePersonAttribute",
        data: "{'sourceRef':'" + $("#cnpin").val() + "','userId':'" + $('#UserSessionToken').val() + "','attributeId':'" + $('#personAttributes').val() + "','isCurrent':'true','attributeValue':'" + $('#personAttributesValue').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != true) {
                console.log('error: CreatePersonAttribute returned FALSE');
            } else {
                $("#personAttributesValue").val("");
                refreshPersonAttributes($("#cnpin").val());
            }
            $('#personAttributeModal').modal('hide');
        },
        failure: function (error) {
            console.log(error);
            $('#personAttributeModal').modal('hide');
        }
    });
}
function createRecoveryCycle() {
    console.log($("#recoveryStartDate").val());
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SetRecoveryCycle",
        data: "{'debtId':'" + $("#selectedDebtId").val() + "','recoveryCycleId':'" + $('#recoveryCycles').val() + "','userId':'" + $('#UserSessionToken').val() + "','recoveryDateTime':'" + $('#recoveryStartDate').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != true) {
                // TODO : LOG ERROR 
            } else {
                $("#recoveryStartDate").val("");
                $("#recoveryCycles").val("");
                $('#createRecoveryCycleModal').modal('hide');
                refreshRecoveryCycles($("#selectedDebtId").val());
            }
            $('#personAttributeModal').modal('hide');
        },
        failure: function (error) {
            // TODO : LOG ERROR
            $('#debtAttributeModal').modal('hide');
        }
    });
}
function createDebtAction(templateItemId, templateId) {
    $("#documentSelection").hide();
    $("#loading").show();
    $.ajax({
        type: "POST",
        url: "DocumentService.aspx/MergeDocument",
        data: "{'documentTemplateId':'" + templateItemId + "','templateId':'" + templateId + "','userId':'" + getUserId() + "','pin':'" + $("#cnpin").val() + "','uprn':'" + $("#uprn").val() + "','debtId':'" + $("#selectedDebtId").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            CKEDITOR.instances['editDocumentContent'].setData(result.d);
            $("#editDocumentContent").attr('templateItemId', templateItemId);
            $("#loading").hide();
            $("#editor").show();
        },
        failure: function (error) {
            alert(error);
        }
    });
    $("#documentEdit").show();
}
function createAgreement(agm_start_date, agm_frequency, agm_day_of_month, agm_day_of_week,
                         agm_start_amount, agm_installment_amount, agm_number_installment,
                         agm_payment_method, agm_agreed_amount, agm_totaldebt_amount,
                         agm_last_amount, agm_agreement_date, agm_payment_date,
                         agm_starting_from_date) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/CreateArrangement",
        data: "{'agm_pin':'" + $("#cnpin").val() + "', 'agm_cd_id':'" + $("#selectedDebtId").val() + "', 'agm_start_date':'" + agm_start_date + "', 'agm_frequency':'" + agm_frequency + "', 'agm_day_of_month':'" + agm_day_of_month + "', 'agm_day_of_week':'" + agm_day_of_week + "', 'agm_start_amount':'" + agm_start_amount + "', 'agm_installment_amount':'" + agm_installment_amount + "', 'agm_number_installment':'" + agm_number_installment + "', 'agm_payment_method':'" + agm_payment_method + "', 'agm_agreed_amount':'" + agm_agreed_amount + "', 'agm_totaldebt_amount':'" + agm_totaldebt_amount + "', 'agm_last_amount':'" + agm_last_amount + "', 'agm_Created_By':'" + getUserId() + "', 'agm_agreement_date':'" + agm_agreement_date + "', 'agm_payment_date':'" + agm_payment_date + "', 'agm_starting_from_date':'" + agm_starting_from_date + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d) {
                alert('True');
            }
        },
        failure: function (error) {
            alert(error);
        }
    });
    $('#tableArrangementsModal').modal('hide');
}

function setCurrent(id) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SetPersonAttributeCurrent",
        data: "{'personAttributeId':'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            refreshCurrentAttributes($("#cnpin").val());
            refreshPersonAttributes($("#cnpin").val());
        },
        failure: function (error) {
            alert(error.message);
        }
    });
}                                                                                                                                                                
function setTagIndicator(resultLength, controlName) {
    controlName = '#' + controlName;
    if (result.length > 0) {
        $(controlName).css("font-weight", "bold");
    } else {
        $(controlName).css("font-weight", "normal"); 
    }
}
function setAttributeStatus(personAttributeId, statusId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SetPersonAttributeStatus",
        data: "{'userId':'" + getUserId() + "','statusId':'" + statusId + "','personAttributeId':'" + personAttributeId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            refreshPersonAttributes($("#cnpin").val());
        },
        failure: function (error) {
            alert(error.message);
        }
    });
}

function groupDebts() {
    var debtIdString = '';
    $(".debtGroupItems:checked").each(function () { debtIdString = debtIdString + $(this).attr("debtGroupDebtId") + ','; });
    console.log(debtIdString);
    $.ajax({
        type: "POST",
        url: "DataService.aspx/CreateDebtGroup", 
        data: "{'debtIdString':'" + debtIdString + "','userId':'" + $('#UserSessionToken').val() + "','partyPin':'" + $("#cnpin").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != true) {
                alert('error: groupDebts returned FALSE');
            } else {
                refreshSingleDebtView();
            }
        },
        failure: function (error) {
            alert(error);
        }
    });
    $('#myModal').modal('hide');
}
function ungroupDebts() {
    var debtIdString = '';
    $(".debtGroupItems:checked").each(function () { debtIdString = debtIdString + $(this).attr("debtGroupDebtId"); });
    console.log(debtIdString);
    $.ajax({
        type: "POST",
        url: "DataService.aspx/RemoveDebtGroup",
        data: "{'debtId':'" + debtIdString + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != true) {
                alert('error: ungroupDebts returned FALSE');
            } else {
                refreshSingleDebtView();
            }
        },
        failure: function (error) {
            alert(error);
        }
    });
    $('#ungroupDebtModal').modal('hide');
}

function doSelect(e, id) {
    var newDropdown = $("#attributeCurrentStatuses").clone();
    var newId = getUUID();
    newDropdown.attr("id", newId);
    newDropdown.css('visibility','');
    $(e).parent().html(newDropdown);
    $('#' + newId).val(id);
    $('#' + newId).change(
        function (event) {
            var s = $('#' + newId).val();
            var v = id;
            setAttributeStatus(v, s);
        }
    );
}
function doAssignUser(e, id) {
    var newDropdown = $("#assignedUserList").clone();
    var newId = getUUID();
    newDropdown.attr("id", newId);
    newDropdown.css('visibility', '');
    $(e).parent().html(newDropdown);
    $('#' + newId).val(id);
    $('#' + newId).change(
        function (event) {
            var s = $('#' + newId).val();
            var v = id;
            //setAttributeStatus(v, s);
        }
    );
}
function createAdHocDocument(actionItemId)  {
    var debtId = $("#selectedDebtId").val();
    $("#debtActionModal").modal("hide");
    $.ajax({
        type: "POST",
        url: "DataService.aspx/CreateAdHocItem",
        data: "{'debtId':'" + debtId + "','actionItemId':'" + actionItemId + "','userId':'" + getUserId() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log('A-OK');
            refreshRecoveryCycles(debtId);
        },
        failure: function (error) {
            //  TODO : LOG ERROR
            console.log('!A-OK');
        }
    });
}
function processAction(itemId, groupId, status) {
    var statusValue = status.toLowerCase();
    console.log("itemid:" + itemId + ", groupId:" + groupId + "status:"+status);
    if (statusValue == 'pending') {
        doActionPending(itemId, groupId);
    }
    if (statusValue == 'saved') {
        doActionSaved(itemId, groupId);
    }
    if (statusValue == 'printed') {
        doActionView(itemId, groupId);
    }
}
function doActionPending(itemId, groupId) {

    $('#debtActionCreateModal').modal({ remote: 'modals/CreateDebtActionDocument.html', width: 925 });
    $("#debtActionCreateModal").attr('itemId', itemId);
    $('#debtActionCreateModal').on('hidden.bs.modal', function () { $(this).data('modal', null); });

    $('#debtActionCreateModal').on('hidden', function () {
        $('#debtActionCreateModal').removeData('bs.modal');
        console.log('CREATE MODAL DESTROYED');
    });

    $.ajax({
        type: "POST",
        url: "DocumentService.aspx/ProcessAdd",
        data: "{'itemId':'" + itemId + "','groupId':'" + groupId + "','userId':'" + getUserId() + "','pin':'" + $("#cnpin").val() + "','uprn':'" + $("#uprn").val() + "','debtId':'" + $("#selectedDebtId").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            CKEDITOR.instances['templateContentCreate'].setData(result.d);
        },
        failure: function (error) {
            // TODO: HANDLE ERROR
        }
    });
}
function doActionSaved(itemId, groupId) {
    $('#debtActionEditModal').attr('itemId', itemId);
    $('#debtActionEditModal').on('hidden.bs.modal', function () {
        $('#debtActionEditModal').removeData('bs.modal');
        console.log('EDIT MODAL DESTROYED');
    });
    $('#debtActionEditModal').modal({ remote: 'modals/AmendDebtActionDocument.html', width: 925 });
}
function doActionView(itemId, groupId) {
    //  TODO : USE UNIQUE ONE TIME HASH FOR FILE REFERENCE
    window.location.href = 'DocumentService.aspx?documentId=' + itemId;
}
function openSavedItem(itemId, destinationControl) {
    $.ajax({
        type: "POST",
        url: "DocumentService.aspx/OpenItemById",
        data: "{'itemId':'" + itemId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            CKEDITOR.instances['templateContentAmend'].setData(result.d);
        },
        failure: function (error) {
            alert(error);
        }
    });
}
function doSave(actionId, documentContent, parentModal) {
    $.ajax({
        type: "POST",
        url: "DocumentService.aspx/ProcessSave",
        data: "{'documentContent':'" + documentContent + "','actionId':'" + actionId + "','userId':'" + getUserId() + "','pin':'" + $("#cnpin").val() + "','uprn':'" + $("#uprn").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //console.log("CALLING - " + $("#selectedDebtId").val() );
            refreshRecoveryCycles($("#selectedDebtId").val());
        },
        failure: function (error) {
            // TODO: HANDLE ERROR HERE
        }
    });
}
function doPrint(actionId) {
    $.ajax({
        type: "POST",
        url: "DocumentService.aspx/ProcessPrint",
        data: "{'actionId':'" + actionId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            refreshRecoveryCycles($("#selectedDebtId").val());
        },
        failure: function (error) {
            // TODO : HANDLE ERROR HERE
        }
    });
}
function loadDebtActionButtons() {
    if ($("#selectedDebtId").val().length > 0) {
        enableDebtActions();
    } else {
        selectedDebtRecord = null;
        disableDebtActions();
    }
    return 0;
}
function disableDebtActions() {
    $("#debtCreateArrangement").addClass('disabled');
    $("#recoveryActionCreate").addClass('disabled');
    $("#debtAttributeCreate").addClass('disabled');
    $("#recoveryCycleSet").addClass('disabled');
    $("#debtGroupRemove").css('visibility', 'hidden');
    $("#debtGroupCreate").css('visibility', 'hidden');
    $("#recoveryActiveStatus").addClass('disabled');
    $("#recoveryNextStep").addClass('disabled');
}
function enableDebtActions() {
    $("#debtCreateArrangement").removeClass('disabled');
    $("#recoveryActionCreate").removeClass('disabled');
    $("#debtAttributeCreate").removeClass('disabled');
    $("#recoveryCycleSet").removeClass('disabled');
    $("#debtGroupRemove").css('visibility', '');
    $("#debtGroupCreate").css('visibility', '');
    $("#recoveryActiveStatus").removeClass('disabled');
    $("#recoveryNextStep").removeClass('disabled');
}

function getHeader() {
    var returnValue = '<table style="width:90%;margin-left:35px;margin-left:35px;margin-top:20px;border:1px solid #ddd">\
                            <tr><td style="padding:5px;padding-bottom:7px;border-bottom: 1px solid #dddddd" colspan="6">' + $("#pageFullNameField").html() + '</td></tr>\
                            <tr>\
                                <td style="padding-top:7px;padding:5px"><strong>Debt Source</strong></td>\
                                <td style="padding-top:7px;padding:5px">' + selectedDebtRecord[1].innerHTML + '</td>\
                                <td style="padding-top:7px;padding:5px"><strong>Debt Account No.</strong></td>\
                                <td style="padding-top:7px;padding:5px">' + selectedDebtRecord[2].innerHTML + '</td>\
                                <td style="padding-top:7px;padding:5px"><strong>Debt Reference</strong></td>\
                                <td style="padding-top:7px;padding:5px">' + selectedDebtRecord[3].innerHTML + '</td>\
                            </tr>\
                        </table>';
    return returnValue;

}
function fileSelected() {
    var file = document.getElementById('fileToUpload').files[0];
    if (file) {
        var fileSize = 0;
        if (file.size > 1024 * 1024) {
            fileSize = (Math.round(file.size * 100 / (1024 * 1024)) / 100).toString() + ' Mb';
        } else {
            fileSize = (Math.round(file.size * 100 / 1024) / 100).toString() + ' Kb';
        }
        document.getElementById('progressNumber').innerHTML = fileSize;
    }
}
function uploadFile() {
    var fd  = new FormData();
    var xhr = new XMLHttpRequest();

    fd.append("DebtId", $("#selectedDebtId").val());
    fd.append("fileToUpload", document.getElementById('fileToUpload').files[0]);
    xhr.upload.addEventListener("progress", uploadProgress, false);
    xhr.addEventListener("load", uploadComplete, false);
    xhr.open("POST", "FileUpload.aspx");
    xhr.send(fd);
}
function uploadProgress(evt) {
    if (evt.lengthComputable) {
        var percentComplete = Math.round(evt.loaded * 100 / evt.total);
        document.getElementById('progressNumber').innerHTML = percentComplete.toString() + '%';
    } else {
        document.getElementById('progressNumber').innerHTML = 'unable to compute';
    }
}
function uploadComplete(evt) {
    //alert(evt.target.responseText);
    document.getElementById('progressNumber').innerHTML = 'Upload Complete';
}