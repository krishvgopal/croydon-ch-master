<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="NewProcess.aspx.cs" Inherits="NewProcess" %>
 
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
        <div class="col-lg-4">    
             <table class="table table-striped table-bordered table-hover" id="dataTableBatchProcessJobs">
                <thead>
                    <tr>
                        <th class="batch_name">Batch Name</th>
                        <th class="bp_debt_source">Source</th>
                        <th class="bp_id">Id</th>
                    </tr>
                </thead>
            </table>
        </div> 
         <div class="col-lg-8">    
              <%--<table class="table table-striped table-bordered table-hover" id="dataTableBatchProcessHistory">
                <thead>
                    <tr>
                        <th class="bph_process_date">Process Date</th>
                        <th class="bp_debt_source">Source</th>
                        <th class="bp_batch_name">Batch Name</th>
                        <th class="pm_name">Process Name</th>
                        <th class="bph_records_affected">Records Affected</th>
                        <th class="UserName">User Name</th>
                    </tr>
                </thead>
               </table>--%>

             <%-- 
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
                  --%>

         </div> 
    </div>

    <script type="text/javascript" charset="utf8" src="js/NewProcess.js"></script>
</asp:Content>

