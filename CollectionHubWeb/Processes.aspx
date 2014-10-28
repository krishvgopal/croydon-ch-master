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
              <table class="table table-striped table-bordered table-hover" id="dataTableBatchProcessHistory">
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
               </table>
         </div> 
    </div>

    <script type="text/javascript" charset="utf8" src="js/Processes.js"></script>
</asp:Content>

