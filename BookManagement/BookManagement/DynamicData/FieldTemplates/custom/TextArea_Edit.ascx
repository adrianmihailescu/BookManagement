<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextArea_Edit.ascx.cs" Inherits="BookManagement.DynamicData.FieldTemplates.custom.TextArea_Edit" %>
<asp:TextBox ID="TextBox1" runat="server" Text="<%# FieldValueEditString %>" 
    Columns="10" TextMode="MultiLine" CssClass="TextAreaEdit" ></asp:TextBox>
