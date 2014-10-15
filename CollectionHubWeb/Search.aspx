<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>
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
        <h3 class="page-header">Search</h3>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="row">
        <div class="col-lg-12">    
            <div class="row">
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>First Name</label>
                        <input class="form-control-compact" id="firstName" name="firstName">
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Last Name</label>
                        <input class="form-control-compact" id="lastName" name="lastName">
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>NINO</label>
                        <input class="form-control-compact">
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>D.O.B.</label>
                        <input class="form-control-compact">
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Address</label>
                        <input class="form-control-compact">
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Street</label>
                        <input class="form-control-compact">
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Postcode</label>
                        <input class="form-control-compact">
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <br />
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
                                <th class="record_selector">Debtor Name</th>  
                                <th>Address</th>  
                                <th>Total Debt</th>
                                <th class="source"></th>
                                <th class="pin_id"></th>
                                <th class="cn_pin"></th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" charset="utf8" src="js/Search.js"></script>
</asp:Content>