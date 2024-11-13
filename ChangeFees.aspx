<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ChangeFees.aspx.cs" Inherits="WebApplication1.WebForm8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Modal HTML -->
            <div id="loginModal" class="modal fade" tabindex="-1" aria-labelledby="loginModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="loginModalLabel">Admin Login</h5>
                        </div>
                        <div class="modal-body">
                            <asp:TextBox ID="txtModalPassword" runat="server" TextMode="Password" placeholder="Enter Password" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Visible="false" />

                        </div>
                        <div class="modal-footer">
                            <%--<button type="button" class="btn btn-secondary" data-bs-dismiss="modal" onclick="closeModal()">Close</button>--%>
                            <asp:Button ID="btnCloseModal" runat="server" Text="Close" OnClick="btnCloseModal_Click"  CssClass="btn btn-secondary"/>
                            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary"/>
                        </div>
                    </div>
                </div>
            </div>


            <asp:Panel ID="pnlParameters" runat="server" Visible="false">
                <asp:TextBox ID="txtCASPensii" runat="server" placeholder="CAS PENSII"></asp:TextBox>
                <asp:TextBox ID="txtCASSanatate" runat="server" placeholder="CASS SANATATE"></asp:TextBox>
                <asp:TextBox ID="txtImpozit" runat="server" placeholder="IMPOZIT"></asp:TextBox>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('loginModal'), {
                backdrop: 'static',  
                keyboard: false      
            });

            myModal.show();
        }

        function closeModal() {
            var myModal = new bootstrap.Modal(document.getElementById('loginModal'));
            myModal.hide();
            myModal.dispose();
            
            var existingBackdrops = document.querySelectorAll('.modal-backdrop');
            existingBackdrops.forEach(function (backdrop) {
                backdrop.remove();
            });
        }
    </script>
</asp:Content>
