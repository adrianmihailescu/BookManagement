<%@ Control Language="C#" CodeBehind="ForeignKey.ascx.cs" Inherits="BookManagement.ForeignKeyFilter" %>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $('#<%=ListBox1.ClientID %>').click(function () {
            // alert($('#<%=ListBox1.ClientID %>').val());

            if (
            ($('#<%=ListBox1.ClientID %>').val() != null)
            && ($('#<%=ListBox1.ClientID %>').val() != '')

            ) {
                // alert($('#<%=ListBox1.ClientID %>').val());

                var $text = '../../<%=GetTableName() %>/Details.aspx?<%=GetPrimaryKeyColumnName()%>=' + $('#<%=ListBox1.ClientID %>').val();

                $('#<%=lnkEntityDetails.ClientID %>').attr('target', '_blank');
                $('#<%=lnkEntityDetails.ClientID %>').attr('href', $text);
            }

            else {
                // alert('null');
                $('#<%=lnkEntityDetails.ClientID %>').attr('target', '_self');
                $('#<%=lnkEntityDetails.ClientID %>').attr('href', '');
            }

        });
    })
</script>

<div id="ddlContainer" runat="server">
    <table>
        <tr>
            <td>
                <div class="divListBoxMode">
                                <asp:DropDownList runat="server" ID="ListBox1" AutoPostBack="false"
                                OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" CssClass="ListBoxMaxWidth">
<%--                                                                        <asp:ListItem Text="-- All --" Value="" Selected="true"/>--%>
                                </asp:DropDownList>
                </div>      
            </td>
            <td>
                <table>
                        <tr>
                            <td>
                                <asp:HyperLink ID="lnkEntityNew" runat="server" Target="_blank" NavigateUrl=""></asp:HyperLink>
                            </td>                            
                            <td>
                                <asp:HyperLink ID="lnkEntityDetails" runat="server" NavigateUrl=""></asp:HyperLink>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkEntityRefresh" runat="server" Text="refresh" 
                                    onclick="lnkEntityRefresh_Click"></asp:LinkButton>
                            </td>
                        </tr>
               </table> 
            </td>
        </tr>
    </table>
</div>