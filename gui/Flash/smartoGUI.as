﻿package
{
	import flash.display.*;
	import flash.events.*;
	import fl.events.SliderEvent;
	import flash.external.ExternalInterface;
	import caurina.transitions.Tweener;
	
	import org.papervision3d.cameras.Camera3D;
	import org.papervision3d.materials.MovieMaterial;
    import org.papervision3d.materials.ColorMaterial;
    import org.papervision3d.objects.primitives.Plane;
	 import org.papervision3d.objects.primitives.Cylinder;
	 import org.papervision3d.view.BasicView;   
	
	public class smartoGUI extends MovieClip
    {
		private var intensityMatrix:Array = new Array(26);
		private var dotMatrix:Array = new Array(26);
		private var dotMatrixContainer:MovieClip = new MovieClip();
		private var cylinderMatrix:Array = new Array(26);
		private var cylinderColor:Array = new Array();
		
		private var SENSOR_X:Array = new Array();
		private var SENSOR_Y:Array = new Array();
		
     	private var view:BasicView;
		private var mc_material:MovieMaterial;
		private var clr_material:ColorMaterial;
		private var foot3d:Plane;
		private var footMotor:Plane;
		
		function loop(e:Event):void{
			view.singleRender();
		}
		
		// ------- Constructor -------
		public function smartoGUI()
		{
			
			//2D
			var r:int = 1;
			var c:int = 2;
			createDotMatrix();
			initializeIntensityMatrix();
			initializeSensorLocations();
			
			
			//Tweener.addTween(s1,{x:100,time:1});
			//init s1
			s1.maximum = 200;
			s1.minimum = 0;
			s1.value = 100;
			s1.snapInterval = 20;
			s1.tickInterval = 20;
			
			//addEventListener(MouseEvent.MOUSE_MOVE,interactiveColor);
			btnTest.addEventListener(MouseEvent.CLICK,showRS232);
			
			m1up.addEventListener(MouseEvent.CLICK,runMotor2);
			m1down.addEventListener(MouseEvent.CLICK,runMotor2);
			m2up.addEventListener(MouseEvent.CLICK,runMotor2);
			m2down.addEventListener(MouseEvent.CLICK,runMotor2);
			m3up.addEventListener(MouseEvent.CLICK,runMotor2);
			m3down.addEventListener(MouseEvent.CLICK,runMotor2);
			
			s1.addEventListener(MouseEvent.MOUSE_DOWN,mouseDownSlider);
			s1.addEventListener(Event.CHANGE,runMotor);
			s1.addEventListener(SliderEvent.THUMB_DRAG,updateText);
			
			ledADC.addEventListener(MouseEvent.CLICK,ADCHandler);
			ledMotors.addEventListener(MouseEvent.CLICK,MotorsHandler);
			lblADC.addEventListener(MouseEvent.CLICK,ADCHandler);
			lblMotors.addEventListener(MouseEvent.CLICK,MotorsHandler);
			ledADC.buttonMode = true;
			ledMotors.buttonMode = true;
			//lblADC.buttonMode = true;
			//lblMotors.buttonMode = true;
			
			ExternalInterface.addCallback("vbColorFoot",colorFoot);
			
			
			//3D
			view = new BasicView(stage.stageWidth, stage.stageHeight, false, false);
			addChild(view);
			clr_material = new ColorMaterial(0xFF00FF,100,false);
			//mc_material = new MovieMaterial(new dot());
			mc_material = new MovieMaterial(dotMatrixContainer);
			mc_material.animated = true;
			
			for(var i:int=0; i<10; i++) {
				cylinderColor.push(new ColorMaterial(0x0000FF,100,false));
			}
			for(var i:int=0; i<10; i++) {
				cylinderColor.push(new ColorMaterial(0x00FFFF,100,false));
			}
			for(var i:int=0; i<10; i++) {
				cylinderColor.push(new ColorMaterial(0x00FF00,100,false));
			}
			for(var i:int=0; i<10; i++) {
				cylinderColor.push(new ColorMaterial(0xFFFF00,100,false));
			}
			for(var i:int=0; i<10; i++) {
				cylinderColor.push(new ColorMaterial(0xFF0000,100,false));
			}
			foot3d = new Plane(new ColorMaterial(0x000000, 0),500,1300,4,4);
			foot3d.rotationX = 400;
			foot3d.rotationY = 300;
			foot3d.rotationZ = 45;
			foot3d.scale = 1.3;
			foot3d.y = 100;
			
			footMotor = new Plane(mc_material,500,1300,4,4);
			footMotor.rotationX = 380;
			footMotor.rotationY = 300;
			footMotor.rotationZ = 60;
			footMotor.z = -300;
			footMotor.y = -350;
			footMotor.x = -300;
			footMotor.scale = 0.6;
			view.scene.addChild(footMotor);
			view.scene.addChild(foot3d);
			addChild(m1lbl);
			addChild(m2lbl);
			addChild(m3lbl);
			addChild(m1up);
			addChild(m2up);
			addChild(m3up);
			addChild(m1down);
			addChild(m2down);
			addChild(m3down);
			
			addEventListener(Event.ENTER_FRAME, loop);
			//addEventListener(MouseEvent.MOUSE_MOVE,moveFoot);
			create3dFoot();
			
			//call  coloring function
			colorFoot([0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]);
		}
		function ADCHandler(e:MouseEvent):void {
			if(ledADC.currentFrame==1) {
				ledADC.nextFrame();
				lblADC.text = "ADC ON";
				ExternalInterface.call("vbStartADC");
			} else {
				ledADC.prevFrame();
				lblADC.text = "ADC OFF";
				ExternalInterface.call("vbStopADC");
			}
		}
		function MotorsHandler(e:MouseEvent):void {
			if(ledMotors.currentFrame==1) {
				ledMotors.nextFrame();
				lblMotors.text = "Motors ON";
				ExternalInterface.call("vbStartMotors");
			} else {
				ledMotors.prevFrame();
				lblMotors.text = "Motors OFF";
				ExternalInterface.call("vbStopMotors");
			}
		}
		function moveFoot(e:MouseEvent=null):void {
			if(mouseX > 400 && mouseX < 800 && mouseY > 230 && mouseY < 430) {
				foot3d.rotationX += mouseX;
				foot3d.rotationY += mouseY;
			}
			//foot3d.rotationY = mouseY;
			trace(foot3d.rotationX + " X, Z " + foot3d.rotationZ);
		}
		function initializeSensorLocations():void {
			//b i g   s e n s o r s
			SENSOR_X[0] = 2;
			SENSOR_Y[0] = 1;
			SENSOR_X[1] = 2;
			SENSOR_Y[1] = 5;
			//m e d i u m   s e n s o r s
			SENSOR_X[2] = 18;
			SENSOR_Y[2] = 6;
			SENSOR_X[3] = 19;
			SENSOR_Y[3] = 2;
			SENSOR_X[4] = 11;
			SENSOR_Y[4] = 7;
			SENSOR_X[5] = 8;
			SENSOR_Y[5] = 6;
			//s m a l l   s e n s o r s
			SENSOR_X[6] = 16;
			SENSOR_Y[6] = 1;
			SENSOR_X[7] = 8;
			SENSOR_Y[7] = 2;
			SENSOR_X[8] = 24;
			SENSOR_Y[8] = 1;
			SENSOR_X[9] = 23;
			SENSOR_Y[9] = 4;
			SENSOR_X[10] = 22;
			SENSOR_Y[10] = 7;
			SENSOR_X[11] = 16;
			SENSOR_Y[11] = 7;
			SENSOR_X[12] = 12;
			SENSOR_Y[12] = 2;
			SENSOR_X[13] = 15;
			SENSOR_Y[13] = 5;
			SENSOR_X[14] = 12;
			SENSOR_Y[14] = 4;
			SENSOR_X[15] = 5;
			SENSOR_Y[15] = 4;
			SENSOR_X[16] = 0; //move them out to the far right bottom
			SENSOR_Y[16] = 9;
			SENSOR_X[17] = 0;
			SENSOR_Y[17] = 9;
			
			//f l e x   s e n s o r s  right
			SENSOR_X[18] = 7
			SENSOR_Y[18] =6
			SENSOR_X[19] = 10
			SENSOR_Y[19] =6
			SENSOR_X[20] = 13
			SENSOR_Y[20] =6
			//f l e x   s e n s o r s  left
			SENSOR_X[21] = 6
			SENSOR_Y[21] = 2
			SENSOR_X[22] = 9
			SENSOR_Y[22] = 2
			SENSOR_X[23] = 12
			SENSOR_Y[23] = 1
			//f l e x   s e n s o r s  front
			SENSOR_X[24] = 20
			SENSOR_Y[24] =1
			SENSOR_X[25] = 20
			SENSOR_Y[25] =4
			SENSOR_X[26] = 20
			SENSOR_Y[26] =7
		}
		function interactiveColor(e:MouseEvent=null):void {
			/*initializeIntensityMatrix();
			addIntensities(35*(stage.stageWidth-mouseX)/stage.stageWidth,10,5);
			addIntensities(70*(stage.stageWidth-mouseX)/stage.stageWidth,2,4); //
			addIntensities(30*(stage.stageWidth-mouseX)/stage.stageWidth,7,8);
			addIntensities(50*(stage.stageWidth-mouseX)/stage.stageWidth,17,6);
			addIntensities(70*(stage.stageWidth-mouseX)/stage.stageWidth,4,2);
			
			addIntensities(30*(stage.stageWidth-mouseX)/stage.stageWidth,23,2);
			addIntensities(30*(stage.stageWidth-mouseX)/stage.stageWidth,22,6);
			updateIntensities();UA*/ 
		}
		function createDotMatrix():void {
			var r:int = 0;
			var c:int = 0;
			for(r=0; r<26; r++) {
				dotMatrix[r] = new Array();
				for(c=0; c<10; c++) {
					dotMatrix[r].push(new dot());
					dotMatrix[r][c].x = 20*c;
					dotMatrix[r][c].y = 500-20*r;
					//dotMatrix[r][c].alpha = 0.3;
					dotMatrixContainer.addChild(dotMatrix[r][c]);
				}
			}
			addChild(dotMatrixContainer);
			dotMatrixContainer.x = 995;
			dotMatrixContainer.y = 230;
		}
		function create3dFoot():void {
			//cylinderMatrix[] = new Cylinder(clr_material,20,50);//intensityMatrix[1][1]
			for(var r:int = 0; r<26; r++) {
				trace("r: " + r);
				cylinderMatrix[r] = new Array();
				for(var c:int = 0; c<10; c++) {
					trace("r_c: " + r+ "_" +c);
					cylinderMatrix[r].push(new Cylinder(clr_material,20,50,4,4));
					cylinderMatrix[r][c].rotationX = 90;
					cylinderMatrix[r][c].x = c*40;
					cylinderMatrix[r][c].y = r*40;
					foot3d.addChild(cylinderMatrix[r][c]);
				}
			}
		}
		function initializeIntensityMatrix():void {
			var r:int = 0;
			var c:int = 0;
			for(r=0; r<26; r++) {
				intensityMatrix[r] = new Array();
				for(c=0; c<10; c++) {
					intensityMatrix[r].push(1);
				}
			}
		}
		public function addIntensities( sensorValue:int,  sensorRow:int,  sensorCol:int,  radius:int=5):void {
			
			//set the actual sensor point
			intensityMatrix[sensorRow][sensorCol] += sensorValue;

			//interpolation loop
			var i:int = 0;
			var currentIntensity:int;
			var currentIntensityDiagonal:int;
			for (i=1; i<radius-2; i++){
				currentIntensity = int(sensorValue - i*50/radius);
				currentIntensityDiagonal = int(sensorValue - (i+0.5)*50/radius); 
				if(currentIntensity >50) {
					currentIntensity = 50;
				} else if (currentIntensity <1) {
					currentIntensity = 1;
				}
				if(currentIntensityDiagonal >50) {
					currentIntensityDiagonal = 50;
				} else if (currentIntensityDiagonal <1) {
					currentIntensityDiagonal = 1;
				}
				
				if(sensorCol+i <10) {
					intensityMatrix[sensorRow][sensorCol+i]   +=  currentIntensity;
				}
				if(sensorCol-i >=0) {
					intensityMatrix[sensorRow][sensorCol-i]   +=  currentIntensity;
				}
				if(sensorRow+i <26) {
					intensityMatrix[sensorRow+i][sensorCol]   +=  currentIntensity;
				}
				if(sensorRow-i >=0) {
					intensityMatrix[sensorRow-i][sensorCol]   +=  currentIntensity;
				}
				if(sensorRow+i <26 && sensorCol+i <10)
					intensityMatrix[sensorRow+i][sensorCol+i] += currentIntensityDiagonal;
				if(sensorRow+i <26 && sensorCol-i >=0)
					intensityMatrix[sensorRow+i][sensorCol-i] += currentIntensityDiagonal;
				if(sensorRow-i >=0 && sensorCol+i <10)
					intensityMatrix[sensorRow-i][sensorCol+i] += currentIntensityDiagonal;
				if(sensorRow-i >=0 && sensorCol-i >=0)
					intensityMatrix[sensorRow-i][sensorCol-i] += currentIntensityDiagonal;
				
				//var fillerIntensity:int = 51-int((dotMatrix[sensorRow+2][sensorCol+2].currentFrame+dotMatrix[sensorRow+2][sensorCol].currentFrame)/2);
				/*var fillerIntensity:int = (currentIntensity+currentIntensityDiagonal)/2;
				intensityMatrix[sensorRow+1][sensorCol+2] += fillerIntensity;
				intensityMatrix[sensorRow-1][sensorCol+2] += fillerIntensity;
				intensityMatrix[sensorRow+2][sensorCol+1] += fillerIntensity;
				intensityMatrix[sensorRow-2][sensorCol+1] += fillerIntensity;
				intensityMatrix[sensorRow+2][sensorCol-1] += fillerIntensity;
				intensityMatrix[sensorRow-2][sensorCol-1] += fillerIntensity;
				intensityMatrix[sensorRow+1][sensorCol-2] += fillerIntensity;
				intensityMatrix[sensorRow-1][sensorCol-2] += fillerIntensity;*/
			}
		}
		public function updateIntensities():void {
			hideNonFootDots();
			for (var r:int=0;r<26;r++) {
				for (var c:int=0;c<10;c++) {
					dotMatrix[r][c].gotoAndStop(51-intensityMatrix[r][c]);
					cylinderMatrix[r][c].scaleY = 5*intensityMatrix[r][c]/50 +0.5;
					cylinderMatrix[r][c].z = -(5*intensityMatrix[r][c]+0.5)/2;
					cylinderMatrix[r][c].material = cylinderColor[intensityMatrix[r][c]];
				}
			}
		}
		function hideNonFootDots():void {
			for(var c:int=0; c<10; c++) {
				for(var r:int=0; r<26; r++) {
					switch(c){
						case 0:
							if(r<1 || (r>6 && r<12) || (r>20 && r<24) || r>24) {
								intensityMatrix[r][c] = 0; //(r>5 && r<18) ||
								cylinderMatrix[r][c].visible = false;
							}
							break;
						case 1:
							if((r>8 && r<11) || (r>21 && r<23)) {
								intensityMatrix[r][c] = 0; //(r>7 && r<17) ||
								cylinderMatrix[r][c].visible = false;
							}
							break;
						case 2:
							if( (r>21 && r<24)) {
								intensityMatrix[r][c] = 0; //(r>8 && r<17) ||
								cylinderMatrix[r][c].visible = false;
							}
							break;
						case 3:
							if( r>22){
								intensityMatrix[r][c] = 0; //(r>9 && r<17) ||
								cylinderMatrix[r][c].visible = false;
							}
							break;
						case 4:
							if( (r>22 && r<25)){
								intensityMatrix[r][c] = 0; //(r>12 && r<16) ||
								cylinderMatrix[r][c].visible = false;
							}
							break;
						case 5:
							if(r<1 || (r>21 && r<24)){
								intensityMatrix[r][c] = 0;
								cylinderMatrix[r][c].visible = false;
							}
							break;
						case 6:
							if(r<2 || (r>21 && r<23)){
								intensityMatrix[r][c] = 0;
								cylinderMatrix[r][c].visible = false;
							}
							break;
						case 7:
							if(r<6 || (r>20 && r<22) || r>24) {
								intensityMatrix[r][c] = 0;
								cylinderMatrix[r][c].visible = false;
							}
							break;
						case 8:
							if(r<11 || (r>21 && r<23) || r>23) {
								intensityMatrix[r][c] = 0;
								cylinderMatrix[r][c].visible = false;
							}
							break;
						case 9:
							if(r<16 || (r>17 && r<20) || r>20) {
								intensityMatrix[r][c] = 0;
								cylinderMatrix[r][c].visible = false;
							}
							break;
					}
				}
			}
		}
		function runSlider(e:MouseEvent=null):void {
			//ExternalInterface.call("vbRunMotor", e.target.name.charAt(1), e.target.name.substr(2)); // args: fn, motor#, up/down
			//ExternalInterface.call("vbRunMotor", e.target.name.charAt(1), e.target.value-100);
			if(e.target.name.substr(2) == "up") {
				Slider(getChildByName("s" + e.target.name.charAt(1))).value += 20;
			} else {
				Slider(getChildByName("s" + e.target.name.charAt(1))).value -= 20;
			}
			runMotor();
		}
		function mouseDownSlider(e:MouseEvent=null):void {
			trace("mouseDowned");
			//stage.addEventListener(MouseEvent.MOUSE_UP,resetSlider);
		}
		function resetSlider(e:MouseEvent=null):void {
			trace("resetSlider");
			//Tweener.addTween(s1,{value:100,time:1});
		}
		function updateText(e:Event=null):void {
			txtSlider1.text = (s1.value-100).toString();
		}
		function runMotor(e:Event=null):void {
			//ExternalInterface.call("vbRunMotor", e.target.name.charAt(1), e.target.value-100);
			ExternalInterface.call("vbRunMotor", 1, s1.value-100);
			trace(s1.value-100 + " sent to VB");
			Tweener.addTween(s1,{value:100,time:1});
		}
		function runMotor2(e:MouseEvent=null):void {
			if(e.target.name.substr(2) == "up") {
				ExternalInterface.call("vbRunMotor", e.target.name.charAt(1), 60);
				trace(e.target.name.charAt(1) + " | " + 60); 
			} else {
				ExternalInterface.call("vbRunMotor", e.target.name.charAt(1), -60);
				trace(e.target.name.charAt(1) + " | -" + 60); 
			}
			//ExternalInterface.call("vbRunMotor", 2, s1.value-100);
			//trace(s1.value-100 + " sent to VB");
			//Tweener.addTween(s1,{value:100,time:1});
		}
		function showRS232(e:MouseEvent=null):void {
			ExternalInterface.call("vbShowRS232"); 
		}
		function colorFoot(sval:Array):void {
			// testing only
			if (btnTest.visible) {
				btnTest.visible = false;
			} else {
				btnTest.visible = true;
			}
			
			// real code
			initializeIntensityMatrix();
			txtDisplay.text = "";
			for(var i:int=0; i<27; i++) {
				txtDisplay.appendText  (i + ": " + sval[i] + "\n");
				trace(i + " good Alhamdulillah");
				addIntensities(sval[i],SENSOR_X[i],SENSOR_Y[i]);
			}
			updateIntensities();
			
			/*addIntensities(70*(stage.stageWidth-mouseX)/stage.stageWidth,2,4); //
			addIntensities(30*(stage.stageWidth-mouseX)/stage.stageWidth,7,8);
			addIntensities(50*(stage.stageWidth-mouseX)/stage.stageWidth,17,6);
			addIntensities(70*(stage.stageWidth-mouseX)/stage.stageWidth,4,2);
			
			addIntensities(30*(stage.stageWidth-mouseX)/stage.stageWidth,23,2);
			addIntensities(30*(stage.stageWidth-mouseX)/stage.stageWidth,22,6);*/
		}
	}
}
