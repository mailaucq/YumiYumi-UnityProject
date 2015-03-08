/// <summary>
/// Utilidad para hacer mas facil el guardar/descargar datos de la nube de Google Play Games.
/// Creada para la siguiente serie de video tutoriales:
/// https://www.youtube.com/playlist?list=PLREdURb87ks2qkD9svvlIwYwN35FZ3Afv
/// Del canal de Youtube "Hagamos Videojuegos":
/// https://www.youtube.com/juande
/// Eres libre de usar esta clase en tus proyectos siempre y cuando mantengas esta informacion
/// sobre mi canal de Youtube. Gracias! :)
/// </summary>

using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public abstract class GooglePlayGamesCloudHelper<T> : OnStateLoadedListener where T : class{
	
	// Variables privadas
	
	BinaryFormatter bf;
	const int defaultSlot = 0;
	
	// Constructor
	
	public GooglePlayGamesCloudHelper(){
		bf = new BinaryFormatter();
	}
	
	// Metodos principales
	
	/// <summary>
	/// Guarda toda la informacion de "data" en la nube de Google Play Games.
	/// En caso de inconsistencia de los datos, se ejecutara el metodo "ConflictoAlGuardar"
	/// que se tendra que implementar para resolver esta inconsistencia. 
	/// </summary>
	/// <param name="slot">Numero de slot en el que guardaremos esta informacion.</param>
	/// <param name="data">Una instancia de la clase con todos los datos a guardar.</param>	
	public void CloudSave(int slot, T data){
		((PlayGamesPlatform) Social.Active).UpdateState(slot, Object2Bytes(data), this);
	}
	
	/// <summary>
	/// Guarda en toda la informacion de "data" en el slot cero de la nube de Google Play Games.
	/// En caso de inconsistencia de los datos, se ejecutara el metodo "ConflictoAlGuardar"
	/// que se tendra que implementar para resolver esta inconsistencia. 
	/// </summary>
	/// <param name="data">Una instancia de la clase con todos los datos a guardar.</param>		
	public void CloudSave(T data){
		CloudSave(defaultSlot, data);
	}
	
	/// <summary>
	/// Descarga toda la informacion guardada desde la nube de Google Play games.
	/// Si todo ha ido bien, se ejecutara el metodo "DatosDescargados", que se tendra que
	/// implementar para hacer lo que se quiera con esos datos.
	/// </summary>
	/// <param name="slot">Slot.</param>
	public void CloudLoad(int slot){
		((PlayGamesPlatform) Social.Active).LoadState(slot, this);
	}
	
	/// <summary>
	/// Descarga toda la informacion guardada en el slot cero de la nube de Google Play games.
	/// Si todo ha ido bien, se ejecutara el metodo "DatosDescargados", que se tendra que
	/// implementar para hacer lo que se quiera con esos datos.
	/// </summary>
	/// <param name="slot">Slot.</param>
	public void CloudLoad(){
		CloudLoad(defaultSlot);
	}
	
	// Metodos auxiliares para pasar de instancia a byte[] y a la inversa
	
	private byte[] Object2Bytes(T data){
		MemoryStream ms = new MemoryStream();		
		bf.Serialize(ms, data);	
		return ms.ToArray();
	}
	
	private T Bytes2Object(byte[] data){
		MemoryStream memoryStream = new MemoryStream();
		memoryStream.Write(data, 0, data.Length);
		memoryStream.Seek(0, SeekOrigin.Begin);
		return bf.Deserialize(memoryStream) as T;
	}
	
	// Metodos abstractos que tendra que implementar el programador para personalizar el funcionamiento de esta clase
	
	/// <summary>
	/// Este metodo sera llamado si los datos que tiene guardados Google Play pertenecen a este juego,
	/// pero instalado en otro dispositivo.
	/// Se tendra que añadir dentro de este metodo una logica que decida que informacion es la que
	/// tiene que quedar al final guardada. Como infomacion adicional, se recibira el objeto local que 
	/// se esta guardando y el que ahora hay en el servidor guardado desde otro dispositivo.
	/// </summary>
	/// <returns>La version final de los datos que deberan guardarse.</returns>
	/// <param name="slot">Numero de slot en el que se ha producido esta inconsistencia.</param>
	/// <param name="local">Los datos que estas intentando guardar desde este dispositivo.</param>
	/// <param name="server">Los ultimos datos que tiene guardados Google Play recibidos desde otro dispositivo.</param>
	protected abstract T ConflictoAlGuardar(int slot, T local, T server);
	
	/// <summary>
	/// Este metodo sera llamado una vez se haya descargado la informacion de Google Play Games.
	/// El programador debera de hacer lo que crea conveniente con estos datos descargados.
	/// Generalmente debera actualizar la partida guardada con esta informacion.
	/// </summary>
	/// <param name="slot">El slot de donde se ha descargado la informacion.</param>
	/// <param name="data">El objeto reconstruido con toda la informacion</param>
	protected abstract void DatosDescargados(int slot, T data);
	
	// Interfaces a implementar
	
	public void OnStateLoaded (bool success, int slot, byte[] data)
	{
		if (success && data!=null) DatosDescargados(slot, Bytes2Object(data));
	}
	
	public byte[] OnStateConflict (int slot, byte[] localData, byte[] serverData)
	{
		T local = Bytes2Object(localData);
		T server = Bytes2Object(serverData);
		T final = ConflictoAlGuardar(slot, local, server);	
		return Object2Bytes(final);
	}
	
	public void OnStateSaved (bool success, int slot)
	{
		// No hacemos nada. ;)
	}
	
}
