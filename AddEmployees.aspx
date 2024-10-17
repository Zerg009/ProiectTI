<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddEmployees.aspx.cs" Inherits="WebApplication1.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Message Label--%>
    <div class="form-container">
        <asp:Label ID="lblMessage" runat="server" Text="Introduceti datele:"></asp:Label>

        <%--Label and TextBox for Nume--%>
        <div class="form-group">
            <asp:Label ID="lblNume" runat="server" Text="Nume:"></asp:Label>
            <asp:TextBox ID="txtNume" runat="server" Placeholder="Enter Nume"></asp:TextBox>
        </div>

        <%--Label and TextBox for Prenume--%>
        <div class="form-group">
            <asp:Label ID="lblPrenume" runat="server" Text="Prenume:"></asp:Label>
            <asp:TextBox ID="txtPrenume" runat="server" Placeholder="Enter Prenume"></asp:TextBox>
        </div>

        <%--Label and TextBox for Salariu--%>
        <div class="form-group">
            <asp:Label ID="lblSalariu" runat="server" Text="Salariu:"></asp:Label>
            <asp:TextBox ID="txtSalariu" runat="server" Placeholder="Enter Salariu"></asp:TextBox>
        </div>

        <%--Submit Button--%>
        <div class="form-group">
            <asp:Button ID="btnAddEmployee" runat="server" Text="Add Employee" OnClick="btnAddEmployee_Click" />
        </div>
    </div>

</asp:Content>
