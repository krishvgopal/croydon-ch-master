$(function () {
    $("#datepicker").datepicker();
    loadRecoveryCycleList();
});
var vDataMainTable = null;
$.ajax({
    type: "POST",
    url: "DataService.aspx/GetDebts",
    data: "{'source':'" + $("#sourceValue").val() + "','sourceRef':'" + $("#sourceRefValue").val() + "'}",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        loadDebtsView(data);
    }
});
function loadDebtsView(result) {

    if (result.hasOwnProperty("d")) { result = result.d; }

    vDataMainTable = $("#dataTableMain").dataTable({
        "order": [[ 2, "desc" ]],
        "aaData": result,
        aoColumns: [
            { mData: 'DebtId' },
            { mData: 'DebtId' },
            { mData: 'DebtSource' },
            { mData: 'DebtAccRef' },
            { mData: 'DebtReference' },
            { mData: 'DebtTotal' },
            { mData: 'DebtOutstanding' },
            { mData: 'PartyPin' },
            { mData: 'PropertyReference' },
            { mData: 'RecoveryCycle' },
            { mData: 'Status' },
            { mData: 'Type' }
        ],
        "aoColumnDefs": [
            {
              "sTitle": "Debt ID"
            , "aTargets": ["debt_id"]
            , "mRender": function (value, type, full) {
                return '<a href="#" onclick="selectRow(' + value + ')">' + value + '</a>';
                }
            },
            {
              "sTitle": "<input id=\"debtGroupAll\" type=\"checkbox\" class=\"debtGroupAll\">"
            , "bSortable": false
            , "bSearchable": false
            , "aTargets": ["select_id"]
            , "mRender": function (value, type, full) {
                return '<input type="checkbox" class="debtGroupItems" debtGroupDebtId="' + value + '">';
            },
            }],
            "initComplete": function (settings, json) {
                $("#debtGroupAll").click(function () {
                    $(".debtGroupItems").prop('checked', $(this).prop('checked'));
                });
                $('#dataTableMain tbody').on('click', 'tr', function () {
                    vDataMainTable.$('tr.selected').removeClass('active');
                    if ($(this).hasClass('active')) {
                        $(this).removeClass('active');
                    }
                    else {
                        vDataMainTable.$('tr.active').removeClass('active');
                        $(this).addClass('active');
                    }
                });
            }
    });
}

function selectRow(idValue) {

    $("#selectedDebtId").val(idValue);
    var sourcePin = $("#sourceRefValue").val();

    refreshRecoveryCycles(idValue);
    refreshPaymentHistory(idValue);
    refreshDebtAttributes(idValue);
    refreshPersonAttributes(sourcePin);
    refreshNotes(idValue);
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
                alert(item.AttributeText)
                if (item.IsDebtAttribute) {
                    $('#debtAttributes').append($('<option>', {
                        value: item.AttributeId,
                        text: item.AttributeText
                    }));
                }
            });
        }
    });
}
function loadRecoveryCycleList() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetRecoveryCycles",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result.d, function (i, item) {
                $('#recoveryCycles').append($('<option>', {
                    value: item.RecoveryCycleId,
                    text: item.RecoveryCycleName
                }));
            });
        }
    });
}
function refreshRecoveryCycles(debtId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetRecoveryCycles",
        data: "{'debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableRecovery").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'RecoveryCycle' },
                    { mData: 'Stage' },
                    { mData: 'StageType' },
                    { mData: 'Status' },
                    { mData: 'Days' },
                    { mData: 'TargetDate' }
                ]
            });
        }
    });
}
function refreshPaymentHistory(debtId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetPaymentsByDebtId",
        data: "{'debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tablePayments").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'PaymentId' },
                    { mData: 'PaymentSource' },
                    { mData: 'PaymentSourceAccountReference' },
                    { mData: 'PaymentDebtReference' },
                    { mData: 'PaymentPartyPin' },
                    { mData: 'PaymentAmount' },
                    { mData: 'PaymentDate' },
                    { mData: 'PaymentCreatedDate' }
                ]
            });
        }
    });
}
// TO COMPLETE -------------------------------------
function refreshParties(debtId) { }
function refreshArrangements(debtId) { }
// -------------------------------------------------
function refreshDebtAttributes(debtId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetDebtAttribute",
        data: "{'debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableDebt").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'Type' },
                    { mData: 'Information' }
                ]
            });
        }
    });
}
function refreshPersonAttributes(sourcePin) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetPersonAttribute",
        data: "{'sourcePin':'" + sourcePin + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tablePerson").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'Type' },
                    { mData: 'Information' },
                    { mData: 'Current' }]
            });
        }
    });
}
function refreshNotes(debtId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetDebtNotes",
        data: "{'debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableNotes").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'CreatedDate' },
                    { mData: 'User' },
                    { mData: 'Note' },
                    { mData: 'NoteId' }]
            });
        }
    });
}
function refreshCurrentAttributes(debtId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetCurrentAttribute",
        data: "{'debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableCurrent").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'CreatedDate' },
                    { mData: 'User' },
                    { mData: 'Note' },
                    { mData: 'NoteId' }]
            });
        }
    });
}
function createNote() { }
function createDebtAttribute() {
    alert("Debt {'debtId':'" + $("#selectedDebtId").val() + "','userId':'" + $('#UserSessionToken').val() + "','AttributeId':'" + $('#debtAttributes').val() + "','debtAttributesValue':'" + $('#debtAttributesValue').val() + "'");
}
function createPersonAttribute() {
    alert("Person {'debtId':'" + $("#selectedDebtId").val() + "','userId':'" + $('#UserSessionToken').val() + "','AttributeId':'" + $('#personAttributes').val() + "','debtAttributesValue':'" + $('#debtAttributesValue').val() + "'");
}
function setRecoveryCycle() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SetRecoveryCycle",
        data: "{'debtId':'" + $("#selectedDebtId").val() + "','recoveryCycleId':'" + $('#recoveryCycles').val() + "','userId':'" + $('#UserSessionToken').val() + "','recoveryDateTime':'" + $('#datepicker').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            alert(result);
        }
    });
}


function ajh() {
    alert('Save');
    $('#myModal').modal('hide');
}

function groupDebts() {
    var checkedInvoiceLineIds = [];
    $(".debtGroupItems:checked").each(function () {
        checkedInvoiceLineIds.push($(this).data("debtGroupDebtId"));
    });
    $('#myModal').modal('hide');
}





//function loadSampleTable(tableName, fixedValue) {
//    $.ajax({
//        type: "POST",
//        url: "DataService.aspx/GetSampleData",
//        data: "{'fixedValue':'" + fixedValue + "','count':'100'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (result) {

//            if (result.hasOwnProperty("d")) { result = result.d; }

//            $("#" + tableName).dataTable({
//                "destroy": true,
//                "aaData": result,
//                aoColumns: [
//                    { mData: 'FixedValue' },
//                    { mData: 'ColumnA' },
//                    { mData: 'ColumnB' },
//                    { mData: 'ColumnC' },
//                    { mData: 'ColumnD' }
//                ]
//                ,
//                "aoColumnDefs": [{
//                    "sTitle": "Debt ID"
//                    , "aTargets": ["debt_id"]
//                    , "mRender": function (value, type, full) {
//                        return '<a href="#">' + value + '</a>';
//                    }
//                }]
//            });
//        }
//    });
//}
