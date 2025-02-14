<%@ Control Language="C#" CodeBehind="ForeignKey_Edit.ascx.cs" Inherits="BookManagement.ForeignKey_EditField" %>

            <script type="text/javascript" language="javascript" src="../../UI/Scripts/datepicker/js/jquery-1.9.1.js"></script>
            <script type="text/javascript" language="javascript" src="../../UI/Scripts/datepicker/js/jquery-ui.js"></script>


<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $('#<%=ListBox1.ClientID %>').click(function () {
//             alert($('#<%=ListBox1.ClientID %>').val());

            if (
            ($('#<%=ListBox1.ClientID %>').val() != null)
            && ($('#<%=ListBox1.ClientID %>').val() != '')

            ) {
//                 alert($('#<%=ListBox1.ClientID %>').val());

                var $text = '../../<%=GetTableName() %>/Details.aspx?<%=GetPrimaryKeyColumnName()%>=' + $('#<%=ListBox1.ClientID %>').val();

                $('#<%=lnkEntityDetails.ClientID %>').attr('target', '_blank');
                $('#<%=lnkEntityDetails.ClientID %>').attr('href', $text);
            }

            else {
//                alert('null');
                $('#<%=lnkEntityDetails.ClientID %>').attr('target', '_self');
                $('#<%=lnkEntityDetails.ClientID %>').attr('href', '');
            }

        });
    })
</script>
<table>
    <tr>
        <td>
            <div class="divListBoxMode">
                        <asp:DropDownList ID="ListBox1" runat="server" ValidationGroup="ForeignKeyValidator" >
                                            <%--<asp:ListItem Text="-- All --" Value="" Selected="true"/>--%>
                        </asp:DropDownList>
            </div>
        </td>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:HyperLink ID="lnkEntity" runat="server" Target="_blank" ValidationGroup="NoValidation" ></asp:HyperLink>
                    </td>
                    <td>
                        <asp:HyperLink ID="lnkEntityDetails" runat="server" NavigateUrl=""></asp:HyperLink>
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkEntityRefresh" runat="server" Text="refresh" 
                                                    onclick="lnkEntityRefresh_Click" ValidationGroup="NoValidation"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </td>        
        <td>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ListBox1" Display="Static" Enabled="false" ValidationGroup="ForeignKeyValidator" />
            <asp:DynamicValidator runat="server" ID="DynamicValidator1" ControlToValidate="ListBox1" Display="Static" />
        </td>
    </tr>
</table>