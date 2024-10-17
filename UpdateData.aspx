<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UpdateData.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="NR_CRT"
        DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True"
        OnPageIndexChanging="GridView1_PageIndexChanging" OnSorting="GridView1_Sorting"
        OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
        OnRowCancelingEdit="GridView1_RowCancelingEdit">

        <Columns>
            <asp:BoundField DataField="NR_CRT" HeaderText="NR_CRT" ReadOnly="True" SortExpression="NR_CRT" />
            <asp:BoundField DataField="NUME" HeaderText="NUME" SortExpression="NUME" />
            <asp:BoundField DataField="PRENUME" HeaderText="PRENUME" SortExpression="PRENUME" />
            <asp:BoundField DataField="VIRAT_CARD" HeaderText="VIRAT_CARD" SortExpression="VIRAT_CARD" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>"
        SelectCommand="SELECT NR_CRT, NUME, PRENUME, VIRAT_CARD FROM SALARII_ANGAJATI"
        UpdateCommand="UPDATE SALARII_ANGAJATI SET NUME=@NUME, PRENUME=@PRENUME, VIRAT_CARD=@VIRAT_CARD WHERE NR_CRT=@NR_CRT">


        <UpdateParameters>
            <asp:Parameter Name="NR_CRT" Type="Int32" />
            <asp:Parameter Name="NUME" Type="String" />
            <asp:Parameter Name="PRENUME" Type="String" />
            <asp:Parameter Name="VIRAT_CARD" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
