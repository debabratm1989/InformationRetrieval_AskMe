<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchResult.aspx.cs" Inherits="AskMe.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .table, th, td {
            border: 1px solid black;
        }
    .auto-style1 {
        width: 30%;
    }
    </style>
     <table class="table" border="1" style="width:500px;margin-left:0px;">
         <tr>
             <td style="width:33.33%">
                 <asp:Label ID="lblGoogle" runat="server" Text="Google Search" ForeColor="#3333FF" Font-Size="14px"></asp:Label>
                 <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/google.jpg" Height="155px" />
             </td>
             <td class="auto-style1">
                  <asp:Label ID="lblBing" runat="server" Text="Bing Search" Font-Bold="True" Font-Size="14px" ForeColor="#3333FF"></asp:Label>
                 <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/img/bing.jpeg" Height="96px" Width="258px" />
             </td>
              <td style="width:33.33%">
                  <asp:Label ID="lblAskMe" runat="server" Text="AskMe Search" Font-Bold="True" Font-Size="14px" ForeColor="#3333FF"></asp:Label>
             </td>
           
         </tr>
        <tr>
            <td style="width:33.33%">
            <script>
                (function () {
                    var cx = '013853568638797180973:ph5unffa8no';
                    var gcse = document.createElement('script');
                    gcse.type = 'text/javascript';
                    gcse.async = true;
                    gcse.src = (document.location.protocol == 'https:' ? 'https:' : 'http:') +
                        '//cse.google.com/cse.js?cx=' + cx;
                    var s = document.getElementsByTagName('script')[0];
                    s.parentNode.insertBefore(gcse, s);
                })();
            </script>
   
            <gcse:searchresults-only></gcse:searchresults-only>
            </td>
                <td style="border-color: #999999;" class="auto-style1">
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
             <td style="border-color: #999999;width:33.33%">
                    <asp:GridView ID="gvSolrSearch" runat="server"
                        AutoGenerateColumns="false" ShowHeader="false" Width="712px"
                        BorderColor="White">
                    <Columns>
                        <asp:TemplateField>
 
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("title") %>'
                                Font-Size="Large"></asp:Label>
                            <br />
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Bind("id") %>'><%#Eval("id") %></asp:HyperLink>
                            <br />
                            
                            <br />
                            <br />
                        </ItemTemplate>
 
                    </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                </td>
           
            </tr>
        </table>
               
</asp:Content>
