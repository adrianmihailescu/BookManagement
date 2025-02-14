<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateFilter.ascx.cs" Inherits="BookManagement.DynamicData.Filters.DateFilter" %>

<script type="text/javascript" src="../../../Scripts/datepicker/js/jquery-1.9.1.js"></script>
<script type="text/javascript" src="../../../Scripts/datepicker/js/jquery-ui.js"></script>

    <link rel="Stylesheet" type="text/css" href="../../../UI/Scripts/datepicker/css/ui-lightness/jquery-ui-1.10.1.custom.css" />
    <link rel="Stylesheet" type="text/css" href="../../../UI/Scripts/datepicker/css/ui-lightness/jquery-ui-1.10.1.custom.min.css" />

    <script type="text/javascript">
        $(document).ready(function () {

            $('#<%=DatePicker1.ClientID %>').datepicker({ dateFormat: 'dd/mm/yy' });
        });
 
  </script>

<asp:TextBox ID="DatePicker1" AutoPostBack="false" runat="server" 
    ontextchanged="DatePicker1_TextChanged"></asp:TextBox>
