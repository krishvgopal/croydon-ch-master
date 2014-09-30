<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainNavigation.ascx.cs" Inherits="Common_MainNavigation" %>

 <ul class="nav" id="side-menu">
    <li class="sidebar-search">
        <div class="input-group custom-search-form">
            <input type="text" class="form-control" placeholder="Search...">
            <span class="input-group-btn">
                <button class="btn btn-default" type="button">
                    <i class="fa fa-search"></i>
                </button>
            </span>
        </div>
    </li>
    <li><a href="Dashboard.aspx"><i class="fa fa-dashboard fa-fw"></i> Dashboard</a></li>
    <li><a href="Search.aspx"><i class="fa fa-search fa-fw"></i> Name & Address </a></li>
    <li><a href="DebtSearch.aspx"><i class="fa fa-search fa-fw"></i> Debt </a></li>

<%--    <li><a href="workqueue.aspx"><i class="fa fa-bars fa-fw"></i> Work Queue</a></li>
    <li><a href="debtrecovery.aspx"><i class="fa fa-jpy fa-fw"></i> Auto Debt Recovery</a></li>--%>
</ul>