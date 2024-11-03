<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddEmployees.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <h2>Adăugare Angajat Nou</h2>

    <div class="mb-3">
        <label for="txtName" class="form-label">Nume</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" required MaxLength="25" />
        <asp:RegularExpressionValidator 
            ID="revName" 
            runat="server" 
            ControlToValidate="txtName" 
            ErrorMessage="Numele trebuie să conțină doar litere." 
            ValidationExpression="^[a-zA-ZăâîșțĂÂÎȘȚ]+$" 
            ForeColor="Red" />
        <asp:CustomValidator 
            ID="cvNameLength" 
            runat="server" 
            ControlToValidate="txtName" 
            ErrorMessage="Numele nu poate depăși 25 de litere." 
            OnServerValidate="ValidateNameLength" 
            ForeColor="Red" />
    </div>

    <div class="mb-3">
        <label for="txtSurname" class="form-label">Prenume</label>
        <asp:TextBox ID="txtSurname" runat="server" CssClass="form-control" required MaxLength="25" />
        <asp:RegularExpressionValidator 
            ID="revSurname" 
            runat="server" 
            ControlToValidate="txtSurname" 
            ErrorMessage="Prenumele trebuie să conțină doar litere." 
            ValidationExpression="^[a-zA-ZăâîșțĂÂÎȘȚ]+$" 
            ForeColor="Red" />
        <asp:CustomValidator 
            ID="cvSurnameLength" 
            runat="server" 
            ControlToValidate="txtSurname" 
            ErrorMessage="Prenumele nu poate depăși 25 de litere." 
            OnServerValidate="ValidateSurnameLength" 
            ForeColor="Red" />
    </div>

    <div class="mb-3">
        <label for="ddlPosition" class="form-label">Funcție</label>
        <asp:DropDownList ID="ddlPosition" runat="server" CssClass="form-select" required="required">
            <asp:ListItem Text="Selectați Funcția" Value="" />
        </asp:DropDownList>
    </div>

    <div class="mb-3">
        <label for="txtBaseSalary" class="form-label">Salariu de baza</label>
        <asp:TextBox ID="txtBaseSalary" runat="server" CssClass="form-control" required type="number" min="0" />
    </div>

    <div class="mb-3">
        <label for="txtSpor" class="form-label">Spor(%)</label>
        <asp:TextBox ID="txtSpor" runat="server" CssClass="form-control" required type="number" min="0" max="100" />
        <asp:RangeValidator 
            ID="rvSpor" 
            runat="server" 
            ControlToValidate="txtSpor" 
            ErrorMessage="Sporul trebuie să fie între 0 și 100." 
            MinimumValue="0" 
            MaximumValue="100" 
            Type="Integer" 
            ForeColor="Red" />
    </div>

    <div class="mb-3">
        <label for="txtGrossBonuses" class="form-label">Premii Brute</label>
        <asp:TextBox ID="txtGrossBonuses" runat="server" CssClass="form-control" required type="number" min="0" />
    </div>

    <div class="mb-3">
        <label for="txtRetineri" class="form-label">Retineri</label>
        <asp:TextBox ID="txtRetineri" runat="server" CssClass="form-control" required type="number" min="0" />
    </div>

    <div class="mb-3">
        <asp:Button ID="btnAddEmployee" runat="server" Text="Adăugați Angajat" CssClass="btn btn-success" OnClick="btnAddEmployee_Click" />
    </div>

    <asp:Label ID="lblMessage" runat="server" CssClass="text-success"></asp:Label>
</div>


</asp:Content>
