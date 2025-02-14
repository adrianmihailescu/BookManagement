<%@ Page Language="C#" MasterPageFile="~/Site.master" Inherits="BookManagement.Edit" CodeBehind="Edit.aspx.cs" %>


<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
            <link rel="Stylesheet" type="text/css" href="../../UI/Css/Site.css" />
            
            <script type="text/javascript" language="javascript" src="../../UI/Scripts/datepicker/js/jquery-1.9.1.js"></script>
            <script type="text/javascript" language="javascript" src="../../UI/Scripts/datepicker/js/jquery-ui.js"></script>

            <script type="text/javascript" language="javascript" src="../../UI/Scripts/general/jquery.cookie.js"></script>
            <script type="text/javascript" language="javascript" src="../../UI/Scripts/general/global.js"></script>

            <link rel="Stylesheet" type="text/css" href="../../UI/Scripts/datepicker/css/ui-lightness/jquery-ui-1.10.1.custom.css" />

            <script language="javascript" type="text/javascript">

                $(document).ready(function () {

                    // alert('start');
                    $('#<%=pnlLoading.ClientID %>').toggleClass("loadingPleaseWaitNone");
                    // alert('none');

                    ToggleFiltersPanel();

                    var prm = Sys.WebForms.PageRequestManager.getInstance();

                    prm.add_endRequest(function () {
                        ToggleFiltersPanel();
                    });

                });
        </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="FormView1" />
        </DataControls>
    </asp:DynamicDataManager>

        <table>
        <tr>
            <td>
                <h2>Edit <%= table.DisplayName %></h2>    
            </td>
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

    <table><!--error information-->
        <tr>
            <td>
                <asp:Panel id="showOperationResult" runat="server" CssClass="divInvisible">
                                    <table>
                                        <tr>
                                            <td class="tdVerticalAlignMiddle">
                                                <asp:Image ID="imgOperationResultInformation" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/information_32.png" CssClass="imgHeaderSmall" />
                                            </td>
                                            <td class="tdVerticalAlignMiddle">
                                                <asp:Image ID="imgOperationResultError" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/dialog_warning.png" CssClass="imgHeaderSmall" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOperationResult" runat="server">
                                                </asp:Label>                                                
                                            </td>
                                            <td>
                                                <asp:DynamicHyperLink ID="DynamicHyperLink2" runat="server" Action="List" Text="Back" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                            <%if (table.Name.ToLower().Equals("lease"))
                          { %>
                                <asp:Panel id="showEmailNotificationResult" runat="server" CssClass="divInvisible">
                                    <table>
                                        <tr>
                                            <td class="tdVerticalAlignMiddle">
                                                <asp:Image ID="imgShowMailError" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/dialog_warning.png" CssClass="imgHeaderSmall" />
                                            </td>
                                            <td>
                                                the user could not be notified by e-mail
                                            </td>
                                            <td>
                                                <asp:DynamicHyperLink ID="DynamicHyperLink1" runat="server" Action="List" Text="Back" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            <%} %>
            </td>
        </tr>
    </table><!--error information-->  

<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                HeaderText="List of validation errors" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" />

            <table>
                <tr>
                    <td>
                        <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource" DefaultMode="Edit"
                                OnItemCommand="FormView1_ItemCommand" 
                                OnItemUpdated="FormView1_ItemUpdated" RenderOuterTable="false" 
                                onitemupdating="FormView1_ItemUpdating">
                                <EditItemTemplate>
                                    <table id="detailsTable" cellpadding="6">
                                        <tr>
                                            <td colspan="2">
                                                <asp:DynamicEntity ID="DynamicEntity1" runat="server" Mode="Edit" />   
                                            </td>
                                        </tr>                        
                                        <tr>
                                            <td colspan="2">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr>
                                                <td colspan="2">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" Text="Update" />
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel" Text="Cancel" CausesValidation="false" />
                                                </td>
                                        </tr>
                                    </table>                   
                    
                                
                                </EditItemTemplate>
                                <EmptyDataTemplate>
                                    <div>No such item.</div>
                                </EmptyDataTemplate>
                            </asp:FormView>
                     </td>
                     <td>
                                   <% if ((table.DisplayName.ToLower() == "book") || (table.DisplayName.ToLower() == "user")) // show preview image only if showing a picture or an user
                                       { %>
                                       <table>
                                            <tr>
                                                <td class="tdVerticalAlignMiddle">
                                                    <asp:Panel ID="pnlShadow" runat="server" CssClass="shadow">
                                                        <asp:Image ID="imgShowPreview" runat="server" CssClass="imgShowPreview" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                       </table>                                       
                                    <% } %>
                     </td>
                </tr>
            </table>

            <asp:EntityDataSource ID="DetailsDataSource" runat="server" EnableUpdate="true" />

            <asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender" runat="server">
                <asp:DynamicRouteExpression />
            </asp:QueryExtender>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

