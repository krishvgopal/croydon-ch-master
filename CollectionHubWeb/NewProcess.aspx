<%@ Page Title="" Language="C#" MasterPageFile="~/AppMain.master" AutoEventWireup="true" CodeFile="NewProcess.aspx.cs" Inherits="NewProcess" %>
 
<%@ Register Src="~/Common/ActionMenu.ascx"     TagName="ActionMenu"        TagPrefix="am" %>
<%@ Register Src="~/Common/MainNavigation.ascx" TagName="NavigationMenu"    TagPrefix="nm" %>
<%@ Register Src="~/Common/SiteHeader.ascx"     TagName="SiteHeader"        TagPrefix="sh" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headMenu" Runat="Server">
    <sh:SiteHeader ID="SiteHeader" runat="server" />
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
        <div class="col-lg-4">    
             <table class="table table-striped table-bordered table-hover" id="dataTableBatchProcessJobs">
                <thead>
                    <tr>
                        <th class="batch_name">Batch Name</th>
                        <th class="bp_debt_source">Source</th>
                        <th class="bp_id">Id</th>
                    </tr>
                </thead>
            </table>
        </div> 
            <%-- <div class="col-lg-8">    
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis eleifend pellentesque auctor. Quisque sagittis vel elit in finibus. Praesent quis feugiat massa. Pellentesque condimentum venenatis diam, non malesuada purus rutrum ac. Sed sapien sapien, ornare ac mattis et, rhoncus vel nisl. Curabitur imperdiet eros vitae est vehicula, vitae feugiat nibh congue. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Integer fringilla feugiat dui et elementum. Etiam ipsum mi, convallis at magna et, pharetra ultricies nisi. Aliquam laoreet ipsum a metus pretium lacinia.</p>
                <p>Etiam non sapien et metus maximus posuere sit amet a diam. Mauris non dapibus turpis. Nunc vel ipsum urna. Vivamus pharetra velit augue, rutrum blandit dolor volutpat et. Integer faucibus magna nulla, non consequat est tristique eu. Phasellus vehicula semper turpis, a pharetra urna lacinia eu. Vestibulum aliquet massa vel est dictum, quis imperdiet orci vestibulum. Curabitur tincidunt non metus vitae suscipit. Mauris sollicitudin elit quis laoreet viverra. Fusce porttitor non arcu et sagittis. Interdum et malesuada fames ac ante ipsum primis in faucibus.</p>
                <p>Ut rhoncus tristique placerat. Nullam pretium felis quis lectus sodales blandit volutpat ut dolor. Maecenas vitae dui egestas nulla cursus tempor a id lorem. Aenean a semper purus. Morbi suscipit justo non massa scelerisque, vel sodales libero maximus. Donec tincidunt tellus id fermentum porttitor. Fusce auctor scelerisque urna. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi et commodo nibh, et vulputate velit. Nam scelerisque dignissim massa at elementum. Nulla semper est in porttitor euismod. Aenean lobortis non justo et faucibus. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec in tellus convallis, finibus lacus sit amet, tempus lectus. Pellentesque neque risus, elementum et ullamcorper a, sollicitudin porta velit.</p>
                <p>Maecenas ullamcorper accumsan mauris. Curabitur vel vestibulum augue. Ut sagittis ante nec neque maximus pulvinar. Ut aliquam justo in metus hendrerit, in cursus quam vestibulum. Pellentesque sit amet tempor justo. Donec accumsan ipsum ut urna posuere, non accumsan sem facilisis. Nam rhoncus egestas arcu, sed rutrum libero. In luctus, sem nec tincidunt tincidunt, massa elit mattis ipsum, vel consectetur dui tellus et nisi. Vestibulum pharetra, neque sed lacinia sodales, arcu ligula luctus erat, eget pharetra leo urna sit amet magna. Nullam at lorem neque.</p>
                <p>Sed in mi sodales, ultrices odio at, vulputate nisl. Mauris laoreet tempor augue, semper malesuada ipsum elementum bibendum. Quisque accumsan ornare arcu faucibus rhoncus. Suspendisse molestie tortor tellus, vitae interdum ipsum iaculis et. Donec ac ex vitae mi elementum commodo. Pellentesque ut ex sem. Sed dignissim vel est at vehicula. In molestie, metus et interdum faucibus, metus eros efficitur nulla, in lacinia erat felis convallis risus. Sed maximus diam maximus neque accumsan, id tristique velit porttitor. Nullam fermentum id sapien mollis commodo. Quisque pretium nisl nec pharetra vestibulum.</p>
            </div> --%>
    </div>
    <script type="text/javascript" charset="utf8" src="js/NewProcess.js"></script>
</asp:Content>

