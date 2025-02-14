<%@ Control Language="C#" CodeBehind="DateTimeReadWrite_Edit.ascx.cs" Inherits="BookManagement.DateTimeReadWrite_EditField" %>

<script type="text/javascript" src="../../UI/Scripts/datepicker/js/jquery-1.9.1.js"></script>
<script type="text/javascript" src="../../UI/Scripts/datepicker/js/jquery-ui.js"></script>

    <link rel="Stylesheet" type="text/css" href="../../UI/Scripts/datepicker/css/ui-lightness/jquery-ui-1.10.1.custom.css" />
    <link rel="Stylesheet" type="text/css" href="../../UI/Scripts/datepicker/css/ui-lightness/jquery-ui-1.10.1.custom.min.css" />

    <script type="text/javascript">
        $(document).ready(function () {

            $('#<%=DatePicker1.ClientID %>').datepicker({ dateFormat: 'dd/mm/yy' });
        });
 
  </script>

<asp:TextBox ID="DatePicker1" Enabled="true" runat="server" Text='<%# FieldValueEditString %>' Columns="20"></asp:TextBox>

<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="DatePicker1" Display="Static" Enabled="false" />
<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="DatePicker1" Display="Static" Enabled="false" />
<asp:DynamicValidator runat="server" ID="DynamicValidator1" ControlToValidate="DatePicker1" Display="Static" />
<asp:CustomValidator runat="server" ID="DateValidator" ControlToValidate="DatePicker1" Display="Static" EnableClientScript="false" Enabled="false" OnServerValidate="DateValidator_ServerValidate" />
<%--Lease.ActualReturnDate--%>