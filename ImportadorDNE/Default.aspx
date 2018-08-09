<%@ Page Title="Home page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
  CodeBehind="Default.aspx.cs" Inherits="ImportadorDNE._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
  <h2>
    Bem-vindo ao Importador DNE!
  </h2>
  <p>
    <b>Obs: </b>Salvar aquivo com Codificação UTF8</p>
  <br />
  <br />
  <table>
    <tr>
      <td>
        <asp:Button ID="Button1" runat="server" Text="Import LOG BAIRRO" OnClick="Button1_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button2" runat="server" Text="Import Log_Faixa_UF" OnClick="Button2_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button3" runat="server" Text="Import LOG_LOCALIDADE" OnClick="Button3_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button4" runat="server" Text="Import LOG_VAR_LOC" OnClick="Button4_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button5" runat="server" Text="import LOG_FAIXA_LOCALIDADE" OnClick="Button5_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button6" runat="server" Text="import LOG_VAR_BAI" OnClick="Button6_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button7" runat="server" Text="import LOG_FAIXA_BAIRRO" OnClick="Button7_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button8" runat="server" Text="import LOG_CPC" OnClick="Button8_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button9" runat="server" Text="import LOG_FAIXA_CPC" OnClick="Button9_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button10" runat="server" Text="import LOG_LOGRADOURO_XX Todos" OnClick="Button10_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button11" runat="server" Text="import LOG_VAR_LOG" OnClick="Button11_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button12" runat="server" Text="import LOG_NUM_SEC" OnClick="Button12_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button13" runat="server" Text="import LOG_GRANDE_USUARIO" OnClick="Button13_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button14" runat="server" Text="import LOG_UNID_OPER" OnClick="Button14_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button15" runat="server" Text="import LOG_FAIXA_UOP" OnClick="Button15_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button16" runat="server" Text="import ECT_PAIS" OnClick="Button16_Click" />
      </td>
    </tr>
    <tr>
      <td>
        <asp:Button ID="Button17" runat="server" Text="Update CAD_MUNI_RADAR" OnClick="Button17_Click" />
      </td>
    </tr>
  </table>
</asp:Content>
