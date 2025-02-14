<%@ Control Language="C#" CodeBehind="DateTimeReadWrite.ascx.cs" Inherits="BookManagement.DateTimeReadWriteField" %>

<div class="divTextMode" title="<%# FieldValueString %>">
                <asp:Label runat="server" ID="Literal1" Text="<%# FieldValueString %>" ToolTip="<%# FieldValueString %>"></asp:Label>    
</div>
<%--Lease.ActualReturnDate--%>