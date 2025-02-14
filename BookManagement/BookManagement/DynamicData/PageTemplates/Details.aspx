<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeBehind="Details.aspx.cs" Inherits="BookManagement.Details" %>


<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
            
            <link rel="Stylesheet" type="text/css" href="../../UI/Css/Site.css" />

            <script type="text/javascript" language="javascript" src="../../UI/Scripts/datepicker/js/jquery-1.9.1.js"></script>
            <script type="text/javascript" language="javascript" src="../../UI/Scripts/datepicker/js/jquery-ui.js"></script>

            <script type="text/javascript" language="javascript" src="../../UI/Scripts/general/jquery.cookie.js"></script>
            <script type="text/javascript" language="javascript" src="../../UI/Scripts/general/global.js"></script>

            <link rel="Stylesheet" type="text/css" href="../../UI/Scripts/datepicker/css/ui-lightness/jquery-ui-1.10.1.custom.css" />
            
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
           
           function ToggleFiltersPanel() {
               SetToggleOn('#<%=pnlUserCalendar.ClientID %>', '#<%=showHideUserCalendar.ClientID %>', 'form_visible_user_calendar', '#<%=imgShowHideUserCalendar.ClientID %>');
               SetToggleOn('#<%=pnlAdvancedInformations.ClientID %>', '#<%=showHideAdvancedInformations.ClientID %>', 'form_visible_advanced_informations', '#<%=imgShowHideAdvancedInformations.ClientID %>');
            }

            $(document).ready(function () {

                ToggleFiltersPanel();

                var prm = Sys.WebForms.PageRequestManager.getInstance();

                prm.add_endRequest(function () {
                    ToggleFiltersPanel();
                });

                $('#<%=pnlLoading.ClientID %>').toggleClass("loadingPleaseWaitNone");

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
                <h2><% =table.Name%>s Details</h2>
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
            <%if (table.Name.ToLower().Equals("user"))
              { %>
            <tr>
                <td>
                                <asp:Panel id="showHideUserCalendar" runat="server" CssClass="divShowFilters">
                                                <table>
                                                    <tr>
                                                        <td class="tdVerticalAlignMiddle">
                                                            <asp:Image ID="imgShowHideUserCalendar" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/book-open-icon.png" CssClass="imgHeaderSmall" />
                                                        </td>
                                                        <td class="tdVerticalAlignMiddle">
                                                            <asp:Label ID="lblShowHideUserCalendar" runat="server" Text="Show / Hide User's Calendar"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                </asp:Panel>
                    </td>
            </tr>
            <%} %>
                        <% if ((table.DisplayName.ToLower() == "book")) // show preview image only if showing a picture or an user
               { %>
               <tr>
                    <td>
                                       <asp:Panel id="showHideAdvancedInformations" runat="server" CssClass="divShowFilters">
                                        <table>
                                            <tr>
                                                <td class="tdVerticalAlignMiddle">
                                                    <asp:Image ID="imgShowHideAdvancedInformations" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/book-open-icon.png" CssClass="imgHeaderSmall" />
                                                </td>
                                                <td class="tdVerticalAlignMiddle">
                                                        <asp:Label ID="lblShowHideAdvancedInformations" runat="server" Text="Show / Hide Advanced Informations"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                            </asp:Panel>
                    </td>
               </tr>
            <%} %>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                HeaderText="List of validation errors" />
            <asp:DynamicValidator runat="server" ID="DetailsViewValidator" ControlToValidate="FormView1" Display="None" />

            <table>
                <tr>
                    <td class="detailsPicturePreview">
                        <asp:FormView runat="server" ID="FormView1" DataSourceID="DetailsDataSource" 
                            OnItemDeleted="FormView1_ItemDeleted" 
                            onitemdeleting="FormView1_ItemDeleting">
                            <ItemTemplate>
                                <table id="detailsTable">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlShowDinamicEntity" runat="server">
                                                <asp:DynamicEntity ID="DynamicEntity1" runat="server"></asp:DynamicEntity>
                                            </asp:Panel>
                                        </td>
                                    </tr>                                    
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:DynamicHyperLink ID="DynamicHyperLink1" runat="server" Action="Edit" Text="Edit" />
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            * <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" Text="Delete" OnClientClick='return confirm("Are you sure you want to delete this item?");' />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                No information is available for this <%=table.Name %>.<br />Please add.
                            </EmptyDataTemplate>
                        </asp:FormView>
                    </td>
                    <% if ((table.DisplayName.ToLower() == "book") || (table.DisplayName.ToLower() == "user")) // show preview image only if showing a picture or an user
                       { %>
                    <td class="tdVerticalAlignMiddle">
                        <asp:Panel ID="imhShadow" runat="server" CssClass="shadow">
                            <asp:Image ID="imgShowPreview" runat="server" CssClass="imgShowPreview" />
                        </asp:Panel>                            
                    </td>
                    <% } %>

                    <% if ((table.DisplayName.ToLower() == "user")) // show preview image only if showing a picture or an user
                       { %>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel id="pnlUserCalendar" runat="server">
                                                    <asp:Calendar ID="userCalendar" runat="server"
                                                        ondayrender="userCalendar_DayRender" BackColor="White" BorderColor="White" 
                                                        BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="#a03033" 
                                                        Height="190px" NextPrevFormat="FullMonth" Width="350px" 
                                                        SelectionMode="None">
                                                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#696969" 
                                                            VerticalAlign="Bottom" />
                                                        <OtherMonthDayStyle ForeColor="Black" />
                                                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="1px" 
                                                            Font-Bold="True" Font-Size="12pt" ForeColor="#696969" />
                                                        <TodayDayStyle BackColor="#CCCCCC" />
                                                    </asp:Calendar>                                        
                                                    books scheduled to be returned by the selected user
                                                </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>                          
                    </td>
                    <% } %>

                </tr>
            </table>

            <table>
                            <tr>
                                        <td class="tdVerticalAlignMiddle">
                                            <asp:Image ID="imgInformation" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/information_32.png" CssClass="imgHeaderSmall" />
                                        </td>
                                        <td>
                                            * The <% =table.Name%> will be marked as deleted
                                        </td>
                                    </tr>
                                    <%if (table.Name.ToLower().Equals("lease"))
                                      { %>
                                        <tr>
                                            <td class="tdVerticalAlignMiddle">
                                                &nbsp;
                                            </td>
                                            <td>
                                                * The user will be notified by e-mail when this book lease is deleted.
                                            </td>
                                        </tr>
                                    <%} %>
                                                                
                                                               <%if (table.Name.ToLower().Equals("book"))
                                      { %>
                                      <tr>
                                        <td colspan="2">
                                            <div class="divTextMode500">
                                                <asp:Panel id="pnlAdvancedInformations" runat="server">
                                                    <table class="BookScheduledToBeReturned">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblBookScheduledToBeReturned" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>            
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                      </tr>
                                                                <%} %>

            </table>

            <asp:EntityDataSource ID="DetailsDataSource" runat="server" EnableDelete="true" />

            <asp:QueryExtender TargetControlID="DetailsDataSource" ID="DetailsQueryExtender" runat="server">
                <asp:DynamicRouteExpression />
            </asp:QueryExtender>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:BookManagementConnectionString %>" 
    SelectCommand="GetScheduledBooksForTodayByUser" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Name="IDUser" Type="Int32" />
            <asp:Parameter Name="data" Type="DateTime" />
        </SelectParameters>
    </asp:SqlDataSource>

            <br />

            <div>
                <asp:DynamicHyperLink ID="ListHyperLink" runat="server" Action="List">Show All Items</asp:DynamicHyperLink>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

