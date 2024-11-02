<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UpdateData.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:textbox id="txtSearch" runat="server" placeholder="Căutare..."></asp:textbox>
        <asp:button id="btnSearch" runat="server" text="Caută" onclick="btnSearch_Click" />
        <asp:button id="btnSaveAll" runat="server" text="Salvează Tot" onclick="btnSaveAll_Click" />

        <asp:panel id="PaginationPanel" runat="server" cssclass="d-flex justify-content-end mb-3">
            <div class="d-flex align-items-center">
                <asp:Label ID="lblEntriesDropdown" runat="server" Text="Rows per page:" CssClass="me-2" />
                <asp:DropDownList ID="PageSizeDropdown" runat="server" CssClass="form-select me-2" 
                                    AutoPostBack="true" OnSelectedIndexChanged="PageSizeDropdown_SelectedIndexChanged" 
                                    style="width: 80px;"> 
                    <asp:ListItem Text="5" Value="5" />
                    <asp:ListItem Text="20" Value="20" />
                    <asp:ListItem Text="50" Value="50" />
                </asp:DropDownList>

                <asp:TextBox ID="PageNumberTextbox" runat="server" CssClass="form-control me-2" 
                             Text="1" OnTextChanged="PageNumberTextbox_TextChanged" 
                             AutoPostBack="true" type="number" min="1" 
                             style="width: 60px;" /> 

                <asp:Button ID="PreviousPageButton" runat="server" CssClass="btn btn-primary me-1" 
                             Text="&laquo;" OnClick="PreviousPageButton_Click" />

                <asp:Button ID="NextPageButton" runat="server" CssClass="btn btn-primary me-2" 
                             Text="&raquo;" OnClick="NextPageButton_Click"  />
            </div>
    
            
        </asp:panel>
        <asp:gridview id="gvEmployees" runat="server" cssclass="gridview-container" autogeneratecolumns="False"
            allowsorting="True" onsorting="gvEmployees_Sorting" allowpaging="False"
            onpageindexchanging="gvEmployees_PageIndexChanging" datakeynames="NR_CRT">
            <Columns>
                <asp:BoundField DataField="NR_CRT" HeaderText="ID" ReadOnly="True" />
                <asp:TemplateField HeaderText="Nume" ItemStyle-CssClass="column-nume">
                    <ItemTemplate >
                        <asp:TextBox ID="txtNUME" runat="server" Text='<%# Bind("NUME") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Prenume" ItemStyle-CssClass="column-prenume">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPRENUME" runat="server" Text='<%# Bind("PRENUME") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Funcție" ItemStyle-CssClass="column-functie">
                    <ItemTemplate>
                        <asp:TextBox ID="txtFUNCTIE" runat="server" Text='<%# Bind("FUNCTIE") %>'/>
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
        </asp:gridview>
    </div>
</asp:Content>
