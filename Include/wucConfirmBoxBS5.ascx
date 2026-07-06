<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucConfirmBoxBS5.ascx.vb" Inherits="Include_wucConfirmBoxBS5" %>
&nbsp;&nbsp;
 
 <asp:HiddenField runat="server" ID="hfModalType" />
<asp:HiddenField runat="server" ID="hfPrompt" />

<svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
    <symbol id="check-circle-fill" fill="currentColor" viewBox="0 0 16 16">
        <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zm-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z" />
    </symbol>
</svg>

<!-- MODAL BOX -->
<div id="pnlPending2" class="modal fade" data-bs-backdrop="false" data-bs-keyboard="false">
    <div class="modal-dialog">

        <!-- Modal content-->
        <div class="modal-content">
            <%-- <asp:UpdatePanel ID="updatePanel6" runat="server">
                <ContentTemplate>--%>
            <div class="modal-header" style="text-align: center; font-weight: bold; font-size: large; color: black" runat="server" id="divHeader">
                <asp:Label runat="server" ID="lblMsgBoxHeader" Style="font-size: x-large; font-weight: bold" Text="CONFIRMATION"></asp:Label>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body" style="padding: 10px 10px;">

                <div class="form-group">
                    <div class="form-group">
                        <div class="input-group">
                            <asp:Label runat="server" ID="lblMsgBoxMessage" Style="font-size: x-large" Text="Are you sure you want to save this record?"></asp:Label>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">

                <button type="button" id="btnMsgBoxYes" runat="server" class="btn btn-primary" data-bs-dismiss="modal"><span class="glyphicon glyphicon-ok"></span>&nbsp;Yes</button>&nbsp;&nbsp;&nbsp;
                <button type="button" id="btnMsgBoxNo" runat="server" class="btn btn-danger " data-bs-dismiss="modal"><span class="glyphicon glyphicon-remove"></span>&nbsp;No</button>
            </div>
            <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>

    </div>

</div>
<!--END MODAL BOX -->


<!-- NOTIFICATION BOX -->

<div class="side-notification" id="notification">
    <div class="alert alert-success" role="alert" style="border-style: solid; border-color: black; border-width: 1px;">
        <div class="row">
            <div class="col-lg-1 mb-1">
                <svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Success:">
                    <use xlink:href="#check-circle-fill" />
                </svg>
            </div>
            <div class="col-lg-11 mb-1">
                <div runat="server" id="infoContent">
                    This is a side notification.
                </div>

            </div>
          
        </div>

    </div>
</div>

<!-- END NOTIFICATION BOX -->
