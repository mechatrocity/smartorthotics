package
{
	import flash.display.*;
	import flash.events.*;
	import flash.external.ExternalInterface;
	
	public class smartoGUI extends MovieClip
    {
		private var intensityMatrix:Array = new Array(26);
		private var dotMatrix:Array = new Array(26);
		private var dotMatrixContainer:MovieClip = new MovieClip();
		
		private var SENSOR_X:Array = new Array();
		private var SENSOR_Y:Array = new Array();
		
		
		// ------- Constructor -------
		public function smartoGUI()
		{
			var r:int = 1;
			var c:int = 2;
			createDotMatrix();
			initializeIntensityMatrix();
			initializeSensorLocations();
			
			colorFoot([50,50,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1]);
			
			//addEventListener(MouseEvent.MOUSE_MOVE,interactiveColor);
			btnTest.addEventListener(MouseEvent.CLICK,showRS232);
			
			m1up.addEventListener(MouseEvent.CLICK,runMotor);
			m1down.addEventListener(MouseEvent.CLICK,runMotor);
			m2up.addEventListener(MouseEvent.CLICK,runMotor);
			m2down.addEventListener(MouseEvent.CLICK,runMotor);
			m3up.addEventListener(MouseEvent.CLICK,runMotor);
			m3down.addEventListener(MouseEvent.CLICK,runMotor);
			ExternalInterface.addCallback("vbColorFoot",colorFoot);
		}
		function initializeSensorLocations():void {
			//b i g   s e n s o r s
			SENSOR_X[0] = 2;
			SENSOR_Y[0] = 4;
			SENSOR_X[1] = 4;
			SENSOR_Y[1] = 2;
			//m e d i u m   s e n s o r s
			SENSOR_X[2] = 7;
			SENSOR_Y[2] = 8;
			SENSOR_X[3] = 10;
			SENSOR_Y[3] = 5;
			SENSOR_X[4] = 17;
			SENSOR_Y[4] = 6;
			SENSOR_X[5] = 14;
			SENSOR_Y[5] = 3;
			//s m a l l   s e n s o r s
			SENSOR_X[6] = 23;
			SENSOR_Y[6] = 2;
			SENSOR_X[7] = 22;
			SENSOR_Y[7] = 6;
			SENSOR_X[8] = 0;
			SENSOR_Y[8] = 0;
			SENSOR_X[9] = 0;
			SENSOR_Y[9] = 0;
			SENSOR_X[10] = 0;
			SENSOR_Y[10] = 0;
			SENSOR_X[11] = 0;
			SENSOR_Y[11] = 0;
			SENSOR_X[12] = 0;
			SENSOR_Y[12] = 0;
			SENSOR_X[13] = 0;
			SENSOR_Y[13] = 0;
			SENSOR_X[14] = 0;
			SENSOR_Y[14] = 0;
			SENSOR_X[15] = 0;
			SENSOR_Y[15] = 0;
			SENSOR_X[16] = 0;
			SENSOR_Y[16] = 0;
			SENSOR_X[17] = 0;
			SENSOR_Y[17] = 0;
		}
		function interactiveColor(e:MouseEvent=null):void {
			initializeIntensityMatrix();
			addIntensities(35*(stage.stageWidth-mouseX)/stage.stageWidth,10,5);
			addIntensities(70*(stage.stageWidth-mouseX)/stage.stageWidth,2,4); //
			addIntensities(30*(stage.stageWidth-mouseX)/stage.stageWidth,7,8);
			addIntensities(50*(stage.stageWidth-mouseX)/stage.stageWidth,17,6);
			addIntensities(70*(stage.stageWidth-mouseX)/stage.stageWidth,4,2);
			
			addIntensities(30*(stage.stageWidth-mouseX)/stage.stageWidth,23,2);
			addIntensities(30*(stage.stageWidth-mouseX)/stage.stageWidth,22,6);
			updateIntensities();
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
			dotMatrixContainer.x = 560;
			dotMatrixContainer.y = 50;
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
				}
			}
		}
		function hideNonFootDots():void {
			for(var c:int=0; c<10; c++) {
				for(var r:int=0; r<26; r++) {
					switch(c){
						case 0:
							if(r<1 || (r>6 && r<12) || (r>20 && r<24) || r>24) intensityMatrix[r][c] = 0; //(r>5 && r<18) ||
							break;
						case 1:
							if((r>8 && r<11) || (r>21 && r<23)) intensityMatrix[r][c] = 0; //(r>7 && r<17) ||
							break;
						case 2:
							if( (r>21 && r<24)) intensityMatrix[r][c] = 0; //(r>8 && r<17) ||
							break;
						case 3:
							if( r>22)intensityMatrix[r][c] = 0; //(r>9 && r<17) ||
							break;
						case 4:
							if( (r>22 && r<25))intensityMatrix[r][c] = 0; //(r>12 && r<16) ||
							break;
						case 5:
							if(r<1 || (r>21 && r<24))intensityMatrix[r][c] = 0;
							break;
						case 6:
							if(r<2 || (r>21 && r<23))intensityMatrix[r][c] = 0;
							break;
						case 7:
							if(r<6 || (r>20 && r<22) || r>24) intensityMatrix[r][c] = 0;
							break;
						case 8:
							if(r<11 || (r>21 && r<23) || r>23) intensityMatrix[r][c] = 0;
							break;
						case 9:
							if(r<16 || (r>17 && r<20) || r>20) intensityMatrix[r][c] = 0;
							break;
					}
				}
			}
		}
		function runMotor(e:MouseEvent=null):void {
			ExternalInterface.call("vbRunMotor", e.target.name.charAt(1), e.target.name.substr(2)); // args: fn, motor#, up/down
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
			for(var i:int=0; i<18; i++) {
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
