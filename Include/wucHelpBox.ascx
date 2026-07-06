<%@ Control Language="VB" AutoEventWireup="false" CodeFile="wucHelpBox.ascx.vb" Inherits="Include_wucHelpBox" %>

          <asp:Panel ID="pnlModal" runat="server" role="dialog" CssClass="modal fade">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header" style="padding: 15px 30px;">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h2><span class="glyphicon glyphicon-lock"></span>Login</h2>
                            </div>
                            <div class="modal-body" style="padding: 40px 50px;">
                              
                                   <asp:Literal runat="server" ID="litHelpVal" ></asp:Literal>
                            </div>
                            <div class="modal-footer">
                                <button runat="server" onclick="" type="submit" class="btn btn-danger btn-default pull-left" data-dismiss="modal"><span class="glyphicon glyphicon-remove"></span>Cancel</button>
                              
                            </div>
                        </div>

                    </div>

                </asp:Panel>