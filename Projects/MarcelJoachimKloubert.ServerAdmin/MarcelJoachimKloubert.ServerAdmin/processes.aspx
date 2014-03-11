<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="processes.aspx.cs" Inherits="MarcelJoachimKloubert.ServerAdmin.processes" %>

<asp:Content ID="Content_Header" ContentPlaceHolderID="ContentPlaceHolder_Header" runat="server">
    <style type="text/css">
        #processList {
            width: auto !important;
        }

        #processList th.id {
            width: 96px;
        }

        #processList th.machineName {
            width: 128px;
        }

        #processList td {
            padding-left: 8px;
            padding-right: 8px;
        }

        #processList td.id {
            text-align: center;
        }

        #processList td.machineName {
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content_Content" ContentPlaceHolderID="ContentPlaceHolder_Content" runat="server">
    <script runat="server">
    
        private IList<System.Diagnostics.Process> GetProcesses()
        {
            var result = new List<System.Diagnostics.Process>();
            
            try
            {
                result.AddRange(System.Diagnostics.Process
                                                  .GetProcesses()
                                                  .OrderBy(p => TryGetProcessValue(p, x => x.ProcessName), StringComparer.InvariantCultureIgnoreCase)
                                                  .ThenBy(p => TryGetProcessValue(p, x => x.MachineName), StringComparer.InvariantCultureIgnoreCase)
                                                  .ThenBy(p => TryGetProcessValue(p, x => x.Id)));
            }
            catch
            {
                // ignore here
            }

            return result;
        }
        
        private static T TryGetProcessValue<T>(System.Diagnostics.Process p,
                                               Func<System.Diagnostics.Process, T> func,
                                               T failedResult = default(T))
        {
            try
            {
                return func(p);
            }
            catch
            {
                return failedResult;
            }
        }
    
    </script>

    <h2>Processes</h2>
    <table id="processList">
        <tr>
            <th class="id">ID</th>
            <th class="name">Name</th>
            <th class="machineName">Machine</th>
        </tr>

        <%
            foreach (var p in this.GetProcesses())
            {
                %><tr class="row"><%
                          
                %><td class="id"><%= TryGetProcessValue(p, x => x.Id) %></td><%   
                %><td class="name"><%= TryGetProcessValue(p, x => x.ProcessName) %></td><%
                %><td class="machineName"><%= TryGetProcessValue(p, x => x.MachineName) %></td><% 
                                                     
                %></tr><%
            }
        %>
    </table>
</asp:Content>

<asp:Content ID="Content_Sidebar" ContentPlaceHolderID="ContentPlaceHolder_Sidebar" runat="server">

</asp:Content>
