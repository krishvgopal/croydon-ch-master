<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Collection Hub</title>
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet">
    <link href="css/sb-admin.css" rel="stylesheet">
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
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/sb-admin.js"></script>
    </form>
</body>
</html>

