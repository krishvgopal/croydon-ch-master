<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteHeader.ascx.cs" Inherits="Common_SiteHeader" %>

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
        <P style="padding-top:15px;padding-right:15px">
            Logged in as <asp:Literal runat="server" ID="userName"></asp:Literal>
        </P>
    </div>
</nav>
