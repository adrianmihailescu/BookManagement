<%@ Control Language="C#" CodeBehind="Boolean_Edit.ascx.cs" Inherits="BookManagement.Boolean_EditField" %>

<script type="text/javascript" src="../../UI/Scripts/datepicker/js/jquery-1.9.1.js"></script>
<script type="text/javascript" src="../../UI/Scripts/datepicker/js/jquery-ui.js"></script>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {

        $('#<%=CheckBox1.ClientID %>').simpleImageCheck({
            image: 'images/off.gif"',
            imageChecked: 'images/on.gif"'//,
        });
    });
</script>

<asp:CheckBox runat="server" ID="CheckBox1" />

