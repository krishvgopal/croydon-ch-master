<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="headMenu" Runat="Server">
        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".sidebar-collapse">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
        </button>
        <a class="navbar-brand" href="index.html">Collection Hub v1.1</a>
     

       <%--     <script src="js/demo/dashboard-demo.js"></script>--%>
            <script src="js/plugins/morris/raphael-2.1.0.min.js"></script>
            <script src="js/plugins/morris/morris.js"></script>
     

    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="headInfo" Runat="Server">
        <ul class="nav navbar-top-links navbar-right">

        <li class="dropdown">

        <a class="dropdown-toggle" data-toggle="dropdown" href="#">
            <i class="fa fa-envelope fa-fw"></i>  <i class="fa fa-caret-down"></i>
        </a>

        <ul class="dropdown-menu dropdown-messages">

            <li>
                <a href="#">
                    <div>
                        <strong>John Smith</strong>
                        <span class="pull-right text-muted">
                            <em>Yesterday</em>
                        </span>
                    </div>
                    <div>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque eleifend...</div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="#">
                    <div>
                        <strong>John Smith</strong>
                        <span class="pull-right text-muted">
                            <em>Yesterday</em>
                        </span>
                    </div>
                    <div>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque eleifend...</div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="#">
                    <div>
                        <strong>John Smith</strong>
                        <span class="pull-right text-muted">
                            <em>Yesterday</em>
                        </span>
                    </div>
                    <div>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque eleifend...</div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a class="text-center" href="#">
                    <strong>Read All Messages</strong>
                    <i class="fa fa-angle-right"></i>
                </a>
            </li>
        </ul>



    </li>
    <!-- /.dropdown -->
    <li class="dropdown">
        <a class="dropdown-toggle" data-toggle="dropdown" href="#">
            <i class="fa fa-tasks fa-fw"></i>  <i class="fa fa-caret-down"></i>
        </a>
        <ul class="dropdown-menu dropdown-tasks">
            <li>
                <a href="#">
                    <div>
                        <p>
                            <strong>Task 1</strong>
                            <span class="pull-right text-muted">40% Complete</span>
                        </p>
                        <div class="progress progress-striped active">
                            <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 40%">
                                <span class="sr-only">40% Complete (success)</span>
                            </div>
                        </div>
                    </div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="#">
                    <div>
                        <p>
                            <strong>Task 2</strong>
                            <span class="pull-right text-muted">20% Complete</span>
                        </p>
                        <div class="progress progress-striped active">
                            <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="20" aria-valuemin="0" aria-valuemax="100" style="width: 20%">
                                <span class="sr-only">20% Complete</span>
                            </div>
                        </div>
                    </div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="#">
                    <div>
                        <p>
                            <strong>Task 3</strong>
                            <span class="pull-right text-muted">60% Complete</span>
                        </p>
                        <div class="progress progress-striped active">
                            <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                <span class="sr-only">60% Complete (warning)</span>
                            </div>
                        </div>
                    </div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="#">
                    <div>
                        <p>
                            <strong>Task 4</strong>
                            <span class="pull-right text-muted">80% Complete</span>
                        </p>
                        <div class="progress progress-striped active">
                            <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                <span class="sr-only">80% Complete (danger)</span>
                            </div>
                        </div>
                    </div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a class="text-center" href="#">
                    <strong>See All Tasks</strong>
                    <i class="fa fa-angle-right"></i>
                </a>
            </li>
        </ul>
        <!-- /.dropdown-tasks -->
    </li>
    <!-- /.dropdown -->
    <li class="dropdown">
        <a class="dropdown-toggle" data-toggle="dropdown" href="#">
            <i class="fa fa-bell fa-fw"></i>  <i class="fa fa-caret-down"></i>
        </a>
        <ul class="dropdown-menu dropdown-alerts">
            <li>
                <a href="#">
                    <div>
                        <i class="fa fa-comment fa-fw"></i> New Comment
                        <span class="pull-right text-muted small">4 minutes ago</span>
                    </div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="#">
                    <div>
                        <i class="fa fa-twitter fa-fw"></i> 3 New Followers
                        <span class="pull-right text-muted small">12 minutes ago</span>
                    </div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="#">
                    <div>
                        <i class="fa fa-envelope fa-fw"></i> Message Sent
                        <span class="pull-right text-muted small">4 minutes ago</span>
                    </div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="#">
                    <div>
                        <i class="fa fa-tasks fa-fw"></i> New Task
                        <span class="pull-right text-muted small">4 minutes ago</span>
                    </div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="#">
                    <div>
                        <i class="fa fa-upload fa-fw"></i> Server Rebooted
                        <span class="pull-right text-muted small">4 minutes ago</span>
                    </div>
                </a>
            </li>
            <li class="divider"></li>
            <li>
                <a class="text-center" href="#">
                    <strong>See All Alerts</strong>
                    <i class="fa fa-angle-right"></i>
                </a>
            </li>
        </ul>
        <!-- /.dropdown-alerts -->
    </li>
    <!-- /.dropdown -->
    <li class="dropdown">
        <a class="dropdown-toggle" data-toggle="dropdown" href="#">
            <i class="fa fa-user fa-fw"></i>  <i class="fa fa-caret-down"></i>
        </a>
        <ul class="dropdown-menu dropdown-user">
            <li>
                <a href="#"><i class="fa fa-user fa-fw"></i> User Profile</a>
            </li>
            <li>
                <a href="#"><i class="fa fa-gear fa-fw"></i> Settings</a>
            </li>
            <li class="divider"></li>
            <li>
                <a href="login.html"><i class="fa fa-sign-out fa-fw"></i> Logout</a>
            </li>
        </ul>
        <!-- /.dropdown-user -->
    </li>
    <!-- /.dropdown -->
