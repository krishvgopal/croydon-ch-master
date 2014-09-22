$(function () {
    var sourcePin = $("#sourceRefValue").val();
    $("#datepicker").datepicker();

    loadRecoveryCycleList();
    refreshPersonAttributes(sourcePin);
    refreshCurrentAttributes(sourcePin);
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

function selectRow(idValue) {

    $("#selectedDebtId").val(idValue);
    var sourcePin = $("#sourceRefValue").val();

    refreshRecoveryCycles(idValue);
    refreshPaymentHistory(idValue);
    refreshDebtAttributes(idValue);
    refreshPersonAttributes(sourcePin);
    refreshCurrentAttributes(sourcePin);
    refreshNotes(idValue);
}

function loadDebtsView(result) {

    if (result.hasOwnProperty("d")) { result = result.d; }
    vDataMainTable = $("#dataTableMain").dataTable({
        "order": [[2, "desc"]],
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
        url: "DataService.aspx/GetRecoveryCycleHistory",
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
function refreshParties(debtId) { }
function refreshArrangements(debtId) { }
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
                    { mData: 'AttributeText' },
                    { mData: 'AttributeValue' }],
                "columnDefs": [
                    { "width": "200px", "targets": 0 },
                    { "width": "*%", "targets": 1 }]
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
                    { mData: 'AttributeText' },
                    { mData: 'AttributeValue' },
                    { mData: 'IsCurrent' },
                    { mData: 'PersonAttributeId' }],
                "aoColumnDefs": [{
                    "sTitle": "Current Attribute"
                    , "aTargets": ["set_current"]
                    , "mRender": function (value, type, full) {
                        if (value == true) {
                            return 'Current'
                        }
                        else {
                            return '<a href="#" onclick="setCurrent(\'' + full.PersonAttributeId + '\');">Set Current</a>';
                        }
                    }
                },
                {
                        "aTargets": ["person_attribute_id"]
                    ,   "bVisible": false},
                { "width": "200px", "targets": 0 },
                { "width": "*%", "targets": 1 },
                { "width": "150px", "targets": 2 }]
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
                    { mData: 'UserId' },
                    { mData: 'NoteText' },
                    { mData: 'NoteId' }],
                "aoColumnDefs": [
                    { 
                        "aTargets": ["note_id"]
                       ,"bVisible": false 
                    },
                    {   "sTitle": "Created Date"
                        , "aTargets": ["created_date"]
                        , "mRender": function (value, type, full) {
                            var dtStart = new Date(parseInt(value.substr(6)));
                            var dtStartWrapper = moment(dtStart);
                            return dtStartWrapper.format('DD/MM/YYYY');
                        }
                    },
                    { "width": "100px", "targets": 0 },
                    { "width": "100px", "targets": 1 },
                    { "width": "*%", "targets": 2 }]
            })
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
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableCurrent").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'AttributeText' },
                    { mData: 'AttributeValue' },
                    { mData: 'CreatedDate' },
                    { mData: 'IsCurrent' }],
                "aoColumnDefs": [
                     {
                         "sTitle": "Created Date"
                        , "aTargets": ["created_date"]
                        , "mRender": function (value, type, full) {
                            var dtStart = new Date(parseInt(value.substr(6)));
                            var dtStartWrapper = moment(dtStart);
                            return dtStartWrapper.format('DD/MM/YYYY');
                        }
                     },
                     {  "sTitle": "Status"
                        , "aTargets": ["status"]
                        , "mRender": function (value, type, full) {
                            if (value == true) {
                                return 'Current'
                            }
                            else {
                                return 'Not Current';
                            }
                        }
                    },
                    { "width": "200px", "targets": 0},
                    { "width": "*%", "targets": 1 },
                    { "width": "150px", "targets": 2 },
                    { "width": "150px", "targets": 3 }]
            });
        }
    });
}

function createNote() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/CreateNote",
        data: "{'debtId':'" + $("#selectedDebtId").val() + "','userId':'" + $('#UserSessionToken').val() + "','noteText':'" + $('#noteText').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != true) {
                alert('error: createNote returned FALSE');
            } else {
                $("#noteText").val("");
                refreshNotes($("#selectedDebtId").val());
            }
            $('#noteModal').modal('hide');
        },
        failure: function (error) {
            alert(error);
            $('#noteModal').modal('hide');
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
    $.ajax({
        type: "POST",
        url: "DataService.aspx/CreatePersonAttribute",
        data: "{'sourceRef':'" + $("#sourceRefValue").val() + "','userId':'" + $('#UserSessionToken').val() + "','attributeId':'" + $('#personAttributes').val() + "','isCurrent':'true','attributeValue':'" + $('#personAttributesValue').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != true) {
                alert('error: CreatePersonAttribute returned FALSE');
            } else {
                $("#personAttributesValue").val("");
                refreshPersonAttributes($("#sourceRefValue").val());
            }
            $('#personAttributeModal').modal('hide');
        },
        failure: function (error) {
            alert(error);
            $('#personAttributeModal').modal('hide');
        }
    });
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
function setCurrent(id) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SetPersonAttributeCurrent",
        data: "{'personAttributeId':'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
                refreshCurrentAttributes($("#sourceRefValue").val());
                refreshPersonAttributes($("#sourceRefValue").val());
        },
        failure: function (error) {
            alert(error.message);
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