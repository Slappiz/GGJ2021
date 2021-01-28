// using UnityEngine;
// using UnityEngine.Experimental.Rendering.Universal;
//
// namespace Lights
// {
// 	public class FlickeringLight : MonoBehaviour {
//
// 			public enum WaveForm {Sine, Triangle, Square, Sawtooth, ReverseSaw, Noise}; 
// 			public WaveForm waveform = WaveForm.Sine;   
//
// 			public float baseStart = 0.0f; // start 
// 			public float amplitude = 1.0f; // amplitude of the wave
// 			public float phase = 0.0f; // start point inside on wave cycle
// 			public float frequency = 0.5f; // cycle frequency per second
//
// 			// Keep a copy of the original color
// 			private Color _originalColor; 
// 			private Light2D _light;
//
// 			// Store the original color
// 			void Start () {   
// 				_light = GetComponent<Light2D>(); 
// 				_originalColor = _light.color;
// 			}
//
// 			void FixedUpdate () {  
// 				_light.color = _originalColor * (EvalWave());
// 			}
//
// 			float EvalWave () { 
// 				float x = (Time.time + phase) * frequency;
// 				float y;
// 				x = x - Mathf.Floor(x); // normalized value (0..1)
//
// 				switch (waveform)
// 				{
// 					case WaveForm.Sine:
// 					{
// 						y = Mathf.Sin(x * 2 * Mathf.PI);
// 						break;
// 					}
// 					case WaveForm.Triangle:
// 					{
// 						if (x < 0.5f)
// 							y = 4.0f * x - 1.0f;
// 						else
// 							y = -4.0f * x + 3.0f;  
// 						
// 						break;
// 					}
// 					case WaveForm.Square:
// 					{
// 						if (x < 0.5f) y = 1.0f;
// 						else y = -1.0f;  
// 						
// 						break;
// 					}
// 					case WaveForm.Sawtooth:
// 					{
// 						y = x;
// 						break;
// 					}
// 					case WaveForm.ReverseSaw:
// 					{
// 						y = 1.0f - x;
// 						break;
// 					}
// 					case WaveForm.Noise:
// 					{
// 						y = 1f - (Random.value * 2);
// 						break;
// 					}
// 					default:
// 						y = 1.0f;
// 						break;
// 				}
// 		       
// 				return (y * amplitude) + baseStart;    
// 			}
// 		}
// }
