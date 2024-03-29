﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="DocumentTemplates.aspx.cs" Inherits="NewProcess" %>
 
<%@ Register Src="~/Common/ActionMenu.ascx"     TagName="ActionMenu"        TagPrefix="am" %>
<%@ Register Src="~/Common/MainNavigation.ascx" TagName="NavigationMenu"    TagPrefix="nm" %>
<%@ Register Src="~/Common/SiteHeader.ascx"     TagName="SiteHeader"        TagPrefix="sh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMenu" Runat="Server">
	<sh:SiteHeader ID="SiteHeader" runat="server" />
	<input type="hidden" runat="server" id="selectedTemplateId" />
	<script type="text/javascript" src="Scripts/ckeditor/ckeditor.js"></script>
	<script type="text/javascript" src="Scripts/ckfinder/ckfinder.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headInfo" Runat="Server">
	<am:ActionMenu ID="pageActionMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="menuSide" Runat="Server">
	<nm:NavigationMenu ID="NavigationMenu" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pageTitle" Runat="Server">
	<div class="row">
		<br/>
	</div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="pageBody" Runat="Server">
	<div class="row">
		<div class="col-lg-8" id="documentList">    
			<fieldset>
				<input type="checkbox" onchange="refreshBatchProcessJobs();" value="true" id="selectAll">
				<label for="selectAll" onclick="refreshBatchProcessJobs();">Show All Templates</label>
			</fieldset>
			<br />
			<table class="table table-striped table-bordered table-hover" id="dataTableDocumentTemplates">
				<thead>
					<tr>
						<th class="CHT_Name">Template Name</th>
						<th class="CHT_Notes">Notes</th>
						<th class="CHT_ID">Id</th>
					</tr>
				</thead>
			</table>
			<br />
			<a data-toggle="modal" href="modals/CreateNewTemplate.html" data-target="#createTemplate" class="btn btn-outline btn-primary">Create New Template</a>
		</div>

		<div class="col-lg-8" id="documentDetails">
			<div id="templateName"          name="templateName"             contenteditable="true" style="display:inline-block;width:100%;height:auto;margin-bottom:15px; border-bottom: 1px solid #B6B6B4"></div>
			<div id="templateDescription"   name="templateDescription"      contenteditable="true" style="display:inline-block;width:100%;height:auto;min-height: 25px;"></div>
			<textarea class="ckeditor"      name="templateDocumentContent"  ID="templateContent" ></textarea>
			<hr />
			<a href="#" onclick="saveTemplate();" class="btn btn-outline btn-primary">Save Template</a>
		</div>

	</div>

	<div class="modal fade" id="createTemplate" tabindex="-1" role="dialog" aria-hidden="true" >
		<div class="modal-dialog" >
			<div class="modal-content"></div>
		</div>
	</div>
  
	<script type="text/javascript" charset="utf8" src="js/DocumentTemplates.js"></script>

</asp:Content>

