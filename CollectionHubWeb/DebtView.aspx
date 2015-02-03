<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="DebtView.aspx.cs" Inherits="Search" %>
<%@ Register Src="~/Common/ActionMenu.ascx"     TagName="ActionMenu" TagPrefix="am" %>
<%@ Register Src="~/Common/MainNavigation.ascx" TagName="NavigationMenu" TagPrefix="nm" %>
<%@ Register Src="~/Common/SiteHeader.ascx"     TagName="SiteHeader" TagPrefix="sh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMenu" Runat="Server">
    <sh:SiteHeader      ID="SiteHeader"        runat="server" />
    <asp:HiddenField    ID="sourceValue"       runat="server" ClientIDMode="Static" />
    <asp:HiddenField    ID="sourceRefValue"    runat="server" ClientIDMode="Static" />
    <asp:HiddenField    ID="selectedDebtId"    runat="server" ClientIDMode="Static" />
    <asp:HiddenField    ID="cnpin"             runat="server" ClientIDMode="Static" />
    <asp:HiddenField    ID="uprn"              runat="server" ClientIDMode="Static" />
    <asp:HiddenField    ID="userId"            runat="server" ClientIDMode="Static" />
    <asp:HiddenField    ID="debtRowTotalValue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField    ID="assignedToUserId"  runat="server" ClientIDMode="Static" />
    <script src="Scripts/ckeditor/ckeditor.js"></script>
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
            <div style="border-bottom: 1px solid rgb(215,229,223); height:90px" > 
                <div class="col-lg-4" style="padding-left:0px !important; padding-bottom: 10px !important; width: 90px">
                    <asp:Image runat="server" ID="pageIcon" ClientIDMode="Static" CssClass="pageIcon"/>
                </div> 
                <div class="col-lg-2" style="width:245px">
                    <p class="form-control-compact-static">
                        <span id="pageFullNameField"><asp:Literal ID="pageFullName" runat="server"></asp:Literal></span>
                        <br/>
                        <asp:Literal ID="pageFullAddress" runat="server"></asp:Literal>
                        <br/><br/>
