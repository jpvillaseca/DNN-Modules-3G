<%@ Control Language="C#" AutoEventWireup="true" Inherits="Digevo.Digevo.Condorimeme.index" CodeFile="index.ascx.cs" %>

<% 
	bool isFullscreen = Convert.ToBoolean(Session["showFullscreen"]);
    bool hasId = Convert.ToBoolean(Session["hasId"]);
    bool memeExists = Convert.ToBoolean(Session["memeExists"]);
    string memeId = Session["memeId"].ToString();
    string memePath = Session["memePath"].ToString();
    string[] images = (string[])Session["images"];
    string[] history = (string[])Session["history"];
%>
	<% if(isFullscreen) { %>
		<link rel="stylesheet" type="text/css" href="/DesktopModules/Digevo/Digevo.Condorimeme/css/fullscreen.css" />	
	<% } %>
    <link rel="stylesheet" type="text/css" href="/DesktopModules/Digevo/Digevo.Condorimeme/css/style2.css" />
    <link rel="stylesheet" type="text/css" href="/DesktopModules/Digevo/Digevo.Condorimeme/css/font-awesome.min.css" />
    <script type="text/javascript" src="/DesktopModules/Digevo/Digevo.Condorimeme/js/jquery.waitforimages.min.js"></script>
    <script type="text/javascript" src="/DesktopModules/Digevo/Digevo.Condorimeme/js/jquery.scrollbox.min.js"></script>
	<div class="condorimemes">
        <div class="table">
            <div class="cell">
                <section>
                    <header>
                        <h1><img src="/DesktopModules/Digevo/Digevo.Condorimeme/img/logo.png" /></h1>
                        <h2>¡Crea tu condorimeme y compártelo con tus amigos!</h2>
                    </header>
                    <% if (hasId) { %>
                        <content id="meme">
                            <% if (!memeExists)
                                { %>
                                <div class="full text_center">
                                    <div class="og_container">
                                        <span class="fa fa-exclamation-triangle"></span><span>El meme especificado no existe.</span>
                                    </div>
                                </div>
                            <% } else { %>
                                <div class="left v_middle text_center">
                                    <div class="og_container">
                                        <div class="og_frame">
                                            <img id="<%=memeId%>" src="/DesktopModules/Digevo/Digevo.Condorimeme/img/memes/<%=(memeId + ".jpg")%>" />
                                        </div>
                                        <div class="meta">
                                            <span>CREADO ###### A LAS #####</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="right v_middle text_center">
                                    <div class="url">
                                        <input type="text" value="" placeholder="" onClick="this.select();" readonly="readonly" />
                                    </div>
                                    <div class="social">
                                        <div class="facebook"><span class="fa fa-facebook fa-fw"></span></div>
                                        <div class="twitter"><span class="fa fa-twitter fa-fw"></span></div>
                                        <div class="whatsapp"><span class="fa fa-whatsapp fa-fw"></span></div>
                                    </div>
                                    <br />
                                    <div class="download">
                                        <div class="device">
                                            <a href="/DesktopModules/Digevo/Digevo.Condorimeme/img/memes/<%=(memeId + ".jpg")%>" download="<%=(memeId + ".jpg")%>">
                                                <span class="fa fa-download fa-fw"></span><span>GUARDAR</span>
                                            </a>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="goto">
                                        <div class="factory"><span class="fa fa-asterisk"></span><span>CREA TU MEME</span></div>
                                    </div>
                                </div>
                            <% } %>
                        </content>
                    <% } %>

                    <% if (memeExists) { %>
                        <div class="goback"><div class="preview"><span class="fa fa-angle-double-up"></span><span>VOLVER AL MEME</span></div></div> <% } %>

                    <content id="factory" <% if (memeExists) {%> class="hidden" <% } %>>
                        <div class="overlay solid loading sup"></div>
                        <div class="left text_center">
                            <div class="title">
                                <h1>1</h1>
                                <span>Selecciona una imagen</span>
                            </div>
                            <div class="gallery_container">
                                <div class="overlay hidden"></div>
                                <div class="gallery">
                                    <% foreach (string image in images) { %> 
                                        <div class="img"><div class="overlay loading"></div><img id="<%=image %>" src="/DesktopModules/Digevo/Digevo.Condorimeme/img/src/<%=image %>.jpg" /></div> <% } %>
                                </div>
                            </div>
                        </div>
                        <div class="right text_center">
                            <div class="title">
                                <h1>2</h1>
                                <span>Dale tu toque</span>
                            </div>
                            <div class="meme_container">
                                <div class="overlay"></div>
                                <div class="canvas_container">
                                    <div class="canvas_frame">
                                        <div class="overlay solid hidden"></div>
                                        <canvas id="condorimeme">
                                            Tu navegador no soporta canvas. Prueba con otro navegador.
                                        </canvas>
                                    </div>
                                    <img id="default" src="/DesktopModules/Digevo/Digevo.Condorimeme/img/default.jpg" alt="" />
                                </div>
                                <div class="controls">
                                    <input type="text" value="" id="txt" placeholder="Escribe tu mensaje aquí." />
                                    <div class="counter">
                                        <span id="current">0</span> / <span id="total">0</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="full text_center">
                            <div class="title text_center">
                                <h1>3</h1>
                                <span>Comparte o descárgalo en tu dispositivo</span>
                            </div>
                            <div class="rel_container">
                                <div class="overlay">
                                    <div class="table">
                                        <div class="cell"><br /><span></span></div>
                                    </div>
                                </div>
                                <div class="table">
                                    <div class="cell">
                                        <div class="generate">
                                            <div class="make"><span class="fa fa-magic"></span><span>CREAR MEME</span></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </content>
                    <content id="history">
                        <div class="history_container">
                            <ul>
                                <% foreach (string image in history) { %>
                                        <li><a href="?id=<%=image%>"><img src="/DesktopModules/Digevo/Digevo.Condorimeme/img/memes/<%=(image + ".jpg")%>" /></a></li>
                                <% } %>
                            </ul>
                        </div>
                    </content>
                </section>
                <footer>&copy; 2015 - Digevo Group.</footer>
            </div>
        </div>
	</div>
	<script type="text/javascript" src="/DesktopModules/Digevo/Digevo.Condorimeme/js/init.js"></script>