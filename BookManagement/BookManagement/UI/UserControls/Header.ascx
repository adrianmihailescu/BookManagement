<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="BookManagement.UI.UserControls.Header" %>
    <link href="../UI/Css/Site.css" rel="stylesheet" type="text/css" />

            <%--<script type="text/javascript" language="javascript" src="Scripts/datepicker/js/jquery-1.9.1.js"></script>
            <script type="text/javascript" language="javascript" src="Scripts/datepicker/js/jquery-ui.js"></script>
            <script type="text/javascript" language="javascript" src="Scripts/datepicker/js/jquery-1.9.1.js"></script>--%>

            <link rel="Stylesheet" type="text/css" href="../../UI/Scripts/datepicker/css/ui-lightness/jquery-ui-1.10.1.custom.css" />

<table>
                                                <tr>
                                                    <td>
                                                        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/" runat="server">                                                            
                                                            <asp:Image ID="imgHome" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/Home-icon6173.png" CssClass="imgHeader" />
                                                        </asp:HyperLink>                                                        
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="lnkHome" Text="Home" NavigateUrl="~/" runat="server">                                                            
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="HyperLink3" NavigateUrl='<%$ AppSettings:MainPageLinkLease %>' runat="server">
                                                            <asp:Image ID="imgLease" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/address_book.png" CssClass="imgHeader" />
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="lnkLease" Text="Book Leases" NavigateUrl='<%$ AppSettings:MainPageLinkLease %>' runat="server"></asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="HyperLink2" NavigateUrl='<%$ AppSettings:MainPageLinkUser %>' runat="server">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/user-group-icon.png" CssClass="imgHeader" />
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="lnkUsers" Text="Users List" NavigateUrl='<%$ AppSettings:MainPageLinkUser %>' runat="server"></asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="HyperLink4" NavigateUrl='<%$ AppSettings:MainPageLinkBook %>' runat="server">
                                                            <asp:Image ID="imgBooks" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/emblem_library.png" CssClass="imgHeader" />
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="lnkBooks" Text="Books List" NavigateUrl='<%$ AppSettings:MainPageLinkBook %>' runat="server"></asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="HyperLink8" NavigateUrl='<%$ AppSettings:MainPageLinkCategory %>' runat="server">
                                                            <asp:Image ID="imgCategories" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/folder_home2.png" CssClass="imgHeader" />
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="HyperLink5" NavigateUrl='<%$ AppSettings:MainPageLinkCategory %>' runat="server">
                                                            <asp:HyperLink ID="lnkCategories" Text="Book Categories" NavigateUrl='<%$ AppSettings:MainPageLinkCategory %>' runat="server"></asp:HyperLink>
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="HyperLink6" NavigateUrl='<%$ AppSettings:MainPageLinkAuthor %>' runat="server">
                                                            <asp:Image ID="imgAuthors" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/book3.png" CssClass="imgHeader" />
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="lnkAuthors" Text="Authors List" NavigateUrl='<%$ AppSettings:MainPageLinkAuthor %>' runat="server"></asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="HyperLink7" NavigateUrl="~/UI/UserManual/Book Management - User's guide.docx" runat="server">
                                                            <asp:Image ID="imgHelp" runat="server" ImageUrl="~/DynamicData/Content/Images/ui/question1.png" CssClass="imgHeader" />
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="lnkHelp" Text="Help" NavigateUrl="~/UI/UserManual/Book Management - User's guide.docx" runat="server"></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>