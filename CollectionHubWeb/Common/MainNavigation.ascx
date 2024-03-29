﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainNavigation.ascx.cs" Inherits="Common_MainNavigation" %>

 <ul class="nav" id="side-menu">
    <li><a title="Dashboard"            name="menuList" href="Dashboard.aspx"           thisLabel="&nbsp;Dashboard"                 thisFA="fa-dashboard">  <i class="fa fa-dashboard fa-fw"></i>   </a></li>
    <li><a title="Search"               name="menuList" href="Search.aspx"              thisLabel="&nbsp;Name & Address Search"     thisFA="fa-search">     <i class="fa fa-search fa-fw"></i>      </a></li>
    <li><a title="Debt Search"          name="menuList" href="DebtSearch.aspx"          thisLabel="&nbsp;Debt Search"               thisFA="fa-search">     <i class="fa fa-search fa-fw"></i>      </a></li>
    <li><a title="Work Search"          name="menuList" href="WorkSearch.aspx"          thisLabel="&nbsp;Work Search"               thisFA="fa-crosshairs"> <i class="fa fa-crosshairs fa-fw"></i>  </a></li>
    <li><a title="My Cases"             name="menuList" href="MyCases.aspx"             thisLabel="&nbsp;My Cases"                  thisFA="fa-briefcase">  <i class="fa fa-briefcase fa-fw"></i>   </a></li>
    <%--<li><a title="Case Overview"        name="menuList" href="CaseOverview.aspx"        thisLabel="&nbsp;Case Overview"             thisFA="fa-eye">        <i class="fa fa-eye fa-fw"></i>         </a></li>--%>
    <li><a title="New Process"          name="menuList" href="NewProcess.aspx"          thisLabel="&nbsp;New Process"               thisFA="fa-cogs">       <i class="fa fa-cogs fa-fw"></i>        </a></li>
    <li><a title="Processes"            name="menuList" href="Processes.aspx"           thisLabel="&nbsp;Processes"                 thisFA="fa-tasks">      <i class="fa fa-tasks fa-fw"></i>       </a></li>
    <li><a title="Document Templates"   name="menuList" href="DocumentTemplates.aspx"   thisLabel="&nbsp;Documents"                 thisFA="fa-file">       <i class="fa fa-file fa-fw"></i>        </a></li>
    <li><a title="Application Settings" name="menuList" href="ApplicationSettings.aspx" thisLabel="&nbsp;Application Settings"      thisFA="fa-wrench">     <i class="fa fa-wrench fa-fw"></i>      </a></li>
    <ul class="nav" id="side-menu">
        <li><a href="#" onclick="toggleNavigation();"> <i id="resizeContainer" class="fa fa-expand"></i> </a></li>
    </ul>
</ul>