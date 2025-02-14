<%@ Control Language="C#" CodeBehind="EmailAddress.ascx.cs" Inherits="BookManagement.EmailAddressField" %>

<div class="divTextMode" title="<%# FieldValueString %>">
    <table>
            <tr>
                <td class="tdVerticalAlignMiddle">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="<%# FieldValueString %>" Target="_blank" />
                </td>
            </tr>
    </table>
</div>

