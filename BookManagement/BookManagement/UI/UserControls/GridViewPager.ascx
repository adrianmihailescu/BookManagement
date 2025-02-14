<%@ Control Language="C#" CodeBehind="GridViewPager.ascx.cs" Inherits="BookManagement.GridViewPager" %>

<link rel="Stylesheet" type="text/css" href="../UI/Css/Site.css" />

            <table class="tableCenter">
                <tr>
                    <td>
                        <asp:ImageButton AlternateText="First page" ToolTip="First page" 
                            ID="ImageButtonFirst" runat="server" 
                            ImageUrl="~/DynamicData/Content/Images/PgFirst.gif" Width="8" Height="9" 
                            CommandName="Page" CommandArgument="First" onclick="ImageButtonFirst_Click" />        
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:ImageButton AlternateText="Previous page" ToolTip="Previous page" 
                            ID="ImageButtonPrev" runat="server" 
                            ImageUrl="~/DynamicData/Content/Images/PgPrev.gif" Width="5" Height="9" 
                            CommandName="Page" CommandArgument="Prev" onclick="ImageButtonPrev_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="LabelPage" runat="server" Text="Page " AssociatedControlID="TextBoxPage" />
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPage" runat="server" Columns="5" AutoPostBack="true" ontextchanged="TextBoxPage_TextChanged" Width="20px" CssClass="DDControl" />
                    </td>
                    <td>
                        of
                    </td>
                    <td>
                        <asp:Label ID="LabelNumberOfPages" runat="server" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:ImageButton AlternateText="Next page" ToolTip="Next page" 
                            ID="ImageButtonNext" runat="server" 
                            ImageUrl="~/DynamicData/Content/Images/PgNext.gif" Width="5" Height="9" 
                            CommandName="Page" CommandArgument="Next" onclick="ImageButtonNext_Click" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:ImageButton AlternateText="Last page" ToolTip="Last page" 
                            ID="ImageButtonLast" runat="server" 
                            ImageUrl="~/DynamicData/Content/Images/PgLast.gif" Width="8" Height="9" 
                            CommandName="Page" CommandArgument="Last" onclick="ImageButtonLast_Click" />
                    </td>
                </tr>
           </table>
