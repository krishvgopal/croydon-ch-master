<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="MyCases.aspx.cs" Inherits="MyCases" %>

<%@ Register Src="~/Common/ActionMenu.ascx"     TagName="ActionMenu"        TagPrefix="am" %>
<%@ Register Src="~/Common/MainNavigation.ascx" TagName="NavigationMenu"    TagPrefix="nm" %>
<%@ Register Src="~/Common/SiteHeader.ascx"     TagName="SiteHeader"        TagPrefix="sh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMenu" Runat="Server">
    <sh:SiteHeader   ID="SiteHeader"        runat="server" />
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
    <div class="col-lg-12">    
        <div class="row">
            <div class="col-lg-3">
                <div class="form-group">
                    <label>In-Tray Status</label>
                    <select class="form-control-compact" id="intrayStatus" onClick="showAutoProcess();">
                        <option value="0">All</option>
                        <option value="1">Automatic</option>
                        <option value="2">In-Tray</option>
                    </select>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="form-group">
                    <label>Select User</label>
                    <select class="form-control-compact" id="userList" onClick="doTypeSearch();doItemSearch(0);"></select>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-12" style="padding: 0px;">  
        <hr/>
    </div>
    <div class="col-lg-3" id="corresTypeResult">    
        <table class="table compact table-striped table-bordered table-hover" width="100%" id="dataTableTypes">
             <thead> 
                <tr>
                    <th class="group_id"></th> 
                    <th class="group_name">Correspondance Type</th> 
                </tr>
             </thead>
        </table>
    </div>
    <div class="col-lg-9" id="corresItemsResult">    
        <table class="table compact table-striped table-bordered table-hover" width="100%" id="dataTableItems">
            <thead>
                <tr>
                    <th class="select_id"></th> 
                    <th class="debtId"></th> 
                    <th class="debtReference">Debt Reference</th>
                    <th class="debtDate">Debt Date</th>
                    <th class="documentName">Document Name</th>
                    <th class="documentDescription">Document Description</th>
                    <th class="debtOnDate">Debt On Date</th>
                    <th class="pin"></th>
                    <th class="uprn"></th>
                </tr>
            </thead>
        </table>
        <button type="button" class="btn btn-default" onclick="doProcess();" id="autoComplete" style="margin-top:20px;visibility:hidden;">Auto Complete</button>
    </div>
    <div id="DisplayPDF" style="visibility: hidden"></div>
    <script type="text/javascript" charset="utf8" src="js/MyCases.js"></script>
</asp:Content>

