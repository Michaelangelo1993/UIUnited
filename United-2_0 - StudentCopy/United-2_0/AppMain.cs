using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.UI;
using Sce.PlayStation.Core.Audio;
using System.Threading;

namespace United2_0
{
	public class AppMain
	{
		private static GraphicsContext graphics;
		private static int screenWidth;
		private static int screenHeight;
		private static BgmPlayer mp3Player, mp3PlayerScotland, mp3PlayerEngland, mp3PlayerN_Ireland, mp3PlayerWales;
		private static Rectangle scotlandRect, englandRect, n_irelandRect, walesRect;
		private static Bgm funWithFlags, scotlandVoice, englandVoice, walesVoice, n_irelandVoice;
		private static TouchStatus currentTouchStatus, previousTouchStatus;
		public static void Main (string[] args)
		{
			Initialize ();

			while (true) {	// change true condition
				
				SystemEvents.CheckEvents ();
                Update ();
                Render ();
			}
			
			// Clean up after exiting game loop
		}

		public static void Initialize ()
		{
			graphics = new GraphicsContext ();
			 
			// Initialize UI Toolkit
            UISystem.Initialize(graphics);
			screenWidth = UISystem.FramebufferWidth;
			screenHeight = UISystem.FramebufferHeight;
				
            // Create scene
            Scene scene = new Sce.PlayStation.HighLevel.UI.Scene();
			
			#region Big Bang - Fun with Flags - Intro sound
			
			
			#endregion
			#region Scotland Flag and sounds
            ImageBox scotlandFlag = new ImageBox();
	    	scotlandFlag.Image = new ImageAsset("/Application/flags/scotland.png");
	    	scotlandFlag.ImageScaleType = ImageScaleType.AspectInside;
	    	
			scotlandFlag.Width = scotlandFlag.Image.Width /2;
			scotlandFlag.Height = scotlandFlag.Image.Height / 2;
			
			scotlandFlag.X = screenWidth * 0.25f - (scotlandFlag.Width / 2);
			scotlandFlag.Y = screenHeight * 0.25f - (scotlandFlag.Height / 2);
			
			scotlandRect = new Rectangle(scotlandFlag.X, scotlandFlag.Y, scotlandFlag.Width, scotlandFlag.Height);
			/***********************sounds***************************/
			
			
			#endregion
			scene.RootWidget.AddChildLast(scotlandFlag);
			
			#region England Flag
			ImageBox englandFlag = new ImageBox();
	    	englandFlag.Image = new ImageAsset("/Application/flags/england.png");
	    	englandFlag.ImageScaleType = ImageScaleType.AspectInside;
	    	
			englandFlag.Width = englandFlag.Image.Width / 2;
			englandFlag.Height = englandFlag.Image.Height / 2;
			
			englandFlag.X = screenWidth * 0.75f - (englandFlag.Width / 2);
			englandFlag.Y = screenHeight * 0.25f - (englandFlag.Height / 2);
			
			englandRect = new Rectangle(englandFlag.X, englandFlag.Y, englandFlag.Width, englandFlag.Height);
			
			#endregion
			scene.RootWidget.AddChildLast(englandFlag);
			
			#region Northern Ireland Flag
			ImageBox n_irelandFlag = new ImageBox();
	    	n_irelandFlag.Image = new ImageAsset("/Application/flags/n_ireland.png");
	    	n_irelandFlag.ImageScaleType = ImageScaleType.AspectInside;
	    	
			n_irelandFlag.Width = n_irelandFlag.Image.Width / 2;
			n_irelandFlag.Height = n_irelandFlag.Image.Height / 2;
			
			n_irelandFlag.X = screenWidth * 0.25f - (n_irelandFlag.Width / 2);
			n_irelandFlag.Y = screenHeight * 0.75f - (n_irelandFlag.Height / 2);
			
			n_irelandRect = new Rectangle(n_irelandFlag.X, n_irelandFlag.Y, n_irelandFlag.Width, n_irelandFlag.Height);
			
			#endregion
			scene.RootWidget.AddChildLast(n_irelandFlag);
	    	
			#region Wales Flag
			ImageBox walesFlag = new ImageBox();
	    	walesFlag.Image = new ImageAsset("/Application/flags/wales.png");
	    	walesFlag.ImageScaleType = ImageScaleType.AspectInside;
	    	
			walesFlag.Width = walesFlag.Image.Width / 2;
			walesFlag.Height = walesFlag.Image.Height / 2;
			
			walesFlag.X = screenWidth * 0.75f - (walesFlag.Width / 2);
			walesFlag.Y = screenHeight * 0.75f - (walesFlag.Height / 2);
			
			walesRect = new Rectangle(walesFlag.X, walesFlag.Y, walesFlag.Width, walesFlag.Height);
			#endregion
			scene.RootWidget.AddChildLast(walesFlag);
            // Set scene
            UISystem.SetScene(scene, null);
			
			#region Instantiate and Setup audio
			Bgm funWithFlags = new Bgm("/Application/sounds/funWithFlags.mp3");
			mp3Player = funWithFlags.CreatePlayer();
			#endregion
			/*************Play starting mp3 - funWithFlag s***********/
			mp3Player.Play();
		}

