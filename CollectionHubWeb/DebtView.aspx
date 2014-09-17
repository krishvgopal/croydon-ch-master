<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="DebtView.aspx.cs" Inherits="Search" %>
<%@ Register Src="~/Common/ActionMenu.ascx" TagName="ActionMenu" TagPrefix="am" %>
<%@ Register Src="~/Common/MainNavigation.ascx" TagName="NavigationMenu" TagPrefix="nm" %>
<%@ Register Src="~/Common/SiteHeader.ascx" TagName="SiteHeader" TagPrefix="sh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMenu" Runat="Server">
    <sh:SiteHeader ID="SiteHeader" runat="server" />
    <asp:HiddenField ID="sourceValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="sourceRefValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="selectedDebtId" runat="server" ClientIDMode="Static" />
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
    <div class="row">
        <div class="col-lg-12">
            <div class="well" style="height:75px;">
                <div class="col-lg-2">
                    <div class="form-group">
                        <label>Full Name</label>
                        <p class="form-control-static">
                            <asp:Literal ID="pageFullName" runat="server"></asp:Literal>
                        </p>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="form-group">
                        <label>Address</label>
                        <p class="form-control-static">
                            <asp:Literal ID="pageFullAddress" runat="server"></asp:Literal>
                        </p>
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class="form-group">
                        <label>Total Debt</label>
                        <p class="form-control-static">
                            <strong>
                                <asp:Literal ID="pageTotalDebt" runat="server"></asp:Literal>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="col-lg-2">
                    <div class="form-group">
                        <label>Total Debt Outstanding</label>
                        <p class="form-control-static">
                            <strong>
                                <asp:Literal ID="pageDebtOutstanding" runat="server"></asp:Literal>
                            </strong>
                        </p>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <br/>
                <table class="table table-striped table-bordered table-hover" id="dataTableMain">
                    <thead>
                        <tr>
                            <th class="select_id">Select</th>
                            <th class="debt_id">Debt Id</th>
                            <th>Debt Source</th>
                            <th>Debt Acc Ref</th>
                            <th>Debt Reference</th>
                            <th>Debt Total</th>
                            <th>Debt Outstanding</th>
                            <th>Party PIN</th>
                            <th>Property Reference</th>
                            <th>Recovery Cycle</th>
                            <th>Status</th>
                            <th>Type</th>
                        </tr>
                    </thead>
                </table>
                <a data-toggle="modal" href="modals/page.html" data-target="#myModal">Create Group</a>
                <a data-toggle="modal" href="modals/CreateDebtAttributes.html" data-target="#myModal">Create Debt Attribute</a>
            </div>
            <div style="clear:both"><br /></div>
            <ul class="nav nav-tabs">
                <li class="active"><a href="#recovery" data-toggle="tab">Recovery History</a></li>
                <li><a href="#payments"     data-toggle="tab">Payments</a></li>
                <li><a href="#parties"      data-toggle="tab">Parties</a></li>
                <li><a href="#arrangements" data-toggle="tab">Arrangements</a></li>
                <li><a href="#debt"         data-toggle="tab">Debt Attributes</a></li>
                <li><a href="#person"       data-toggle="tab">Person Attributes</a></li>
                <li><a href="#notes"        data-toggle="tab">Notes</a></li>
                <li><a href="#current"      data-toggle="tab">Current Attributes</a>
                </li>
            </ul>

            <div class="tab-content">
                <div class="tab-pane fade in active" id="recovery">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableRecovery">
                            <thead>
                                <tr>
                                    <th>Recovery Cycle</th>
                                    <th>Stage</th>
                                    <th>Stage Type</th>
                                    <th>Status</th>
                                    <th>Days</th>
                                    <th>Target Date</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <div>
                            <br />
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Create New Recovery Cycle
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>Recovery Cycle</label>
                                            <select id="recoveryCycles" class="form-control"></select>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <label>Start Date</label>
                                            <input type="text" id="datepicker" class="date-picker form-control">
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <br />
                                            <a data-toggle="modal" href="modals/ConfirmCreateRecoveryCycle.html" data-target="#myModal2" class="btn btn-default">Create Cycle</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                    </div>

                </div>

                <div class="tab-pane fade" id="payments">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tablePayments">
                            <thead>
                                <tr>
                                    <th>Payment Id</th>
                                    <th>Source</th>
                                    <th>Acc. Reference</th>
                                    <th>Debt Ref.</th>
                                    <th>Party Pin</th>
                                    <th>Amount</th>
                                    <th>Date</th>
                                    <th>Created Date</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
                <div class="tab-pane fade" id="parties">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableParties">
                            <thead>
                                <tr>
                                    <th>Recovery Cycle</th>
                                    <th>Stage</th>
                                    <th>Method</th>
                                    <th>Status</th>
                                    <th>Complete</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
                <div class="tab-pane fade" id="arrangements">
                    <br/>
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableArrangements">
                            <thead>
                                <tr>
                                    <th>Recovery Cycle</th>
                                    <th>Stage</th>
                                    <th>Method</th>
                                    <th>Status</th>
                                    <th>Complete</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>

                <div class="tab-pane fade" id="debt">
                    <br/>
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableDebt">
                            <thead>
                                <tr>
                                    <th>Recovery Cycle</th>
                                    <th>Stage</th>
                                    <th>Method</th>
                                    <th>Status</th>
                                    <th>Complete</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>

                <div class="tab-pane fade" id="person">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tablePerson">
                            <thead>
                                <tr>
                                    <th>Recovery Cycle</th>
                                    <th>Stage</th>
                                    <th>Method</th>
                                    <th>Status</th>
                                    <th>Complete</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>

                <div class="tab-pane fade" id="notes">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableNotes">
                            <thead>
                                <tr>
                                    <th>Recovery Cycle</th>
                                    <th>Stage</th>
                                    <th>Method</th>
                                    <th>Status</th>
                                    <th>Complete</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>

                <div class="tab-pane fade" id="current">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableCurrent">
                            <thead>
                                <tr>
                                    <th>Recovery Cycle</th>
                                    <th>Stage</th>
                                    <th>Method</th>
                                    <th>Status</th>
                                    <th>Complete</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div> 
            </div>
        </div>

        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel2" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </div>

        <div class="modal fade" id="myModal3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel3" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </div>

</div>
<script type="text/javascript" charset="utf8" src="js/DebtView.js"></script>
</asp:Content>

