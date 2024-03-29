﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="SearchPage" %>
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
                        <label>Organisation Name</label>
                        <input class="form-control-compact" id="organisationName" name="organisationName">
                    </div>
                </div>
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
                        <label>National Insurance Number</label>
                        <input class="form-control-compact" id="nino" name="nino">
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Date Of Birth</label><br/>
                        <input class="form-control-compact ui-datepicker" style="width:33%;float:left;" id="dobDay" placeholder="Day" name="dob">
                        <input class="form-control-compact ui-datepicker" style="width:33%;float:left;" id="dobMonth" placeholder="Month" name="dob">
                        <input class="form-control-compact ui-datepicker" style="width:33%;float:left;" id="dobYear" placeholder="Year" name="dob">
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Address/Block No.</label>
                        <input class="form-control-compact" id="address" name="address">
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Street/Block Name</label>
                        <input class="form-control-compact" id="street" name="street">
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Postcode</label>
                        <input class="form-control-compact" id="postcode" name="postcode">
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Current Addresses Only</label>
                         <select class="form-control-compact" id="addressCurrent">
                             <option value="false">No</option>
                             <option value="true" selected="">Yes</option>
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
                                <th class="record_selector">Debtor Name</th>  
                                <th>Address</th> 
                                <th class="fromDate">From Date</th>
                                <th class="untilDate">Until Date</th> 
                                <th class="debt_total">Total Debt</th>
                                <th class="debt_outstanding">OS Debt</th>
                                <th class="source"></th>
                                <th class="pin_id"></th>
                                <th class="cn_pin"></th>
                                <th class="uprn"></th>
                                <th class="DebtAddresses">No. Debt Addresses</th>
                                <th class="CommAddresses">No. Comm Addresses</th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" charset="utf8" src="js/Search.js"></script>
</asp:Content>