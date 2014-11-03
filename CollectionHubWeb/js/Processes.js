
refreshBatchRunHistory();

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
                "iDisplayLength": 50,
                "aoColumns":
                    [
                        { mData: 'B_ID'         },
                        { mData: 'UserName'     },
                        { mData: 'BatchName'    },
                        { mData: 'DateCreated'  },
                        { mData: 'Records'      },
                        { mData: 'BatchStatus'  },
                        { mData: 'DebtAtStart'  },
                        { mData: 'DebtOSNow'    }
                    ],
                "aoColumnDefs": [
                {
                    "sTitle": "",
                    "aTargets": ["B_ID"],
                    "mRender": function(value, type, full) {
                        return '<a target="_blank" href="ProcessResults.aspx?p=' + full.B_ID + ' ">View</a>';
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
                },
                      { "width": "10px",  "targets": 0 }
                    , { "width": "100px", "targets": 1 }
                    , { "width": "*%",    "targets": 2 }
                    , { "width": "100px", "targets": 3 }
                    , { "width": "100px", "targets": 4 }
                    , { "width": "250px", "targets": 5 }
                    , { "width": "100px", "targets": 6 }
                    , { "width": "100px", "targets": 7 }
                ]

            });
        }
    });
};
