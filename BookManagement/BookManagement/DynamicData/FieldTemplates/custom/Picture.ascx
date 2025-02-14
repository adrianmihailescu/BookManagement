<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Picture.ascx.cs" Inherits="BookManagement.DynamicData.EntityTemplates.Picture" %>

<%--<asp:Image AlternateText="can't find preview file" runat="server" ID="previewImage" />--%>
<%--<div class="divTextMode">--%>
    <table class="FieldPicture">
        <tr>
            <td>
                <asp:Literal runat="server" ID="Literal1" Text="<%# FieldValueString %>" />
            </td>
        </tr>
    </table>
<%--</div>--%>
<%--Book.Picture--%>

