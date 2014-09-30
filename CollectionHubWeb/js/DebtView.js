$(function () {
    var sourcePin = $("#cnpin").val();
    refreshPersonAttributes(sourcePin);
    refreshCurrentAttributes(sourcePin);
});

var vDataMainTable = null;
$.ajax({
    type: "POST",
    url: "DataService.aspx/GetDebts",
    data: "{'pin':'" + $("#cnpin").val() + "'}",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        loadDebtsView(data);
        var sourcePin = $("#cnpin").val();
        console.log(sourcePin);
        refreshPersonAttributes(sourcePin);
        refreshCurrentAttributes(sourcePin);
        refreshAddresses(sourcePin);
    }
});

function selectRow(idValue) {
    $("#selectedDebtId").val(idValue);
    refreshRecoveryCycles(idValue);
    refreshPaymentHistory(idValue);
    refreshParties(idValue);
    refreshDebtAttributes(idValue);
    refreshNotes(idValue);
    refreshArrangements(idValue);
}

function loadDebtsView(result) {

    if (result.hasOwnProperty("d")) { result = result.d; }
    vDataMainTable = $("#dataTableMain").dataTable({
        "order": [[2, "desc"]],
        "aaData": result,
        aoColumns: [
            { mData: 'DebtId' },
            { mData: 'DebtSource' },
            { mData: 'DebtAccRef' },
            { mData: 'DebtReference' },
            { mData: 'DebtTotal' },
            { mData: 'DebtOutstanding' },
            { mData: 'RecoveryCycle' },
            { mData: 'Status' },
            { mData: 'Type' }
        ],
        "aoColumnDefs": [
            {
              "sTitle": "Debt ID"
            , "aTargets": ["debt_id"]
            , "mRender": function (value, type, full) {
                return '<a href="#" onclick="selectRow(' + value + ')">' + value + '</a>';}
            }, {
                  "sTitle": "<input id=\"debtGroupAll\" type=\"checkbox\" class=\"debtGroupAll\">"
                , "bSortable": false
                , "bSearchable": false
                , "aTargets": ["select_id"]
                , "mRender": function (value, type, full) {
                    return '<input type="checkbox" class="debtGroupItems" debtGroupDebtId="' + value + '" debtRowTotal="' + full.DebtTotal + '">';
                },
            }],
            "initComplete": function (settings, json) {
            $("#debtGroupAll").click(function () {
                $(".debtGroupItems").prop('checked', $(this).prop('checked'));
            });

            $('#dataTableMain tbody').on('click', 'tr', function (ee) {

                var selectedRowHtml     = $(ee.currentTarget.cells[0]);
                var selectedRowValue    = selectedRowHtml.find('input:checkbox');

                // THESE WILL BE REMOVED AT PRODUCTION
                console.log('rowId:' + selectedRowValue.attr('debtGroupDebtId'));
                console.log('rowDebtTotal:' + selectedRowValue.attr('debtRowTotal'));

                $('#debtRowTotalValue').val(selectedRowValue.attr('debtRowTotal'));

                // CHECK FOR EXISTING CONTROL, IF ALREADY LOADED FORCE REFRESH
                if ($('#agmTotalDebtAmount') != 'undefined') {
                    console.log('Modal_Exists');
                    $('#agmTotalDebtAmount').val(selectedRowValue.attr('debtRowTotal'));
                    $('#agmAgreedAmount').val(selectedRowValue.attr('debtRowTotal'));
                } else {
                    console.log('Modal_Null');
                }

                selectRow(selectedRowValue.attr('debtGroupDebtId'));

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
                console.log(item.ArrangementName);
                $('#agmPaymentMethod').append($('<option>', {
                    value: item.PaymentMethodCode,
                    text: item.PaymentMethodName
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
                    { mData: 'TargetDateFormatted' }],
                "aoColumnDefs": [
               { "width": "200px", "targets": 0 },
               { "width": "*%", "targets": 1 },
               { "width": "*%", "targets": 2 },
               { "width": "75px", "targets": 4 },
               { "width": "100px", "targets": 5 }]
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
                    { mData: 'PaymentAmount' },
                    { mData: 'PaymentDate' }
                ]
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
            if (result.hasOwnProperty("d")) { result = result.d; }

            $("#tableParties").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'PartyType' },
                    { mData: 'PartyFullName' },
                    { mData: 'PrimaryFlag' }
                ],
                //"columnDefs": [
                //    { "width": "200px", "targets": 0 },
                //    { "width": "*%", "targets": 1 }]
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
            if (result.hasOwnProperty("d")) { result = result.d; }
            console.log(result);
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
                //, "aoColumnDefs": [
                //     {
                //         "sTitle": "Address"
                //        , "aTargets": ["address"]
                //        , "mRender": function (value, type, full) {
                //            return value;
                //        }
                //     }, {
                //         "sTitle": "From Date"
                //        , "aTargets": ["from_date"]
                //        , "mRender": function (value, type, full) {
                //            if (value != null) {
                //                var dtStart = new Date(parseInt(value.substr(6)));
                //                var dtStartWrapper = moment(dtStart);
                //                return dtStartWrapper.format('DD/MM/YYYY');
                //            } else { return ''; }
                //        }
                //     }, {
                //         "sTitle": "Until Date"
                //        , "aTargets": ["until_date"]
                //        , "mRender": function (value, type, full) {
                //            if (value != null) {
                //                var dtStart = new Date(parseInt(value.substr(6)));
                //                var dtStartWrapper = moment(dtStart);
                //                return dtStartWrapper.format('DD/MM/YYYY');
                //            } else { return ''; }
                //        }
                //     },
                //    { "width": "*%", "targets": 0 },
                //    { "width": "150px", "targets": 1 },
                //    { "width": "150px", "targets": 2 }
                //]
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
function refreshAddresses(partyPin) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetLinkedAddress",
        data: "{'sourcePin':'" + partyPin + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            console.log(result);
            $("#tableAddress").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'Address' },
                    { mData: 'FromDate' },
                    { mData: 'UntilDate' }],
                "aoColumnDefs": [
                     {
                         "sTitle": "Address"
                        , "aTargets": ["address"]
                        , "mRender": function (value, type, full) {
                             return value;
                         }
                     },{
                         "sTitle": "From Date"
                        , "aTargets": ["from_date"]
                        , "mRender": function (value, type, full) {
                             if (value != null) {
                                 var dtStart = new Date(parseInt(value.substr(6)));
                                 var dtStartWrapper = moment(dtStart);
                                 return dtStartWrapper.format('DD/MM/YYYY');
                             } else {return '';}
                         }
                     },{
                         "sTitle": "Until Date"
                        , "aTargets": ["until_date"]
                        , "mRender": function (value, type, full) {
                             if (value != null) {
                                 var dtStart = new Date(parseInt(value.substr(6)));
                                 var dtStartWrapper = moment(dtStart);
                                 return dtStartWrapper.format('DD/MM/YYYY');
                             } else {return '';}
                         }
                     },
                    { "width": "*%",    "targets": 0 },
                    { "width": "150px", "targets": 1 },
                    { "width": "150px", "targets": 2 }]
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
    console.log('createPersonAttribute');
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
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SetRecoveryCycle",
        data: "{'debtId':'" + $("#selectedDebtId").val() + "','recoveryCycleId':'" + $('#recoveryCycles').val() + "','userId':'" + $('#UserSessionToken').val() + "','recoveryDateTime':'" + $('#datepicker').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            alert(result.d);
        },
        failure: function (error) {
            alert(error.message);
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
            refreshCurrentAttributes($("#cnpin").val());
            refreshPersonAttributes($("#cnpin").val());
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