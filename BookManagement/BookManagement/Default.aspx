<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Default.aspx.cs" Inherits="BookManagement._Default" %>

<%@ Register src="~/UI/UserControls/GridViewPager.ascx" tagname="GridViewPager" tagprefix="asp" %>
<%@ Register TagPrefix="uc" TagName="Email" Src="~/DynamicData/FieldTemplates/EmailAddress.ascx" %>
<%@ Import Namespace="BookManagement" %>


<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">

                <link rel="Stylesheet" type="text/css" href="../../UI/Css/Site.css" />

            <script type="text/javascript" language="javascript" src="UI/Scripts/datepicker/js/jquery-1.9.1.js"></script>
            <script type="text/javascript" language="javascript" src="UI/Scripts/datepicker/js/jquery-ui.js"></script>

            <script type="text/javascript" language="javascript" src="UI/Scripts/general/jquery.cookie.js"></script>
            <script type="text/javascript" language="javascript" src="UI/Scripts/general/global.js"></script>

            <link rel="Stylesheet" type="text/css" href="UI/Scripts/datepicker/css/ui-lightness/jquery-ui-1.10.1.custom.css" />

            

        <script language="javascript" type="text/javascript">

            function Highlight(row) {
                row.className = "gridViewRowHighlight";
            }

            function UnHighlight(row) {
                row.className = "gridViewRowUnHighlight";
            }

            function ToggleFiltersPanel() {
                SetToggleOn('#<%=pnlTodayBooks.ClientID %>', '#<%=showHideTodayBooks.ClientID %>', 'form_visible_filters', '#<%=imgShowHideTodayBooks.ClientID %>');
            }

            $(document).ready(function () {

                ToggleFiltersPanel();

                var prm = Sys.WebForms.PageRequestManager.getInstance();

                prm.add_endRequest(function () {
                    ToggleFiltersPanel();
                });

                $('#<%=pnlLoading.ClientID %>').toggleClass("loadingPleaseWaitNone");

            }); 
    </script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server" />
    <h2>Books with return due on <%=Utils.FormatDateTime(Convert.ToDateTime(SqlDataSource1.SelectParameters["data"].DefaultValue)) %> (<%=GetNumberOfRowsInGrid() %>)</h2>


        <div class="divMainContent">
            <table>
                <tr>
                    <td>
                        <asp:Panel ID="pnlLoading" runat="server" CssClass="loadingPleaseWait">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Image ID="imgLoading" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/ajax-loader.gif" />
                                    </td>
                                    <td>
                                        Loading...
                                    </td>
                                </tr>
                            </table>                    
                        </asp:Panel>
                    </td>
                </tr>
            </table>

            <table>
                        <tr>
                            <td>
                                <asp:Panel id="showHideTodayBooks" runat="server" CssClass="divShowFilters">
                                                <table>
                                                    <tr>
                                                        <td class="tdVerticalAlignMiddle">
                                                            <asp:Image ID="imgShowHideTodayBooks" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/book-open-icon.png" CssClass="imgHeaderSmall" />
                                                        </td>
                                                        <td class="tdVerticalAlignMiddle">
                                                            <asp:Label ID="lblShowHideFilters" runat="server" Text="Show / Hide"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                </asp:Panel>
                            </td>
                            <td>
                            &nbsp;&nbsp;&nbsp;
                            </td>
                           <td class="tdVerticalAlignMiddle">                            
                                <asp:ImageButton ID="imgExportToExcel" runat="server" CssClass="imgHeaderSmall" ImageUrl="~/DynamicData/Content/Images/ui/file-extension-xls-icon.png" OnClick="btnExportToExcel_Click" />
                           </td>
                           <td class="tdVerticalAlignMiddle">
                                <asp:LinkButton ID="btnExportToExcel" runat="server" onclick="btnExportToExcel_Click" Text="Export List To Excel"></asp:LinkButton>
                           </td>
                        </tr>
            </table>

            <table class="mainContent">
                <tr>
                    <td>
                        <asp:HiddenField ID="toggleState1" runat="server" />

                                <asp:Panel id="pnlTodayBooks" runat="server" CssClass="shadow">
                                    <asp:UpdatePanel ID="udatePanel1" runat="server">
                                        <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                            onrowdatabound="GridView1_RowDataBound" 
                                            onpageindexchanging="GridView1_PageIndexChanging" AllowPaging="True" 
                                                DataSourceID="SqlDataSource1">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortDays" runat="server" Text="Days" CommandName="Days" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextModeRight" title="<%# Eval("Days")%>">
                                                                        <%# Eval("Days")%> 
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortScheduledReturnDate" runat="server" Text="ScheduledReturnDate" CommandName="ScheduledReturnDate" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextMode" title="<%# Utils.FormatDateTime(Convert.ToDateTime(Eval("ScheduledReturnDate")))%>">
                                                                        <a href="Lease/Details.aspx?IDLease=<%# Eval("IDLease")%>" target="_blank">
                                                                            <%# Utils.FormatDateTime(Convert.ToDateTime(Eval("ScheduledReturnDate")))%>
                                                                        </a>  
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortLeaseDate" runat="server" Text="LeaseDate" CommandName="LeaseDate" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextMode" title="<%# Utils.FormatDateTime(Convert.ToDateTime(Eval("LeaseDate")))%>">
                                                                        <a href="Lease/Details.aspx?IDLease=<%# Eval("IDLease")%>" target="_blank">
                                                                            <%# Utils.FormatDateTime(Convert.ToDateTime(Eval("LeaseDate")))%>
                                                                        </a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortUserName" runat="server" Text="UserName" CommandName="UserName" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextModeLeft" title="<%# Eval("UserName")%>">
                                                                        <a href="User/Details.aspx?IDUser=<%# Eval("IDUser")%>" target="_blank">
                                                                            <%# Eval("UserName")%>
                                                                        </a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortFullName" runat="server" Text="FullName" CommandName="FullName" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextModeLeft" title="<%# Eval("FullName")%>">
                                                                        <a href="User/Details.aspx?IDUser=<%# Eval("IDUser")%>" target="_blank">
                                                                            <%# Eval("FullName")%>
                                                                        </a> 
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortEmail" runat="server" Text="Email" CommandName="Email" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextModeLeft" title="<%# Eval("Email")%>">
                                                                        <a href="mailto:<%# Eval("Email")%>"><%# Eval("Email")%></a>
                                                        </div>
                                     
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortCNP" runat="server" Text="CNP" CommandName="CNP" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextModeLeft" title="<%# Eval("CNP")%>">
                                                                        <%# Eval("CNP")%>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortBookName" runat="server" Text="BookName" CommandName="BookName" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextModeLeft" title="<%# Eval("BookName")%>">
                                                                        <a href="Book/Details.aspx?IDBook=<%# Eval("IDBook")%>" target="_blank">
                                                                            <%# Eval("BookName")%>
                                                                        </a>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortISBN" runat="server" Text="ISBN" CommandName="ISBN" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextModeLeft" title="<%# Eval("ISBN")%>">
                                                                        <a href="Book/Details.aspx?IDBook=<%# Eval("IDBook")%>" target="_blank">
                                                                            <%# Eval("ISBN")%>
                                                                        </a>  
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortHasDisk" runat="server" Text="HasDisk" CommandName="HasDisk" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextMode" title="<%# Eval("HasDisk")%>">
                                                                        <%#Eval("HasDisk").ToString().ToLower() == "yes" ?
                                                                        "<img src='DynamicData/Content/Images/ui/ok.png' alt='no image here'" : ""%>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortCopies" runat="server" Text="Copies" CommandName="Copies" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextModeRight" title="<%# Eval("Copies")%>">
                                                                        <%# Eval("Copies")%>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lnkSortRemarks" runat="server" Text="Remarks" CommandName="Remarks" OnClick="GridView1_Sorting">
                                                        </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="divTextModeLeft" title="<%# Eval("Remarks")%>">
                                                                        <%# Eval("Remarks")%>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                no books scheduled to be returned today
                                            </EmptyDataTemplate>
                                            <PagerTemplate>
                                                        <asp:GridViewPager ID="gvPager1"  runat="server" />
                                            </PagerTemplate>
                                        </asp:GridView>

                                        </ContentTemplate>  
                                    </asp:UpdatePanel>  
                            </asp:Panel>                                                
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:BookManagementConnectionString %>" 
    SelectCommand="GetScheduledBooksForToday" SelectCommandType="StoredProcedure" 
        onselected="SqlDataSource1_Selected">
        <SelectParameters>
            <asp:Parameter Name="data" Type="DateTime" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>


