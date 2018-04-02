using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCanvas : MonoBehaviour {
	public RectTransform healthSlider;
	public RectTransform energySlider;
	public RectTransform repairKitText;
	public RectTransform scrapText;
	// Use this for initialization
	void Awake () {
		/*
		RectTransform	hs = Instantiate(healthSlider, new Vector3(0,0,0), transform.rotation);
		RectTransform	es = Instantiate(energySlider, transform.position, transform.rotation);
		RectTransform	rt = Instantiate(repairKitText, transform.position, transform.rotation);
		RectTransform	st = Instantiate(scrapText, transform.position, transform.rotation);

		hs.transform.SetParent (this.transform);
		es.transform.SetParent (this.transform);
		rt.transform.SetParent (this.transform);
		st.transform.SetParent (this.transform);
		hs.name = "HS";
		es.name = "ES";
		rt.name = "RT";
		st.name = "ST";
		hs.SetSiblingIndex (0);
		es.SetSiblingIndex (1);


		hs.transform.localPosition  = new Vector3 (-86.1f,119.6f,20f);
		es.transform.localPosition  = new Vector3 (-113.3f, 88.9f,20f);
		print (hs.transform.position);
		rt.transform.localPosition  = new Vector3 (-112.6f,56.55f,0f);
		st.transform.localPosition  = new Vector3 (9.7f,54.58f,0f); */
	}
	void Update(){
	}


}
