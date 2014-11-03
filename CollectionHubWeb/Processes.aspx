<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="Processes.aspx.cs" Inherits="Processes" %>

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
        <br/>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="row">
         <div class="col-lg-12">    
            <table class="table compact table-striped table-bordered table-hover" id="dataTableBatchRunHistory">
                <thead>
                    <tr>
                        <th class="B_ID">B_ID</th>
                        <th class="User_name">User name</th>
                        <th class="Batch_name">Batch name</th>
                        <th class="Date_Created">Date Created</th>
                        <th class="Records">Records</th>
                        <th class="Batch_Status">Batch Status</th>
                        <th class="Debt_at_start">Debt at start</th>
                        <th class="Debt_OS_now">Debt OS now</th>
                    </tr>
                </thead>
            </table>
         </div> 
    </div>
    <br/>
    <br/>
    <script type="text/javascript" charset="utf8" src="js/Processes.js"></script>
</asp:Content>

