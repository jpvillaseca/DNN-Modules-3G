<%@ Control Language="C#" AutoEventWireup="true" Inherits="Digevo.Digevo.showFullscreen.index" CodeFile="index.ascx.cs" %>

<% bool isFullscreen = Convert.ToBoolean(Session["showFullscreen"]); %>
<% if(isFullscreen) { %>
    <link rel="stylesheet" type="text/css" href="/DesktopModules/Digevo/Digevo.showFullscreen/fullscreen.css" />
<% } %>
<script type="text/javascript" src="/DesktopModules/Digevo/Digevo.showFullscreen/init.js"></script>
<div id="showFullscreen"></div>

