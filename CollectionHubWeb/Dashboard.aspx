<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>
<%@ Register Src="~/Common/ActionMenu.ascx" TagName="ActionMenu" TagPrefix="am" %>
<%@ Register Src="~/Common/MainNavigation.ascx" TagName="NavigationMenu" TagPrefix="nm" %>
<%@ Register Src="~/Common/SiteHeader.ascx" TagName="SiteHeader" TagPrefix="sh" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="headMenu" Runat="Server">
    <sh:SiteHeader ID="SiteHeader" runat="server" />
    <script src="js/plugins/morris/raphael-2.1.0.min.js"></script>
    <script src="js/plugins/morris/morris.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headInfo" Runat="Server">
    <am:ActionMenu ID="pageActionMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menuSide" Runat="Server">
    <nm:NavigationMenu ID="NavigationMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" Runat="Server">
    <%-- <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">Dashboard</h2>
            </div>
        </div>--%>
    <br/>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="col-lg-12">
        <div class="panel panel-default">
            <div class="panel-heading">
                Recovery Overview
                <div class="pull-right">
                    <div class="btn-group">
                        <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                            Actions
                            <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu pull-right" role="menu">
                            <li><a href="#" onclick="displayPercentageReport();">Show Percentage</a></li>
                            <li><a href="#" onclick="displayAmountReport();">Show Amounts</a></li>
                        </ul>
                    </div>
                </div>
            </div>        
            <div class="panel-body">
                <div id="morris-area-chart"></div>
            </div>      
            <script>
                       
                //displayPercentageReport();
                //displayAmountReport();

                function createChart(yMin, yMax) {
                    $('#morris-area-chart').empty();
                    var chart = Morris.Bar({
                        element: 'morris-area-chart',
                        data: "[0,0]",
                        xkey: 'y',
                        ykeys: ['a', 'b', 'c', 'd'],
                        labels: ['Council Tax', 'Housing Arrears', 'Housing Benifits', 'Parking'],
                        ymin: yMin,
                        ymax: yMax
                    });
                    return chart;
                }

                function displayPercentageReport() {
                    console.log('displayPercentageReport');
                    var thisChart = createChart(0, 100);
                    $.ajax({
                        type: "POST",
                        url: "DataService.aspx/GetDashboardDataPercentByYear",
                        data: "{'sourceId':'0','historic':'1'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(result) {
                            var jsonValue = jQuery.parseJSON(result.d);
                            thisChart.setData(jsonValue);
                        }
                    });
                }
                function displayAmountReport() {
                    console.log('displayAmountReport');
                    var thisChart = createChart(0, 30000000);
                    $.ajax({
                        type: "POST",
                        url: "DataService.aspx/GetDashboardDataAmountByYear",
                        data: "{'sourceId':'0','historic':'1'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            var jsonValue = jQuery.parseJSON(result.d);
                            thisChart.setData(jsonValue);
                        }
                    });
                }
                function displayDebtsReport() {
                    console.log('displayDebtsReport');
                    var thisChart = createChart(0, 100);
                    $.ajax({
                        type: "POST",
                        url: "DataService.aspx/GetDashboardDataDebtsByYear",
                        data: "{'sourceId':'0','historic':'1'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            var jsonValue = jQuery.parseJSON(result.d);
                            chart.setData(jsonValue);
                            chart.ymax = 10000000;
                        }
                    });
                }
                function displayBalanceReport() {
                    $.ajax({
                        type: "POST",
                        url: "DataService.aspx/GetDashboardDataBalanceByYear",
                        data: "{'sourceId':'0','historic':'1'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            var jsonValue = jQuery.parseJSON(result.d);
                            chart.setData(jsonValue);
                            chart.ymax = 10000000;
                        }
                    });
                }

            </script>


        </div>
    </div>
</asp:Content>

