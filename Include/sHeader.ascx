<%@ Control Language="VB" AutoEventWireup="false" CodeFile="sHeader.ascx.vb" Inherits="Include_sHeader" %>


<asp:Table runat="server" ID="tblHearder" BackColor="#FFFFFF" BorderWidth="1" CellPadding="0" CellSpacing="0" Width="100%">
 

    <asp:TableRow Height="40px">
        <asp:TableCell HorizontalAlign="center" style="background-image:url(~/Images/subheader.gif);">
            <asp:Table runat="server" ID="tblHearderSub" BorderWidth="0" CellPadding="0" CellSpacing="0" Width="100%">
                <asp:TableRow>
                    <asp:TableCell>
                        &nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;<asp:Label runat="server" ID="lblSecured" Font-Names="Arial" Text="Welcome" Font-Bold="true" ForeColor="#006699" Font-Size="13px"></asp:Label>
                        <asp:Label runat="server" ID="lblUsername" Font-Names="Arial" Text="Admin" ForeColor="#006699" Font-Size="12px" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Center"  VerticalAlign="Top" runat="server" Visible="false">
                        <asp:LinkButton runat="server" ID="lnkLogout" SkinID="lnkButton" Text="Logout"/>&nbsp;|
                        <asp:LinkButton runat="server" ID="lnkChangePassword" SkinID="lnkButton" Text="Change Password"/>
                        
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>