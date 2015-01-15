<%@ Page Title="" Language="C#" MasterPageFile="~/popups/PopupMaster.master" AutoEventWireup="true" CodeFile="DebtNote.aspx.cs" Inherits="popups_DebtNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerContent" Runat="Server">
    <script type="text/javascript" charset="utf8" src="../js/NewNote.js"></script>
    <style>
        .formLabel          {width:175px;float:left}
        .formLabelNarrow    {width:125px;float:left}
        .formContent        {width:275px;float:left}
        .formContentWide    {width:550px;float:left}
        .formContentMedium  {width:425px;float:left}
        .formContentNarrow  {width:125px;float:left}
        .formClear          {clear:both;}
    </style>
</asp:Content>
<asp:Content ID="pageContent" ContentPlaceHolderID="bodyContent" Runat="Server">
    
    <input type="hidden" id="debtData" debtId="" noteId="" pin="" uprn="">

    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                
                <div class="formLabel"><label>Assign To</label></div>
                <div class="formContent"><div class="form-group"><select class="form-control-compact" id="assignTo"><option value="false" selected>Debtor</option></select></div></div>
                
                <div class="formClear"></div>

                <div class="formLabel"><label>Category</label></div>
                <div class="formContent"><div class="form-group"><select class="form-control-compact" id="category"></select></div></div>
                
                <div class="formClear"></div>
                
                <div class="formLabel"><label>Our Reference</label></div>
                <div class="formContent"><div class="form-group"><span class="form-control-static" id="ourRef"></span></div></div>
                
                <div class="formClear"></div>

                <div class="formLabel"><label>Their Reference</label></div>
                <div class="formContent"><div class="form-group"><input class="form-control-compact" id="theirRef"></div></div>
                
                <div class="formClear"></div>

                <div class="formLabel"><label>Subject</label></div>
                <div class="formContent"><div class="form-group"><input class="form-control-compact" id="subject"></div></div>
                
                <div class="formClear"></div>
                
                <div class="formLabel"><label>Subject Address</label></div>
                <div class="formContentWide"><div class="form-group"><input class="form-control-compact" id="subjectAddress"></div></div>
                
                <%--<div class="formClear"></div>
                <div class="formLabel"><label>Communicating With</label></div>
                <div class="formContent"><div class="form-group"><input class="form-control-compact" id="comunicationLink"></div></div>--%>
                
                <div class="formClear"></div>

                <div class="formLabel"><label>Reason For Call</label></div>
                <div class="formContent"><div class="form-group"><input class="form-control-compact" id="reason"></div></div>
                
                <div class="formClear"></div>
                
                <div class="formLabel"><label>Note</label></div>
                <div class="formContentWide"><div class="form-group"><textarea class="form-control-compact" style="height:100px" rows="8" id="noteText"></textarea></div></div>

                <div class="formClear"></div>
                <div class="formLabel"><label>Contact Details</label></div>

                <div class="formContentWide">
                    
                    <div class="formLabelNarrow"><label>Current Mobile</label></div>
                    <div class="formContentNarrow"><span class="form-control-static" id="oldMobile"></span></div>
                    <div class="formLabelNarrow"><label>New Mobile</label></div>
                    <div class="formContentNarrow"><input class="form-control-compact" id="newMobile"></div>

                    <div style="clear:both;padding-top:7px;padding-bottom:7px"></div>

                    <div class="formLabelNarrow"><label>Current Phone</label></div>
                    <div class="formContentNarrow"><span class="form-control-static" id="oldPhone"></span></div>
                    <div class="formLabelNarrow"><label>New Phone</label></div>
                    <div class="formContentNarrow"><input class="form-control-compact" id="newPhone"></div>

                    <div style="clear:both;padding-top:7px;padding-bottom:7px"></div>

                    <div class="formLabel"><label>Current Email</label></div>
                    <div style="width:175px;float:left"><span class="form-control-static" id="oldEmail"></span></div>

                    <div style="clear:both;padding-top:7px;padding-bottom:7px"></div>

                    <div class="formLabel"><label>New Email</label></div>
                    <div style="width:125px;float:left"><input class="form-control-compact" id="newEmail"></div>

                </div>

            </div>

        </div>

    </div>
    
     <script>
         
         loadDebtorNote(3);

     </script>

</asp:Content>
