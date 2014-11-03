$("#showClearedLoadingImage").hide();
var progressInterval    = 20;
var progressValue       = 0;

$(function () {
    var sourcePin = $("#cnpin").val();
    refreshSingleDebtView();
});

function selectRow(idValue) {

    $("#selectedDebtId").val(idValue);

    progressInterval    = 17;
    progressValue       = 0;
    refreshRecoveryCycles(idValue);
    refreshPaymentHistory(idValue);
    refreshParties(idValue);
    refreshDebtAttributes(idValue);
    refreshNotes(idValue);
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

                console.log('rowId:' + selectedRowValue.attr('debtGroupDebtId') + ', ' + 'rowDebtTotal:' + selectedRowValue.attr('debtRowTotal'));

                $('#debtRowTotalValue').val(selectedRowValue.attr('debtRowTotal'));

                if ($('#agmTotalDebtAmount') != 'undefined') {
                    $('#agmTotalDebtAmount').val(selectedRowValue.attr('debtRowTotal'));
                    $('#agmAgreedAmount').val(selectedRowValue.attr('debtRowTotal'));
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

            var sourcePin   = $("#cnpin").val();
            var uprn        = $("#uprn").val();
            
            loadDebtsView(data);

            refreshPersonAttributes(sourcePin);
            refreshCurrentAttributes(sourcePin);
            refreshAddresses(sourcePin);
            refresMatchList(sourcePin);
            refresMisMisMatchList(sourcePin);
            refreshPersonDetails(sourcePin, uprn);
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
            doProgress(result.d.length, 'arefRecovery');
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
            doProgress(result.d.length, 'arefParties');
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#tableParties").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'PartyType' },
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
                    },
                {
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
function refreshNotes(debtId) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetDebtNotes",
        data: "{'debtId':'" + debtId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefNotes');
            if (result.hasOwnProperty("d")) { result = result.d; }
            
            $("#tableNotes").dataTable({
                "destroy": true,
                "aaData": result,
                aoColumns: [
                    { mData: 'CreatedDate' },
                    { mData: 'UserId' },
                    { mData: 'NoteText' },
                    { mData: 'NoteId' }
                ],
                "aoColumnDefs": [
                    {
                        "aTargets": ["note_id"],
                        "bVisible": false
                    },
                    {
                        "sTitle": "Created Date",
                        "aTargets": ["created_date"],
                        "mRender": function(value, type, full) {
                            var dtStart = new Date(parseInt(value.substr(6)));
                            var dtStartWrapper = moment(dtStart);
                            return dtStartWrapper.format('DD/MM/YYYY');
                        }
                    },
                    { "width": "100px", "targets": 0 },
                    { "width": "100px", "targets": 1 },
                    { "width": "*%", "targets": 2 }
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
function refreshAddresses(partyPin) {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetLinkedAddress",
        data: "{'sourcePin':'" + partyPin + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            doProgress(result.d.length, 'arefRelatedAddresses');
            if (result.hasOwnProperty("d")) { result = result.d; }
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
function updateProgressBar(percentage) {
    if (percentage == 0) {
        $("#loadProgress").hide();
        $("#loadProgress").css("width", percentage + "%");
    } else {
        $("#loadProgress").show();
        $("#loadProgress").css("width", percentage + "%");
    }
}
function doProgress(resultLength, controlName) {

    var fontWeight      = 'normal';
    progressValue = progressValue + progressInterval;

    console.log(controlName + ' - ' + progressValue + ' - ' + resultLength + ' - ' + fontWeight);
    
    if (resultLength > 0)                       {  fontWeight = 'bold'; }
        
    $("#" + controlName).css("font-weight", fontWeight);
    $("#loadProgress").css("width", progressValue + "%");

    if (progressValue <= 0) {
        $("#loadProgress").hide();
    }
    if (progressValue >= 100) {
        setTimeout(function () { $("#loadProgress").hide(); progressInterval = 10; progressValue = 0; }, 1000);
    }

    if (progressValue > 0) {
        $("#loadProgress").show();
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

Array.prototype.contains = function (v) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] === v) return true;
    }
    return false;
};
Array.prototype.unique = function () {
    var arr = [];
    for (var i = 0; i < this.length; i++) {
        if (!arr.contains(this[i])) {
            arr.push(this[i]);
        }
    }
    return arr;
}