		public static void Update ()
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
			List<TouchData> touches = Touch.GetData(0);
			foreach(TouchData data in touches)
			{
				currentTouchStatus = data.Status;
				float xPos = (data.X + 0.5f) * screenWidth;
				float yPos = (data.Y + 0.5f) * screenHeight;
				// Are we presing down on the touchpad?
				if(data.Status == TouchStatus.Down)
				{
					// Check to see if we are intersecting a flag
					if(InsideRect(xPos, yPos, scotlandRect))
					{
						if(mp3Player.Status == BgmStatus.Playing)
						{
							
							mp3Player.Stop();
							mp3Player.Dispose();
						}
						Bgm scotlandVoice = new Bgm("/Application/sounds/scotlandVoice.mp3");
						mp3Player = scotlandVoice.CreatePlayer();
						mp3Player.Play();
						
						
						
					}
					if(InsideRect(xPos, yPos, englandRect))
					{
						if(mp3Player.Status == BgmStatus.Playing)
						{
							mp3Player.Stop();
							mp3Player.Dispose();
						}
						Bgm englandVoice = new Bgm("/Application/sounds/englandVoice.mp3");
						mp3Player = englandVoice.CreatePlayer();
						mp3Player.Play();
						
					}
					
					if(InsideRect(xPos, yPos, n_irelandRect))
					{
						if(mp3Player.Status == BgmStatus.Playing)
						{
							mp3Player.Stop();
							mp3Player.Dispose();
						}
						Bgm n_irelandVoice = new Bgm("/Application/sounds/n_irelandVoice.mp3");
						mp3Player = n_irelandVoice.CreatePlayer();
						mp3Player.Play();
					}
					
					if(InsideRect(xPos, yPos, walesRect))
					{
						if(mp3Player.Status == BgmStatus.Playing)
						{
							mp3Player.Stop();
							mp3Player.Dispose();
						}
						Bgm walesVoice = new Bgm("/Application/sounds/walesVoice.mp3");
						mp3Player = walesVoice.CreatePlayer();
						mp3Player.Play();
					}
					
				}
				
				previousTouchStatus = currentTouchStatus;
				/*
				Console.WriteLine("ID "+data.ID);	
				Console.WriteLine("Status "+data.Status);	
				Console.WriteLine("Xcoord "+(data.X + 0.5f) * screenWidth);
				Console.WriteLine("Ycoord "+(data.Y + 0.5f) * screenHeight);
				*/
			}
		}

		public static void Render ()
		{
			// Clear the screen
			graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			graphics.Clear ();

			// Render UI Toolkit
            UISystem.Render ();

            // Present the screen
            graphics.SwapBuffers ();
		}
		
		/// Inside rectangle test
	    private static bool InsideRect(float pixelX, float pixelY, Rectangle hitTestArea)
	    {
			
	        if (hitTestArea.X <= pixelX && hitTestArea.X + hitTestArea.Width >= pixelX &&
	            hitTestArea.Y <= pixelY && hitTestArea.Y + hitTestArea.Height >= pixelY) {
	            return true;
	        }
	
	        return false;
	    }
	}
}
