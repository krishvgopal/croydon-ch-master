
refreshBatchProcessJobs();
refreshBatchProcessHistory();
refreshBatchRunHistory();

function refreshBatchProcessHistory() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetBatchProcessHistory",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#dataTableBatchJobs").dataTable({
                "destroy": true,
                "scrollCollapse": true,
                "paging": false,
                "bFilter": false,
                "aaData": result,
                "aoColumns": [
                    { mData: 'bph_process_date' },
                    { mData: 'bp_debt_source' },
                    { mData: 'bp_batch_name' },
                    { mData: 'pm_name' },
                    { mData: 'bph_records_affected' },
                    { mData: 'UserName' }
                ],
            });
        }
    });
};

function refreshBatchProcessJobs() {
    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetBatchProcessJobs",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#dataTableBatchProcessJobs").dataTable({
                "destroy": true,
                "scrollCollapse": true,
                "paging": false,
                "bFilter": false,
                "aaData": result,
                "aoColumns": [
                    { mData: 'batch_name' },
                    { mData: 'bp_debt_source' },
                    { mData: 'bp_id' }
                ],
                "aoColumnDefs": [
                {
                    "sTitle": "Debt ID",
                    "aTargets": ["batch_name"],
                    "mRender": function(value, type, full) {
                        return '<a target="_blank" href="ProcessView.aspx?p=' + full.bp_id + ' ">' + value + '</a>';
                    }
                },
                {
                    "aTargets": ["bp_id"],
                    "bVisible": false
                    }
                ]
            });
        }
    });
};

function refreshBatchRunHistory() {

    $.ajax({
        type: "POST",
        url: "DataService.aspx/GetBatchRunHistory",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.hasOwnProperty("d")) { result = result.d; }
            $("#dataTableBatchRunHistory").dataTable({
                "destroy": true,
                "aaData": result,
                "aoColumns": [
                    { mData: 'B_ID' },
                    { mData: 'UserName' },
                    { mData: 'BatchName' },
                    { mData: 'DateCreated' },
                    { mData: 'Records' },
                    { mData: 'BatchStatus' },
                    { mData: 'DebtAtStart' },
                    { mData: 'DebtOSNow' }
                ],
                "aoColumnDefs": [
                {
                    "sTitle": "",
                    "aTargets": ["B_ID"],
                    "mRender": function(value, type, full) {
                        return '<a target="_blank" href="ProcessView.aspx?p=' + full.B_ID + ' ">View</a>';
                    }
                },
                {
                        "sTitle": "Date Created"
                        , "aTargets": ["Date_Created"]
                        , "mRender": function (value, type, full) {
                            if (value != null) {
                                var dtStart = new Date(parseInt(value.substr(6)));
                                var dtStartWrapper = moment(dtStart);
                                return dtStartWrapper.format('DD/MM/YYYY');
                            } else { return ''; }
                        }
                    }

                ]

            });
        }
    });
};
