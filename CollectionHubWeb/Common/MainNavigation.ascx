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
    <li><a href="Dashboard.aspx">   <i class="fa fa-dashboard fa-fw"></i>   &nbsp;Dashboard</a></li>
    <li><a href="Search.aspx">      <i class="fa fa-search fa-fw"></i>      &nbsp;Name & Address</a></li>
    <li><a href="DebtSearch.aspx">  <i class="fa fa-search fa-fw"></i>      &nbsp;Debt</a></li>
    <li><a href="NewProcess.aspx">   <i class="fa fa-cogs fa-fw"></i>        &nbsp;New Process</a></li>
    <li><a href="Processes.aspx">    <i class="fa fa-cogs fa-fw"></i>        &nbsp;Processes</a></li>
    <li><a href="DocumentTemplates.aspx">    <i class="fa fa-file fa-fw"></i>        &nbsp;Documents</a></li>


    <ul class="nav" id="side-menu">
        <li><a href="#" onclick="toggleNavigation();"> <i id="resizeContainer" class="fa fa-expand"></i> </a></li>
    </ul>

</ul>

<script>

    function toggleNavigation()
    {
        var currentValue = $("#resizeContainer").attr("class");

        if (currentValue == 'fa fa-expand') {
            $("#page-wrapper").css({ 'margin': '0px 0px 0px 225px' });
            $("#side-nav-master").css({ 'width': '225px' });
            $("#resizeContainer").attr('class', 'fa fa-compress');
        } else {
            $("#page-wrapper").css({ 'margin': '0px 0px 0px 175px' });
            $("#side-nav-master").css({ 'width': '175px' });
            $("#resizeContainer").attr('class', 'fa fa-expand');
        }

        //if (currentValue == 'fa fa-expand') {
        //    $("#page-wrapper").css({ 'margin': '0px 0px 0px 500px' });
        //    $("#side-nav-master").css({ 'width': '500px' });
        //    $("#resizeContainer").attr('class', 'fa fa-compress');
        //} else {
        //    $("#page-wrapper").css({ 'margin': '0px 0px 0px 175px' });
        //    $("#side-nav-master").css({ 'width': '175px' });
        //    $("#resizeContainer").attr('class', 'fa fa-expand');
        //}

    };
</script>

