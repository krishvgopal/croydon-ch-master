<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="CaseOverview.aspx.cs" Inherits="CaseOverview" %>

<%@ Register Src="~/Common/ActionMenu.ascx"     TagName="ActionMenu"        TagPrefix="am" %>
<%@ Register Src="~/Common/MainNavigation.ascx" TagName="NavigationMenu"    TagPrefix="nm" %>
<%@ Register Src="~/Common/SiteHeader.ascx"     TagName="SiteHeader"        TagPrefix="sh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMenu" Runat="Server">
    <sh:SiteHeader   ID="SiteHeader"        runat="server" />
<%--    <asp:HiddenField ID="sourceValue"       runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="sourceRefValue"    runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="selectedDebtId"    runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="cnpin"             runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="debtRowTotalValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="BatchRunId"        runat="server" ClientIDMode="Static" />--%>

    <asp:HiddenField ID="ParentId"          runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="ThisId"            runat="server" ClientIDMode="Static" />
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
    <div class="row">
        <div class="col-lg-12">    
            <div class="row">
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Select User</label>
                        <select class="form-control-compact" id="userOverview"></select>
                    </div>
                </div>
            </div>
            <hr/>
        </div>
        <div class="col-lg-12" id="resultsPane">    
            <table class="table compact table-striped table-bordered table-hover" width="100%" id="dataTableBatchProcessResults">
                <thead>
                    <tr>
                        <th class="RowIdentifier">RowIdentifier</th> 
                        <th class="RecordID">RecordID</th>
                        <th class="PIN">PIN</th>
                        <th class="UPRN">UPRN</th>
                        <th class="Source">Source</th>
                        <th class="DebtAccount">DebtAccount</th>
                        <th class="FullName">FullName</th>
                        <th class="FullAddress">FullAddress</th>
                        <th class="ThisDebt">ThisDebt</th>
                        <th class="ThisDebtOS">ThisDebtOS</th>          
                        <th class="Debt_As_At">Debt As At</th>
                        <th class="Debt_OS">Debt OS</th>
                        <th class="Reduction">Reduction</th>
                        <th class="Included">Included</th>
                    </tr>
                </thead>
            </table>
        </div> 
    </div>
    <script type="text/javascript" charset="utf8" src="js/MyCases.js"></script>
</asp:Content>

