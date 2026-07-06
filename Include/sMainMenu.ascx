<%@ Control Language="VB" AutoEventWireup="false" CodeFile="sMainMenu.ascx.vb" Inherits="Include_sMainMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Table ID="tblMainMenu" runat="server" Width="180px" CellPadding="0" CellSpacing="0">
    <asp:TableRow BackColor="#ffff99">
        <asp:TableCell HorizontalAlign="Left">
            <ajaxToolkit:Accordion ID="accMainMenu" runat="server" 
                HeaderCssClass="AjaxMenuHeader" FadeTransitions="false" FramesPerSecond="40" TransitionDuration="250"
                AutoSize="None" RequireOpenedPane="true" SuppressHeaderPostbacks="false" BackColor="#AFEEEE">
                <Panes>
                </Panes>               
            </ajaxToolkit:Accordion>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>

