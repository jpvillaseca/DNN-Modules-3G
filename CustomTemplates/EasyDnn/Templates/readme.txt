/httpdocs/DesktopModules/EasyDNNnews/Templates/_default/Ozone/News


Tienen que estar configurados los tokens:
[EasyDNNnewsToken:DigevoViralCore.Config] //Par�metros de configuraci�n
[EasyDNNnewsToken:DigevoViralCore.Source] //M�todos necesarios para la ejecuci�n de core
[EasyDNNnewsToken:DigevoViralCore.ShareButtons] //Botones para compartir
[EasyDNNnewsToken:DigevoViralCore.Bootstrapper] //Activador del viral core


Instalaci�n:

Si el m�dulo ViralEngineAdapter est� agregado y configurado en la p�gina, aunque est� desactivado, va a permitir tener controles virales en la p�gina.
	Solo ser�a necesario configurar [EasyDNNnewsToken:DigevoViralCore.ShareButtons] con los botones de SHare

Si el m�dulo no est� en la p�gina, se deben agregar los m�todos JS y variables d configuraci�n correspondientes a todos los tokens.


Requerimientos:
	-FontAwesome 4.3.0 o superior (revisar carpeta CustomTemplates/EasyDNN/font-awesome-4.3.0)
	-EasyDNN y un template que haga uso de estos tokens (revisar carpeta CustomTemplates/EasyDNN/Templates)
	-EasyDNN tiene que estar configurado para hacer uso de ese template
	-Los tokens que hagan referencia a ese template tienen que estar configurados en cada portal
