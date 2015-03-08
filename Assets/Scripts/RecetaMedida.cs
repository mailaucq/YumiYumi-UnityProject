using UnityEngine;
using System.Collections;

public class RecetaMedida{
	public string medidaName;
	public float cantidad;
	public string ingredienteName;
	public string utencilioOrigenName;
	public string utencilioDestinoName;
	public string accionStepName;
	public float scaleMedida;
	public float rangeMedida;
	
	// Default constructor:
	public RecetaMedida()
	{
		cantidad = 0;
		medidaName = "";
		ingredienteName = "";
		accionStepName = "";
		utencilioOrigenName = "";
		utencilioDestinoName = "";
		scaleMedida = 0;
		rangeMedida = 0;
	}
	// Constructor:
	public RecetaMedida(float cantidad, string medidaName, string ingredienteName, string utencilioOrigenName, string utencilioDestinoName,
	                    string accionStepName, float scaleMedida, float rangeMedida)
	{
		this.cantidad = cantidad;
		this.medidaName = medidaName;
		this.ingredienteName = ingredienteName;
		this.accionStepName = accionStepName;
		this.utencilioOrigenName = utencilioOrigenName;
		this.utencilioDestinoName = utencilioDestinoName;
		if (scaleMedida > cantidad || scaleMedida == 0) {
			scaleMedida = cantidad;
		}
		this.scaleMedida = scaleMedida;
		this.rangeMedida = rangeMedida;
	}
	public string PrintRecetaMedida()
	{
		return accionStepName + " " + cantidad.ToString() + " " + medidaName + " " + ingredienteName + " ";
	}
}