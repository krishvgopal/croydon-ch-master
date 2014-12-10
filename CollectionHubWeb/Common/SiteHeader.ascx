<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteHeader.ascx.cs" Inherits="Common_SiteHeader" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI.HtmlControls" Assembly="System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" %>

<nav class="navbar navbar-default navbar-fixed-top" role="navigation" style="margin-bottom: 0">
    <div class="navbar-header">
        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".sidebar-collapse">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
        </button>
        <a class="navbar-brand" href="index.aspx">Collection Hub v1.3</a> 
        <asp:Literal runat="server" ID="versionInfo" ClientIDMode="Static"></asp:Literal>
    </div>
    <div class="nav navbar-top-links navbar-right" id="right_Link">
        <p style="padding-top:15px;padding-right:15px">
            <asp:HtmlGenericControl runat="server" ID="userName"></asp:HtmlGenericControl>
        </p>
    </div>
</nav>