</ul>
    </asp:Content>

    <asp:Content ID="Content3" ContentPlaceHolderID="menuSide" Runat="Server">
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
            <li><a href="dashboard.aspx"><i class="fa fa-dashboard fa-fw"></i> Dashboard</a></li>
            <li><a href="search.aspx"><i class="fa fa-search fa-fw"></i> Search</a></li>
            <li><a href="workqueue.aspx"><i class="fa fa-bars fa-fw"></i> Work Queue</a></li>
            <li><a href="debtrecovery.aspx"><i class="fa fa-jpy fa-fw"></i> Auto Debt Recovery</a></li>
        </ul>
    </asp:Content>

    <asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" Runat="Server">
       <%-- <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">Dashboard</h2>
            </div>
        </div>--%>
        <br/>
    </asp:Content>

    <asp:Content ID="Content5" ContentPlaceHolderID="pageBody" Runat="Server">
        <div class="col-lg-8">

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            
                            Recovery Overview

                            <div class="pull-right">
                                
                                <div class="btn-group">
                                    
                                    <button type="button" class="btn btn-default btn-xs dropdown-toggle" data-toggle="dropdown">
                                        Actions
                                        <span class="caret"></span>
                                    </button>

                                    <ul class="dropdown-menu pull-right" role="menu">
                                        <li><a href="#">Show Percentage</a></li>
                                        <li><a href="#">Show Amounts</a></li>
                                        <li><a href="#">Something else here</a></li>
                                        <li class="divider"></li>
                                        <li><a href="#">Separated link</a></li>
                                    </ul>

                                </div>

                            </div>
                            

                           

                        </div>
                        
                        
                          <div class="panel-body">
                            <div id="morris-area-chart"></div>
                        </div>
                       
                        
                      <script>
                          
                          var chart = Morris.Bar({ element: 'morris-area-chart',
                                                    data: "[0,0]",
                                                    xkey: 'y',
                                                    ykeys: ['a', 'b', 'c', 'd'],
                                                    labels: ['Council Tax', 'Housing Arrears', 'Housing Benifits', 'Parking'],
                                                    ymin: 0.0,
                                                    ymax: 100.0});

                          displayPercentageReport();

                          

                          function displayPercentageReport() {
                              $.ajax({
                                  type: "POST",
                                  url: "DataService.aspx/GetDashboardDataPercentByYear",
                                  data: "{'sourceId':'0','historic':'1'}",
                                  contentType: "application/json; charset=utf-8",
                                  dataType: "json",
                                  success: function(result) {
                                      var jsonValue = jQuery.parseJSON(result.d);
                                      chart.setData(jsonValue);
                                  }
                              });
                          }

                          function displayAmountReport() {
                              $.ajax({
                                  type: "POST",
                                  url: "DataService.aspx/GetDashboardDataAmountByYear",
                                  data: "{'sourceId':'0','historic':'1'}",
                                  contentType: "application/json; charset=utf-8",
                                  dataType: "json",
                                  success: function (result) {
                                      var jsonValue = jQuery.parseJSON(result.d);
                                      chart.setData(jsonValue);
                                      chart.ymax = 10000000;
                                  }
                              });
                          }

                          function displayDebtsReport() {
                              $.ajax({
                                  type: "POST",
                                  url: "DataService.aspx/GetDashboardDataDebtsByYear",
                                  data: "{'sourceId':'0','historic':'1'}",
                                  contentType: "application/json; charset=utf-8",
                                  dataType: "json",
                                  success: function (result) {
                                      var jsonValue = jQuery.parseJSON(result.d);
                                      chart.setData(jsonValue);
                                      chart.ymax = 10000000;
                                  }
                              });
                          }

                          function displayBalanceReport() {
                              $.ajax({
                                  type: "POST",
                                  url: "DataService.aspx/GetDashboardDataBalanceByYear",
                                  data: "{'sourceId':'0','historic':'1'}",
                                  contentType: "application/json; charset=utf-8",
                                  dataType: "json",
                                  success: function (result) {
                                      var jsonValue = jQuery.parseJSON(result.d);
                                      chart.setData(jsonValue);
                                      chart.ymax = 10000000;
                                  }
                              });
                          }

                          function displayAverageDebtValueReport() {
                              $.ajax({
                                  type: "POST",
                                  url: "DataService.aspx/GetDashboardDataBalanceByYear",
                                  data: "{'sourceId':'0','historic':'1'}",
                                  contentType: "application/json; charset=utf-8",
                                  dataType: "json",
                                  success: function (result) {
                                      var jsonValue = jQuery.parseJSON(result.d);
                                      chart.setData(jsonValue);
                                      chart.ymax = 10000000;
                                  }
                              });
                          }

                          


                      </script>


                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <i class="fa fa-bar-chart-o fa-fw"></i> Donut Chart Example
                        </div>
                        <div class="panel-body">
                            <div id="morris-donut-chart"></div>
                            <a href="#" class="btn btn-default btn-block">View Details</a>
                        </div>
                    </div>
                </div>
        

</asp:Content>

