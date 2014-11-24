<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="DebtSearch.aspx.cs" Inherits="Search" %>
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
        <h3 class="page-header">Debt Search</h3>
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
                
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Stream Count</label>
                        <select class="form-control-compact" id="debtStreamCount">
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                        </select>
                    </div>
                </div>
                

                <div style="clear: both;"></div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Includes Debt Streem</label>
                        <select class="form-control-compact" id="debtStreamCode">
                        </select>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Last Payment Made</label>
                        <select class="form-control-compact" id="lastPayment">
                            <option value="0">All</option>
                            <option value="1">Within Last Week</option>
                            <option value="2">Within Last Month</option>
                            <option value="3">Within Last Quarter</option>
                            <option value="4">Within Last Year</option>
                            <option value="5">No Payments Made</option>
                        </select>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Debt Age</label>
                        <select class="form-control-compact" id="debtAge">
                            <option value="0">All</option>
                            <option value="1">Within Last Week</option>
                            <option value="2">Within Last Month</option>
                            <option value="3">Within Last Quarter</option>
                            <option value="4">Within Last Year</option>
                            <option value="5">Within Last Two Years</option>
                            <option value="6">Within Last Six Years</option>
                            <option value="7">Six Years Of Greater</option>
                        </select>
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
                    <table class="table compact table-striped table-bordered table-hover" id="searchTable">
                        <thead> 
                            <tr>
                                <th class="full_name">Full Name</th>
                                <th>Full Address</th>
                                <th>Debt Stream</th>
                                <th>No. Debts</th>
                                <th>Total Amount</th>
                                <th class="last_paid">Last Paid</th>
                                <th class="latest_debt">Latest Debt</th>
                                <th class="source">Souce</th>
                                <th class="cnpin">CnPin</th>
                                <%--<th class="urpin">UrPin</th>--%>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" charset="utf8" src="js/DebtSearch.js"></script>
</asp:Content>