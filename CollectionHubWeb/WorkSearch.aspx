<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="WorkSearch.aspx.cs" Inherits="DebtSearchPage" %>
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
            <div class="row">
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Amount From</label>
                        <input class="form-control-compact" id="amountFrom" name="firstName">
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Amount To</label>
                        <input class="form-control-compact" id="amountTo" name="lastName">
                    </div>
                </div>
                <div style="clear: both;"></div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Includes Debt Stream</label>
                        <select class="form-control-compact" id="debtStreamCode" multiple></select>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Current Treatment Cycle</label>
                        <select class="form-control-compact" id="currentTreatmentCycle"></select>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Debts Raised Within Last No. Days</label>
                        <input class="form-control-compact" id="daysOld" name="daysOld">
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <br />
                        <button type="button" class="btn btn-default" onclick="clearMainForm();">Clear Search</button>
                        <button type="button" class="btn btn-default" onclick="doSearch();">Search</button>
                        <img id="loadingImage" src="img/gif-load.gif" alt="Loading Image" />
                    </div>
                </div>
            </div>                
        </div>  
        <div id="searchResults" >
            <div class="col-lg-12">
                <br />
                <div class="table-responsive" id="searchResultsTable">
                    <select class="form-control-compact" id="assignedUserList" style="visibility: hidden"></select>
                    <table class="table compact table-striped table-bordered table-hover" id="searchTable">
                        <thead> 
                            <tr>
                                <th class="debt_id">Debt Id</th>
                                <th class="source">Debt Source</th>
                                <th class="SourceAccountReference">Source Account Reference</th>
                                <th class="debt_total">Total Amount</th>
                                <th class="debt_outstanding">Outstanding Amount</th>
                                <th class="debt_date">Debt Date</th>
                                <th class="cnpin">PIN</th>
                                <th class="uprn">UPRN</th>
                                <th class="assign">Assign To user</th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" charset="utf8" src="js/WorkSearch.js"></script>
</asp:Content>