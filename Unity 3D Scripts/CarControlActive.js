var CarControl : GameObject;
var Dreamcar01 : GameObject;
var	OverheadCamera : GameObject;
var Wheelcamera : GameObject;

function Start () {
	CarControl.GetComponent("VehicleFree").enabled = true;
	CarControl.GetComponent("CarScaling").enabled = true;
	Dreamcar01.GetComponent("CarAIControl").enabled = true;
	OverheadCamera.GetComponent("Something").enabled = true;
	Wheelcamera.GetComponent("Something").enabled = true;
	
}
