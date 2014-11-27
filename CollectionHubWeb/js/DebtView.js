$("#showClearedLoadingImage").hide();

$('#pageBody_pageIcon').click(function (e) {
    console.log("ClickedOnImage1");
    refreshSingleDebtView();
});

$('#pageIcon').click(function (e) {
    e.preventDefault();
    console.log("ClickedOnImage2");
    refreshSingleDebtView();
});

$(function () {
    refreshSingleDebtView();
});

function selectRow(idValue, source, sourceAccRef) {

    $("#selectedDebtId").val(idValue);

    progressInterval = 25;
    progressValue = 0;

    refreshRecoveryCycles(idValue);
    refreshPaymentHistory(idValue, source, sourceAccRef);
    refreshParties(idValue);
    refreshDebtAttributes(idValue);
    refreshArrangements(idValue);
}

function loadDebtsView(result) {
    if (result.hasOwnProperty("d")) { result = result.d; }
    var vDataMainTable = $("#dataTableMain").dataTable({
        "destroy": true,
        "bSort": false,
        "order": [[2, "desc"]],
        "aaData": result,
        "scrollY": "250px",
        "scrollCollapse": true,
        "paging": false,
        "bFilter": false,
        aoColumns: [
            { mData: 'DebtId' },
            { mData: 'DebtSource' },
            { mData: 'DebtAccRef' },
            { mData: 'DebtReference' },
            { mData: 'DebtTotal' },
            { mData: 'DebtOutstanding' },
            { mData: 'RecoveryCycle' },
            { mData: 'Status' },
            { mData: 'Type' },
            { mData: 'GroupOrder' }
        ],
        "aoColumnDefs": [
            {
                "sTitle": "Debt ID",
                "aTargets": ["debt_id"],
                "mRender": function(value, type, full) {
                    return '<a href="#" onclick="selectRow(' + value + ')">' + value + '</a>';
                }
            }, {
                "sTitle": "<input id=\"debtGroupAll\" type=\"checkbox\" class=\"debtGroupAll\">",
                "bSortable": false,
                "bSearchable": false,
                "aTargets": ["select_id"],
                "mRender": function(value, type, full) {
                    return '<input type="checkbox" class="debtGroupItems" debtGroupDebtId="' + value + '" debtRowTotal="' + full.DebtTotal + '">';
                },
            }, {
                "aTargets": ["group_order"],
                "bVisible": false,
            }, { "width": "10px", "targets": 0 }
        ],
        "initComplete": function (settings, json) {

            var source = [];
            var groups = [];

            for (var i = 0; i < settings.aoData.length; i++) {
                if (settings.aoData[i].anCells[9].outerText.length > 0) {
                    source.push(settings.aoData[i].anCells[9].outerText + '-' + i);
                    groups.push(settings.aoData[i].anCells[9].outerText);
                }
            }
            for (var j = 0; j < groups.length; j++) {
                var min = 9999;
                var max = 0;
                for (var k = 0; k < source.length; k++) {
                    if (source[k].split("-")[0] == groups[j]) {
                        if (source[k].split("-")[1] < min) { min = parseInt((source[k].split("-")[1])) + 1; }
                        if (source[k].split("-")[1] > max) { max = parseInt((source[k].split("-")[1])) + 1; }
                    }
                }
                min = min++;
                max = max++;
                console.log("min:'" + min + "', max:'" + max + "'");
                $('#dataTableMain tr:nth-child(' + min + ')').addClass('groupTop');
                $('#dataTableMain tr:nth-child(' + max + ')').addClass('groupBottom');
            }

            $("#debtGroupAll").click(function () {
                $(".debtGroupItems").prop('checked', $(this).prop('checked'));
            });
            $('#dataTableMain tbody').on('click', 'tr', function (ee) {
                var selectedRowHtml     = $(ee.currentTarget.cells[0]);
                var selectedRowValue    = selectedRowHtml.find('input:checkbox');

                //console.log('rowId:' + selectedRowValue.attr('debtGroupDebtId') + ', ' + 'rowDebtTotal:' + selectedRowValue.attr('debtRowTotal'));

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
                console.log(item.ArrangementName);
                $('#agmPaymentMethod').append($('<option>', {
                    value: item.PaymentMethodCode,
                    text: item.PaymentMethodName
                }));
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
    var showCleared = 'false';
    if ($("#showCleared").val() == 2) { showCleared = 'true'; }
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

    //TODO : ADD RECOVERY HISTORY BY PIN
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
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetRecoveryCycleHistory",
        data: "{'debtId':'" + debtId + "'}",
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
                    { mData: 'DebtReference' }
                ],
                "aoColumnDefs": [
                    {
                        "aTargets": ["id"],
                        "bVisible": false,
                    },{
                        "aTargets": ["debtId"],
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
               //{ "width": "200px", "targets": 0 },
               //{ "width": "*%", "targets": 1 },
               //{ "width": "*%", "targets": 2 },
               //{ "width": "75px", "targets": 4 },
               //{ "width": "100px", "targets": 5 }
                ],
                "initComplete": function(settings, json) {
                    for (var i = 0; i < settings.aoData.length; i++) {
                        //console.log("VALUE (" + i + ") IS (" + settings.aoData[i].anCells[6].outerText + ")");
                        if (settings.aoData[i].anCells[6].outerText.toLowerCase() == "bold") {
                            //console.log("#tableRecovery tr:eq(" + i + ")");
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
                "aoColumnDefs": [{
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
function refreshPaymentHistory(debtId, source, sourceAccRef) {

    //console.log("{'debtId':'" + debtId + "','source':'" + source + "','sourceAccountReference':'" + sourceAccRef + "'}");

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
                    "sTitle": "Payment Date",
                    "aTargets": ["payment_date"],
                    "mRender": function(value, type, full) {
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
                    //{ mData: 'PartyType' },
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
                    { mData: 'IsCurrent' },
                    { mData: 'Streams' },
                    { mData: 'ToDate' },
                    { mData: 'FromDate' },
                    { mData: 'PersonAttributeId' }],
                "aoColumnDefs": [
                    {
                        "sTitle": "To Date"
                        , "aTargets": ["from_date"]
                        , "mRender": function (value, type, full) {
                            if (value != null) {
                                var dtStart = new Date(parseInt(value.substr(6)));
                                var dtStartWrapper = moment(dtStart);
                                return dtStartWrapper.format('DD/MM/YYYY');
                            } else { return ''; }
                        }
                    }, {
                        "sTitle": "From Date"
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
                        if (value == true) {
                            return 'Current';
                        }
                        else {
                            return '<a href="#" onclick="setCurrent(\'' + full.PersonAttributeId + '\');">Set Current</a>';
                        }
                    }
                    },{
                    "aTargets": ["person_attribute_id"]
                    , "bVisible": false
                },
                { "width": "200px", "targets": 0 },
                { "width": "*%", "targets": 1 },
                { "width": "150px", "targets": 2 }]
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
                    { mData: 'AttributeValue' },
                    { mData: 'CreatedDate' },
                    { mData: 'IsCurrent' }],
                "aoColumnDefs": [
                     {    "sTitle": "Created Date"
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
                        "sTitle": "To Date"
                        , "aTargets": ["dob"]
                        , "mRender": function (value, type, full) {
                            if (value != null) {
                                var dtStart = new Date(parseInt(value.substr(6)));
                                //consle.log('TE' + dtStart);
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
            $("#debtActionTabMenu").append('<ul class="nav nav-tabs">' + lineItem + '</ul>');

            // TODO : REMOVE THIS NASTY HACK
            var v = $("#debtActionTabPanels").html();
            $("#debtActionTabPanels").html('<div class="tab-content">' + v + '</div>');

        }
    });
}
function refreshDebtActionItems(isActive, groupName, debtId) {
    var lineItem = '';
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
                //lineItem = lineItem + '<p><a href="#" onclick="createDebtAction(' + value.TemplateItemId + ',' + value.TemplateId + ');">' + value.ItemName + '</a></p>';
                //lineItem = lineItem + '<p><a href="#" onclick="createDebtAction(1,1);">' + value.Name + '</a></p>';
                lineItem = lineItem + '<p><a href="#" onclick="createAdHocDocument('+ value.Id +');">' + value.Name + '</a></p>';
                // createAdHocDocument()
            });
            if (isActive) {
                $("#debtActionTabPanels").append('<div class="tab-pane fade in active" id="debtRecoveryTab_' + groupName + '"><div style="padding-top:15px">' + lineItem + '</div></div>');
            } else {
                $("#debtActionTabPanels").append('<div class="tab-pane fade in" id="debtRecoveryTab_' + groupName + '"><div style="padding-top:15px">' + lineItem + '</div></div>');
            }
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
    console.log($("#recoveryStartDate").val());
    $.ajax({
        type: "POST",
        url: "DataService.aspx/SetRecoveryCycle",
        data: "{'debtId':'" + $("#selectedDebtId").val() + "','recoveryCycleId':'" + $('#recoveryCycles').val() + "','userId':'" + $('#UserSessionToken').val() + "','recoveryDateTime':'" + $('#recoveryStartDate').val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != true) {
                console.log('error: createRecoveryCycle returned FALSE');
            } else {
                $("#recoveryStartDate").val("");
                $("#recoveryCycles").val("");
                $('#createRecoveryCycleModal').modal('hide');
                refreshRecoveryCycles($("#selectedDebtId").val());
            }
            $('#personAttributeModal').modal('hide');
        },
        failure: function (error) {
            console.log(error.message);
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
        data: "{'documentTemplateId':'" + templateItemId + "','templateId':'" + templateId + "','userId':'" + getUserId() + "','pin':'" + $("#cnpin").val() + "','uprn':'" + $("#uprn").val() + "'}",
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
function createAgreement(agm_start_date, agm_frequency, agm_day_of_month, agm_day_of_week, agm_start_amount, agm_installment_amount, agm_number_installment, agm_payment_method, agm_agreed_amount, agm_totaldebt_amount, agm_last_amount, agm_agreement_date, agm_payment_date, agm_starting_from_date) {

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

    //console.log(controlName + ' ' + result.length);

    if (result.length > 0) {
        $(controlName).css("font-weight", "bold");
    } else {
        $(controlName).css("font-weight", "normal"); 
    }

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

function createAdHocDocument(actionItemId) {

    var debtId = $("#selectedDebtId").val();

    $("#debtActionModal").modal("hide");

    console.log("{'debtId':'" + debtId + "','actionItemId':'" + actionItemId + "','userId':'" + getUserId() + "'}");

    $.ajax({
        type: "POST",
        url: "DataService.aspx/CreateAdHocItem",
        data: "{'debtId':'" + debtId + "','actionItemId':'" + actionItemId + "','userId':'" + getUserId() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            //alert(result.d);
            refreshRecoveryCycles(debtId);

        },
        failure: function (error) {
            //alert(error);
        }
    });
}