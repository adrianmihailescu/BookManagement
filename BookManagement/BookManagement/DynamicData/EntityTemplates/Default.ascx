<%@ Control Language="C#" CodeBehind="Default.ascx.cs" Inherits="BookManagement.DefaultEntityTemplate" %>

<table>
    <asp:EntityTemplate runat="server" ID="EntityTemplate1">
        <ItemTemplate>
            <tr>
                <td class="tdVerticalAlignMiddleDetails">
                    <asp:Label runat="server" OnInit="Label_Init" />
                </td>
                <td class="tdVerticalAlignMiddle">
                    <asp:DynamicControl runat="server" OnInit="DynamicControl_Init" />
                </td>
            </tr>
        </ItemTemplate>
    </asp:EntityTemplate>
</table>