<%--                        <span>Assigned To:&nbsp;<a href="#" onclick="doAssignUser($(this), $('#assignedToUserId').val());">No one</a></span>
                        --%>
                    </p>
                </div>
                <div class="col-lg-2" style="width:245px">
                    <div class="form-group">
                        <label>Total Initial Debt</label>
                        <span style="float:right">
                            <asp:Literal ID="pageTotalDebt" runat="server" ></asp:Literal>    
                        </span>
                        <br/>
                        <label>Total Outstanding</label>
                        <span style="float:right">
                            <asp:Literal ID="pageDebtOutstanding" runat="server"></asp:Literal>
                        </span>
                        <br/>
                        <select id="showCleared" class="form-control-compact" style="width:100% !important;float:left;margin-top: 5px" onchange="refreshDebtsList();" >
                            <option value="1">Show Open Debts</option>
                            <option value="2" selected>Show All Debts</option>
                        </select>
                        <img id="showClearedLoadingImage" src="img/gif-load.gif" alt="Loading Image" style="float:left;padding-left: 5px;padding-top: 3px;" />
                    </div>
                </div>
                <div class="col-lg-3" style="float:left">
                    <i class="fa fa-bullhorn" style="font-size: 18px;"></i>  
                    <a href="#" onclick="createNote();">Record Contact Note</a>
                </div>
            </div>
            <div class="table-responsive">
                <br/>
                <table class="table compact table-striped table-bordered table-hover" id="dataTableMain">
                    <thead>
                        <tr>
                            <th class="select_id">Select</th>
                            <th>Debt Source</th>
                            <th>Acc Ref</th>
                            <th>Debt Reference</th>
                            <th class="debt_initial">Initial Debt1</th>
                            <th class="debt_outstanding">Outstanding2</th>
                            <th class="recoveryCycle">Recovery Cycle</th>
                            <th class="respUserId">Responsible Id</th>
                            <th class="respUserName">Responsible User</th>
                            <th class="type">Type</th>
                            <th class="status">Status</th>
                            
                            <th class="group_order">GroupOrder</th>
                        </tr>
                    </thead>
                </table>
                <select id="recoveryCycleQuickSet" class="form-control-compact" style="visibility: hidden;"></select>
                <select id="assignedUserList" class="form-control-compact" style="visibility: hidden;"></select>
                <a data-toggle="modal" href="modals/CreateDebtGroup.html" data-target="#myModal"  id="debtGroupCreate">Create Group</a>
                <a data-toggle="modal" href="modals/RemoveDebtGroup.html" data-target="#ungroupDebtModal" id="debtGroupRemove">Remove Group</a>
            </div>
            <div style="clear:both"><br /></div>
            <ul class="nav nav-tabs">
                <li class="active"> 
                    <a href="#recovery"     data-toggle="tab" id="arefRecovery">         Recovery History  </a></li>
                <li><a href="#payments"     data-toggle="tab" id="arefPayments">         Payments          </a></li>
                <li><a href="#parties"      data-toggle="tab" id="arefParties">          Liable Parties    </a></li>
                <li><a href="#arrangements" data-toggle="tab" id="arefArrangements">     Arrangements      </a></li>
                <li><a href="#debt"         data-toggle="tab" id="arefDebtAttributes">   Debt Attributes   </a></li>
                <li><a href="#person"       data-toggle="tab" id="arefPersonAttributes"> Person Attributes </a></li>
                <li><a href="#current"      data-toggle="tab" id="arefCurrentAttributes">Current Attributes</a></li>
                <li><a href="#matchTab1"    data-toggle="tab" id="arefMatches">          Matches           </a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane fade in active" id="recovery">
                    <br />
                    <div style="width:150px;float:left;"><strong>Status Filter</strong></div>
                    <div style="width:225px;float:left;padding-bottom:15px;">
                        <select class="form-control-compact" id="recoveryActiveStatus" onchange="updateRecoveryHistory();"></select>
                    </div>
                    <div style="width:150px;float:left;margin-left:20px;"><strong>Next Step</strong></div>
                    <div style="width:100px;float:left;padding-bottom:15px;">
                        <select class="form-control-compact" id="recoveryNextStep" onchange="updateRecoveryHistory();">
                            <option value="1" selected>Yes</option>
                            <option value="0">No</option>
                        </select>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableRecovery">
                            <thead>
                                <tr>
                                    <th class="onClick"></th>
                                    <th>Action</th>
                                    <th>ActionStatus</th>
                                    <th>ProcessMethod</th>
                                    <th class="actionGroup">ActionGroup</th>
                                    <th>Actionable</th>
                                    <th class="columnFormat">ColumnFormat</th>
                                    <th class="scheduled">Scheduled</th>
                                    <th class="id"></th>
                                    <th class="debtId"></th>
                                    <th class="debtReference"></th>
                                    <th class="actionStatus"></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <a data-toggle="modal" href="modals/CreateRecoveryCycle.html" data-target="#createRecoveryCycleModal" class="btn btn-primary" id="recoveryCycleSet">Set Recovery Cycle</a>
                    <a data-toggle="modal" href="modals/CreateDebtAction.html"    data-target="#debtActionModal"          class="btn btn-primary" id="recoveryActionCreate">Add Debt Action</a>
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
                                    <th class="payment_amount">Amount</th>
                                    <th class="payment_date">Date</th>
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
                                    <th>Party Full Name</th>
                                    <th>Primary Flag</th>
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
                    <a data-toggle="modal" href="modals/CreateArrangement.html" data-target="#tableArrangementsModal" class="btn btn-primary" id="debtCreateArrangement">Create Arrangement</a>
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
                    <a data-toggle="modal" href="modals/CreateDebtAttributes.html" data-target="#debtAttributeModal" class="btn btn-primary" id="debtAttributeCreate">Create Debt Attribute</a>
                </div>
                <div class="tab-pane fade" id="person">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tablePerson">
                            <thead>
                                <tr>
                                    <th>Type</th>
                                    <th>Information</th>
                                    <th>From Streams</th>
                                    <th class="to_date">Date To</th>
                                    <th class="from_date">Date From</th>
                                    <th class="set_current">StatusText</th>
                                    <th class="person_attribute_id"></th>
                                </tr>
                            </thead>
                        </table>
                        <select id="attributeCurrentStatuses" class="form-control-compact"style="visibility: hidden;"></select>
                    </div>
                    <a data-toggle="modal" href="modals/CreatePersonAttributes.html" data-target="#personAttributeModal" class="btn btn-primary">Create Person Attribute</a>
                </div>
                <div class="tab-pane fade" id="current">
                    <br />
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="tableCurrent">
                            <thead>
                                <tr>
                                    <th>Type</th>
                                    <th>Information</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
                <div class="tab-pane fade" id="matchTab1">
                    <table class="table compact table-striped table-bordered table-hover" id="dataTablePersonDetails">
                        <thead>
                            <tr>
                                <th class="AccountRef">Source Acc Ref</th>
                                <th class="FULLNAME">Full Name</th>
                                <th class="FULLADDRESS">Full Address</th>
                                <th class="NINO">NINO</th>
                                <th class="DOB">DOB</th>
                            </tr>
                        </thead>
                    </table>
                    <br />
                    <h4>Matched Records</h4>
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="matchTable1">
                            <thead>
                                <tr>
                                    <th class="infoHover"></th>
                                    <th>Source</th>
                                    <th>A/C Ref</th>
                                    <th>Full Name</th>
                                    <th>Full Address</th>
                                    <th>NINO</th>
                                    <th class="dob">DOB</th>
                                    <th class="MatchId"></th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <br />
                    <h4>Unmatched Records</h4>
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="matchTable2">
                            <thead>
                                <tr>
                                    <th class="infoHover"></th>
                                    <th>Source</th>
                                    <th>A/C Ref</th>
                                    <th>Full Name</th>
                                    <th>Full Address</th>
                                    <th>NINO</th>
                                    <th class="dob">DOB</th>
                                    <th class="MatchId"></th>
                                </tr>
                            </thead>
                        </table>
                      </div>
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
            <div class="modal-dialog" style="width:825px">
                <div class="modal-content">
                </div>
            </div>
        </div>
        <div class="modal fade" id="ungroupDebtModal" tabindex="-1" role="dialog" aria-labelledby="ungroupDebtModal" aria-hidden="true" >
            <div class="modal-dialog">
                <div class="modal-content">
                </div>
            </div>
        </div>
        <div class="modal fade" id="debtActionModal" tabindex="-1" role="dialog" aria-labelledby="debtActionModal" aria-hidden="true" >
            <div class="modal-dialog" style="width:825px">
                <div class="modal-content">
                </div>
            </div>
        </div>
    
        <div class="modal fade" id="debtActionCreateModal" tabindex="-1" role="dialog" aria-labelledby="debtActionCreateModal" aria-hidden="true" >
            <div class="modal-dialog" style="width:825px">
                <div class="modal-content">
                </div>
            </div>
        </div>
    

        <div class="modal fade" id="debtActionEditModal" tabindex="-1" role="dialog" aria-labelledby="debtActionEditModal" aria-hidden="true" >
            <div class="modal-dialog" style="width:825px">
                <div class="modal-content">
                </div>
            </div>
        </div>
   
    <% if (System.Configuration.ConfigurationManager.AppSettings["UseMinifiedJs"] == "true") { %>
    <script type="text/javascript" charset="utf8" src="js/DebtView.js"></script><% } else { %>
    <script type="text/javascript" charset="utf8" src="js/DebtView.js"></script><% } %>
</asp:Content>

