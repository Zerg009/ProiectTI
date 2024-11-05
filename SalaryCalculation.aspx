<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SalaryCalculation.aspx.cs" Inherits="WebApplication1.WebForm4" %>
<%@ Register Src="~/EmployeeTable.ascx" TagPrefix="uc" TagName="EmployeeTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:EmployeeTable ID="EmployeeTable1" runat="server" />
</asp:Content>
