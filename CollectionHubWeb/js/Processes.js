
refreshBatchProcessJobs();
refreshBatchProcessHistory();

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
