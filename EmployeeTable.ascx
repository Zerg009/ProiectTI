<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTable.ascx.cs" Inherits="WebApplication1.EmployeeTable" %>
<div>
    <%-- ADD FILTERING AND SORTING--%>

    <div class="d-flex justify-content-end align-items-center mb-3">
        <asp:LinkButton ID="btnSaveAll" runat="server" CssClass="btn btn-success ms-3" OnClick="btnSaveAll_Click">
                <i class="fas fa-save me-2"></i> Salvează
        </asp:LinkButton>
    </div>

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
            <asp:BoundField DataField="NR_CRT" HeaderText="ID" ReadOnly="True" />
            <asp:TemplateField HeaderText="Nume" ItemStyle-CssClass="column-nume">
                <ItemTemplate>
                    <asp:TextBox ID="txtNUME" runat="server" Text='<%# Bind("NUME") %>' />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNUME" Display="None" 
                                                 ErrorMessage="Nume is required." />
                    <asp:RegularExpressionValidator
                        ID="regexNume"
                        runat="server"
                        ControlToValidate="txtNUME"
                        ValidationExpression="^[a-zA-Z]+$"
                        Display="None"
                        ErrorMessage="Nume should contain only letters." />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Prenume" ItemStyle-CssClass="column-prenume">
                <ItemTemplate>
                    <asp:TextBox ID="txtPRENUME" runat="server" Text='<%# Bind("PRENUME") %>' />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPRENUME" Display="None"
                        ErrorMessage="Prenume is required." />
                    <asp:RegularExpressionValidator
                        ID="regexPrenume"
                        runat="server"
                        ControlToValidate="txtPRENUME"
                        ValidationExpression="^[a-zA-Z]+$"
                        Display="None"
                        ErrorMessage="Prenume should contain only letters." />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Funcție" ItemStyle-CssClass="column-functie">
                <ItemTemplate>
                    <asp:TextBox ID="txtFUNCTIE" runat="server" Text='<%# Bind("FUNCTIE") %>' />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFUNCTIE" Display="None"
                        ErrorMessage="Funcție is required." />
                    <asp:RegularExpressionValidator
                        ID="regexFunctie"
                        runat="server"
                        ControlToValidate="txtFUNCTIE"
                        ValidationExpression="^[a-zA-ZăâîșțĂÂÎȘȚ]+$"
                        Display="None"
                        ErrorMessage="Funcție should contain only letters." />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Salariul de Bază" ItemStyle-CssClass="column-salar-baza ">
                <ItemTemplate>
                    <asp:TextBox ID="txtSALAR_BAZA" runat="server" Text='<%# Bind("SALAR_BAZA") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bonus %" ItemStyle-CssClass="column-bonus">
                <ItemTemplate>
                    <asp:TextBox ID="txtSPOR" runat="server" Text='<%# Bind("SPOR") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Premii Brute" ItemStyle-CssClass="column-premii-brute">
                <ItemTemplate>
                    <asp:TextBox ID="txtPREMII_BRUTE" runat="server" Text='<%# Bind("PREMII_BRUTE") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Retineri" ItemStyle-CssClass="column-retineri">
                <ItemTemplate>
                    <asp:TextBox ID="txtRETINERI" runat="server" Text='<%# Bind("RETINERI") %>' />
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
        </Columns>
        <EmptyDataTemplate>
            <tr>
                <td colspan="12" style="text-align: center;">
                    <span class="no-records-message">No entries found.</span>
                </td>
            </tr>
        </EmptyDataTemplate>
    </asp:GridView>
</div>
