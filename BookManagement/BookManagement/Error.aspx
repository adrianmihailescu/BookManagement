<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="BookManagement.Error"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td class="tdVerticalAlignMiddle">
                <asp:Image ID="imgError" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/error-icon.png" />
            </td>
            <td class="tdVerticalAlignMiddle">
                <asp:Label ID="lblErrorMessage" runat="server">An error has occured !</asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Click <asp:HyperLink ID="lnkDefaultPage" runat="server" Text="here" NavigateUrl="~/Default.aspx"></asp:HyperLink> to return to the home page.
                <br />The error has been logged.
            </td>
        </tr>
    </table>
</asp:Content>
