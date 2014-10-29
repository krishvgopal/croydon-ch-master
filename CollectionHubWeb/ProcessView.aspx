<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="ProcessView.aspx.cs" Inherits="ProcessViewer" %>

<%@ Register Src="~/Common/ActionMenu.ascx" TagName="ActionMenu" TagPrefix="am" %>
<%@ Register Src="~/Common/MainNavigation.ascx" TagName="NavigationMenu" TagPrefix="nm" %>
<%@ Register Src="~/Common/SiteHeader.ascx" TagName="SiteHeader" TagPrefix="sh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMenu" Runat="Server">
    <sh:SiteHeader ID="SiteHeader" runat="server" />
    <asp:HiddenField ID="sourceValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="sourceRefValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="selectedDebtId" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="cnpin" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="debtRowTotalValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="BatchRunId" runat="server" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headInfo" Runat="Server">
    <am:ActionMenu ID="pageActionMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menuSide" Runat="Server">
    <nm:NavigationMenu ID="NavigationMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" Runat="Server">
    <div class="row">
        <div class="col-lg-12"><br /></div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="col-lg-12">
        <div id="processTitle"></div>
        <div id="processDescription"></div>
        <hr />
    </div>
    <div class="col-lg-3" id="searchPane">
        <div id="processFieldTags"></div>
        <br />
        <a href="#" onclick="postValues();" class="btn btn-primary">Run Query</a>
    </div>
    <div class="col-lg-12" id="resultsPane">    
        <table class="table table-striped table-bordered table-hover" width="100%" id="dataTableBatchResults">
            <thead>
                <tr>
                    <th class="RowIdentifier"></th>
                    <th class="Source">Source</th>
                    <th class="DebtAccount">Debt Account</th>
                    <th class="FullName">Full Name</th>
                    <th class="FullAddress">Full Address</th>
                    <th class="ThisDebt">This Debt</th>
                    <th class="ThisDebtOS">This Debt OS</th>
                    <th class="DebtCount">Debt Count</th>
                    <th class="AllDebtAmount">All Debt Amount</th>
                    <th class="AllDebtOS">All Debt OS</th>
                    <th class="FromDate">From Date</th>
                    <th class="UntilDate">Until Date</th>
                </tr>
            </thead>
        </table>
         <a data-toggle="modal" href="modals/CancelBatch.html"  data-target="#cancelApprove" class="btn btn-outline btn-primary">Cancel Batch</a>
         <a data-toggle="modal" href="modals/ApproveBatch.html" data-target="#acceptApprove" class="btn btn-outline btn-primary">Activate Batch</a>
    </div> 

    <div class="modal fade" id="cancelApprove" tabindex="-1" role="dialog" aria-labelledby="cancelApproveModal" aria-hidden="true" >
        <div class="modal-dialog" >
            <div class="modal-content">
            </div>
        </div>
    </div>
    <div class="modal fade" id="acceptApprove" tabindex="-1" role="dialog" aria-labelledby="acceptApproveModal" aria-hidden="true" >
        <div class="modal-dialog">
            <div class="modal-content">
            </div>
        </div>
    </div>

    <script type="text/javascript" charset="utf8" src="js/ProcessView.js"></script>
</asp:Content>


