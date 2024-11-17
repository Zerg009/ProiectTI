<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ChangeFees.aspx.cs" Inherits="WebApplication1.WebForm8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="d-flex justify-content-center mb-3">
            <asp:Label ID="lblPageHeader" runat="server" CssClass="h2 text-center" Text="Modificare Impozite"></asp:Label>
        </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <%--  Login Modal  --%> 
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
                        <div class="modal-footer d-flex justify-content-between w-100">
                            <button type="button" class="btn btn-info btn-sm" onclick="showChangePasswordModal()">Change Password</button>

                            <div class="ml-auto">
                                <asp:Button ID="btnCloseModal" runat="server" Text="Close" OnClick="btnCloseModal_Click" CssClass="btn btn-secondary" />
                                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Change Password Modal HTML -->
            <div id="changePasswordModal" class="modal fade" tabindex="-1" aria-labelledby="changePasswordModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="changePasswordModalLabel">Change Password</h5>
                        </div>
                        <div class="modal-body">
                            <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" placeholder="Current Password" CssClass="form-control mb-2"></asp:TextBox>
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" placeholder="New Password" CssClass="form-control mb-2"></asp:TextBox>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm New Password" CssClass="form-control"></asp:TextBox>
                            <asp:Label ID="lblChangePasswordError" runat="server" ForeColor="Red" Visible="false" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCloseChangePasswordModal" runat="server" Text="Close" OnClick="btnCloseChangePasswordModal_Click" CssClass="btn btn-secondary" />
                            <asp:Button ID="btnSubmitNewPassword" runat="server" Text="Submit" OnClick="btnSubmitNewPassword_Click" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlParameters" runat="server" Visible="false" CssClass="container mt-4">
                <div class="row mb-3">
                    <label for="txtCASPensii" class="col-sm-3 col-form-label">CAS Pensii</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txtCASPensii" runat="server" placeholder="CAS PENSII" CssClass="form-control" />
                        <asp:RegularExpressionValidator
                            ID="revCASPensii"
                            runat="server"
                            ControlToValidate="txtCASPensii"
                            ValidationExpression="^(0(\.\d{1,2})?)?$"
                            ForeColor="Red"
                            ErrorMessage="Please enter a valid number less than 1."
                            Display="Dynamic" />
                    </div>
                </div>

                <div class="row mb-3">
                    <label for="txtCASSanatate" class="col-sm-3 col-form-label">CASS Sănătate</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txtCASSanatate" runat="server" placeholder="CASS SANATATE" CssClass="form-control" />
                        <asp:RegularExpressionValidator
                            ID="revCASSanatate"
                            runat="server"
                            ControlToValidate="txtCASSanatate"
                            ValidationExpression="^(0(\.\d{1,2})?)?$"
                            ForeColor="Red"
                            ErrorMessage="Please enter a valid number less than 1."
                            Display="Dynamic" />
                    </div>
                </div>

                <div class="row mb-3">
                    <label for="txtImpozit" class="col-sm-3 col-form-label">Impozit</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txtImpozit" runat="server" placeholder="IMPOZIT" CssClass="form-control" />
                        <asp:RegularExpressionValidator
                            ID="revImpozit"
                            runat="server"
                            ControlToValidate="txtImpozit"
                            ValidationExpression="^(0(\.\d{1,2})?)?$"
                            ForeColor="Red"
                            ErrorMessage="Please enter a valid number less than 1."
                            Display="Dynamic" />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-sm-12">
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-center" Visible="false"></asp:Label>
                    </div>
                </div>

                <div class="row mt-3">
                    <div class="col-sm-12 text-center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        // Show the login modal
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('loginModal'), {
                backdrop: 'static',
                keyboard: false
            });
            myModal.show();
        }

        // Close the login modal
        function closeModal() {
            var myModal = new bootstrap.Modal(document.getElementById('loginModal'));
            myModal.hide();

            var existingBackdrops = document.querySelectorAll('.modal-backdrop');
            existingBackdrops.forEach(function (backdrop) {
                backdrop.remove();
            });
        }

        // Show the change password modal
        function showChangePasswordModal() {
            closeModal();
            var changePasswordModal = new bootstrap.Modal(document.getElementById('changePasswordModal'), {
                backdrop: 'static',
                keyboard: false
            });
            changePasswordModal.show();
        }

        // Close the change password modal
        function closeChangePasswordModal() {
            var changePasswordModal = new bootstrap.Modal(document.getElementById('changePasswordModal'));
            changePasswordModal.hide();

            var existingBackdrops = document.querySelectorAll('.modal-backdrop');
            existingBackdrops.forEach(function (backdrop) {
                backdrop.remove();
            });

            showModal();
        }
    </script>
</asp:Content>
