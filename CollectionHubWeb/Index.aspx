<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Collection Hub</title>

    <script type="text/javascript" charset="utf8" src="js/jquery-1.11.1.min.js"></script>
        <script type="text/javascript" charset="utf8" src="js/jquery-ui.min.js"></script>
        <script type="text/javascript" charset="utf8" src="js/jquery.dataTables.min.js"></script>
        <script type="text/javascript" charset="utf8" src="js/bootstrap.min.js"></script>
        <script type="text/javascript" charset="utf8" src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
        <script type="text/javascript" charset="utf8" src="js/sb-admin.min.js"></script>
        <script type="text/javascript" charset="utf8" src="js/plugins/moment/moment.js"></script>
        <link rel="stylesheet" type="text/css" href="css/jquery.dataTables.min.css">
        <link rel="stylesheet" type="text/css" href="css/jquery-ui.min.css">
        <link rel="stylesheet" type="text/css" href="css/font-awesome/css/font-awesome.min.css">
        <link rel="stylesheet" type="text/css" href="css/plugins/morris/morris-0.4.3.min.css">
        <link rel="stylesheet" type="text/css" href="css/bootstrap.css">
        <link rel="stylesheet" type="text/css" href="css/sb-admin.css">
        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
            <script src="http://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
            <script src="http://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
        <![endif]-->
    <link rel="icon" type="image/png" href="favicon.png">
</head>
<body>
    <form role="form" name="mainForm" id="mainForm" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 col-md-offset-3">
                <asp:Panel runat="server" ID="badAuth" Visible="false" CssClass="alert alert-danger">
                    Could not authenticate with the provided credentials. Please check and try again.
                </asp:Panel>
            </div>
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Please Sign In</h3>
                    </div>
                    <div class="panel-body">
                        <fieldset>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" runat="server" ID="authLoginName" placeholder="E-mail" name="email" type="email" autofocus></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" runat="server" ID="authPassword" placeholder="Password" name="password" type="password" value=""></asp:TextBox>
                            </div>
                            <asp:Button runat="server" ID="submitForm" Text="Login" CssClass="btn btn-lg btn-success btn-block" OnClick="submitForm_Click" />
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

