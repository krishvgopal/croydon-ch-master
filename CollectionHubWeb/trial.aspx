<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="trial.aspx.cs" Inherits="Trial" %>
<%@ Register Src="~/Common/ActionMenu.ascx"     TagName="ActionMenu"        TagPrefix="am" %>
<%@ Register Src="~/Common/MainNavigation.ascx" TagName="NavigationMenu"    TagPrefix="nm" %>
<%@ Register Src="~/Common/SiteHeader.ascx"     TagName="SiteHeader"        TagPrefix="sh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMenu" Runat="Server">
    <sh:SiteHeader ID="SiteHeader" runat="server" />
    <script type="text/javascript" charset="utf8" src="js/NewNote.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headInfo" Runat="Server">
    <am:ActionMenu ID="pageActionMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menuSide" Runat="Server">
    <nm:NavigationMenu ID="NavigationMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" Runat="Server">
    <div class="row">
        <div style="height:25px"></div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="pageBody" Runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div style="width:175px;float:left"><label>Assign To</label></div>
                <div style="width:275px;float:left">
                     <div class="form-group">
                        <select class="form-control-compact" id="assignTo">
                             <option value="false" selected>Debtor</option>
                         </select>
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div style="width:175px;float:left"><label>Category</label></div>
                <div style="width:275px;float:left">
                     <div class="form-group">
                         <select class="form-control-compact" id="category"></select>
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div style="width:175px;float:left"><label>Our Reference</label></div>
                <div style="width:275px;float:left">
                     <div class="form-group">
                        <span class="form-control-static" id="ourRef"></span>
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div style="width:175px;float:left"><label>Their Reference</label></div>
                <div style="width:275px;float:left">
                     <div class="form-group">
                        <input class="form-control-compact" id="theirRef">
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div style="width:175px;float:left"><label>Subject</label></div>
                <div style="width:275px;float:left">
                     <div class="form-group">
                        <input class="form-control-compact" id="subject">
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div style="width:175px;float:left"><label>Subject Address</label></div>
                <div style="width:550px;float:left">
                     <div class="form-group">
                        <input class="form-control-compact" id="subjectAddress">
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div style="width:175px;float:left"><label>Communicating With</label></div>
                <div style="width:275px;float:left">
                     <div class="form-group">
                        <input class="form-control-compact" id="comunicationLink">
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div style="width:175px;float:left"><label>Reason For Call</label></div>
                <div style="width:275px;float:left">
                     <div class="form-group">
                        <input class="form-control-compact" id="reason">
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div style="width:175px;float:left"><label>Note</label></div>
                <div style="width:550px;float:left">
                     <div class="form-group">
                         <textarea class="form-control-compact" style="height:100px" rows="8" id="noteText"></textarea>
                    </div>
                </div>
                <div style="clear:both;"></div>
                <div style="width:175px;float:left">
                     <label>Contact Details</label>
                </div>
                <div style="width:550px;float:left">
                    <div style="width:125px;float:left"><label>Current Mobile</label></div>
                    <div style="width:125px;float:left">
                         <span class="form-control-static" id="oldMobile"></span>
                    </div>
                    <div style="width:125px;float:left"><label>New Mobile</label></div>
                    <div style="width:175px;float:left">
                         <input class="form-control-compact" id="newMobile">
                    </div>
                    <div style="clear:both;padding-top:7px;padding-bottom:7px"></div>
                    <div style="width:125px;float:left"><label>Current Phone</label></div>
                    <div style="width:125px;float:left">
                        <span class="form-control-static" id="oldPhone"></span>
                    </div>
                    <div style="width:125px;float:left"><label>New Phone</label></div>
                    <div style="width:175px;float:left">
                         <input class="form-control-compact" id="newPhone">
                    </div>
                    <div style="clear:both;padding-top:7px;padding-bottom:7px"></div>
                    <div style="width:125px;float:left"><label>Current Email</label></div>
                    <div style="width:175px;float:left">
                        <span class="form-control-static" id="oldEmail"></span>
                    </div>
                    <div style="clear:both;padding-top:7px;padding-bottom:7px"></div>
                    <div style="width:125px;float:left"><label>New Email</label></div>
                    <div style="width:425px;float:left">
                        <input class="form-control-compact" id="newEmail">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        loadDebtorNote(3);
    </script>
</asp:Content>

