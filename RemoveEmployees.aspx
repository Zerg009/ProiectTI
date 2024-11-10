<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RemoveEmployees.aspx.cs" Inherits="WebApplication1.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <%-- ADD FILTERING AND SORTING--%>
        <asp:Panel ID="PaginationPanel" runat="server" CssClass="mb-3">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <div class="d-flex">
                    <div class="input-group me-1" style="width: 200px;">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Căutare..." />
                        <div class="input-group-append">
                            <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click">
                            <i class="fas fa-search"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>

                <div class="d-flex align-items-center">
                    <asp:Label ID="lblEntriesDropdown" runat="server" Text="Rows per page:" CssClass="me-2" />
                    <asp:DropDownList ID="PageSizeDropdown" runat="server" CssClass="form-select me-2"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                        Style="width: 80px;">
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="20" Value="20" />
                        <asp:ListItem Text="50" Value="50" />
                    </asp:DropDownList>

                    <asp:TextBox ID="PageNumberTextbox" runat="server" CssClass="form-control me-2"
                        Text="1" OnTextChanged="txtPageNumber_TextChanged"
                        AutoPostBack="true" type="number" min="1"
                        Style="width: 60px;" />

                    <asp:Button ID="PreviousPageButton" runat="server" CssClass="btn btn-primary me-1"
                        Text="&laquo;" OnClick="btnPreviousPage_Click" />

                    <asp:Button ID="NextPageButton" runat="server" CssClass="btn btn-primary"
                        Text="&raquo;" OnClick="btnNextPage_Click" />
                </div>
            </div>
        </asp:Panel>

        <asp:GridView ID="gvEmployees" runat="server" CssClass="gridview-container" AutoGenerateColumns="False"
            AllowSorting="True" OnSorting="gridViewEmployees_Sorting" AllowPaging="False"
            OnPageIndexChanging="gridViewEmployees_PageIndexChanging" DataKeyNames="NR_CRT">
            <Columns>
                <asp:BoundField DataField="NR_CRT" HeaderText="ID" ReadOnly="True" ItemStyle-CssClass="column-id"/>
                <asp:TemplateField HeaderText="Nume" ItemStyle-CssClass="column-nume">
                    <ItemTemplate>
                        <asp:Label ID="lblNUME" runat="server" Text='<%# Bind("NUME") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Prenume" ItemStyle-CssClass="column-prenume">
                    <ItemTemplate>
                        <asp:Label ID="lblPRENUME" runat="server" Text='<%# Bind("PRENUME") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Funcție" ItemStyle-CssClass="column-functie">
                    <ItemTemplate>
                        <asp:Label ID="lblFUNCTIE" runat="server" Text='<%# Bind("FUNCTIE") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Salariul de Bază" ItemStyle-CssClass="column-salar-baza">
                    <ItemTemplate>
                        <asp:Label ID="lblSALAR_BAZA" runat="server" Text='<%# Bind("SALAR_BAZA") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Bonus %" ItemStyle-CssClass="column-bonus">
                    <ItemTemplate>
                        <asp:Label ID="lblSPOR" runat="server" Text='<%# Bind("SPOR") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Premii Brute" ItemStyle-CssClass="column-premii-brute">
                    <ItemTemplate>
                        <asp:Label ID="lblPREMII_BRUTE" runat="server" Text='<%# Bind("PREMII_BRUTE") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Retineri" ItemStyle-CssClass="column-retineri">
                    <ItemTemplate>
                        <asp:Label ID="lblRETINERI" runat="server" Text='<%# Bind("RETINERI") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total Brut" ItemStyle-CssClass="column-total-brut">
                    <ItemTemplate>
                        <asp:Label ID="lblTOTAL_BRUT" runat="server" Text='<%# Eval("TOTAL_BRUT") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Brut Impozabil" ItemStyle-CssClass="column-brut-impozabil">
                    <ItemTemplate>
                        <asp:Label ID="lblBRUT_IMPOZABIL" runat="server" Text='<%# Eval("BRUT_IMPOZABIL") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CAS" ItemStyle-CssClass="column-cas">
                    <ItemTemplate>
                        <asp:Label ID="lblCAS" runat="server" Text='<%# Eval("CAS") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CASS" ItemStyle-CssClass="column-cass">
                    <ItemTemplate>
                        <asp:Label ID="lblCASS" runat="server" Text='<%# Eval("CASS") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Impozit" ItemStyle-CssClass="column-impozit">
                    <ItemTemplate>
                        <asp:Label ID="lblIMPOZIT" runat="server" Text='<%# Eval("IMPOZIT") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="VIRAT_CARD" HeaderText="Virat Card" ReadOnly="True" ItemStyle-CssClass="column-virat-card" />

                <asp:TemplateField HeaderText="Actions" ItemStyle-CssClass="column-last">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" 
                            CssClass="btn btn-danger btn-sm" 
                            CommandArgument='<%# Eval("NR_CRT") %>' 
                            OnClientClick='<%# "setEmployeeId(" + Eval("NR_CRT") + "); return false;" %>'>
                            <i class="fa fa-trash"></i> Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
        <tr>
            <td colspan="12" style="text-align: center;">
                <span class="no-records-message">No entries found.</span>
            </td>
        </tr>
    </EmptyDataTemplate>
        </asp:GridView>
        <div class="modal fade" id="confirmationModal" tabindex="-1" role="dialog" aria-labelledby="confirmationModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="confirmationModalLabel">Confirm Deletion</h5>
                    </div>
                    <div class="modal-body">
                        Are you sure you want to delete this employee?
           
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="btnCancelDelete" runat="server" CssClass="btn btn-secondary" data-dismiss="modal" OnClick="btnCancelDelete_Click">Cancel</asp:LinkButton>
                        <asp:LinkButton ID="btnConfirmDelete" runat="server" CssClass="btn btn-danger" OnClick="btnConfirmDelete_Click">Delete</asp:LinkButton>
                        <asp:HiddenField ID="hdnEmployeeId" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <script>
            function setEmployeeId(employeeId) {
                document.getElementById('<%= hdnEmployeeId.ClientID %>').value = employeeId;
                $('#confirmationModal').modal('show');
            }
        </script>
</asp:Content>
