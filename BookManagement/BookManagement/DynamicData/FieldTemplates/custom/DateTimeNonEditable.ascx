<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateTimeNonEditable.ascx.cs" Inherits="BookManagement.DynamicData.FieldTemplates.custom.DateTimeNonEditable" %>

<div class="divTextMode" title="<%# FieldValueString %>">
                <asp:Label runat="server" ID="Literal1" Text="<%# FieldValueString %>" ToolTip="<%# FieldValueString %>" CssClass="tdVerticalAlignMiddle"></asp:Label>
</div>
<%--Lease.LeaseDate--%>