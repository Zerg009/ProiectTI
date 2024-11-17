<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="WebApplication1.homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="welcomepage-container mt-5">
        <h2 class="text-center mb-4">Bine ați venit în Aplicația de Management al Salariilor</h2>
        <p class="text-center">Iată o prezentare generală a principalelor funcții pe care le puteți accesa din meniu.</p>

        <div class="row">
            
            <div class="col-lg-8 offset-lg-2">
                
                <div class="help-section mb-4">
                    <h4>Acasă</h4>
                    <p>Această secțiune servește drept panou de control principal, unde puteți vizualiza sarcinile curente și accesați linkuri rapide către alte secțiuni ale aplicației.</p>
                </div>

               
                <div class="help-section mb-4">
                    <h4>Gestionare Date</h4>
                    <p>Vă permite să efectuați toate sarcinile esențiale de gestionare a datelor, inclusiv:</p>
                    <ul>
                        <li><strong>Actualizare Date</strong> - Modificați informațiile existente despre angajați, salarii și datele asociate.</li>
                        <li><strong>Adăugare Angajați</strong> - Adăugați noi angajați în baza de date cu informațiile salariale relevante.</li>
                        <li><strong>Eliminare Angajați</strong> - Eliminați angajați din baza de date atunci când este necesar.</li>
                        <li><strong>Vizualizare Angajați</strong> - Vizualizați toate înregistrările angajaților, cu opțiuni de detaliere și căutare.</li>
                    </ul>
                </div>

                <div class="help-section mb-4">
                    <h4>Tipărire</h4>
                    <p>Această secțiune oferă opțiuni de tipărire pentru diverse rapoarte:</p>
                    <ul>
                        <li><strong>Status Plăți</strong> - Generați un raport cu starea plăților pentru toți angajații.</li>
                        <li><strong>Fluturași de Salariu</strong> - Tipăriți fluturași de salariu pentru angajați individuali sau pentru un grup de angajați într-o anumită perioadă.</li>
                    </ul>
                </div>

                <div class="help-section mb-4">
                    <h4>Instrucțiuni Suplimentare</h4>
                    <p>Folosiți bara de navigare din partea de sus pentru a accesa aceste secțiuni. Faceți clic pe oricare dintre opțiuni pentru a deschide paginile respective. Pentru o experiență optimă, utilizați instrumentele de navigare din cadrul aplicației, nu butoanele de navigare ale browserului.</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
