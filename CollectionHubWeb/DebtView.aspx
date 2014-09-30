<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="DebtView.aspx.cs" Inherits="Search" %>
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
                            <th>Debt Source</th>
                            <th>Acc Ref</th>
                            <th>Debt Reference</th>
                            <th>Initial Debt</th>
                            <th>Outstanding</th>
                            <th>Recovery Cycle</th>
                            <th>Status</th>
                            <th>Type</th>
                        </tr>
                    </thead>
                </table>
                <a data-toggle="modal" href="modals/page.html" data-target="#myModal">Create Group</a>
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
                <li><a href="#addresses"    data-toggle="tab">Related Addresses</a>
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
                    <a data-toggle="modal" href="modals/CreateRecoveryCycle.html" data-target="#createRecoveryCycleModal" class="btn btn-outline btn-primary">Create Recovery Cycle</a>
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
                                    <th>Payment Ref.</th>
                                    <th>Amount</th>
                                    <th>Date</th>
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
                                    <th>PartyType</th>
                                    <th>PartyFullName</th>
                                    <th>PrimaryFlag</th>
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
                                    <th>Created Date</th>
                                    <th>Aggreed Amount</th>
                                    <th>Frequency</th>
                                    <th>Frequency Description</th>
                                    <th>Installment Amount</th>
                                    <th>No. Installments</th>
                                    <th>Agm Status</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <a data-toggle="modal" href="modals/CreateArrangement.html" data-target="#tableArrangementsModal" class="btn btn-outline btn-primary">Create Arrangement</a>
                </div>
                <div class="tab-pane fade" id="debt">
                    <br/>
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableDebt">
                            <thead>
                                <tr>
                                    <th>Type</th>
                                    <th>Information</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <a data-toggle="modal" href="modals/CreateDebtAttributes.html" data-target="#debtAttributeModal" class="btn btn-outline btn-primary">Create Debt Attribute</a>
                </div>
                <div class="tab-pane fade" id="person">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tablePerson">
                            <thead>
                                <tr>
                                    <th>Type</th>
                                    <th>Information</th>
                                    <th class="set_current">Current</th>
                                    <th class="person_attribute_id"></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <a data-toggle="modal" href="modals/CreatePersonAttributes.html" data-target="#personAttributeModal" class="btn btn-outline btn-primary">Create Person Attribute</a>
                </div>
                <div class="tab-pane fade" id="notes">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableNotes">
                            <thead>
                                <tr>
                                    <th class="created_date">Created Date</th>
                                    <th>Created By</th>
                                    <th>Note</th>
                                    <th class="note_id"></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <a data-toggle="modal" href="modals/CreateDebtNote.html" data-target="#noteModal" class="btn btn-outline btn-primary">Create Note</a>
                </div>
                <div class="tab-pane fade" id="current">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableCurrent">
                            <thead>
                                <tr>
                                    <th>Type</th>
                                    <th>Information</th>
                                    <th class="created_date">Created Date</th>
                                    <th class="status">Status</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
                <div class="tab-pane fade" id="addresses">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableAddress">
                            <thead>
                                <tr>
                                    <th class="address">Full Address</th>
                                    <th class="from_date">From Date</th>
                                    <th class="until_date">Until Date</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <%--<a data-toggle="modal" href="modals/CreateNewAddress.html" data-target="#addressModal" class="btn btn-outline btn-primary">Create New Address</a>--%>
                </div> 
            </div>
        </div>
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </div>
        <div class="modal fade" id="createRecoveryCycleModal" tabindex="-1" role="dialog" aria-labelledby="createRecoveryCycleModal" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </div>
        <div class="modal fade" id="debtAttributeModal" tabindex="-1" role="dialog" aria-labelledby="debtAttributeModal" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </div>
        <div class="modal fade" id="personAttributeModal" tabindex="-1" role="dialog" aria-labelledby="personAttributeModal" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </div>
        <div class="modal fade" id="noteModal" tabindex="-1" role="dialog" aria-labelledby="noteModal" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </div>
        <div class="modal fade" id="tableArrangementsModal" tabindex="-1" role="dialog" aria-labelledby="tableArrangementsModal" aria-hidden="true" >
            <div class="modal-dialog" style="width:800px">
                <div class="modal-content">
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" charset="utf8" src="js/DebtView.js"></script>
</asp:Content>

