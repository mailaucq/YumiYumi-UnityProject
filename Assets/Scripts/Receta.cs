using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Receta{
	public Dictionary<string,RecetaMedida> recetaMedidaMap;
	public int numberStep;
	public Receta(){
		recetaMedidaMap = new Dictionary<string,RecetaMedida>();
		numberStep = 0;
	}
	public Dictionary<string,RecetaMedida> get(){
		return recetaMedidaMap;
	}
	public void addRecetaMedida(string key, RecetaMedida recetaMedida){
		recetaMedidaMap.Add(key,recetaMedida);
		numberStep++;
	}
	
}
