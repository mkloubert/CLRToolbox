<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MarcelJoachimKloubert.MetalVZ.Default" %>
<asp:Content ID="Header" ContentPlaceHolderID="MVZHeader" runat="server">

    <script type="text/javascript">
        MetalVZ.page.events.loaded = function (ctx) {
            alert(ctx.time);
        };
    </script>

</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MVZContent" runat="server">
    
</asp:Content>
