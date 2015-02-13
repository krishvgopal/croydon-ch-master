<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="ServerSideGrid.aspx.cs" Inherits="ServerSideGrid" %>
<%@ Register Src="~/Common/ActionMenu.ascx"     TagName="ActionMenu"        TagPrefix="am" %>
<%@ Register Src="~/Common/MainNavigation.ascx" TagName="NavigationMenu"    TagPrefix="nm" %>
<%@ Register Src="~/Common/SiteHeader.ascx"     TagName="SiteHeader"        TagPrefix="sh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMenu" Runat="Server">
    <sh:SiteHeader ID="SiteHeader" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headInfo" Runat="Server">
    <am:ActionMenu ID="pageActionMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menuSide" Runat="Server">
    <nm:NavigationMenu ID="NavigationMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" Runat="Server">
    <div class="row">
        <div style="height:25px"></div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="pageBody" Runat="Server">
    
    <div class="row">
        
        <div class="col-lg-12">    
    
            <table class="table compact table-striped table-bordered table-hover" id="searchTable">
                <thead>
                    <tr>
                        <th>First name</th>
                        <th>Last name</th>
                        <th>Position</th>
                        <th>Office</th>
                        <th>Start date</th>
                        <th>Salary</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th>First name</th>
                        <th>Last name</th>
                        <th>Position</th>
                        <th>Office</th>
                        <th>Start date</th>
                        <th>Salary</th>
                    </tr>
                </tfoot>
            </table>
            
        </div>
            
    </div>

    <script>
               
        $(document).ready(function () {
            $('#searchTable').dataTable({
                "processing": false,
                "serverSide": true,
                "ajax": "scripts/pro.aspx",
                "aoColumnDefs": [
                   {
                       "aTargets": [0],
                       "bVisible": true,
                   }
                ],
                "initComplete": function (settings, json) {
                    $('#searchTable tfoot th').each(function () {
                        var title = $('#searchTable thead th').eq($(this).index()).text();
                        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
                    });
                    var table = $('#searchTable').DataTable();
                    table.columns().eq(0).each(function (colIdx) {
                        $('input', table.column(colIdx).footer()).on('keypress', function (e) {
                            var code = (e.keyCode ? e.keyCode : e.which);
                            if (code == 13) {
                                table
                                .column(colIdx)
                                .search(this.value)
                                .draw();
                            }
                        });
                    });
                }
            });
        });

    </script>     
             
</asp:Content>