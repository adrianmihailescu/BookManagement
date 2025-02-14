<%@ Control Language="C#" CodeBehind="Text.ascx.cs" Inherits="BookManagement.TextField" %>

<div class="divTextModeLeft" title="<%# FieldValueString %>">
    <asp:Label runat="server" ID="Literal1" Text="<%# FieldValueString %>" ToolTip="<%# FieldValueString %>"></asp:Label>     
</div>