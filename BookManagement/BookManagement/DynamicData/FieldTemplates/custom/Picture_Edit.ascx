<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Picture_Edit.ascx.cs" Inherits="BookManagement.DynamicData.FieldTemplates.Picture_Edit" %>



<asp:FileUpload ID="fuBookImage" runat="server" />

    <%--ControlToValidate="fuBookImage" ValidationExpression="^.*\.(jpg|jpeg|bmp|png|gif|tif|tiff)$" --%>

<asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
    ControlToValidate="fuBookImage" ValidationExpression="^.*\.(jpg|JPG|jpeg|JPEG|bmp|BMP|png|PNG|gif|GIF|tif|TIF|tiff|TIFF)$"
    runat="server" ErrorMessage="Please upload only image files (jpg|jpeg|bmp|png|gif|tif|tiff).">*</asp:RegularExpressionValidator>
<br />
<asp:Label ID="lblFileTypes" runat="server"></asp:Label>
<%--Book.Picture--%>

