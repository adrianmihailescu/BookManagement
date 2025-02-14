<%@ Control Language="C#" CodeBehind="ForeignKey.ascx.cs" Inherits="BookManagement.ForeignKeyField" %>

<div class="divTextModeLeft" title="<%# GetDisplayString() %>">
    <asp:HyperLink ID="HyperLink1" runat="server"
        Text="<%# GetDisplayString() %>"
        NavigateUrl="<%# GetNavigateUrl() %>"  />
</div>

