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
        <div id="processFieldTags"></div>
    </div>
    
    <!--
        -->
    


    <script type="text/javascript" charset="utf8" src="js/ProcessView.js"></script>
</asp:Content>


