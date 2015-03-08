using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Receta{
	public List<RecetaMedida> recetaMedidaList;
	public Receta(){
		recetaMedidaList = new List<RecetaMedida>();
	}
	public List<RecetaMedida> get(){
		return recetaMedidaList;
	}
	public void add(RecetaMedida recetaMedida){
		recetaMedidaList.Add (recetaMedida);
	}
	
}
