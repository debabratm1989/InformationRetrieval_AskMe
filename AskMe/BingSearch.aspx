<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BingSearch.aspx.cs" Inherits="AskMe.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div>
   
        <table style="width:100%;">
            <tr>
                <td align="center" bgcolor="#99CCFF" colspan="2">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Georgia"
                        Text="Integrate Bing Search API into ASP.Net application"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="80px">
                    &nbsp;</td>
                <td height="40px">
                    <asp:TextBox ID="txtSearch" runat="server" Height="30px" Width="639px"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Names="Georgia"
                        Height="30px" Text="Search" Width="65px" onclick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                    </td>
            </tr>
            <tr>
                <td style="border-color: #999999">&nbsp;</td>
                <td style="border-color: #999999">
                    <asp:GridView ID="GridViewSearchResult" runat="server"
                        AutoGenerateColumns="false" ShowHeader="false" Width="712px"
                        BorderColor="White">
                    <Columns>
                    <asp:TemplateField>
 
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'
                                Font-Size="Large"></asp:Label>
                            <br />
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Bind("Url") %>'><%#Eval("Url") %></asp:HyperLink>
                            <br />
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                            <br />
                            <br />
                        </ItemTemplate>
 
                    </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
   
    </div>
</asp:Content>
