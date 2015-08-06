/httpdocs/DesktopModules/EasyDNNnews/Templates/_default/Ozone/News


Tienen que estar configurados los tokens:
[EasyDNNnewsToken:DigevoViralCore.Config] //Parámetros de configuración
[EasyDNNnewsToken:DigevoViralCore.Source] //Métodos necesarios para la ejecución de core
[EasyDNNnewsToken:DigevoViralCore.ShareButtons] //Botones para compartir
[EasyDNNnewsToken:DigevoViralCore.Bootstrapper] //Activador del viral core


Instalación:

Si el módulo ViralEngineAdapter está agregado y configurado en la página, aunque esté desactivado, va a permitir tener controles virales en la página.
	Solo sería necesario configurar [EasyDNNnewsToken:DigevoViralCore.ShareButtons] con los botones de SHare

Si el módulo no está en la página, se deben agregar los métodos JS y variables d configuración correspondientes a todos los tokens.


Requerimientos:
	-FontAwesome 4.3.0 o superior (revisar carpeta CustomTemplates/EasyDNN/font-awesome-4.3.0)
	-EasyDNN y un template que haga uso de estos tokens (revisar carpeta CustomTemplates/EasyDNN/Templates)
	-EasyDNN tiene que estar configurado para hacer uso de ese template
	-Los tokens que hagan referencia a ese template tienen que estar configurados en cada portal
